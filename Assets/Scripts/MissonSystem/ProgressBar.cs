using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private float max;
    [SerializeField] private float current;
    [SerializeField] private Image fillBar;
    [SerializeField] private TMP_Text text;
    [SerializeField] private GameObject completedPanel;
    private bool Missioncomplete = false;
    // Start is called before the first frame update
    void Start()
    {
        setText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool addCurrent()
    {
        if(Missioncomplete!= true)
        {
            current += 1.0f;
            if(current < max)
            {
                GetCurrentPrecent();
                return Missioncomplete;
            }
            else
            {
                GetCurrentPrecent();
                completedPanel.SetActive(true);
                Missioncomplete = true;
                return Missioncomplete;
            }
        }
        return Missioncomplete;
    }

    private void GetCurrentPrecent()
    {
        float percent = current / max;
        fillBar.fillAmount = percent;
        setText();
    }

    private void setText()
    {
        text.SetText(current + "/" + max);
    }
}
