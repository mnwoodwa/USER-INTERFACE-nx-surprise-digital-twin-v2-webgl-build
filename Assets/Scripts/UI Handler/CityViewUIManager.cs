using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CityViewUIManager : MonoBehaviour
{
    [SerializeField] GameObject apiHolder;
    [SerializeField] TMP_Dropdown propertyHolder;
    private GameObject currProperty;
    [SerializeField] DataFetch popSummary;
    [SerializeField] GameObject DataHolder;
    [SerializeField] GameObject TextObj;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            popSummary.GeoEnrichment();
            foreach (var k in popSummary.DataSetsDict)
            {
                GameObject instantiatedObject = Instantiate(TextObj);
                instantiatedObject.transform.SetParent(DataHolder.transform, false);
                instantiatedObject.GetComponent<TextMeshProUGUI>().text = k.ToString();
            }
        }
    }
    void populateProperties(GameObject parent)
    {
        //List<GameObject> props = apiHolder.GetComponentInChildren<DataFetch>().GetObj();
    }
    public void DeleteChildren(GameObject parent)
    {
        // Loop through all child objects
        foreach (Transform child in parent.transform)
        {
            // Destroy the child GameObject
            GameObject.Destroy(child.gameObject);
        }
    }
}
