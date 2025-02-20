using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationAlignment : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        SetSelfRotation();
    }

    private void SetSelfRotation() 
    {
        Vector3 rot = this.transform.eulerAngles;
        rot.z = -Input.compass.magneticHeading;
        this.transform.eulerAngles = rot;
    }
}
