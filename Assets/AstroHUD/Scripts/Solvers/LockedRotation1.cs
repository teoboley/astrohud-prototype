using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedRotation1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // transform.rotation = Quaternion.identity;
        Vector3 newRot = transform.eulerAngles;
        newRot.z = -Camera.main.transform.eulerAngles.y;
        transform.eulerAngles = newRot;
    }
}
