using System.Collections.Generic;
using UnityEngine;

public class DataItemWorld : MonoBehaviour
{
    public static DataItemWorld main;
    private void Awake()
    {
        main = this;
    }
    public DisplayItemTile[,] Tiles;
    public void FinalizeWorld()
    {
        foreach (DisplayItemTile tile in Tiles)
        {
            if (tile != null)
            {
                tile.InitNeighbors( false);
            }
        }
    }
    #region WorldSize
    public void InitializeWorld(int width, int height)
    {
        Tiles = new DisplayItemTile[width, height];
    }
    public void PushTile(DisplayItemTile tile, Vector2Int pos)
    {
        PushTile(pos.x,pos.y,tile);
    }
    public void PushTile(int iX, int iY, DisplayItemTile tile)
    {
        Tiles[iX, iY] = tile;
        tile.gridPos = new Vector2Int(iX, iY);

        tile.name = tile.data.tilePrefab.name + " " + tile.gridPos;
        Vector2 bidipos = tile.gridPos;
        tile.transform.position = new Vector3(bidipos.x, 0, bidipos.y);
    }
    #endregion

    #region Translate
    public Vector2Int TranslateCoordinate(Vector2 point)
    {
        return Vector2Int.RoundToInt(point);
    }
    public DisplayItemTile GetClosestToPoint(Vector2Int point)
    {
        float sqrDist = Mathf.Infinity;
        DisplayItemTile dest = null;
        foreach (DisplayItemTile n in Tiles)
        {
            if (n.IsPassible())
            {
                float dist = ((Vector2)n.gridPos - point).sqrMagnitude;
                if (dist < sqrDist)
                {
                    dest = n;
                    sqrDist = dist;
                }
            }
        }
        return dest;
    }
    /*public DisplayItemTile GetClosestToPoint(EntityBase e, Vector2Int point)
    {
        return GetClosestToPoint(e, new HexCoords(point));
    }
    public DisplayItemTile GetClosestToPoint(EntityBase e, HexCoords point)
    {
        float sqrDist = Mathf.Infinity;
        DisplayItemTile dest = null;
        foreach (DisplayItemTile n in Tiles)
        {
            if (n.CanFitEntity(e))
            {
                float dist = n.gridPos.DistanceFrom(point);
                if (dist < sqrDist)
                {
                    dest = n;
                    sqrDist = dist;
                }
            }
        }
        return dest;
    }*/
    #endregion
    #region Find Tiles
    public DisplayItemTile GetTile(int x, int y, bool real)
    {
        return GetTile(new Vector2Int(x,y), real);
    }
        
        public DisplayItemTile GetTile(Vector2Int pos, bool real)
        {
            if (!real)
        {
            int w = Tiles.GetLength(0);
            pos.x = pos.x % w;
            if (pos.x < 0) pos.x += w;

            int h = Tiles.GetLength(1);
            pos.y = pos.y % h;
            if (pos.y < 0) pos.y += h;
        }
        else if (pos.x < 0 || pos.y < 0 || pos.x >= Tiles.GetLength(0) || pos.y >= Tiles.GetLength(1))
            return null;
        return Tiles[pos.x, pos.y];
    }
    public DisplayItemTile[] GetTilesInRect(Vector2Int mins, Vector2Int Maxs, bool real)
    {
        List<DisplayItemTile> foundTiles = new List<DisplayItemTile>();

        for (int iX = mins.x; iX <= Maxs.x; iX++)
            for (int iY = mins.y; iY <= Maxs.y; iY++)
            {
                DisplayItemTile foundTile = GetTile(iX, iY, real);
                if (foundTile != null)
                {
                    foundTiles.Add(foundTile);
                }
            }

        return foundTiles.ToArray();
    }
    public DisplayItemTile[] GetTilesInCirc(Vector2Int center, int radius, bool real)
    {
        List<DisplayItemTile> foundTiles = new List<DisplayItemTile>();
        for (int iX = center.x - radius; iX <= center.x + radius; iX++)
            for (int iY = center.y - radius; iY <= center.y + radius; iY++)
            {
                DisplayItemTile foundTile = GetTile(iX, iY, real);
                if (foundTile != null && (center - new Vector2(iX, iY)).sqrMagnitude <= radius * radius)
                {
                    foundTiles.Add(foundTile);
                }
            }

        return foundTiles.ToArray();
    }
    public DisplayItemTile[] GetTilesInLine(Vector2Int center, Vector2Int direction, int radius, bool real)
    {
        List<DisplayItemTile> foundTiles = new List<DisplayItemTile>();
        for (int step = 0; step <= radius; step++)
        {
            DisplayItemTile selTile = GetTile(center + step * direction, real);
            foundTiles.Add(selTile);
        }

        return foundTiles.ToArray();
    }
    #endregion

}
