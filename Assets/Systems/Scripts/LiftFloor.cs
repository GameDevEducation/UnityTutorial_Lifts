using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class LiftFloor : MonoBehaviour
{
    [SerializeField] string _DisplayName;
    [SerializeField] string SupportedTag = "Player";
    [SerializeField] LiftController LinkedController;
    [SerializeField] Transform LiftTarget;

    Animator LinkedAnimator;

    public string DisplayName => _DisplayName;
    public float TargetY => LiftTarget.position.y;
    public bool LiftPresent => LinkedController.ActiveLift.CurrentFloor == this;

    List<GameObject> Openers = new List<GameObject>();

    private void Awake()
    {
        LinkedAnimator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(SupportedTag))
        {
            Openers.Add(other.gameObject);

            if (LiftPresent)
            {
                LinkedAnimator.ResetTrigger("Close");
                LinkedAnimator.SetTrigger("Open");
                LinkedController.ActiveLift.OpenDoors();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(SupportedTag))
        {
            Openers.Remove(other.gameObject);

            LinkedAnimator.ResetTrigger("Open");
            LinkedAnimator.SetTrigger("Close");
            LinkedController.ActiveLift.CloseDoors();
        }
    }

    public void OnCallLiftForUp()
    {
        LinkedController.CallLift(this, true);
    }

    public void OnCallLiftForDown()
    {
        LinkedController.CallLift(this, false);
    }

    public void OnLiftDeparted(Lift activeLift)
    {
        LinkedAnimator.ResetTrigger("Open");
        LinkedAnimator.SetTrigger("Close");
        LinkedController.ActiveLift.CloseDoors();
    }

    public void OnLiftArrived(Lift activeLift)
    {
        if (Openers.Count > 0)
        {
            LinkedAnimator.ResetTrigger("Close");
            LinkedAnimator.SetTrigger("Open");
            LinkedController.ActiveLift.OpenDoors();
        }
    }
}
