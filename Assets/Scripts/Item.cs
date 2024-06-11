using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class Item 
{
    public string Structures;
    public string Road;
    public string Walkway_Park;
    public string F_B;
    public string Retail;
    public string Entertainment;
    public string Parking;
    public string Under_Construction;
    public string Property_Values;
    public string For_Sale;
    public string Footfall;
    public string Residential;
    public string Office_Space;
    //public string Miscellaneous;
    public Item(Item t)
    {
        Structures = t.Structures;
        Road = t.Road;
        Walkway_Park = t.Walkway_Park;
        F_B = t.F_B;
        Retail = t.Retail;
        Entertainment = t.Entertainment;    
        Parking = t.Parking;
        Under_Construction = t.Under_Construction;
        Property_Values = t.Property_Values;
        For_Sale = t.For_Sale;
        Footfall = t.Footfall;
        Residential = t.Residential;
        Office_Space = t.Office_Space;
    }



}
