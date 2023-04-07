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
    /// <summary>
    /// test
    /// </summary>
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
    public AudioClip missSound;
    public string PlayerHitMessage = "You hit the {M} for {damage} damage!";
    public string PlayerBlockedMessage = "Your attack was blocked!";
    public string PlayerMissMessage = "You missed!";
    public void PlayerAttack(bool miss)
    {
        ScriptableAttack playerattack = player.GetNextAttack();
        if (playerattack != null && player.ActionPoints == 0 && player.NumAttacks < playerattack.AttackCount)
        {
            if (!miss)
            {
                float resultingDamage = monster.TakeDamage(playerattack.AttackValue);

                if (resultingDamage > 0)
                {
                    string hitMsg = PlayerHitMessage;
                    hitMsg= hitMsg.Replace("{M}", monster.Character.name);
                    hitMsg = hitMsg.Replace("{damage}", resultingDamage+"");
                    MessageManager.ShowMessage(hitMsg);
                }
                else
                    MessageManager.ShowMessage(PlayerBlockedMessage);

                if (playerattack.AbilitySound!=null)
                PlayerMob.main.audio.PlayOneShot(playerattack.AbilitySound);
            }
            else
            {
                if (missSound != null)
                { PlayerMob.main.audio.PlayOneShot(missSound); }
                MessageManager.ShowMessage(PlayerMissMessage);
            }
            player.NumAttacks++;
            if (player.NumAttacks >= playerattack.AttackCount)
            {
                GameInterface.main.CombatInterface.monsterTargetingComponent.SetTargetText(false);
                GameInterface.main.ChangeCursor(GameInterface.CursorMode.normal);
            }
        }
    }

    public string MonsterHitMessage = "The {M} hits you for {damage} damage!";
    public string MonsterBlockedMessage = "The {M}'s attack was blocked!";
    public string MonsterMoveMessage = "The {M} moves around!";
    public void MonsterAttack()
    {
        ScriptableAttack monsterattack = monster.GetNextAttack();
        if (monsterattack != null )
        {
            if (monsterattack.AttackCount > 0)
            {
                float resultingDamage = 0;
                for (int i = 0; i < monsterattack.AttackCount; i++)
                {
                    resultingDamage += player.TakeDamage(monsterattack.AttackValue);
                }
                if (resultingDamage > 0)
                {
                    string hitMsg = MonsterHitMessage;
                    hitMsg = hitMsg.Replace("{M}", monster.Character.name);
                    hitMsg = hitMsg.Replace("{damage}", resultingDamage + "");
                    MessageManager.ShowMessage(hitMsg);
                }
                else
                {
                    string blockMsg = MonsterBlockedMessage;
                    blockMsg = blockMsg.Replace("{M}", monster.Character.name);
                    MessageManager.ShowMessage(blockMsg);
                }
                GameInterface.main.CombatInterface.monsterTargetingComponent.ShowMonsterAttack();
            }
            else
            {
                GameInterface.main.CombatInterface.monsterTargetingComponent.MoveEnemyAround();

                string dodgeMsg = MonsterMoveMessage;
                dodgeMsg = dodgeMsg.Replace("{M}", monster.Character.name);
                MessageManager.ShowMessage(dodgeMsg);
            }
            if (monsterattack.AbilitySound != null)
                PlayerMob.main.audio.PlayOneShot(monsterattack.AbilitySound);
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
    public string DefeatMessage = " was defeated!";
    void HandleVictory()
    {
        MessageManager.ShowMessage($"{monster.Character.name}{DefeatMessage}");
        GameInterface.main.ChangeState(GameInterface.State.hubworld);
        enabled = false;
        GameInterface.main.ChangeCursor(GameInterface.CursorMode.normal);
    }
}
