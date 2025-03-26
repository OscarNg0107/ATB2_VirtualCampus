using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;

public class QRCodeManager : MonoBehaviour
{
    [SerializeField]
    private ARSession session;
    [SerializeField]
    private ARSessionOrigin sessionOrigin;
    [SerializeField]
    ARTrackedImageManager m_TrackedImageManager;
    [SerializeField]
    List<NavigationTarget> navigationTargets = new List<NavigationTarget>();
    [SerializeField]
    private Camera MapCamera;

    [SerializeField] private MissionManager missionManager;

    [SerializeField] private GameObject MapPanel;
    [SerializeField] private GameObject HidenMap;

    private Color alphaColor;
    private bool ReceptionfirstDiscover = false;
    private bool PaintingfirstDiscover = false;

    private void Start()
    {
        alphaColor = HidenMap.GetComponent<MeshRenderer>().material.color;
    }

    IEnumerator FadeOut()
    {
        for(float f = 1.0f; f>= - 0.05f; f -= 0.05f)
        {
            alphaColor.a = f;
            HidenMap.GetComponent<MeshRenderer>().material.color = alphaColor;
            Debug.Log(f);
            yield return new WaitForSeconds(0.05f);
        }
    }

    private void StartFading()
    {
        StartCoroutine("FadeOut");
    }

    void OnEnable() => m_TrackedImageManager.trackedImagesChanged += OnChanged;

    void OnDisable() => m_TrackedImageManager.trackedImagesChanged -= OnChanged;

    void OnChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var newImage in eventArgs.added)
        {
            //Handle added event
            switch (newImage.referenceImage.name)
            {
                case "NorthEntrance":
                    
                    SetQrCodeLocTarget("NorthEntrance");
                    break;

                case "SouthEntrance":
                    SetQrCodeLocTarget("SouthEntrance");
                    break;

                case "Reception":
                    SetQrCodeLocTarget("Reception");
                    if (!ReceptionfirstDiscover)
                    {
                        MapPanel.SetActive(true);
                        StartFading();
                        missionManager.updateMission1();
                        ReceptionfirstDiscover = true;
                    }
                    break;
                case "Painting":
                    if (!PaintingfirstDiscover)
                    {
                        missionManager.updateMission3();
                        PaintingfirstDiscover = true;
                    }
                    break;
            }
        }

        foreach (var updatedImage in eventArgs.updated)
        {
            // Handle updated event
            //Debug.Log(updatedImage.name);
        }

        foreach (var removedImage in eventArgs.removed)
        {
            // Handle removed event
            Debug.Log(removedImage.name);
        }
    }
    void ListAllImages()
    {
        Debug.Log(
            $"There are {m_TrackedImageManager.trackables.count} images being tracked.");

        foreach (var trackedImage in m_TrackedImageManager.trackables)
        {
            Debug.Log($"Image: {trackedImage.referenceImage.name} is at " +
                      $"{trackedImage.transform.position}");
        }
    }

    private void SetQrCodeLocTarget(string targetText)
    {
        NavigationTarget currentTarget = navigationTargets.Find(x => x.targetName.ToLower().Equals(targetText.ToLower()));
        if (currentTarget != null)
        {
            //Reset pos and rot of ARSession
            session.Reset();

            //Add offset for recentering
            sessionOrigin.transform.position = currentTarget.targetGO.transform.position;
            sessionOrigin.transform.rotation = currentTarget.targetGO.transform.rotation;
            resetCamePos();
        }
    }

    public void resetCamePos()
    {
        MapCamera.transform.position = new Vector3(sessionOrigin.transform.position.x,
                                                       5,
                                                       sessionOrigin.transform.position.z);
    }
}
