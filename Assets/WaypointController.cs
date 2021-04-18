using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaypointController : MonoBehaviour
{
    [Header("Configuration")]
    public WaypointType type;
    public string waypointLabel;
    public bool isMicroNavigation;

    [Header("Render Targets")]
    public TextMeshPro NameLabel;
    public TextMeshPro DistanceLabel;
    public TextMeshPro TravelTimeLabel;

    public GameObject HeadObject;
    public GameObject TailObject;


    [Header("Styling")]
    public Sprite MacroNavigationSprite;
    public Sprite MicroNavigationSprite;

    public Material PrimaryMaterial;
    public Material SecondaryMaterial;
    public Material PingMaterial;

    public Color PrimaryColor;
    public Color SecondaryColor;
    public Color PingColor;


    public enum WaypointType
    {
        Primary,
        Secondary,
        Ping
    }

    void Start()
    {
        HeadObject.GetComponent<SpriteRenderer>().color = GetColor();
        HeadObject.GetComponent<SpriteRenderer>().sprite = GetSprite();

        TailObject.GetComponent<MeshRenderer>().sharedMaterial = GetMaterial();
        NameLabel.text = waypointLabel;
    }

    void Update()
    {
        
    }

    private Material GetMaterial()
    {
        switch(type)
        {
            case WaypointType.Primary:
                return PrimaryMaterial;
            case WaypointType.Secondary:
                return SecondaryMaterial;
            default:
                return PingMaterial;
        }
    }

    private Color GetColor()
    {
        switch (type)
        {
            case WaypointType.Primary:
                return PrimaryColor;
            case WaypointType.Secondary:
                return SecondaryColor;
            default:
                return PingColor;
        }
    }

    private Sprite GetSprite()
    {
        if (isMicroNavigation)
        {
            return MicroNavigationSprite;
        }

        return MacroNavigationSprite;
    }
}
