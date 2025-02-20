using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationAlignment : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        SetSelfRotation();
    }

    private void SetSelfRotation() { 
           gameObject.transform.eulerAngles = new Vector3(
               gameObject.transform.eulerAngles.x,
               AlignWithWorldNorth(),
               gameObject.transform.eulerAngles.z);
    }

    private float AlignWithWorldNorth()
    {
        // Get the device's current compass heading relative to true north
        float compassHeading = Input.compass.trueHeading;

        // Calculate the rotation needed to align the AR scene with the desired north
        //float trueRotation = (compassHeading - _settings.RoomNorthOffset + 360) % 360;
        float trueRotation = compassHeading;

        return trueRotation;
    }
}
