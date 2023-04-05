using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityButtonComponent : MonoBehaviour
{
    public TMPro.TextMeshProUGUI text;
    ScriptableAttack attack;
    int attackID = 0;
    public void AssignAbility(int attackID, ScriptableAttack attack)
    {
        this.attack = attack;
        gameObject.SetActive(true);
        this.attackID = attackID;

        text.text = attack.name;
        
    }
    public void PlayerSelectAbility()
    {
        if (attack!=null)
        {
            if (PlayerMob.main.damageable.ActionPoints == 0 && PlayerMob.main.damageable.NumAttacks == 0)
            {
                PlayerMob.main.damageable.PickAttack(attackID);
            }
        }
    }
    public void ClearAbility()
    {
        gameObject.SetActive(false);
    }

}
