using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatInterface : MonoBehaviour
{
    public CombatantStatsComponent playerInfo;
    public CombatantStatsComponent enemyInfo;
    public MonsterTargetingComponent monsterTargetingComponent;
    public void AssignFighterSide(FighterComponent fighter, bool player)
    {
        if (player)
        { playerInfo.AssignFighter(fighter); }
        else
        { enemyInfo.AssignFighter(fighter);
            monsterTargetingComponent.Randomize();
        }
    }

    public void RegisterPlayerHit()
    {
        CombatController.main.PlayerAttack(false);
    }
    public void RegisterPlayerMiss()
    {
        CombatController.main.PlayerAttack(true);
    }
}
