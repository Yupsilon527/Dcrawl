using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private void Update()
    {
        ReadKeyboard();
    }
    void ReadKeyboard()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            MovePlayerForward();
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            TurnaPlayerLeft();
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            TurnaPlayerRight();
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            TurnaPlayerRight();
        }
        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.LeftControl))
        {
            HandleEcho();
        }
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space))
        {
            HandleTouch();
        }
    }
    public void MovePlayerForward()
    {
        if (PlayerMob.main != null && PlayerMob.main.movement != null)
        {
              if (PlayerMob.main.movement.MoveForward())
            {
                GameManager.main.ForwardTurn(PlayerMob.main.MoveAP);
            }
        }
    }
    public void TurnaPlayerRight()
    {
        if (PlayerMob.main != null && PlayerMob.main.movement != null)
        {
            PlayerMob.main.movement.Turn(true,.5f);
            GameManager.main.ForwardTurn(PlayerMob.main.TurnAP);
        }
    }
    public void TurnaPlayerLeft()
    {
        if (PlayerMob.main != null && PlayerMob.main.movement != null)
        {
            PlayerMob.main.movement.Turn(false, .5f);
            GameManager.main.ForwardTurn(PlayerMob.main.TurnAP);
        }
    }
    public void HandleWait()
    {
        GameManager.main.ForwardTurn(PlayerMob.main.WaitAP);
    }
    public void HandleTouch()
    {
        if (PlayerMob.main != null && PlayerMob.main.sense != null)
        {
            PlayerMob.main.sense.HandlePlayerTouch();
        }
    }
    public void HandleEcho()
    {
        if (PlayerMob.main != null && PlayerMob.main.sense != null)
        {
            PlayerMob.main.sense.HandlePlayerEcho();
        }
    }
}
