using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadData : MonoBehaviour
{
    public Item structure;
    public List<Item> StructureData = new List<Item>();

    public void LoadItem()
    {
        StructureData.Clear();
        List<Dictionary<string, object>> data = CSVReader.Read("Plan");
        for(var i = 0; i < data.Count; i++)
        {
            string Structures = data[i]["Structures"].ToString();
            string Road = data[i]["Road"].ToString();
            string Walkway_Park = data[i]["Walkway_Park"].ToString();
            string F_B = data[i]["F_B"].ToString();
            string Retail = data[i]["Retail"].ToString();
            string Entertainment = data[i]["Entertainment"].ToString();
            string Parking = data[i]["Parking"].ToString();
            string Under_Construction = data[i]["Under_Construction"].ToString();
            string Property_Values = data[i]["Property_Values"].ToString();
            string For_Sale = data[i]["For_Sale"].ToString();
            string Footfall = data[i]["Footfall"].ToString();
            string Residential = data[i]["Residential"].ToString();
            string Office_Space = data[i]["Office_Space"].ToString();
            AddItem(Structures,Road,Walkway_Park,F_B,Retail,Entertainment,Parking,Under_Construction,Property_Values,For_Sale,Footfall,Residential,Office_Space);

        }
    }
    public void AddItem(string structures, string road, string walkwayPark, string fb, string retail, string entertainment, string parking, string underConstruction, string propertyValues, string forSale, string footfall, string residential, string officeSpace)
    {
        Item newItem = new Item(structure);
        newItem.Structures = structures;
        newItem.Road = road;
        newItem.Walkway_Park = walkwayPark;
        newItem.F_B = fb;
        newItem.Retail = retail;
        newItem.Entertainment = entertainment;
        newItem.Parking = parking;
        newItem.Under_Construction = underConstruction;
        newItem.Property_Values = propertyValues;
        newItem.For_Sale = forSale;
        newItem.Footfall = footfall;
        newItem.Residential = residential;
        newItem.Office_Space = officeSpace;


        StructureData.Add(newItem);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            //Debug.Log(StructureData);
        }
    }
}
