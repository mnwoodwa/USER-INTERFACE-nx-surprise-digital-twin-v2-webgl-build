using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildingManager : MonoBehaviour
{
    public GameObject[] objects;
    public GameObject pendingObject;

    private Vector3 position;
    private RaycastHit hit;

    [SerializeField]
    private LayerMask layerMask;

    public float gridSize;
    bool gridOn;
    [SerializeField]
    private Toggle gridToggle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if(pendingObject != null)
        {
            if(gridOn)
            {
                pendingObject.transform.position = new Vector3(
                    roundtoNearestGrid(position.x),
                    roundtoNearestGrid(position.y),
                    roundtoNearestGrid(position.z)
                    );
            }
            else
            {
                pendingObject.transform.position = position;
            }

            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                placeObject();
            }
        }
    }

    public void placeObject()
    {
        pendingObject = null;
    }

    private void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        if(Physics.Raycast(ray, out hit, 1000, layerMask))
        {
            position = hit.point;
        }
    }

    public void selectObject(int index)
    {
        pendingObject = Instantiate(objects[index], position, transform.rotation);
    }

    public void ToggleGrid()
    {
        if(gridToggle.isOn)
        {
            gridOn = true;
        }
        else
        {
            gridOn = false;
        }
    }

    float roundtoNearestGrid(float position)
    {
        float xpos = position % gridSize;

        position -= xpos;
        if(xpos > (gridSize/2))
        {
            position += gridSize;
        }
        return position;
    }
}
