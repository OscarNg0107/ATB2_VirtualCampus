using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class SetNavigationTarget : MonoBehaviour
{
    //[SerializeField]private Camera topDownCamera;
    //[SerializeField] private GameObject navTargetObject;

    [SerializeField] private TMP_Dropdown naviTargetDropDown;
    [SerializeField] private List<NavigationTarget> navigationTargets = new List<NavigationTarget>();

    private NavMeshPath path; //curent calculated path
    private LineRenderer line; // linerenderer to display path
    private Vector3 targetPos = Vector3.zero;

    private bool lineToggle = false;

    // Start is called before the first frame update
    private void Start()
    {
        path = new NavMeshPath();
        line = transform.GetComponent<LineRenderer>();
        line.enabled = lineToggle;
    }

    // Update is called once per frame
    private void Update()
    {
        //if((Input.touchCount >0)&& (Input.GetTouch(0).phase == TouchPhase.Began))
        //{
        //    lineToggle = !lineToggle;
        //}
        //if (lineToggle)
        //{
        //    NavMesh.CalculatePath(transform.position, navTargetObject.transform.position, NavMesh.AllAreas, path);
        //    line.positionCount = path.corners.Length;
        //    line.SetPositions(path.corners);
        //    line.enabled = true;
        //}
        if (lineToggle && targetPos != Vector3.zero)
        {
            NavMesh.CalculatePath(transform.position, targetPos, NavMesh.AllAreas, path);
            line.positionCount = path.corners.Length;
            line.SetPositions(path.corners);
            Debug.Log(targetPos);
        }
    }

    public void SetTarget(int selectedID)
    {
        targetPos = Vector3.zero;
        string selectedText = naviTargetDropDown.options[selectedID].text;
        NavigationTarget currentTarget = navigationTargets.Find(x => x.targetName.Equals(selectedText));
        if(currentTarget != null)
        {
            targetPos = currentTarget.targetGO.transform.position;
        }

    }

    public void ToggleLine()
    {
        lineToggle = !lineToggle;
        line.enabled = lineToggle;
    }
}
