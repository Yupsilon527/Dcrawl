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
       // ReviseEntity();
    }
    /*void ReviseEntity()
    {
        if (data.LocatedEntity != null)
            data.LocatedEntity.movement.MoveToPosition(worldPos, 0, false);
    }*/
    #region Visibility

    /*public bool IsVisible()
    {
        return worldPos.DistanceFrom(EntityPlayer.main.movement.gridPos, DataItemMap.main.GetFullSize()) <= WorldDefines.PlayerLos;

    }*/
    public void ChangeVisibility(bool value)
    {
        mesh.SetActive(value);
    }

    #endregion

    public DisplayItemTile[] neighbors;
    public void InitNeighbors(bool real)
    {
        Vector2Int axial = gridPos;
        neighbors = new DisplayItemTile[]
        {
            DataItemWorld.main.GetTile(axial.x + 1,axial.y,real),
        DataItemWorld.main.GetTile(axial.x - 1, axial.y ,real),
        DataItemWorld.main.GetTile(axial.x, axial.y + 1,real),
            DataItemWorld.main.GetTile(axial.x ,axial.y - 1,real),
        };
    }


    #region entity
    /*public EntityBase LocatedEntity;
    public bool CanFitEntity(EntityBase e)
    {
        if (!IsPassible())
            return false;
        if (e != null)
            switch (e.GetEntitySize())
            {
                case EntityBase.EntitySize.small:
                    return EntityCheck(e);
                case EntityBase.EntitySize.medium:
                    bool passible = EntityCheck(e);
                    if (passible && (!neighbors[0].IsPassible() || !neighbors[0].EntityCheck(e)))
                    {
                        passible = false;
                    }
                    if (passible && (!neighbors[1].IsPassible() || !neighbors[1].EntityCheck(e)))
                    {
                        passible = false;
                    }
                    return passible;
                case EntityBase.EntitySize.large:
                    passible = EntityCheck(e);
                    if (passible)
                    {
                        foreach (var n in neighbors)
                        {
                            if (!n.IsPassible() || !n.EntityCheck(e))
                            {
                                passible = false;
                                break;
                            }
                        }
                    }
                    return passible;
            }
        return IsPassible() && EntityCheck(e);
    }
    public bool EntityCheck(EntityBase e)
    {
        return LocatedEntity == null || LocatedEntity == e;
    }*/
    public bool IsPassible()
    {
        return !data.Solid;
    }
    #endregion
}
