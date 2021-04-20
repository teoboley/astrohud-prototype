using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class TaskListServerAdapter : MonoBehaviour
{
    #region Public Fields
    public TaskListController taskListController;
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
        if (TaskListMessage.Is(message))
        {
            Debug.Log("Received task list update within task list");
            var tasks = new TaskListMessage(message).payload.tasks;
            taskListController.SetTaskItems(
                tasks.ConvertAll(new Converter<TaskState, TaskController.Task>(ServerTaskToTask))
            );
        }
    }

    public static TaskController.Task ServerTaskToTask(TaskState taskState)
    {
        return new TaskController.Task(taskState.description, taskState.time, taskState.subtasks);
    }
}
