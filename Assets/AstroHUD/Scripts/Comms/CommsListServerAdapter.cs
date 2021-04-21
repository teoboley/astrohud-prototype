using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class CommsListServerAdapter : MonoBehaviour
{
    #region Public Fields
    public CommsIndicatorListController commsIndicatorListController;
    public AstroHUDServerConnection serverConnection;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        serverConnection.messageReceived.AddListener(
            ServerMessageReceived
        );
    }

    void ServerMessageReceived(IMessage message)
    {
        if (CommsListMessage.Is(message))
        {
            Debug.Log("Received comms list update within comms list");
            var voiceIndicators = new CommsListMessage(message).payload.voiceIndicators;
            commsIndicatorListController.SetIndicatorItems(
                voiceIndicators.ConvertAll(new Converter<AstronautVoiceIndicatorState, CommsIndicatorListController.AstronautIndicatorData>(ServerIndicatorToLocalData))
            );
        }
    }

    public static CommsIndicatorListController.AstronautIndicatorData ServerIndicatorToLocalData(AstronautVoiceIndicatorState indicatorState)
    {
        Color newCol = Color.blue;
        ColorUtility.TryParseHtmlString(indicatorState.color, out newCol);

        return new CommsIndicatorListController.AstronautIndicatorData(indicatorState.astronautName, indicatorState.active, newCol);
    }
}
