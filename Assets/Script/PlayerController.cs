using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public void MovePlayerForward()
    {
        PlayerMob.main.movement.MoveForward();
    }
    public void TurnaPlayerRight()
    {
        PlayerMob.main.movement.Turn(true);
    }
    public void TurnaPlayerLeft()
    {
        PlayerMob.main.movement.Turn(false);
    }
}
