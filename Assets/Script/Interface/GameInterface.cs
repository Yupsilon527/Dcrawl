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
        ChangeCursor(CursorMode.normal);
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
    #region Cursor
    public enum CursorMode
    {
        normal,
        attack
    }
    public Sprite defaultCursor;
    public Sprite attackCursor;

    public void ChangeCursor(CursorMode nCursor)
    {
        switch (nCursor)
        {
            case CursorMode.attack:
                if (attackCursor!=null)
                Cursor.SetCursor(attackCursor.texture, attackCursor.pivot, UnityEngine.CursorMode.ForceSoftware);
                break;
            case CursorMode.normal:
                if (defaultCursor != null)
                    Cursor.SetCursor(defaultCursor.texture, defaultCursor.pivot, UnityEngine.CursorMode.ForceSoftware);
                break;
        }
    }
    #endregion
}
