using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : MonoBehaviour
{
    public GameObject WaypointPrefab;
    public GameObject NewWaypointRoot;
    public GameObject AdminPanel;
    public LineRenderer LineRenderer;
    public float groundLevel = 0.3f;

    public bool adminEnabled = false;

    public List<GameObject> Waypoints = new List<GameObject>();
    private bool shouldUpdate = false;
    private LineRenderer lineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        if (!adminEnabled)
        {
            AdminPanel.SetActive(false);
            shouldUpdate = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldUpdate)
        {
            Debug.Log("Updating path.");
            shouldUpdate = false;
            RenderWaypointLines();
        }
    }

    public void CreateWaypoint()
    {
        Debug.Log("Creating waypoint.");
        var waypoint = Instantiate(WaypointPrefab);
        waypoint.transform.position = NewWaypointRoot.transform.position;

        Waypoints.Add(waypoint);
        shouldUpdate = !adminEnabled;
    }

    public void RenderWaypointLines()
    {
        List<Vector3> waypointPositions = new List<Vector3>();


        foreach (GameObject waypoint in Waypoints)
        {
            waypoint.transform.eulerAngles = new Vector3(0f, 0f, 0f);
            waypoint.transform.position = new Vector3(waypoint.transform.position.x, groundLevel, waypoint.transform.position.z);
            waypointPositions.Add(waypoint.transform.position);
        }

        LineRenderer.positionCount = waypointPositions.Count;
        LineRenderer.SetPositions(waypointPositions.ToArray());
    }
}
