using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RotationAlignment : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshPro m_TextMeshPro;

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
        m_TextMeshPro.text = Input.compass.magneticHeading.ToString();
    }
}
