using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;


public class NavigationManager : MonoBehaviour
{

    #region Public Fields

    [Header("Layer Visibility Controls")]
    public GameObject ChevronControlButton;
    public GameObject AstronautControlButton;
    public GameObject LandmarkControlButton;
    public GameObject ArtificialLandmarkControlButton;


    [Header("Layer Control Materials")]
    public Material enabledControlMaterial;
    public Material disabledControlMaterial;


    [Header("Map Layers")]
    public GameObject ChevronLayer;
    public GameObject AstronautLayer;
    public GameObject LandmarkLayer;
    public GameObject ArtificialLandmarkLayer;


    [Header("Direction Labels")]
    public TextMeshPro CompassLabel;
    public TextMeshProUGUI WristMenuCompassLabel;
    public TextMeshPro DegreeLabel;


    [Header("Compass")]
    public GameObject Compass;

    [Header("Chevron")]
    public GameObject Chevron;

    #endregion

    #region Resources

    //private string enabledMaterialLabel = "HolographicButtonPlateEnabled";

    //private string disabledMaterialLabel = "HolographicButtonPlateDisabled";


    #endregion

    // Start is called before the first frame update
    void Start()
    {
        ChevronLayer.SetActive(false);
        AstronautLayer.SetActive(false);
        LandmarkLayer.SetActive(false);
        ArtificialLandmarkLayer.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        var v = Compass.GetComponent<Transform>().forward;
        v.y = 0;
        v.Normalize();

        var newDegree = (float) Math.Round(Vector3.Angle(v, Vector3.forward));

        DegreeLabel.text = newDegree.ToString() + "°";

      //  Chevron.transform.rotation = Quaternion.Euler(0, (float) newDegree, 0);

        if (Vector3.Angle(v, Vector3.forward) <= 45.0)
        {
            UpdateText("N");
            
        }
        else if (Vector3.Angle(v, Vector3.right) <= 45.0)
        {
            UpdateText("E");
        }
        else if (Vector3.Angle(v, Vector3.back) <= 45.0)
        {
            UpdateText("S");
        }
        else
        {
            UpdateText("W");
        }
    }

    void UpdateText(string text)
    {
        WristMenuCompassLabel.text = text;
        CompassLabel.text = text;
    }

    #region Toggles

    public void toggleChevron()
    {
        toggleLayer(ChevronControlButton, ChevronLayer);
    }

    public void toggleAstronauts()
    {
        toggleLayer(AstronautControlButton, AstronautLayer);
    }

    public void toggleLandmarks()
    {
        toggleLayer(LandmarkControlButton, LandmarkLayer);
    }

    public void toggleArtificialLandmarks()
    {
        toggleLayer(ArtificialLandmarkControlButton, ArtificialLandmarkLayer);
    }

    private void toggleLayer(GameObject control, GameObject layer)
    {

        Image controlRenderer = control.GetComponent<Image>();

        Debug.Log(controlRenderer.material.name);

        if (control == null || layer == null)
        {
            return;
        }

        if (controlRenderer.material.name == enabledControlMaterial.name)
        {
            Debug.Log("Switching to false");
            controlRenderer.material = disabledControlMaterial;
            layer.SetActive(false);
        }
        else
        {
            Debug.Log("Switching to true");
            controlRenderer.material = enabledControlMaterial;
            layer.SetActive(true);
        }

        Debug.Log(controlRenderer.material.name);

    }
    #endregion
}