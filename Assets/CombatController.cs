using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    public static CombatController main;
    private void Awake()
    {
        main = this;
        enabled = false;
    }
    public enum GameState
    {
        waitforplayer,
        gameprogress,
        monsterattack
    }
    GameState cState;
    public bool IsInCombat()
    {
        return enabled;
    }
    public FighterComponent player;
    public FighterComponent monster;
    public void InitialzieCombat(FighterComponent player, FighterComponent monster)
    {
        enabled = true;
        this.player = player;
        this.monster = monster;
        InitInterface();
    }
    void InitInterface()
    {
        if (GameInterface.main!=null)

        {
            GameInterface.main.ChangeState(GameInterface.State.combat);

            GameInterface.main.CombatInterface.AssignFighterSide(player, true);
            GameInterface.main.CombatInterface.AssignFighterSide(monster, false);
        }
    }
    public void PlayerAttack(bool miss)
    {
        ScriptableAttack playerattack = player.GetNextAttack();
        if (playerattack != null && player.ActionPoints == 0 && player.NumAttacks < playerattack.AttackCount)
        {
            if (!miss)
            {
                monster.TakeDamage(playerattack.AttackValue);
            }
            player.NumAttacks++;
        }
    }
    private void Update()
    {
        PlayerTurn();
        MonsterTurn();
    }
    void PlayerTurn()
    {
        if (player.IsAlive())
        {
            ScriptableAttack playerattack = player.GetNextAttack();

            if (playerattack != null)
            {
                if (player.NumAttacks == playerattack.AttackCount)
                {
                    if (player.ActionPoints < playerattack.ActionPoints)
                    {
                        player.ActionPoints++;
                        cState = GameState.gameprogress;
                    }
                    else
                    {
                        player.Attack = -1;
                        cState = GameState.waitforplayer;
                    }
                    
                }
            }
            else
            {
                cState = GameState.waitforplayer;
            }
        }
        else
        {
            //Handle Defeat
        }
    }
    void MonsterTurn()
    {
        if (monster.IsAlive())
        {
            ScriptableAttack monsterattack = monster.GetNextAttack();

            if (monsterattack != null)
            {
                if (cState == GameState.gameprogress)
                {
                    if (monster.ActionPoints < monsterattack.ActionPoints)
                    {
                        monster.ActionPoints++;
                    }
                    else
                    {
                        //Handle monster attack
                    }
                }
            }
            else
            {
                monster.HandleAi();
            }
        }
        else
        {
            //Handle Victory
        }
    }
}
