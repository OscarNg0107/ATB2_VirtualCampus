using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTapGameObject : MonoBehaviour
{
    [SerializeField]
    Camera cam;

    [SerializeField] private float rayLength = 50.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 1)
        {
            Interact(Input.GetTouch(0).position);
        }
    }

    private void Interact(Vector3 pos)
    {
        Ray ray = cam.ScreenPointToRay(pos);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, rayLength))
        {
            switch(hit.transform.tag)
            {
                case "POI":
                    //Script m_script = hit.transform.GetComponent<Script>();
                    PointOfInterest poi = hit.transform.GetComponent<PointOfInterest>();
                    //Call script's public function
                    poi.OnInteracted();
                    break;
                case "Coins":
                    Coin coin = hit.transform.GetComponent<Coin>();
                    coin.OnInteracted();
                    break;
            }
        }
    }
}
