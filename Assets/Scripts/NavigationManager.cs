using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    #endregion

    #region Resources

    private string enabledMaterialLabel = "HolographicButtonPlateEnabled";

    private string disabledMaterialLabel = "HolographicButtonPlateDisabled";

    private Material enabledControl;

    private Material disabledControl;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        enabledControl = Resources.Load(enabledMaterialLabel, typeof(Material)) as Material;
        disabledControl = Resources.Load(disabledMaterialLabel, typeof(Material)) as Material;
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

        Debug.Log("Hey there");

        Renderer controlRenderer = control.GetComponent<Renderer>();
        // Renderer layerRenderer = layer.GetComponent<Renderer>();

        if (control == null || layer == null)
        {
            return;
        }

        if (controlRenderer.material.name == enabledMaterialLabel)
        {
            controlRenderer.material = disabledControl;
            layer.SetActive(false);
        }
        else
        {
            controlRenderer.material = enabledControl;
            layer.SetActive(true);
        }

    }
    #endregion
}