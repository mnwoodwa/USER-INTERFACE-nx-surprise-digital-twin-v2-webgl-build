using UnityEngine;

public class CamController : MonoBehaviour
{
    public float dragSpeed = 2f; // Adjust the speed of the drag
    public float rotationSpeed = 100f; // Adjust the speed of the rotation
    public float zoomSpeed = 10f; // Adjust the speed of the zoom

    private Vector3 dragOrigin;
    private Vector3 rotateOrigin;

    void Update()
    {
        // Handle left mouse button for translation
        if (Input.GetMouseButtonDown(0))
        {
            // Record the initial mouse position when the left button is pressed
            dragOrigin = Input.mousePosition;
            return;
        }

        if (Input.GetMouseButton(0))
        {
            // Calculate the drag distance
            Vector3 difference = Input.mousePosition - dragOrigin;

            // Calculate the movement relative to the camera's forward direction
            Vector3 move = transform.up * (-difference.y * dragSpeed * Time.deltaTime) +
                           transform.right * (-difference.x * dragSpeed * Time.deltaTime);
            transform.Translate(move, Space.World);

            // Update the drag origin for the next frame
            dragOrigin = Input.mousePosition;
        }

        // Handle right mouse button for rotation
        if (Input.GetMouseButtonDown(1))
        {
            // Record the initial mouse position when the right button is pressed
            rotateOrigin = Input.mousePosition;
            return;
        }

        if (Input.GetMouseButton(1))
        {
            // Calculate the rotation based on mouse movement
            float mouseX = Input.mousePosition.x - rotateOrigin.x;
            float mouseY = Input.mousePosition.y - rotateOrigin.y;

            // Apply the rotation to the camera
            transform.Rotate(Vector3.forward, mouseX * rotationSpeed * Time.deltaTime, Space.World);
            transform.Rotate(Vector3.right, -mouseY * rotationSpeed * Time.deltaTime);

            // Update the rotate origin for the next frame
            rotateOrigin = Input.mousePosition;
        }

        // Handle scroll wheel for zoom
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0.0f)
        {
            // Calculate the zoom relative to the camera's forward direction
            Vector3 zoom = transform.forward * scroll * zoomSpeed * Time.deltaTime * 1000;
            transform.Translate(zoom, Space.World);
        }
    }
}
