using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class TaskListController : MonoBehaviour
{
    #region Public Fields
    [Header("Containers")]
    public GameObject TaskContainer;

    [Header("Task Configuration")]
    public List<TaskController.Task> taskItems = new List<TaskController.Task>();
    public GameObject TaskPrefab;
    #endregion

    private List<GameObject> taskGameObjects = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        SetTaskItems(new List<TaskController.Task> {
            new TaskController.Task("Navigate to Lander", "00:00", new string[] { "A Subtask" }),
             new TaskController.Task("Grab tools", "00:15", new string[] { "A Subtask" }),
              new TaskController.Task("Return to base", "00:30", new string[] { "A Subtask" })
        });
    }

    public void SetTaskItems(List<TaskController.Task> newTaskItems)
    {
        this.taskItems = newTaskItems;
        UpdateTaskItems();
    }

    public void UpdateTaskItems()
    {
        // TODO: clear old subtasks
        for (int i = 0; i < taskGameObjects.Count; i++)
        {
            Destroy(taskGameObjects[i]);
        }
        this.taskGameObjects.Clear();

        // add new subtasks
        for (int i = 0; i < taskItems.Count; i++)
        {
            var task = Instantiate(TaskPrefab, TaskContainer.transform);
            task.GetComponentInChildren<TaskController>().SetTask(taskItems[i]);
            this.taskGameObjects.Add(task);
        }
    }
}
