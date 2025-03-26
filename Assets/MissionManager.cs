using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    [SerializeField] private ProgressBar mission1progress;
    [SerializeField] private ProgressBar mission2progress;
    [SerializeField] private ProgressBar mission3progress;
    [SerializeField] private ProgressBar mission4progress;

    [SerializeField] private GameObject updateHint;

    private bool mission1Completed = false;
    private bool mission2Completed = false;
    private bool mission3Completed = false;
    private bool mission4Completed = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void updateMission1()
    {
        if (!mission1Completed)
        {
            mission1Completed = mission1progress.addCurrent();
        }
        ShowUpdateHint();
    }
    public void updateMission2()
    {
        if (!mission2Completed)
        {
            mission2Completed = mission2progress.addCurrent();
        }
        ShowUpdateHint();
    }
    public void updateMission3()
    {
        if (!mission3Completed)
        {
            mission3Completed = mission3progress.addCurrent();
        }
        ShowUpdateHint();
    }
    public void updateMission4()
    {
        if (!mission4Completed)
        {
            mission4Completed = mission4progress.addCurrent();
        }
        ShowUpdateHint();
    }

    private void ShowUpdateHint()
    {
        updateHint.SetActive(true);
    }
}