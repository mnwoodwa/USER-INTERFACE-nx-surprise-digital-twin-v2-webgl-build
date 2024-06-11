using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    public GameObject targetObject; // Reference to the 3D object to fill
    public int rows = 10; // Number of rows in the grid
    public int columns = 10; // Number of columns in the grid
    public Material cellMaterial; // Material for the cells


    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        // Get the size of the target object
        Vector3 size = targetObject.GetComponent<Renderer>().bounds.size;

        // Calculate the size of each cell
        float cellSizeX = size.x / columns;
        float cellSizeZ = size.z / rows;

        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                // Calculate the position of the current cell
                Vector3 position = new Vector3(
                    x * cellSizeX + cellSizeX / 2f,
                    0,
                    y * cellSizeZ + cellSizeZ / 2f
                ) + targetObject.transform.position - size / 2f;

                // Create a new GameObject for each cell
                GameObject cell = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cell.transform.position = position;
                cell.transform.localScale = new Vector3(cellSizeX, 1, cellSizeZ);

                // Parent the cell to the Grid GameObject
                cell.transform.parent = this.transform;
                // Set the cell's material
                MeshRenderer renderer = cell.GetComponent<MeshRenderer>();
                if (cellMaterial != null)
                {
                    renderer.material = cellMaterial;
                }
                Cell cellComponent = cell.AddComponent<Cell>();
                cellComponent.SetCoordinates(x.ToString(), y.ToString());

                // Parent the cell to the Grid GameObject
                cell.transform.parent = this.transform;
            }
        }
    }
}
