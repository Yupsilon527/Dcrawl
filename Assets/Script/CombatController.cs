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
    public DamageableComponent player;
    public DamageableComponent monster;
    public void InitialzieCombat(DamageableComponent player, DamageableComponent monster)
    {
        enabled = true;
        this.player = player;
        player.ClearAttack();
        this.monster = monster;
        monster.ClearAttack();
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
    public void MonsterAttack()
    {
        ScriptableAttack monsterattack = monster.GetNextAttack();
        if (monsterattack != null )
        {
            player.TakeDamage(monsterattack.AttackValue * monsterattack.AttackCount); 
        }
    }
    float lastUpdateTime = 0;
    public float CombatUpdateTime = .2f;
    private void Update()
    {
        FrameUpdate();
    }
    void FrameUpdate()

    {

        if (lastUpdateTime < Time.time)
        {
            lastUpdateTime = Time.time + CombatUpdateTime;
            PlayerTurn();
            MonsterTurn();
        }
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
                        player.ClearAttack() ;
                        cState = GameState.waitforplayer;
                    }
                    
                }
            }
            else
            {
                cState = GameState.waitforplayer;
            }
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
                        MonsterAttack();
                        monster.PickAiAttack();
                    }
                }
            }
            else
            {
                monster.PickAiAttack();
            }
        }
        else
        {
            HandleVictory();
        }
    }
    void HandleVictory()
    {
        MessageManager.ShowMessage($"{monster.Character.name} was defeated!");
        GameInterface.main.ChangeState(GameInterface.State.hubworld);
        enabled = false;
    }
}
