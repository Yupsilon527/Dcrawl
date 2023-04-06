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
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            MovePlayerForward();
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            TurnaPlayerLeft();
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            TurnaPlayerRight();
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            HandleWait();
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
        if (PlayerMob.main != null && PlayerMob.main.CanAct() && PlayerMob.main.movement != null)
        {
              if (PlayerMob.main.movement.MoveForward())
            {
                //GameManager.main.ForwardTurn(PlayerMob.main.MoveAP);
            }
            else
            {
                PlayerMob.main.sense.DrawTouchEffect();
            }
        }
    }
    public void TurnaPlayerRight()
    {
        if (PlayerMob.main != null && PlayerMob.main.CanAct() && PlayerMob.main.movement != null)
        {
            PlayerMob.main.movement.Turn(true, PlayerMob.main.movement.DefaultTurnSpeed);
          //  GameManager.main.ForwardTurn(PlayerMob.main.TurnAP);
        }
    }
    public void TurnaPlayerLeft()
    {
        if (PlayerMob.main != null && PlayerMob.main.CanAct() && PlayerMob.main.movement != null)
        {
            PlayerMob.main.movement.Turn(false, PlayerMob.main.movement.DefaultTurnSpeed);
            //GameManager.main.ForwardTurn(PlayerMob.main.TurnAP);
        }
    }
    public void HandleWait()
    {
        //GameManager.main.ForwardTurn(PlayerMob.main.WaitAP);
    }
    public void HandleTouch()
    {
        if (PlayerMob.main != null && PlayerMob.main .CanAct() && PlayerMob.main.sense != null)
        {
            PlayerMob.main.sense.HandlePlayerTouch();
        }
    }
    public void HandleEcho()
    {
        if (PlayerMob.main != null && PlayerMob.main.CanAct() && PlayerMob.main.sense != null)
        {
            PlayerMob.main.sense.HandlePlayerEcho();
        }
    }
}
