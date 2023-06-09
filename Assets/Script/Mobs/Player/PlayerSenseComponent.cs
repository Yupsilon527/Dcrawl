using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerSenseComponent : BaseComponent
{
    public AudioClip EchoSound;

    public AudioMixerGroup mixerGroup; // assign your AudioMixerGroup in the inspector

    private void Start()
    {
        HandlePlayerSight();
    }
    public string WallMessage = "You feel a wall.";
    public string EmptyMessage = "You feel nothing.";
    public string SomethingMessage = "You touch the ";
    public void HandlePlayerTouch()
    {
        DisplayItemTile forwardTile = parent.movement.GetForwardTile();
        if (forwardTile != null)
        {
            if (!forwardTile.IsPassible())
            {
                MessageManager.ShowMessage(WallMessage);
                DrawTouchEffect();
                    AudioManager.Instance.PlaySfx("Wall sound", 4); //SFX player touches wall

            }
            else if (forwardTile.LocatedEntity != null)
            {
                MessageManager.ShowMessage($"{SomethingMessage}{forwardTile.LocatedEntity.name}");
                forwardTile.LocatedEntity.OnPlayerTouch(parent);
            }
            else 
            {
                MessageManager.ShowMessage(EmptyMessage);
                AudioManager.Instance.PlaySfx("Empty hand swipe", 0);   //SFX player touches empty space
            }
            //GameManager.main.ForwardTurn(PlayerMob.main.ActionAP);
        }
    }
    public void DrawTouchEffect()
    {
        if (FrontWallParticle != null)
            FrontWallParticle.Play();
    }
    public GameObject SoundParticle;
    public GameObject CuriosityParticle;
    public float ActionDuration = .15f;
    public float ParticleInterval = .5f;
    public int EchoRange = 10;
    public void HandlePlayerEcho()
    {
        Vector2Int startPos = parent.movement.myTile.gridPos;
        Vector2Int dir = parent.movement.GetForward();
        //EchoSounds.PlayOneShot(EchoSound);

        if (EchoCoroutine == null)
            StartCoroutine(EchoEffect(startPos,dir));
    }
    Coroutine EchoCoroutine;
    public string EchoMessage = "You call out...";
    IEnumerator EchoEffect(Vector2Int startPos, Vector2Int dir)
    {
        MessageManager.ShowMessage(EchoMessage);
        yield return new WaitForSeconds(ActionDuration);
        AudioManager.Instance.PlaySfx("Player Call", 2);    //SFX player shout
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
                    dit.LocatedEntity.OnPlayerEcho(parent);
                    intrerupted = true;
                }
                else if (dit.IsPassible() && SoundParticle != null)
                {
                    Effect = GameManager.main.effectPool.PoolItem(SoundParticle);
                    dit.Revealed = true;

                    if (MinimapManager.main != null)
                        MinimapManager.main.UpdateTile(dit);
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
        //GameManager.main.ForwardTurn(PlayerMob.main.ActionAP);
        if (MinimapManager.main != null)
            MinimapManager.main.UpdateMinimapAroundPlayer();
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
