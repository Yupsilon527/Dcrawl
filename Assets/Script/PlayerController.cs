using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
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
            PlayerMob.main.movement.Turn(true);
            GameManager.main.ForwardTurn(PlayerMob.main.TurnAP);
        }
    }
    public void TurnaPlayerLeft()
    {
        if (PlayerMob.main != null && PlayerMob.main.movement != null)
        {
            PlayerMob.main.movement.Turn(false);
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
