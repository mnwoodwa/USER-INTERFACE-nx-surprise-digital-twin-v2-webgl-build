using Unity.VisualScripting;
using UnityEngine;

public class StructureSelection : MonoBehaviour
{
    private GameObject lastSelectedObject;
    private Color lastSelectedObjectOriginalColor;
    public GameObject AddDataToStructure;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject hitObject = hit.collider.gameObject;

                if (hitObject.CompareTag("Draggable"))
                {
                    // Restore the color of the last selected object
                    if (lastSelectedObject != null)
                    {
                        SetObjectColor(lastSelectedObject, lastSelectedObjectOriginalColor);
                    }

                    // Store the original color of the currently selected object
                    lastSelectedObject = hitObject;
                    lastSelectedObjectOriginalColor = GetObjectColor(lastSelectedObject);

                    // Change the color of the currently selected object to blue
                    SetObjectColor(lastSelectedObject, Color.blue);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (lastSelectedObject != null)
            {
                Debug.Log(lastSelectedObject.name.ToString());
            }
        }

    }

    private Color GetObjectColor(GameObject obj)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            return renderer.material.color;
        }
        return Color.white; // Default color if no renderer found
    }

    private void SetObjectColor(GameObject obj, Color color)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = color;
        }
    }
    public GameObject GetObjectVariable()
    {
        return lastSelectedObject;
    }
}
