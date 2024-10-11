using System;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class LinePosition
{
    [field: SerializeField] public List<Vector2Position> positions;
    [field: SerializeField] public int line {  get; set; }
    [field: SerializeField] public Vector2 position { get; private set; }


    public void SetStartPosition(Vector3 position)
    {
        positions[0].position = position;
    }
    public void SetupPositionY(float spaceX, float spaceY)
    {
        this.position = new Vector2( position.x + spaceX, position.y + spaceY);
    }
    public void SetupPositionX(float spaceX )
    {
        int i = 0;
        foreach (var position in positions)
        {
            if (position != positions[0])
            { 
                position.position = new Vector2(positions[i - 1].position.x + spaceX,this. position.y );
            }
            else
            {
                position.position = new Vector2(position.position.x  , this.position.y);

            }
                position.number = i;
                i++;
            
        }
    }

    
}
 