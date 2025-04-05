using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Unity.XR.CoreUtils;
using TMPro;

public class RotationAlignment : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_TextMeshPro;
    [SerializeField] private ARSession aRSession;
    [SerializeField] private ARCameraManager aRCamera;
    [SerializeField] private ARSessionOrigin aRSessionOrigin;
    [SerializeField] private XROrigin xROrigin;
    private float magneticHead;
    private bool doOnce = false;

    public void Awake()
    {
        Input.compass.enabled = true;
        Input.location.Start(10, 0.01f);
        //float north = Input.compass.trueHeading;
        //this.transform.rotation = this.transform.rotation = Quaternion.Euler(0, north, 0);

    }
    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!doOnce)
        {
            FindStartRotation();
        }
        m_TextMeshPro.text = Input.compass.magneticHeading.ToString();
        //Debug.Log("y rotation: " + aRSessionOrigin.transform.eulerAngles.y);
    }

    void FindStartRotation() {

        if(Input.compass.magneticHeading != 0)
        {
            magneticHead = Input.compass.magneticHeading;
            Debug.Log(magneticHead);
            aRSessionOrigin.transform.rotation = Quaternion.Euler(
                aRSessionOrigin.transform.eulerAngles.x,
                magneticHead,
                aRSessionOrigin.transform.eulerAngles.z);
            //Debug.Log("y rotation: " + aRSessionOrigin.transform.eulerAngles.y);
            doOnce = true;
        }
        
        
    }
    private void SetSelfRotation() 
    {
        float cameraRot = Input.compass.magneticHeading;
        this.transform.rotation = Quaternion.Euler(0, cameraRot, 0);
        
    }
}
