using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionMob : Mob
{
    public float HealthGiven = 10;

    public string EchoMessage = "There's something there...";
    public string PickupMessage = "You drink the potion of healing and recover {healing} life!";
    public override void OnPlayerEcho(Mob other)
    {
        base.OnPlayerEcho(other);

        MessageManager.ShowMessage(EchoMessage);
    }
    public override void OnPlayerTouch(Mob other)
    {
        if (other == PlayerMob.main)
        {
            base.OnPlayerTouch(other);
            string healMsg = PickupMessage;
            healMsg = healMsg.Replace("{healing}", HealthGiven + "");
            MessageManager.ShowMessage(healMsg);

            if (other.audio != null && monsterCall != null)
                other.audio.PlayOneShot(monsterCall);

            other.damageable.HealRawDamage(HealthGiven);
            Die();
        }
    }
}
