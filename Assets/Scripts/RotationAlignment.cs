using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RotationAlignment : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_TextMeshPro;

    public void Awake()
    {
        Input.compass.enabled = true;
        Input.location.Start(10, 0.01f);
    }
    // Update is called once per frame
    void Update()
    {
        SetSelfRotation();
    }

    private void SetSelfRotation() 
    {
        float cameraRot = Input.compass.magneticHeading;
        this.transform.rotation = Quaternion.Euler(0, cameraRot, 0);
        m_TextMeshPro.text = Input.compass.magneticHeading.ToString();
    }
}
