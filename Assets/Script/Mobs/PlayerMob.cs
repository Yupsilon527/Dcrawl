using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMob : Mob
{
    public static PlayerMob main;
    protected override void Awake()
    {
        base.Awake();
        main = this;
    }
}
