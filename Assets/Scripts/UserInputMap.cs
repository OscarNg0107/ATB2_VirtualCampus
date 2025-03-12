using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInputMap : MonoBehaviour
{
    [SerializeField] Camera TopdownCamera;
    [SerializeField] float zoomOutMax = 20;
    [SerializeField] float zoomOutMin = 7;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount == 2)
        {
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);

            Vector2 touch0PrevPos = touch0.position - touch0.deltaPosition;
            Vector2 touch1PrevPos = touch1.position - touch1.deltaPosition;

            float prevMagnitude = (touch0PrevPos - touch1PrevPos).magnitude;
            float currentMagnitude = (touch0.position - touch1.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;

            zoom(difference * 0.01f);
        }
    }

    void zoom(float increment)
    {
        TopdownCamera.orthographicSize = Mathf.Clamp(TopdownCamera.orthographicSize - increment, zoomOutMin, zoomOutMax);
    }
}
