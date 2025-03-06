using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRot : MonoBehaviour
{
    public GameObject g;

    public void AddR() {
        g.transform.rotation = Quaternion.Euler(
            g.transform.eulerAngles.x,
            g.transform.eulerAngles.y + 10.0f,
            g.transform.eulerAngles.z);
    }
}
