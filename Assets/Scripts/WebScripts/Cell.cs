using UnityEngine;

public class Cell : MonoBehaviour
{
    public string row = "-112.07366198397744";
    public string column = "33.46592518188455";

    public void SetCoordinates(string row, string column)
    {
        this.row = row;
        this.column = column;
    }

    void OnMouseDown()
    {
        Debug.Log($"Cell clicked: Row {row}, Column {column}");
    }

    public string GetXCoordinate()
    {
        return row;
    }

    public string GetYCoordinate()
    {
        return column;
    }
}
