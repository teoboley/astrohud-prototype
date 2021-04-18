using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationMenuController : MonoBehaviour
{
    [Header("Map")]
    public GameObject MiniMap;

    public float scaleModifier = .01f;
    
    public void CenterMap()
    {
        MiniMap.transform.position = new Vector3(0f, 0f, 0f);
    }

    public void ZoomIn()
    {
        MiniMap.transform.localScale += new Vector3(scaleModifier, scaleModifier, scaleModifier);
    }

    public void ZoomOut()
    {
        MiniMap.transform.localScale -= new Vector3(scaleModifier, scaleModifier, scaleModifier);

    }
}
