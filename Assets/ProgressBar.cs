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
    // Start is called before the first frame update
    void Start()
    {
        setText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void addCurrent()
    {
        current += 1.0f;
    }

    public void GetCurrentPrecent()
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
