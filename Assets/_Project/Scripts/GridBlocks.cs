using System.Collections.Generic;
using UnityEngine; 

public class GridBlocks : MonoBehaviour
{ 
    [SerializeField] private Cell prefabGrid;
    [SerializeField] private List<LinePosition> linePositions;
    [SerializeField] private float space;
    [SerializeField] private Transform content;
    private List<Cell> cells = new List<Cell>();
    private void Start()
    {
        cells.ForEach(s => Destroy(s.gameObject));
        cells.Clear();
        foreach (var line in linePositions)
        {
            line.SetStartPosition(content.position);
        }
       int lineIterator = 0; 
        foreach (var item in linePositions)
        {
            if (linePositions[0] != item)
            {
                float pos = linePositions[lineIterator - 1].position.y;
                item.SetupPositionY(space, pos- space);
                item.SetupPositionX(space);
            }
            else
            {
                item.SetupPositionY(content.position.x, content.position.y);
                item.SetupPositionX(space);

            }
            item.line = lineIterator;
            lineIterator++;
        }

        CreateCells();
    }

    private void CreateCells()
    {
        int lineNumber = 0;
        foreach (var line in linePositions)
        {
            foreach (var pos in line.positions)
            {
                var cell = CreateCell(pos.position, pos.number);
                cells.Add(cell);
                cell.line = lineNumber;
                cell.number = pos.number;
            }
            lineNumber++;
        }
    }

    public Cell CreateCell(Vector2 position, int number)
    {
        var cell = Instantiate(prefabGrid, position, Quaternion.identity,content);
        cell.number = number;
        return cell;
    }
}
