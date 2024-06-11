using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    private Camera mainCamera;
    private GameObject selectedObject;
    private Vector3 offset;
    private float zCoordinate;
    private float resizeSpeed = 0.1f; // Speed at which the object will resize
    public float colorChangeDuration = 2f;
    public Item item;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        // On Mouse Down
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag("Draggable"))
                {
                    selectedObject = hit.transform.gameObject;
                    zCoordinate = mainCamera.WorldToScreenPoint(selectedObject.transform.position).z;
                    offset = selectedObject.transform.position - GetMouseWorldPos();
                }
            }
        }

        // On Mouse Drag
        if (Input.GetMouseButton(0) && selectedObject != null)
        {
            selectedObject.transform.position = GetMouseWorldPos() + offset;
            Vector3 temp = new Vector3(selectedObject.transform.position.x, -1, selectedObject.transform.position.z);
            selectedObject.transform.position = temp;
        }
        if (selectedObject == null)
        {
            Vector3 temp = new Vector3(this.transform.position.x, -2, this.transform.position.z);
            this.transform.position = temp;
        }
        // On Mouse Up
        if (Input.GetMouseButtonUp(0) && selectedObject != null)
        {
            StartCoroutine(ChangeColorOverTime(selectedObject.GetComponent<Renderer>(), Color.blue, colorChangeDuration));
            selectedObject = null;
        }

        // Resize the object using keyboard inputs
        if (selectedObject != null)
        {
            StartCoroutine(ChangeColorOverTime(selectedObject.GetComponent<Renderer>(), Color.green, colorChangeDuration));

            Vector3 newScale = selectedObject.transform.localScale;

            // Adjust x-axis size with A and D keys
            if (Input.GetKey(KeyCode.D))
            {
                newScale.x += resizeSpeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.A))
            {
                newScale.x -= resizeSpeed * Time.deltaTime;
            }

            // Adjust z-axis size with W and S keys
            if (Input.GetKey(KeyCode.W))
            {
                newScale.z += resizeSpeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.S))
            {
                newScale.z -= resizeSpeed * Time.deltaTime;
            }

            // Ensure the scale doesn't go below 0.1
            newScale.x = Mathf.Max(newScale.x, 0.1f);
            newScale.z = Mathf.Max(newScale.z, 0.1f);

            selectedObject.transform.localScale = newScale;
        }
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = zCoordinate; // Maintain z coordinate of the object being dragged
        return mainCamera.ScreenToWorldPoint(mousePoint);
    }

    private IEnumerator ChangeColorOverTime(Renderer objectRenderer, Color newColor, float duration)
    {
        Color currentColor = objectRenderer.material.color;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            t = t * t * (3f - 2f * t); // Smoothstep function for easing
            objectRenderer.material.color = Color.Lerp(currentColor, newColor, t);
            yield return null;
        }

        objectRenderer.material.color = newColor;
    }
}
