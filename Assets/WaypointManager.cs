using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : MonoBehaviour
{
    public GameObject WaypointPrefab;
    public GameObject NewWaypointRoot;
    public LineRenderer LineRenderer;


    public List<GameObject> Waypoints = new List<GameObject>();
    private bool shouldUpdate = false;
    private LineRenderer lineRenderer;

    // Start is called before the first frame update
    void Start()
    {
    //    var line = LineRenderer.gameObject;
    //  line.transform.position = new Vector3(line.transform.position.x, -0.6f, line.transform.position.z);
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
        //shouldUpdate = true;
    }

    public void RenderWaypointLines()
    {
        List<Vector3> waypointPositions = new List<Vector3>();


        foreach (GameObject waypoint in Waypoints)
        {
            waypoint.transform.position = new Vector3(waypoint.transform.position.x, -0.6f, transform.position.z);
            waypointPositions.Add(waypoint.transform.position);
        }

        LineRenderer.positionCount = waypointPositions.Count;
        LineRenderer.SetPositions(waypointPositions.ToArray());
    }
}
