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
    public GameObject SoundParticle;
    public GameObject CuriosityParticle;
    public float ParticleInterval = .5f;
    public void HandlePlayerEcho()
    {
        Vector2Int startPos = parent.movement.myTile.gridPos;
        Vector2Int dir = parent.movement.GetForward();

        if (EchoCoroutine == null)
            StartCoroutine(EchoEffect(startPos,dir));
    }
    Coroutine EchoCoroutine;
    IEnumerator EchoEffect(Vector2Int startPos, Vector2Int dir)
    {
        Vector2Int cTile = startPos + dir;

        for (int i = 1; i < 100; i++)
        {
            DisplayItemTile dit = DataItemWorld.main.GetTile(cTile,true);
            if (dit != null)
            {
                GameObject Effect = null;
                if (dit.LocatedEntity != null && CuriosityParticle != null)
                {
                    Effect = GameManager.main.effectPool.PoolItem(CuriosityParticle);
                }
                else if (dit.IsPassible() && SoundParticle != null)
                {
                    Effect = GameManager.main.effectPool.PoolItem(SoundParticle);
                }
                if (Effect != null)
                {
                    Effect.transform.position = dit.transform.position;
                    Effect.SetActive(true);
                }
                cTile += dir;
                yield return new WaitForSeconds(ParticleInterval);
            }
            else
            {
                break;
            }
        }
    }
}
