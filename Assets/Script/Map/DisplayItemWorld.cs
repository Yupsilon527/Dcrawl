using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayItemWorld : MonoBehaviour
{

    public static DisplayItemWorld main;
    private void Awake()
    {
        main = this;
        if (tilepool == null)
            tilepool = GetComponent<ObjectPool>();
    }
    #region Room Display
    Vector2Int worldCenter = Vector2Int.zero;
    public DisplayItemTile[,] TilesetData;
    
    #endregion
    #region Tile Pools
    public ObjectPool tilepool;
    public void DrawTile(int iX, int iY, DataItemTile tData)
    {
        DrawTile(new Vector2Int(iX, iY), tData);
    }
    public void DrawTile(Vector2Int worldPos, DataItemTile tData)
    {
        if (tData != null)
        {
            GameObject tile = tilepool.PoolItem(tData.tilePrefab);
            if (tile.TryGetComponent(out DisplayItemTile wtile))
            {
                wtile.InitData(tData, worldPos);
                RegisterTile(wtile, worldPos);
            }
        }
    }

    #endregion
    #region Clear
    public void ClearWorld()
    {
        if (TilesetData != null)
            foreach (DisplayItemTile tile in TilesetData)
            {
                ClearTile(tile);
            }
    }
    public void ClearTiles(DisplayItemTile[] tiles)
    {
        foreach (DisplayItemTile tile in tiles)
        {
            ClearTile(tile);
        }
    }
    void ClearTile(DisplayItemTile tile)
    {
        if (tile.data != null)
        {
            name = tile.data.tilePrefab.name;
        }
        tilepool.DeactivateObject(tile.gameObject);
    }
    #endregion
    
    #region Move And Register Tiles
    
    public void RegisterTile(DisplayItemTile tile, Vector2Int worldPos)
    {
        Vector2Int realPos = new Vector2Int(worldPos.x - worldCenter.x, worldPos.y - worldCenter.y);
        print("Register Tile " + realPos);
        TilesetData[realPos.x, realPos.y] = tile;

        tile.name = tile.data.tilePrefab.name + " " + tile.gridPos + "~" + worldPos;

        Vector2 bidipos = worldPos;

        tile.transform.position = new Vector3(bidipos.x, bidipos.y, 0);
    }
    #endregion

}
