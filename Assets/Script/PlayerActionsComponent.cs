using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionsComponent : MonoBehaviour
{
    public AbilityButtonComponent[] abilityButtons;
    DamageableComponent player;
    private void Awake()
    {
        abilityButtons = GetComponentsInChildren<AbilityButtonComponent>();
    }
    public void AssignPlayer(DamageableComponent player)
    {
        this.player = player;
        for (int i = 0; i<abilityButtons.Length; i++)
        {
            if (i< player.Character.Attacks.Length)
            {
                abilityButtons[i].AssignAbility(i,player.Character.Attacks[i]);
            }
            else
            {
                abilityButtons[i].ClearAbility();
            }
        }
    }
}
