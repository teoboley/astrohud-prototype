using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ColorsEnumExtension;
using UnityEditor;
using System.Linq;

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

    // deg dial
    public Image degDialImage;
    public TextMeshProUGUI degValueLabel;

    // psi dial
    public Image psiDialImage;
    public TextMeshProUGUI psiValueLabel;

    // rh dial
    public Image rhDialImage;
    public TextMeshProUGUI rhValueLabel;

    // bq dial
    public Image bqDialImage;
    public TextMeshProUGUI bqValueLabel;

    private Sprite positiveDialSprite;
    private Sprite neutralDialSprite;
    private Sprite negativeDialSprite;

    // Start is called before the first frame update
    void Start()
    {
        List<Sprite> sprites = AssetDatabase.LoadAllAssetsAtPath("Assets/AstroHUD/Textures/Biometrics Sprite Sheet.png").Where(q => q is Sprite).Cast<Sprite>().ToList();
        positiveDialSprite = sprites.Find(s => s.name.Equals("Dial_Green"));
        neutralDialSprite = sprites.Find(s => s.name.Equals("Dial_Blue"));
        negativeDialSprite = sprites.Find(s => s.name.Equals("Dial_Red"));

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

            // time elapsed
            UpdateTimeElapsed(state.payload.missionState.totalMissionLength, state.payload.missionState.missionTimeElapsed);

            // left area
            UpdateBPM(state.payload.lifeSupportState.suitState.heartRate);
            UpdateKCAL(state.payload.lifeSupportState.bodyState.caloriesBurned);

            // main sliders
            UpdateBattery(
                state.payload.lifeSupportState.suitState.currentBattery,
                state.payload.lifeSupportState.suitState.maxBattery,
                state.payload.lifeSupportState.suitState.batteryDrain
            );
            UpdateOxygen(
                state.payload.lifeSupportState.suitState.currentOxygen,
                state.payload.lifeSupportState.suitState.maxOxygen,
                state.payload.lifeSupportState.suitState.currentOxygenConsumption
            );
            UpdateWater();

            // dials
            UpdateDEG((int) state.payload.lifeSupportState.bodyState.bodyTemperature);
            UpdatePSI(state.payload.lifeSupportState.suitState.tankPressure);
            UpdateRH(state.payload.lifeSupportState.suitState.humidity);
            UpdateBQ(state.payload.lifeSupportState.suitState.radioactivity);
        }
    }

    void UpdateBPM(int bpm)
    {
        bpmLabel.text = bpm.ToString();
    }

    void UpdateKCAL(int kcal)
    {
        kcalLabel.text = kcal.ToString();
    }

    void UpdateTimeElapsed(int totalMissionLength, int missionTimeElapsed)
    {
        timeElapsedLabel.text = SecToMinutesStringFmt(missionTimeElapsed);
        timeRemainingLabel.text = SecToMinutesStringFmt(totalMissionLength - missionTimeElapsed);

        UpdateSlider(1.0 - (missionTimeElapsed / (double)totalMissionLength), timeElapsedSlider);
    }

    void UpdateBattery(float currentBattery, float maxBattery, float batteryDrain)
    {
        //batteryTimeRemainingLabel.text = ;
        UpdateMainSlider(currentBattery/maxBattery, batterySlider, batteryPercentLabel, batteryTimeRemainingLabel);
    }

    void UpdateOxygen(float currentOxygen, float maxOxygen, float currentOxygenConsumption)
    {
        //oxygenTimeRemainingLabel.text = ;
        UpdateMainSlider(currentOxygen/maxOxygen, oxygenSlider, oxygenPercentLabel, oxygenTimeRemainingLabel);
    }

    void UpdateWater()
    {
        //waterTimeRemainingLabel.text = ;
        UpdateMainSlider(0.7, waterSlider, waterPercentLabel, waterTimeRemainingLabel);
    }

    void UpdateDEG(int value)
    {
        degValueLabel.text = value.ToString();
        UpdateDialImage(0.5, degDialImage);
    }

    void UpdatePSI(int value)
    {
        psiValueLabel.text = value.ToString();
        UpdateDialImage(0.5, psiDialImage);
    }

    void UpdateRH(float value)
    {
        rhValueLabel.text = value.ToString();
        UpdateDialImage(0.5, rhDialImage);
    }

    void UpdateBQ(float value)
    {
        bqValueLabel.text = value.ToString();
        UpdateDialImage(0.5, bqDialImage);
    }

    private void UpdateDialImage(double value, Image dialImage)
    {
        dialImage.sprite = positiveDialSprite;

        if (value < 0.3)
        {
            dialImage.sprite = negativeDialSprite;
        }
        else if (value < 0.6)
        {
            dialImage.sprite = neutralDialSprite;
        }

        dialImage.transform.rotation = Quaternion.Euler(dialImage.transform.eulerAngles.x, dialImage.transform.eulerAngles.y, (float) (value - 0.5)*-90*2);
    }

    private string SecToMinutesStringFmt(int seconds)
    {
        int minutes = seconds / 60;
        int hours = minutes / 60;

        return hours + ":" + (minutes % 60).ToString("D2");
    }

    private void UpdateMainSlider(double value, Slider slider, TextMeshProUGUI percentLabel, TextMeshProUGUI timeRemainingLabel)
    {
        percentLabel.text = value * 100 + " %";
        UpdateSlider(value, slider);
    }

    private void UpdateSlider(double value, Slider slider)
    {
        slider.value = (float) value;

        GameObject fill = slider.fillRect.gameObject;
        Image img = fill.GetComponent<Image>();
        img.color = ColorsEnum.Positive.ToColor();

        if (value < 0.3)
        {
            img.color = ColorsEnum.Negative.ToColor();
        } else if (value < 0.6)
        {
            img.color = ColorsEnum.Neutral.ToColor();
        }
    }
}
