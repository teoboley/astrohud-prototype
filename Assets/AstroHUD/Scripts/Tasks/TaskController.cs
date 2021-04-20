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
            this.description = description;
            this.time = time;
            this.subtasks = subtasks;
        }
    }

    [Header("Task Information Objects")]
    public TextMeshProUGUI TaskDescription;
    public TextMeshProUGUI TaskTime;

    [Header("Containers")]
    public GameObject SubTaskContainer;

    [Header("Task Configuration")]
    public Task task;
    //public string taskTitle;
    //public string taskTime;
    //public string[] subTaskItems;

    public GameObject SubtaskPrefab;

    [Header("Spacers")]
    public GameObject LineSpacer;
    public GameObject BottomSpacer;
    public GameObject Divider;
    #endregion

    private bool isExpanded = false;
    private List<GameObject> subTaskGameObjects = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        LineSpacer.SetActive(false);
        BottomSpacer.SetActive(false);
        Divider.SetActive(false);
        SubTaskContainer.SetActive(false);

        if (this.task == null)
        {
            var newTask = new Task("Dummy Task", "01:20", new string[] { "Subtask 1", "Subtask 2", "Subtask 3" });
            SetTask(newTask);
        }
    }

    public void SetTask(Task newTask)
    {
        Debug.Log("Set task: " + newTask.description);
        task = newTask;
        UpdateView();
    }

    public void UpdateView()
    {
        TaskDescription.text = task.description;
        TaskTime.text = task.time;
        UpdateSubTaskItems();
    }

    public void Toggle()
    {
        SubTaskContainer.SetActive(!isExpanded);
        LineSpacer.SetActive(!isExpanded);
        BottomSpacer.SetActive(!isExpanded);
        Divider.SetActive(!isExpanded);

        isExpanded = !isExpanded;
    }

    public void UpdateSubTaskItems()
    {
        // clear old subtasks
        for (int i = 0; i < subTaskGameObjects.Count; i++)
        {
            Destroy(subTaskGameObjects[i]);
        }
        this.subTaskGameObjects.Clear();

        // add new subtasks
        for (int i = 0; i < task.subtasks.Length; i++)
        {
            var subtask = Instantiate(SubtaskPrefab, SubTaskContainer.transform);
            subtask.GetComponentInChildren<SubtaskManager>().UpdateDescription(task.subtasks[i]);
            this.subTaskGameObjects.Add(subtask);
        }
    }
}
