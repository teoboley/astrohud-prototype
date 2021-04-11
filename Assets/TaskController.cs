using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class TaskController : MonoBehaviour
{
    #region Public Fields
    public class Task
    {
        public string description;
        public string time;

        public string[] subtasks;

        public Task(string description, string time, string[] subtasks)
        {
            description = description;
            time = time;
            subtasks = subtasks;
        }
    }

    [Header("Task Information Objects")]
    public TextMeshProUGUI TaskDescription;
    public TextMeshProUGUI TaskTime;

    [Header("Containers")]
    public GameObject SubTaskContainer;

    [Header("Task Configuration")]
    public string taskTitle;
    public string taskTime;
    public string[] subTaskItems;

    public bool isExpanded = false;

    public GameObject SubtaskPrefab;

    [Header("Spacers")]
    public GameObject LineSpacer;
    public GameObject BottomSpacer;
    public GameObject Divider;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        LineSpacer.SetActive(false);
        BottomSpacer.SetActive(false);
        Divider.SetActive(false);
        SubTaskContainer.SetActive(false);

        TaskDescription.text = taskTitle;
        TaskTime.text = taskTime;

        for (int i = 0; i < subTaskItems.Length; i++)
        {
            var subtask = Instantiate(SubtaskPrefab, SubTaskContainer.transform);
            subtask.GetComponentInChildren<SubtaskManager>().UpdateDescription(subTaskItems[i]);
        }
    }

    public void toggle()
    {
        SubTaskContainer.SetActive(!isExpanded);
        LineSpacer.SetActive(!isExpanded);
        BottomSpacer.SetActive(!isExpanded);
        Divider.SetActive(!isExpanded);

        isExpanded = !isExpanded;
    }
}
