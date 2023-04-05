using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInterface : MonoBehaviour
{
    public static GameInterface main;
    public HealthbarController playerHealth;
    private void Awake()
    {
        main = this;
        ChangeState(State.hubworld);
    }
    public enum State
    {
        hubworld,
        combat
    }
    public void ChangeState(State nState)
    {
        PlayerControlInterface.gameObject.SetActive(nState == State.hubworld);
        CombatInterface.gameObject.SetActive(nState == State.combat);
    }
    public PlayerController PlayerControlInterface;
    public CombatInterface CombatInterface;
}
