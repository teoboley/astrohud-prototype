using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class AstronautCardController : MonoBehaviour
{
    [Header("Render Targets")]
    public GameObject ContentContainer;
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

    public float minVisibleDistance = .5f;

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

    void Update()
    {
        CheckDistance();
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

    private void CheckDistance()
    {
        var currentPosition = GetComponent<Transform>().position;
        ContentContainer.SetActive(Vector3.Distance(currentPosition, Camera.main.transform.position) < minVisibleDistance);
    }
}
