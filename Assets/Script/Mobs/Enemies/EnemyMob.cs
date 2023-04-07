using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMob : Mob
{
    public AiComponent ai;
    protected override void Awake()
    {
        base.Awake();
        if (ai == null)
            ai = GetComponent<AiComponent>(); 
    }
    public override void OnPlayerEcho(Mob other)
    {
        base.OnPlayerEcho(other);
        ai.SetAggro(true);
    }
    public override void OnPlayerTouch(Mob other)
    {
        base.OnPlayerTouch(other);
        if (CombatController.main!=null)
        {
            CombatController.main.InitialzieCombat(other.damageable, damageable);
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
