using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatInterface : MonoBehaviour
{
    public CombatantStatsComponent playerInfo;
    public CombatantStatsComponent enemyInfo;
    public MonsterTargetingComponent monsterTargetingComponent;
    public PlayerActionsComponent playerActionsComponent;
    private void OnEnable()
    {
        
    }
    public void AssignFighterSide(DamageableComponent fighter, bool player)
    {
        if (player)
        {
            playerInfo.AssignFighter(fighter);
            playerActionsComponent.AssignPlayer(fighter);
        }
        else
        { 
            enemyInfo.AssignFighter(fighter);
            monsterTargetingComponent.MoveEnemyAround();
            monsterTargetingComponent.ShowMonsterAttack();
        }
    }

    public void RegisterPlayerHit()
    {
        CombatController.main.PlayerAttack(false);
        monsterTargetingComponent.ShowMonsterGetHurt();
    }
    public void RegisterPlayerMiss()
    {
        CombatController.main.PlayerAttack(true);
    }

}
