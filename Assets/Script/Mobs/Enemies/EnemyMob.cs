using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMob : Mob
{
    public AiComponent ai;
    public string HearMessage = "The {thing} hears you...";
    protected override void Awake()
    {
        base.Awake();
        if (ai == null)
            ai = GetComponent<AiComponent>(); 
    }
    public override void OnPlayerEcho()
    {
        base.OnPlayerEcho();
        ai.SetAggro(true);
        if (damageable != null && damageable.Character != null)
        {
            string hearMes = HearMessage;
            hearMes = hearMes.Replace("{thing}", damageable.Character.name);
            MessageManager.ShowMessage(hearMes);
        }
    }
    public override void OnPlayerTouch()
    {
        base.OnPlayerTouch();
        if (CombatController.main!=null)
        {
            CombatController.main.InitialzieCombat(PlayerMob.main.damageable, damageable);
        }
    }
    protected override void Start()
    {
        base.Start();
        GameManager.main.RegisterEnemy(this);
    }
    private void OnDisable()
    {
        GameManager.main.UnregisterEnemy(this);
    }
}
