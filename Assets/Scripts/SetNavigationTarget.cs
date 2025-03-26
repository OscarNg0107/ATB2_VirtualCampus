using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using UnityEngine.UI;

public class SetNavigationTarget : MonoBehaviour
{
    //[SerializeField]private Camera topDownCamera;
    //[SerializeField] private GameObject navTargetObject;

    [SerializeField] private TMP_Dropdown naviTargetDropDown;
    [SerializeField] private List<NavigationTarget> navigationTargets = new List<NavigationTarget>();
    [SerializeField] private Slider lineHeightSlider;

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
        if (targetPos != Vector3.zero)
        {
            NavMesh.CalculatePath(transform.position, targetPos, NavMesh.AllAreas, path);
            line.positionCount = path.corners.Length;
            Vector3[] calculatedPath = AddLineOffset();
            line.SetPositions(calculatedPath);
        }
    }

    private Vector3[] AddLineOffset()
    {
        if(lineHeightSlider.value == 0)
        {
            return path.corners;
        }

        Vector3[] calculatedLine = new Vector3[path.corners.Length];
        for(int i =0; i< path.corners.Length; i++)
        {
            calculatedLine[i] = path.corners[i] + new Vector3(0, lineHeightSlider.value, 0);
        }
        return calculatedLine;
    }

    public void SetTarget(int selectedID)
    {
        targetPos = Vector3.zero;
        string selectedText = naviTargetDropDown.options[selectedID].text;
        if (selectedText == "None")
        {
            targetPos = Vector3.zero;
            HideNavLine();
        }
        NavigationTarget currentTarget = navigationTargets.Find(x => x.targetName.ToLower().Equals(selectedText.ToLower()));
        if(currentTarget != null)
        {
            targetPos = currentTarget.targetGO.transform.position;
            ShowNavLine();
        }

    }
    private void ShowNavLine()
    {
        line.enabled = true;
    }

    private void HideNavLine()
    {
        line.enabled = false;
    }
}
