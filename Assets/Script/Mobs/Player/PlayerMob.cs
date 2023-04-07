using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMob : Mob
{
 /*   public int MoveAP = 10;
    public int WaitAP = 10;
    public int TurnAP = 0;
    public int ActionAP = 5;*/
    public static PlayerMob main;
    public PlayerSenseComponent sense;

    public string introMessage = "You wake up in a damp cavern...";
    protected override void Awake()
    {
        main = this;
        base.Awake();
        if (sense == null)
            sense = GetComponent<PlayerSenseComponent>();
    }
    protected override void Start()
    {
        base.Start();
        if (damageable!=null && GameInterface.main!=null && GameInterface.main.playerHealth!=null)
        {
            damageable.Life.TieToHealthBar(GameInterface.main.playerHealth);
        }
        MessageManager.ShowMessage(introMessage);
    }
    public override bool CanAct()
    {
        return base.CanAct() && sense.IsIdle();
    }
    public override void PostMove()
    {
        base.PostMove();
        if (sense!=null)
            sense.HandlePlayerSight();
        if (movement != null)
            movement.myTile.Revealed = true;
        if (MinimapManager.main != null)
            MinimapManager.main.UpdateMinimapAroundPlayer();
    }
    public string deathScene = "You wake up in a damp cavern...";
    public override void Die()
    {
        base.Die();
        if (this == main)
        {
            SceneManager.LoadScene(deathScene, LoadSceneMode.Single);
        }
    }
}
