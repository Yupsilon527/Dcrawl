using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayItemTile : MonoBehaviour
{
    public Vector2Int gridPos;
    public DataItemTile data;
    public DataItemWorld room;

    public GameObject mesh;

    public void InitData(DataItemTile data, Vector2Int worldPos)
    {
        this.gridPos =worldPos;
        this.data = data;
    }

    public DisplayItemTile[] neighbors;
    public void InitNeighbors(bool real)
    {
        Vector2Int coords = gridPos;
        neighbors = new DisplayItemTile[]
        {
            DataItemWorld.main.GetTile(coords.x + 1,coords.y,real),
        DataItemWorld.main.GetTile(coords.x - 1, coords.y ,real),
        DataItemWorld.main.GetTile(coords.x, coords.y + 1,real),
            DataItemWorld.main.GetTile(coords.x ,coords.y - 1,real),
        };
    }


    #region entity
    public Mob LocatedEntity;
    public bool IsPassible()
    {
        return !data.Solid;
    }
    #endregion
    public bool IsAdjecent(DisplayItemTile other)
    {
        return (other.gridPos.x - gridPos.x == 0 && Mathf.Abs(other.gridPos.y - gridPos.y) == 1) || (other.gridPos.y - gridPos.y == 0 && Mathf.Abs(other.gridPos.x - gridPos.x) == 1);
    }
    public int DistanceSquared(DisplayItemTile other)
    {
        Vector2Int delta = gridPos - other.gridPos;
        return delta.sqrMagnitude;
    }
}
