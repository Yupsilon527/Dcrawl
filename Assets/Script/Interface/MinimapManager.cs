using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinimapManager : MonoBehaviour
{
    public static MinimapManager main;
    private void Awake()
    {
        main = this;
    }

    public RawImage outputImage;

    Vector2Int minimapOffset = new Vector2Int(0,0);
    Texture2D minimapImage;
    public void InitializeMinimap()
    {
        int W = DataItemWorld.main.Tiles.GetLength(0);
        int H = DataItemWorld.main.Tiles.GetLength(1);

        if (W < H)
        {
            int mWH = Mathf.Max(W, H);
            minimapImage = new Texture2D(mWH, mWH);
        }
        else
        {
            minimapImage = new Texture2D(W, H);
        }
        minimapImage.filterMode = FilterMode.Point;
        for (int px = 0; px < minimapImage.width; px++)
            for (int py = 0; py < minimapImage.height; py++)
                minimapImage.SetPixel(px,py,Color.clear );
        minimapImage.Apply();

        DrawMinimap();
    }
    public void UpdateMinimapAroundPlayer()
    {
        DrawPlayerPosition();
        minimapImage.Apply();
    }
    public void UpdateTile(DisplayItemTile tile)
    {

        if (tile != null && tile.Revealed)
        {
            minimapImage.SetPixel(tile.gridPos.x + minimapOffset.x, tile.gridPos.y + minimapOffset.y, Color.gray);
        }
    }
    void UpdateRect(int mX, int mY, int MX, int MY)
    {
        for (int px = mX; px <= MX; px++)
            for (int py = mY; py <= MY; py++)
            {
                UpdateTile(DataItemWorld.main.GetTile(px, py,true));
            }
    }
    void DrawPlayerPosition()
    {
        if (PlayerMob.main!=null)
        {
            var gridPos = PlayerMob.main.movement.myTile.gridPos;
            UpdateRect(gridPos.x - 1, gridPos.y - 1, gridPos.x + 1, gridPos.y + 1);
            minimapImage.SetPixel(gridPos.x + minimapOffset.x, gridPos.y + minimapOffset.y, Color.white);
        }
    }
    public void DrawMinimap()
    {
        if (outputImage!=null)
        {
            outputImage.texture = minimapImage;
        }
    }

}
