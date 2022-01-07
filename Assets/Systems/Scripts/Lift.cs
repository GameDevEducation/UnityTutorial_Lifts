using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour
{
    [SerializeField] LiftController LinkedController;
    [SerializeField] LiftFloor StartingFloor;
    [SerializeField] float LiftSpeed = 2f;
    [SerializeField] Transform LiftUIRoot;
    [SerializeField] GameObject LiftUIButtonPrefab;

    Animator LinkedAnimator;

    public LiftFloor CurrentFloor { get; private set; } = null;
    public LiftFloor TargetFloor { get; private set; } = null;
    public bool IsMoving { get; private set; } = false;

    private void Awake()
    {
        LinkedAnimator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(transform.position.x, StartingFloor.TargetY, transform.position.z);
        CurrentFloor = StartingFloor;

        // add the floor UI
        foreach(var floor in LinkedController.Floors)
        {
            var liftUIGO = Instantiate(LiftUIButtonPrefab, LiftUIRoot);
            liftUIGO.GetComponent<LiftUI_FloorButton>().Bind(floor, LinkedController, floor.DisplayName);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // lift moving?
        if (IsMoving)
        {
            Vector3 targetLocation = transform.position;
            targetLocation.y = TargetFloor.TargetY;

            transform.position = Vector3.MoveTowards(transform.position, targetLocation, LiftSpeed * Time.deltaTime);

            // have we arrived?
            if (Vector3.Distance(transform.position, targetLocation) < float.Epsilon)
            {
                IsMoving = false;
                CurrentFloor = TargetFloor;
                TargetFloor = null;

                CurrentFloor.OnLiftArrived(this);
            }
        }
    }

    public void MoveTo(LiftFloor targetFloor)
    {
        IsMoving = true;
        TargetFloor = targetFloor;
        CurrentFloor.OnLiftDeparted(this);
    }

    public void OpenDoors()
    {
        LinkedAnimator.ResetTrigger("Close");
        LinkedAnimator.SetTrigger("Open");
    }

    public void CloseDoors()
    {
        LinkedAnimator.ResetTrigger("Open");
        LinkedAnimator.SetTrigger("Close");
    }
}
