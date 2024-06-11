using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeoCoderWeb : MonoBehaviour
{
    [SerializeField] RadiPlacement placer;
    [SerializeField] Population popAPI;
    void Update()
    {
        // Check if the left mouse button is clicked
        if (Input.GetMouseButtonDown(0))
        {
            // Cast a ray from the mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Check if the ray hits any collider
            if (Physics.Raycast(ray, out hit))
            {
                // Check if the collider has a Cell component
                Cell cell = hit.collider.GetComponent<Cell>();
                if (cell != null)
                {
                    GameObject[] objectsToRemove = GameObject.FindGameObjectsWithTag("RadiPlace");

                    // Loop through each GameObject and destroy it
                    foreach (GameObject obj in objectsToRemove)
                    {
                        Destroy(obj);
                    }
                    // Get the coordinates of the cell
                    string x = cell.GetXCoordinate();
                    string y = cell.GetYCoordinate();

                    // Print the coordinates

                    Debug.Log($"Clicked on cell: X = {x}, Y = {y}");
                    popAPI.Latitude_ = x;
                    popAPI.Longitude_ = y;
                    popAPI.GeoEnrichment(hit.collider.gameObject);


                }
            }
        }
    }
}
