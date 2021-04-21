using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class CommsIndicatorController : MonoBehaviour
{
    public TextMeshProUGUI AstronautNameLabel;
    public GameObject ActiveIndicator;

    private string astronautName = "Astronaut";
    private bool active = false;
    private Color color = Color.blue;

    // Start is called before the first frame update
    void Start()
    {
        UpdateView();
    }

    public void SetAstronautName(string name)
    {
        astronautName = name;
        UpdateView();
    }

    public void SetActive(bool active)
    {
        this.active = active;
        UpdateView();
    }

    public void SetColor(Color color)
    {
        this.color = color;
        UpdateView();
    }

    public void SetAllProperties(string name, bool active, Color color)
    {
        astronautName = name;
        this.active = active;
        this.color = color;
        UpdateView();
    }

    public void UpdateView()
    {
        AstronautNameLabel.text = astronautName;
        ActiveIndicator.SetActive(active);
        ActiveIndicator.GetComponent<Image>().color = this.color;
        if (active)
        {
            AstronautNameLabel.color = this.color;
        } else
        {
            AstronautNameLabel.color = Color.white;
        }
    }

    public void ToggleActive()
    {
        SetActive(!active);
    }
}
