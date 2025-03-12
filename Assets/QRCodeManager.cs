using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

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
                    Debug.Log(newImage.referenceImage.name);
                    SetQrCodeLocTarget("NorthEntrance");
                    break;

                case "SouthEntrance":
                    SetQrCodeLocTarget("SouthEntrance");
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
        }
    }

}
