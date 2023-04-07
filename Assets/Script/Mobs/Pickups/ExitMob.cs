using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitMob : Mob
{
    public string exitScene;
    public override void OnPlayerTouch(Mob other)
    {
        if (other == PlayerMob.main)
        {
            SceneManager.LoadScene(exitScene, LoadSceneMode.Single);
        }
    }
}
