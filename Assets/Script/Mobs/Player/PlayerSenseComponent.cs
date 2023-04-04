using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSenseComponent : BaseComponent
{
    public AudioClip emptyTileSound;
    public AudioClip solidTileSound;
    private void Start()
    {
        HandlePlayerSight();
    }
    public void HandlePlayerTouch()
    {
        DisplayItemTile forwardTile = parent.movement.GetForwardTile();
        if (forwardTile != null)
        {
            if (!forwardTile.IsPassible())
            {
                if (FrontWallParticle != null)
                    FrontWallParticle.Play();
                if (solidTileSound != null)
                parent.audio.PlayOneShot(solidTileSound);
            }
            else if (forwardTile.LocatedEntity != null)
                forwardTile.LocatedEntity.OnPlayerTouch();
            else if (emptyTileSound != null)
                parent.audio.PlayOneShot(emptyTileSound);
            GameManager.main.ForwardTurn(PlayerMob.main.ActionAP);
        }
    }
    public GameObject SoundParticle;
    public GameObject CuriosityParticle;
    public float ParticleInterval = .5f;
    public int EchoRange = 10;
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
        bool intrerupted = false;
        for (int i = 1; i < EchoRange && !intrerupted; i++)
        {
            DisplayItemTile dit = DataItemWorld.main.GetTile(cTile,true);
            if (dit != null)
            {
                GameObject Effect = null;
                if (dit.LocatedEntity != null && CuriosityParticle != null)
                {
                    Effect = GameManager.main.effectPool.PoolItem(CuriosityParticle);
                    dit.LocatedEntity.OnPlayerEcho();
                    intrerupted = true;
                }
                else if (dit.IsPassible() && SoundParticle != null)
                {
                    Effect = GameManager.main.effectPool.PoolItem(SoundParticle);
                }
                else
                {
                    intrerupted = true;
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
        GameManager.main.ForwardTurn(PlayerMob.main.ActionAP);
        EchoCoroutine = null;
    }
    public bool IsIdle()
    {
        return EchoCoroutine == null;
    }
    public MeshRenderer FrontWall;
    public ParticleSystem FrontWallParticle;
    public GameObject FrontHollow;
    public ParticleSystem FrontHollowParticle;
    public void HandlePlayerSight()
    {
        DisplayItemTile forwardTile = parent.movement.GetForwardTile();
        bool facesWall = (forwardTile != null && !forwardTile.IsPassible());

        if (FrontWall != null)
            FrontWall.enabled = facesWall;
            if (FrontWallParticle != null && facesWall)
                FrontWallParticle.Play();
            if (FrontHollowParticle != null)
        {
            if (facesWall)
                FrontHollowParticle.Stop();
            else
                FrontHollowParticle.Play();
        }
        
    }
}
