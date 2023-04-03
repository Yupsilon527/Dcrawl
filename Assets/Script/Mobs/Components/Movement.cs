using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : BaseComponent
{
    public AudioClip footstepSound;
    public AudioClip wallbumpSound;
    #region Facing
    public enum Direction
    {
        North,
        South,
        East,
        West,
    }
    Direction facing = Direction.North;
    public static Vector2Int GetForward(Direction dir)
    {
        switch (dir)
        {
            default:
                return Vector2Int.up;

            case Direction.South:
                return Vector2Int.down;

            case Direction.East:
                return Vector2Int.right;

            case Direction.West:
                return Vector2Int.left;

        }
    }
    public Vector2Int GetForward()
    {
        return GetForward(facing);
    }
    public bool MoveDirection(Direction dir)
    {
        Vector2Int pos = myTile.gridPos + GetForward(dir);
        return  Move (DataItemWorld.main.GetTile(pos.x, pos.y, true));
        
    }
    public bool Move(DisplayItemTile nTile) {
        if (nTile != null)
        {

            if (!nTile.IsPassible())
            {
                if (parent.audio != null && wallbumpSound != null)
                    parent.audio.PlayOneShot(wallbumpSound);
            }
            else if (nTile.LocatedEntity != null)
            {
                if (parent == PlayerMob.main)
                {
                    nTile.LocatedEntity.OnPlayerTouch();
                }
                if (nTile.LocatedEntity == PlayerMob.main)
                {
                    parent.OnPlayerTouch();
                }
            }
            else
            {
                if (parent.audio != null && footstepSound != null)
                    parent.audio.PlayOneShot(footstepSound);
                ChangeTile(nTile);
                return true;
            }
        }
        return false;
    }
    public DisplayItemTile GetForwardTile()
    {
        Vector2Int pos = myTile.gridPos + GetForward();
        return DataItemWorld.main.GetTile(pos.x, pos.y, true);
    }
    public bool MoveForward()
    {
        return MoveDirection(facing);
    }
    public bool Strafe(bool Right)
    {
        switch (facing)
        {
            case Direction.North:
                return MoveDirection(Right ? Direction.East : Direction.West);
            case Direction.South:
                return MoveDirection(Right ? Direction.West : Direction.East);
            case Direction.East:
                return MoveDirection(Right ? Direction.South : Direction.North);
            case Direction.West:
                return MoveDirection(Right ? Direction.North : Direction.South);
        }
        return false;
    }
    public void ChangeDirection(Direction ndir)
    {
        facing = ndir;
        transform.forward = new Vector3(GetForward().x,0, GetForward().y);
    }
    public void Turn(bool Right)
    {
        switch (facing)
        {
            case Direction.North:
                ChangeDirection(Right ? Direction.East : Direction.West);
                break;
            case Direction.South:
                ChangeDirection(Right ? Direction.West : Direction.East);
                break;
            case Direction.East:
                ChangeDirection(Right ? Direction.South : Direction.North);
                break;
            case Direction.West:
                ChangeDirection(Right ? Direction.North : Direction.South);
                break;
        }
    }

    #endregion

    public DisplayItemTile myTile;
    void ChangeTile(DisplayItemTile nTile)
    {
        if (myTile != null)
            myTile.LocatedEntity = null;
        myTile = nTile;
        SnapToTile(nTile);
        nTile.LocatedEntity = parent;
    }
    public override void OnSpawn()
    {
        SnapToStartingTile();
    }
    private void OnValidate()
    {
        SnapToStartingTile();
    }
    void SnapToStartingTile()
    {
        if (myTile != null)
        {
            ChangeTile(myTile);
        }
    }
    public void SnapToTile(DisplayItemTile nTile)
    {
        SnapToPosition(nTile.transform.position);
    }
    public void MoveToTile(DisplayItemTile nTile, float dur)
    {
        MoveToPosition(nTile.transform.position, dur);
    }
    public void MoveToTileLinear(DisplayItemTile nTile, float speed)
    {
        MoveToPositionLinear(nTile.transform.position, speed);
    }
    public void SnapToPosition(Vector3 nPos)
    {
        transform.position = nPos;
    }
    public void MoveToPosition(Vector3 nPos, float dur)
    {
        if (MoveCoroutine != null)
            StopCoroutine(MoveCoroutine);
        MoveCoroutine = StartCoroutine(SlideCoroutine(nPos, dur));
    }
    public void MoveToPositionLinear(Vector3 nPos, float speed)
    {
        if (MoveCoroutine != null)
            StopCoroutine(MoveCoroutine);
        MoveCoroutine = StartCoroutine(SlideCoroutine(nPos, (nPos - transform.position).magnitude / speed));
    }
    Coroutine MoveCoroutine;
    IEnumerator SlideCoroutine(Vector3 nPos, float dur)
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = nPos ;

        float startTime = Time.time;
        for (float t = startTime; t < startTime + dur; t += Time.deltaTime )
        {
            transform.position = Vector3.Lerp(startPos, endPos, (Time.time - startTime) / dur);
            yield return new WaitForEndOfFrame();
        }
        transform.position = endPos;
        MoveCoroutine = null;
    }
}
