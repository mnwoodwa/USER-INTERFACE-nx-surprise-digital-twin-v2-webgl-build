using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIStruct : MonoBehaviour
{
    public GameObject InfoWindow;
    public GameObject textField;
    public GameObject inputField;
    public StructureSelection selectStruct;
    public GameObject LoadedData;
    private List<Item> FetchedData = new List<Item>();
    public TMP_Dropdown dpdown;
    private Item LoadedItem;
    // Start is called before the first frame update
    void Start()
    {
        dpdown.ClearOptions();
        // Ensure the TMP_Dropdown is assigned in the Inspector
        if (dpdown != null)
        {
            // Add listener for when the value of the TMP_Dropdown changes
            dpdown.onValueChanged.AddListener(delegate {
                DropdownValueChanged(dpdown);
            });
        }
        else
        {
            Debug.LogError("TMP_Dropdown not assigned in the Inspector");
        }
    }

    // Update is called once per frame
    void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.L))
        {
            List<string> attr = new List<string>();
            Debug.Log("Pressed");
            if (InfoWindow != null)
            {
                List<TMP_InputField> tmpInputFields = GetAllTMPInputFields(InfoWindow);

                foreach (TMP_InputField _tmpInputField in tmpInputFields)
                {
                    Debug.Log("TMP_InputField: " + _tmpInputField.name + " Value: " + _tmpInputField.text);
                    attr.Add(_tmpInputField.text);
                }
            }
            else
            {
                Debug.LogError("Parent object not assigned in the Inspector");
            }
            MapItem(attr);
            //selectStruct.GetObjectVariable().GetComponent<DragObject>().item = new Item(); 
        }
        
    }
    public void MapItem(List<string> itemValues)
    {
        // Check if the provided list has enough values to map to the attributes of LoadedItem
        if (itemValues.Count < 13)
        {
            Debug.LogError("Insufficient values provided to map to the LoadedItem attributes.");
            return;
        }

        // Create a new Item instance
        Item it = new Item(LoadedItem);

        // Map values from the list to the LoadedItem attributes
        LoadedItem.Structures = itemValues[0];
        LoadedItem.Road = itemValues[1];
        LoadedItem.Walkway_Park = itemValues[2];
        LoadedItem.F_B = itemValues[3];
        LoadedItem.Retail = itemValues[4];
        LoadedItem.Entertainment = itemValues[5];
        LoadedItem.Parking = itemValues[6];
        LoadedItem.Under_Construction = itemValues[7];
        LoadedItem.Property_Values = itemValues[8];
        LoadedItem.For_Sale = itemValues[9];
        LoadedItem.Footfall = itemValues[10];
        LoadedItem.Residential = itemValues[11];
        LoadedItem.Office_Space = itemValues[12];

        // Assign the updated LoadedItem to the item component of the selected structure
        selectStruct.GetObjectVariable().GetComponent<DragObject>().item = it;
    }
    void CreateAttr(string attrName, string attrVal)
    {
        GameObject structItem = selectStruct.GetObjectVariable();
        GameObject newObject = Instantiate(textField);
        GameObject newObjectInput = Instantiate(inputField);
        newObject.GetComponent<TextMeshProUGUI>().text = attrName;
        newObjectInput.GetComponent<TMP_InputField>().text = attrVal;

        // Set the parent of the newly instantiated object
        newObject.transform.SetParent(InfoWindow.transform, false);
        newObjectInput.transform.SetParent(InfoWindow.transform, false);

        // Optionally reset the local position and rotation if needed
        newObject.transform.localPosition = Vector3.zero;
        newObject.transform.localRotation = Quaternion.identity;

        newObjectInput.transform.localPosition = Vector3.zero;
        newObjectInput.transform.localRotation = Quaternion.identity;

        // Optionally adjust the scale if needed
        newObject.transform.localScale = Vector3.one;
        newObjectInput.transform.localScale = Vector3.one;
    }

    void RemoveChildren(GameObject parent)
    {
        // Iterate through all child objects and destroy them
        foreach (Transform child in parent.transform)
        {
            Destroy(child.gameObject);
        }
    }

    void DisplayItemProperties(Item item)
    {
        LoadedItem = item;
        RemoveChildren(InfoWindow);

        // Get all public fields of the Item object
        FieldInfo[] fields = typeof(Item).GetFields(BindingFlags.Public | BindingFlags.Instance);
        foreach (FieldInfo field in fields)
        {
            // Create a new text field for each property
            //GameObject textField = Instantiate(textFieldPrefab, InfoWindow.transform);
            //Text textComponent = textField.GetComponent<Text>();

                // Set the text to display the field name and its value
                string inttext = $"{field.Name}: {field.GetValue(item)}";
            Debug.Log($"{field.Name}: {field.GetValue(item)}");
            CreateAttr(field.Name.ToString(), field.GetValue(item).ToString());
        }
    }
    void DropdownValueChanged(TMP_Dropdown change)
    {
        // Get the index of the selected option
        int selectedIndex = change.value;
        // Get the text of the selected option
        string selectedText = change.options[selectedIndex].text;

        // Output the selected value and text to the console
        Debug.Log("Selected Index: " + selectedIndex);
        Debug.Log("Selected Option: " + selectedText);
    }

    void DropdownValueDisplay(TMP_Dropdown change)
    {
        // Get the index of the selected option
        int selectedIndex = change.value;
        // Get the text of the selected option
        string selectedText = change.options[selectedIndex].text;

        // Output the selected value and text to the console
        Debug.Log("Selected Index: " + selectedIndex);
        Debug.Log("Selected Option: " + selectedText);

        foreach (Item itr in FetchedData)
        {
            if(itr.Structures.Equals(selectedText))
            {
                DisplayItemProperties(itr);
                break;
            }
        }
    }

    public void GetInputValue()
    {
        if (InfoWindow != null)
        {
            TMP_InputField tmpInputField = InfoWindow.GetComponentInChildren<TMP_InputField>();

            if (tmpInputField != null)
            {
                string inputValue = tmpInputField.text;
                Debug.Log("TMP Input Field Value: " + inputValue);
            }
            else
            {
                Debug.LogError("TMP_InputField not found under the specified parent object");
            }
        }
        else
        {
            Debug.LogError("Parent object not assigned in the Inspector");
        }
        GetInputValue();
    }
    List<TMP_InputField> GetAllTMPInputFields(GameObject parent)
    {
        List<TMP_InputField> tmpInputFields = new List<TMP_InputField>();

        foreach (Transform child in parent.transform)
        {
            TMP_InputField tmpInputField = child.GetComponent<TMP_InputField>();
            if (tmpInputField != null)
            {
                tmpInputFields.Add(tmpInputField);
            }
            // Recursively search for TMP_InputFields in children
            tmpInputFields.AddRange(GetAllTMPInputFields(child.gameObject));
        }

        return tmpInputFields;
    }

    void UpdatStruct()
    {
        List<string> attr = new List<string>();
        Debug.Log("Pressed");
        if (InfoWindow != null)
        {
            List<TMP_InputField> tmpInputFields = GetAllTMPInputFields(InfoWindow);

            foreach (TMP_InputField _tmpInputField in tmpInputFields)
            {
                Debug.Log("TMP_InputField: " + _tmpInputField.name + " Value: " + _tmpInputField.text);
                attr.Add(_tmpInputField.text);
            }
        }
        else
        {
            Debug.LogError("Parent object not assigned in the Inspector");
        }
        MapItem(attr);
    }
    //Button Functions
    public void ViewSelectedStruct()
    {
        GameObject structItem = selectStruct.GetObjectVariable();
        Item t = structItem.GetComponent<DragObject>().item;
        DisplayItemProperties(t);
    }

    public void PopulateStruct()
    {
        dpdown.ClearOptions();
        FetchedData = LoadedData.GetComponent<LoadData>().StructureData;
        List<string> options = new List<string>();
        foreach (Item item in FetchedData)
        {
            options.Add(item.Structures);
        }
        dpdown.AddOptions(options);
    }

    public void DropDownView()
    {
        DropdownValueDisplay(dpdown);
    }

    public void UpdateButtonStruct()
    {
        UpdatStruct();
    }
}
