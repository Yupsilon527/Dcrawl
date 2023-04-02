using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMob : Mob
{
    public static PlayerMob main;
    public PlayerSenseComponent sense;
    protected override void Awake()
    {
        main = this;
        base.Awake();
        if (sense == null)
            sense = GetComponent<PlayerSenseComponent>();
    }
}
