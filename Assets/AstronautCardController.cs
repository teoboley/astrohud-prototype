using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class AstronautCardController : MonoBehaviour
{
    [Header("Render Targets")]
    public TextMeshProUGUI AstronautName;
    public TextMeshProUGUI TaskDescription;
    public Image HeartRate;
    public Image WaterLevel;
    public Image BatteryLevel;
    public Image OxygenLevel;
    public Image TemperatureLevel;

    [Header("Content")]
    public string astronautName;
    
    public string currentTask;
    public Status currentTaskStatus;

    public Status heartRateStatus;
    public Status waterLevelStatus;
    public Status batteryLevelStatus;
    public Status oxygenStatus;
    public Status temperatureStatus;

    [Header("Styling")]
    public Color positiveStatusColor;
    public Color warningStatusColor;
    public Color dangerStatusColor;
    
    public enum Status
    {
        Positive,
        Warning,
        Danger
    }

    // Start is called before the first frame update
    void Start()
    {
        HeartRate.color = GetColor(heartRateStatus);
        WaterLevel.color = GetColor(waterLevelStatus);
        BatteryLevel.color = GetColor(batteryLevelStatus);
        OxygenLevel.color = GetColor(oxygenStatus);
        TemperatureLevel.color = GetColor(temperatureStatus);

        TaskDescription.text = currentTask;
        AstronautName.text = astronautName;
    }

    private Color GetColor(Status status)
    {
        switch (status)
        {
            case Status.Positive:
                return positiveStatusColor;
            case Status.Warning:
                return warningStatusColor;
            default:
                return dangerStatusColor;
        }
    }
}
