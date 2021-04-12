using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SubtaskManager : MonoBehaviour
{
    public TextMeshProUGUI Description;


    public void UpdateDescription(string text)
    {
        Description.text = text;
    }
}
