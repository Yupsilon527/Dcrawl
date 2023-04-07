using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitMob : Mob
{
    public string echoMessage;
    public string exitScene;
    public override void OnPlayerEcho(Mob other)
    {
        base.OnPlayerEcho(other);
        MessageManager.ShowMessage(echoMessage);
    }
    public override void OnPlayerTouch(Mob other)
    {
        if (other == PlayerMob.main)
        {
            SceneManager.LoadScene(exitScene, LoadSceneMode.Single);
        }
    }
}
