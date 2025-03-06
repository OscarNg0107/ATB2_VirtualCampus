using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR;

public class MapRotation : MonoBehaviour
{
    [SerializeField] private GameObject pivotPoint;
    [SerializeField] private GameObject Player;
    [SerializeField] private ARSession aRSession;
    [SerializeField] private ARSessionOrigin aRSessionOrigin;

    // Start is called before the first frame update
    void Start()
    {
        //this.transform.parent = pivotPoint.transform;
        this.transform.rotation = Quaternion.Euler(
            this.transform.eulerAngles.x,
            this.transform.eulerAngles.y * Mathf.Rad2Deg + Input.compass.magneticHeading,
            this.transform.eulerAngles.z );
        this.transform.parent = null;
        aRSession.Reset();
        aRSessionOrigin.transform.rotation = Player.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(
            transform.eulerAngles.x,
            transform.eulerAngles.y + 10.0f,
            transform.eulerAngles.z);
        Debug.Log(transform.eulerAngles.y);
    }
}
