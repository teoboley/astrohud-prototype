using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NavigationManager : MonoBehaviour
{

    #region Public Fields

    [Header("Chevron Button")]
    public GameObject ChevronControlButton;

    [Header("Astronaut Button")]
    public GameObject AstronautControlButton;

    [Header("Landmark Button")]
    public GameObject LandmarkControlButton;

    [Header("Artificial Landmark Button")]
    public GameObject ArtificialLandmarkControlButton;



    [Header("Chevron Layer")]
    public GameObject ChevronLayer;

    [Header("Astronaut Layer")]
    public GameObject AstronautLayer;

    [Header("Landmark Layer")]
    public GameObject LandmarkLayer;

    [Header("Artificial Landmark Layer")]
    public GameObject ArtificialLandmarkLayer;

    [Header("Enabled material")]
    public Material enabledControlMaterial;

    [Header("Disabled material")]
    public Material disabledControlMaterial;

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