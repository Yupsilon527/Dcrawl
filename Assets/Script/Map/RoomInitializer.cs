using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomInitializer : MonoBehaviour
{
    private void Start()
    {
        ScanTiles();
        DataItemWorld.main.FinalizeWorld();
    }

    Vector2Int mins = Vector2Int.zero;
    Vector2Int maxs = Vector2Int.zero;
    void ScanTiles()
    {
        var tiles = GetComponentsInChildren<DisplayItemTile>();
        foreach (DisplayItemTile dit in tiles)
        {
            dit.room = DataItemWorld.main;

            dit.gridPos = new Vector2Int(Mathf.FloorToInt(dit.transform.position.x), Mathf.FloorToInt(dit.transform.position.z));

            mins.x = Mathf.FloorToInt(Mathf.Min(dit.gridPos.x, mins.x));
            mins.y = Mathf.FloorToInt(Mathf.Min(dit.gridPos.y, mins.y));

            maxs.x = Mathf.FloorToInt(Mathf.Max(dit.gridPos.x, maxs.x));
            maxs.y = Mathf.FloorToInt(Mathf.Max(dit.gridPos.y, maxs.y));
        }

        DataItemWorld.main.InitializeWorld(Mathf.FloorToInt(maxs.x- mins.x + 1),Mathf.FloorToInt(maxs.y-mins.y + 1));

        foreach (DisplayItemTile dit in tiles)
        {
            if (dit!=null)
                DataItemWorld.main.PushTile( dit, dit.gridPos - mins);
        }
    }
}
