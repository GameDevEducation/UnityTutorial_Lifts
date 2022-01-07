using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LiftUI_FloorButton : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ButtonText;

    LiftController LinkedController;
    LiftFloor LinkedFloor;

    public void Bind(LiftFloor linkedFloor, LiftController linkedController, string floorName)
    {
        LinkedController = linkedController;
        LinkedFloor = linkedFloor;
        ButtonText.text = floorName;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPressed()
    {
        LinkedController.SendLiftTo(LinkedFloor);
    }
}
