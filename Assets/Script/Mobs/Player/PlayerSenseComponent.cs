using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSenseComponent : BaseComponent
{
    public AudioClip emptyTileSound;
    public AudioClip solidTileSound;
    public void HandlePlayerTouch()
    {
        DisplayItemTile forwardTile = parent.movement.GetForwardTile();
        if (forwardTile != null)
        {
            if (forwardTile.LocatedEntity!=null && forwardTile.LocatedEntity.monsterCall!=null)
                parent.audio.PlayOneShot(forwardTile.LocatedEntity.monsterCall);
            if (forwardTile.IsPassible() && emptyTileSound !=null)
                parent.audio.PlayOneShot(emptyTileSound);
            else if (solidTileSound != null)
                parent.audio.PlayOneShot(solidTileSound);
        }
    }
}
