using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatantStatsComponent : MonoBehaviour
{
    public TMPro.TextMeshProUGUI statsText;
    DamageableComponent fighter;
    public void AssignFighter(DamageableComponent combatant)
    {
        fighter = combatant;
        UpdateText();
    }
    public void UpdateText()
    {
        if (fighter == null) return;
        statsText.text = fighter.Character.name;
        if (fighter.GetNextAttack() == null)
        {
            statsText.text += "\nThinking...";
        }
        else
        {
            ScriptableAttack nAttack = fighter.GetNextAttack();
            statsText.text += "\n" + nAttack.name + "\nAttack: " + nAttack.AttackValue + "\nDefense: " + nAttack.DefenseValue + "\nTime: " + (nAttack.ActionPoints - fighter.ActionPoints) ;
        }
    }
    private void Update()
    {
        UpdateText();
    }
}
