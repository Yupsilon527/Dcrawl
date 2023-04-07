using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiComponent : BaseComponent
{
    public enum Behavior
    {
        standing,
        roaming,
        overwatch
    }
    public Behavior behavior;
    public int behaviorData;

    bool aggroed = false;
    public string AggroMessage = "The {thing} hears you...";
    public void SetAggro(bool value)
    {
        aggroed = value;
        if (parent.damageable != null && parent.damageable.Character != null)
        {
            string hearMes = AggroMessage;
            hearMes = hearMes.Replace("{thing}", parent.damageable.Character.name);
            MessageManager.ShowMessage(hearMes);
        }
    }
    public void ResolveTurn()
    {
        if (!parent.CanAct()) return;
        if (aggroed ){ ChasePlayer();        }
        else if (behavior == Behavior.roaming)
        {
            Roam();
        }
        else if (behavior == Behavior.overwatch )
        {
            Overwatch();
        }
    }
    void ChasePlayer()
    {
        if (PlayerMob.main != null)
        {
            DisplayItemTile playerPos = PlayerMob.main.movement.myTile;

            DisplayItemTile tile = null;
            foreach (DisplayItemTile n in parent.movement.myTile.neighbors)
            {
                if (n.IsPassible() && (tile == null || tile.DistanceSquared(playerPos) > n.DistanceSquared(playerPos)))
                {
                    tile = n;
                }
            }
            if (tile != null)
            {
                parent.movement.Move(tile, parent.movement.DefaultWalkSpeed);
            }
        }
    }
    void Overwatch()
    {
        if (PlayerMob.main != null)
        {

            DisplayItemTile playerPos = PlayerMob.main.movement.myTile;
            if (playerPos.DistanceSquared(parent.movement.myTile) <= behaviorData * behaviorData)
            {
                SetAggro(true);
                ChasePlayer();
            }
        }
        }
    void Roam()
    {
        if (Random.Range(0, 100) <= behaviorData)
        {
            parent.movement.MoveDirection((Movement.Direction)Mathf.FloorToInt(Random.Range(0, 5)), parent.movement.DefaultWalkSpeed);
        }
    }

}
