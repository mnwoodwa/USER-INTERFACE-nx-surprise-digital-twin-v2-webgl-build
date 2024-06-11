using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DemographicsInfo : MonoBehaviour
{
    [SerializeField]
    private GameObject infoGrid;

    [SerializeField]
    private Transform infoParent;

    [SerializeField]
    private Population population;

    [SerializeField]
    private GameObject geocoder;

    private GameObject info1, info2, info3;
    private RectTransform transform1, transform2, transform3;
    private TMP_Text text1, text2, text3;

    // Start is called before the first frame update
    void Start()
    {
        //Geocoder is disabled by default
        geocoder.SetActive(false);
    }

    //Spawn The Information Grids
    public void SpawnInfo()
    {
        if(info1 == null)//Only Instantiate if the Information Grid has not yet been instantiated
        {
            //Instantiate Three Information Grids for the Information Display
            info1 = Instantiate(infoGrid, new Vector3(0f, 0f, 0f), Quaternion.identity, infoParent);
            info2 = Instantiate(infoGrid, new Vector3(5f, 0f, 0f), Quaternion.identity, infoParent);
            info3 = Instantiate(infoGrid, new Vector3(10f, 0f, 0f), Quaternion.identity, infoParent);

            //Access the Rect Transform and position them respectively
            transform1 = info1.GetComponent<RectTransform>();
            transform1.anchoredPosition = new Vector2(0.0f, 0.0f);

            transform2 = info2.GetComponent<RectTransform>();
            transform2.anchoredPosition = new Vector2(58.0f, -76.0f);

            transform3 = info3.GetComponent<RectTransform>();
            transform3.anchoredPosition = new Vector2(114.0f, -150.0f);

            GetComponents();
        }
        else
        {
            //If the Information Grid has previously been instantiated then use the previous Grid
            info1.SetActive(true);
            info2.SetActive(true);
            info3.SetActive(true);
        }

        //Geocoder is enabled only after all the Information Grids have been instantiated at least once
        geocoder.SetActive(true);
    }

    //Remove the Information Grids from the UI
    public void RemoveInfo()
    {
        if(info1 != null)
        {
            //Set the Info Inactive to be used again instead of reinstantiating it
            info1.SetActive(false);
            info2.SetActive(false);
            info3.SetActive(false);
        }

        //Geocoder is disabled when the Information grids are removed from the screen
        geocoder.SetActive(false);
    }

    //Access the text and other reusable components of the Information Grid
    private void GetComponents()
    {
        text1 = info1.GetComponentInChildren<TMP_Text>();
        text2 = info2.GetComponentInChildren<TMP_Text>();
        text3 = info3.GetComponentInChildren<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Change the Population Data of the Information Grids
    public void PopulationUpdate(int mileRadius)
    {
        double[] result = population.GetPopulationData();

        //Make the Button always pressed whenever Population data is being modified
        ButtonHandler buttonHandler = new ButtonHandler();
        buttonHandler.updateButton();

        Debug.Log("Population Hit");

        if (result[0] != 0.0d)
        {
            switch(mileRadius)
            {
                case 1:
                    text1.text = "2022 - Males: " + result[0] + "\n2022 - Females: " + result[1] + "\n2022 - Total: " + result[2];
                    break;
                case 3:
                    text2.text = "2022 - Males: " + result[0] + "\n2022 - Females: " + result[1] + "\n2022 - Total: " + result[2];
                    break;
                case 5:
                    text3.text = "2022 - Males: " + result[0] + "\n2022 - Females: " + result[1] + "\n2022 - Total: " + result[2];
                    break;
                default:
                    Debug.LogError("Invalid Radius Data");
                    break;
            }
        }
    }

    //Change the Households Data of the Information Grids
    public void HouseholdsUpdate(int mileRadius)
    {
        double[] result = population.GetHouseholdData();

        //Make the Button always pressed whenever Population data is being modified
        ButtonHandler buttonHandler = new ButtonHandler();
        buttonHandler.updateButton();

        Debug.Log("Households Hit");

        if (result[0] != 0.0d)
        {
            switch (mileRadius)
            {
                case 1:
                    text1.text = "Total Population: " + result[2] + "\nTotal Households: " + result[3] + "\nAverage Household Size: " + result[4];
                    break;
                case 3:
                    text2.text = "Total Population: " + result[2] + "\nTotal Households: " + result[3] + "\nAverage Household Size: " + result[4];
                    break;
                case 5:
                    text3.text = "Total Population: " + result[2] + "\nTotal Households: " + result[3] + "\nAverage Household Size: " + result[4];
                    break;
                default:
                    Debug.LogError("Invalid Radius Data");
                    break;
            }
        }
    }

    //Change the Income Data of the Information Grids
    public void IncomeUpdate()
    {
        Debug.Log("Income Hit");
        text1.text = "Income Button was Hit";
        text2.text = "Income Button was Hit";
        text3.text = "Income Button was Hit";
    }

    //Change the Housing Units Data of the Information Grids
    public void HousingUnitsUpdate()
    {
        Debug.Log("Housing Units Hit");
        text1.text = "Housing Units Button was Hit";
        text2.text = "Housing Units Button was Hit";
        text3.text = "Housing Units Button was Hit";
    }

    //Change the Daytime Population Data of the Information Grids
    public void DaytimePopulationUpdate()
    {
        Debug.Log("Daytime Population Hit");
        text1.text = "Daytime Population Button was Hit";
        text2.text = "Daytime Population Button was Hit";
        text3.text = "Daytime Population Button was Hit";
    }

    //Change the Businesses Data of the Information Grids
    public void BusinessesUpdate()
    {
        Debug.Log("Businesses Hit");
        text1.text = "Businesses Button was Hit";
        text2.text = "Businesses Button was Hit";
        text3.text = "Businesses Button was Hit";
    }
}
