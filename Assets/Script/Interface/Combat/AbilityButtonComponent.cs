using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AbilityButtonComponent : EventTrigger
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
    public string SelectTargetMessage = "Select a target!";
    public void PlayerSelectAbility()
    {
        if (attack!=null)
        {
            if (PlayerMob.main.damageable.CanChangeAttack())
            {
                PlayerMob.main.damageable.PickAttack(attackID);
                GameInterface.main.ChangeCursor(GameInterface.CursorMode.attack);
                MessageManager.ShowMessage(SelectTargetMessage);
                GameInterface.main.CombatInterface.monsterTargetingComponent.SetTargetText(true);
            }
        }
    }
    public void ClearAbility()
    {
        gameObject.SetActive(false);
    }
    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (PlayerMob.main.damageable.CanChangeAttack() && attack != null && !string.IsNullOrEmpty( attack.TooltipDescription))
            MessageManager.ShowMessage(attack.TooltipDescription);
    }

}
