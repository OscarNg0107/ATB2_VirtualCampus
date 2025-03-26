using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PointOfInterest : MonoBehaviour
{
    [SerializeField] private string name;
    [SerializeField] private string info;
    [SerializeField] private Sprite Image;
    [SerializeField] private GameObject PoiInfoPanel;
    [SerializeField] private TMP_Text name_text;
    [SerializeField] private TMP_Text info_text;
    [SerializeField] private Image Image_ui;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnInteracted()
    {
        PoiInfoPanel.SetActive(true);
        name_text.SetText(name);
        info_text.SetText(info);
        Image_ui.sprite = Image;
    }
}
