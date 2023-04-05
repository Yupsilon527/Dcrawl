using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMob : Mob
{
    public int MoveAP = 10;
    public int WaitAP = 10;
    public int TurnAP = 0;
    public int ActionAP = 5;
    public static PlayerMob main;
    public PlayerSenseComponent sense;
    protected override void Awake()
    {
        main = this;
        base.Awake();
        if (sense == null)
            sense = GetComponent<PlayerSenseComponent>();
    }
    protected override void Start()
    {
        base.Start();
        if (damageable!=null && GameInterface.main!=null && GameInterface.main.playerHealth!=null)
        {
            damageable.Life.TieToHealthBar(GameInterface.main.playerHealth);
        }
    }
    public bool CanAct()
    {
        return sense.IsIdle();
    }
    public override void PostMove()
    {
        base.PostMove();
        if (sense!=null)
            sense.HandlePlayerSight();
    }
}
