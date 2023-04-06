using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableComponent : BaseComponent
{
    public ScriptableMonster Character;
    public Resource Life;
    public override void OnSpawn()
    {
        base.OnSpawn();
        Life = new Resource(Character.MaximumLife, name + " life", false, true);
    }
    public int Attack = -1;
    public int ActionPoints = 0;
    public int NumAttacks = 0;
    public ScriptableAttack GetNextAttack()
    {
        if (Attack >= 0 && Attack < Character.Attacks.Length)
        {
            return Character.Attacks[Attack];
        }
        return null;
    }
    public float TakeDamage(float damage)
    {
        float defense = 0;
        if (GetNextAttack() != null)
            defense = GetNextAttack().DefenseValue;

        if (damage - defense > 0)
        {
            TakeRawDamage(damage - defense);
            return damage - defense;
        }
        return 0;
    }
    public bool IsAlive()
    {
        return SanityCheck() && Life.GetValue() > 0;
    }
    public void TakeRawDamage(float healing)
    {
        Life.SubstractValue(healing);
        if (!IsAlive())
            parent.Die();
    }
    public void HealRawDamage(float damage)
    {
        Life.GiveValue(damage);
    }
    public void PickAiAttack()
    {
        PickAttack((Attack+1)% Character.Attacks.Length);
    }
    public void PickAttack(int aID)
    {
        if (aID < 0 || aID >= Character.Attacks.Length) return;
        Attack = aID;
        ActionPoints = 0;
        NumAttacks = 0;
    }
    public void ClearAttack()
    {
        Attack = -1;
        ActionPoints = 0;
        NumAttacks = 0;
    }
    public bool CanChangeAttack()
    {
        return ActionPoints == 0 && NumAttacks == 0;
    }
}
