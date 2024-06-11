using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDemographics : MonoBehaviour
{
    [SerializeField]
    private GameObject demographicsGrid;

    // Start is called before the first frame update
    void Start()
    {
        //Set the demographics Grid False by default
        demographicsGrid.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Trigger from the OnClick button Event to display the demographics grid
    public void SetDemographicsActive()
    {
        demographicsGrid.SetActive(true);
    }

    //Trigger from the OnClick button Event to stop display the demographics grid
    public void SetDemographicsInactive()
    {
        demographicsGrid.SetActive(false);
    }
}
