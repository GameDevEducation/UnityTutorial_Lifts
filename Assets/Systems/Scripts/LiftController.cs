using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftController : MonoBehaviour
{
    [SerializeField] Lift LinkedLift;
    [SerializeField] List<LiftFloor> LiftFloors;

    public Lift ActiveLift => LinkedLift;
    public List<LiftFloor> Floors => LiftFloors;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CallLift(LiftFloor requestedFloor, bool isUp)
    {
        // already at this floor
        if (requestedFloor == ActiveLift.CurrentFloor)
            return;

        LinkedLift.MoveTo(requestedFloor);
    }

    public void SendLiftTo(LiftFloor requestedFloor)
    {
        // already at this floor
        if (requestedFloor == ActiveLift.CurrentFloor)
            return;

        LinkedLift.MoveTo(requestedFloor);
    }
}
