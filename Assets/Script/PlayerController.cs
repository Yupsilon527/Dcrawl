using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public void MovePlayerForward()
    {
        if (PlayerMob.main != null && PlayerMob.main.movement != null)
        PlayerMob.main.movement.MoveForward();
    }
    public void TurnaPlayerRight()
    {
        if (PlayerMob.main != null && PlayerMob.main.movement != null)
            PlayerMob.main.movement.Turn(true);
    }
    public void TurnaPlayerLeft()
    {
        if (PlayerMob.main != null && PlayerMob.main.movement != null)
            PlayerMob.main.movement.Turn(false);
    }
    public void HandleWait()
    {
        //TODO
    }
    public void HandleTouch()
    {
        if (PlayerMob.main != null && PlayerMob.main.sense != null)
            PlayerMob.main.sense.HandlePlayerTouch();
    }
    public void HandleEcho()
    {
        if (PlayerMob.main != null && PlayerMob.main.sense != null)
            PlayerMob.main.sense.HandlePlayerEcho();
    }
}
