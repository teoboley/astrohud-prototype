using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BiometricsMenuUI : MonoBehaviour
{
    public AstroHUDServerConnection serverConnection;

    // time elapsed
    public Slider timeElapsedSlider;
    public TextMeshProUGUI timeElapsedLabel;
    public TextMeshProUGUI timeRemainingLabel;
    
    // left area
    public TextMeshProUGUI bpmLabel;
    public TextMeshProUGUI kcalLabel;

    // battery slider
    public Slider batterySlider;
    public TextMeshProUGUI batteryPercentLabel;
    public TextMeshProUGUI batteryTimeRemainingLabel;

    // oxygen slider
    public Slider oxygenSlider;
    public TextMeshProUGUI oxygenPercentLabel;
    public TextMeshProUGUI oxygenTimeRemainingLabel;

    // water slider
    public Slider waterSlider;
    public TextMeshProUGUI waterPercentLabel;
    public TextMeshProUGUI waterTimeRemainingLabel;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Listening for server message event");
        serverConnection.messageReceived.AddListener(
            ServerMessageReceived
        );
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ServerMessageReceived(IMessage message)
    {
        if (SystemStateMessage.Is(message))
        {
            Debug.Log("Received system state update within biometrics menu script");
            SystemStateMessage state = new SystemStateMessage(message);
            UpdateBPM(state.payload.lifeSupportState.suitState.heartRate);
            UpdateKCAL(state.payload.lifeSupportState.bodyState.caloriesBurned);
        }
    }

    void UpdateTimeElapsed()
    {
        //timeElapsedSlider.value = ;
        //timeElapsedLabel.text = ;
        //timeRemainingLabel.text = ;
    }

    void UpdateBPM(int bpm)
    {
        bpmLabel.text = bpm.ToString();
    }

    void UpdateKCAL(int kcal)
    {
        kcalLabel.text = kcal.ToString();
    }

    void UpdateBattery()
    {
        //batterySlider.value = ;
        //batteryPercentLabel.text = ;
        //batteryTimeRemainingLabel.text = ;
    }

    void UpdateOxygen()
    {
        //oxygenSlider.value = ;
        //oxygenPercentLabel.text = ;
        //oxygenTimeRemainingLabel.text = ;
    }

    void UpdateWater()
    {
        //waterSlider.value = ;
        //waterPercentLabel.text = ;
        //waterTimeRemainingLabel.text = ;
    }
}
