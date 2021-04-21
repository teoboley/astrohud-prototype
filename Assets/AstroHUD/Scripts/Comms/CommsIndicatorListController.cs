using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class CommsIndicatorListController : MonoBehaviour
{
    public GameObject CommsIndicatorPrefab;
    public GameObject IndicatorContainer;

    private List<AstronautIndicatorData> astronautIndicatorItems;
    private List<GameObject> indicatorGameObjects = new List<GameObject>();

    public struct AstronautIndicatorData
    {
        public string name;
        public bool active;
        public Color color;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetIndicatorItems(List<AstronautIndicatorData> astronautIndicatorItems)
    {
        this.astronautIndicatorItems = astronautIndicatorItems;
        UpdateView();
    }

    public void UpdateView()
    {
        // clear old indicators
        for (int i = 0; i < indicatorGameObjects.Count; i++)
        {
            Destroy(indicatorGameObjects[i]);
        }
        this.indicatorGameObjects.Clear();

        // add new indicators
        for (int i = 0; i < astronautIndicatorItems.Count; i++)
        {
            var indicatorData = astronautIndicatorItems[i];

            var indicatorGameObject = Instantiate(CommsIndicatorPrefab, IndicatorContainer.transform);
            indicatorGameObject.GetComponentInChildren<CommsIndicatorController>().SetAllProperties(indicatorData.name, indicatorData.active, indicatorData.color);
            this.indicatorGameObjects.Add(indicatorGameObject);
        }
    }
}
