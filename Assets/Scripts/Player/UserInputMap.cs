using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInputMap : MonoBehaviour
{
    [SerializeField] private Camera TopdownCamera;
    [SerializeField] private float zoomOutMax = 20;
    [SerializeField] private float zoomOutMin = 7;
    [SerializeField] private float zoomSpeed = 10.0f;
    [SerializeField] private SimpleTouchController rightController;
    [SerializeField] private float speedProgressiveLook = 300f;

    [SerializeField] private GameObject floor;

    [SerializeField] private GameObject MapCamBound1; // left upper bound 
    [SerializeField] private GameObject MapCamBound2; // right upper bound
    [SerializeField] private GameObject MapCamBound3; // left lower bound
    [SerializeField] private GameObject MapCamBound4; // right lowe bound 

    private float cameraHalfSize;

    private float minX; //bound x minimum
    private float maxX; //bound x minimum
    private float minY; //bound y maximum
    private float maxY; //bound y maximum

    private Vector3 touchStart;

    void RightController_TouchEvent(Vector2 value)
    {
        
        UpdateAim(value);
        
    }

    // Start is called before the first frame update
    void Start()
    {
        rightController.TouchEvent += RightController_TouchEvent; 
        // calculate x minimum for the bound
        minX = Mathf.Min(MapCamBound1.transform.position.x, Mathf.Min(MapCamBound2.transform.position.x, Mathf.Min(MapCamBound3.transform.position.x, MapCamBound4.transform.position.x))) + cameraHalfSize;
        // calculate x maximum for the bound
        maxX = Mathf.Max(MapCamBound1.transform.position.x, Mathf.Max(MapCamBound2.transform.position.x, Mathf.Max(MapCamBound3.transform.position.x, MapCamBound4.transform.position.x))) - cameraHalfSize;
        // calculate y minimum for the bound
        minY = Mathf.Min(MapCamBound1.transform.position.z, Mathf.Min(MapCamBound2.transform.position.z, Mathf.Min(MapCamBound3.transform.position.z, MapCamBound4.transform.position.z))) + cameraHalfSize;
        // calculate y maximum for the bound
        maxY = Mathf.Max(MapCamBound1.transform.position.z, Mathf.Max(MapCamBound2.transform.position.z, Mathf.Max(MapCamBound3.transform.position.z, MapCamBound4.transform.position.z)))- cameraHalfSize;
        Debug.Log(minY + " " + maxY + " " + minX + " " + maxX);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount == 1)
        {
            //Move camera
            Vector3 movement = new Vector3(
                TopdownCamera.transform.position.x +
                rightController.GetTouchPosition.x * Time.deltaTime * speedProgressiveLook,
                TopdownCamera.transform.position.y,
                TopdownCamera.transform.position.z +
                rightController.GetTouchPosition.y * Time.deltaTime * speedProgressiveLook
                );

            //Clamp the camera's X position 
            movement.x = Mathf.Clamp(TopdownCamera.transform.position.x +
            rightController.GetTouchPosition.x * Time.deltaTime * speedProgressiveLook, minX, maxX);
            //Clamp the camera's Z position 
            movement.z = Mathf.Clamp(TopdownCamera.transform.position.z +
                rightController.GetTouchPosition.y * Time.deltaTime * speedProgressiveLook, minY, maxY);


            TopdownCamera.transform.position = movement;
        }

        // Pinch to zoom 
        if(Input.touchCount == 2)
        {
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);

            Vector2 touch0PrevPos = touch0.position - touch0.deltaPosition;
            Vector2 touch1PrevPos = touch1.position - touch1.deltaPosition;

            float prevMagnitude = (touch0PrevPos - touch1PrevPos).magnitude;
            float currentMagnitude = (touch0.position - touch1.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;

            zoom(difference * Time.deltaTime* zoomSpeed);
            cameraHalfSize = TopdownCamera.orthographicSize;
        }
    }

    private Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, float angle)
    {
        return Quaternion.Euler(0, angle, 0) * (point - pivot) + pivot;
    }

    void UpdateAim(Vector2 value)
    {
       
        TopdownCamera.transform.Translate(new Vector3(value.x, 0, value.y) * Time.deltaTime * speedProgressiveLook, Space.World);
    }

    private Vector3 GetWorldPosition(Vector2 screenPos, float z)
    {
        Ray touchPos = TopdownCamera.ScreenPointToRay(screenPos);
        Plane ground = new Plane(Vector3.forward, new Vector3(0, 0, z));
        ground.Raycast(touchPos, out float dis);
        return touchPos.GetPoint(dis);
    }


    void zoom(float increment)
    {
        TopdownCamera.orthographicSize = Mathf.Clamp(TopdownCamera.orthographicSize - increment, zoomOutMin, zoomOutMax);
    }

    void OnDestroy()
    {
        rightController.TouchEvent -= RightController_TouchEvent;
    }
}
