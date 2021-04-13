using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskListManager : MonoBehaviour
{ 
    [Header("Content")]
    public GameObject ViewPort;

    public GameObject ButtonTemplate;

    public int numButtons = 15;

    // Start is called before the first frame update
    void Start()
    {
        GameObject button = Instantiate(ButtonTemplate) as GameObject;
        button.transform.SetParent(ViewPort.transform);
        button.SetActive(true);
        GameObject button2 = Instantiate(ButtonTemplate) as GameObject;
        button2.transform.SetParent(ViewPort.transform);
        button2.SetActive(true);
       
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
