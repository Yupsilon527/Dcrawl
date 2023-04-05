using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTargetingComponent : MonoBehaviour
{
    public float MaxDelta = .25f;
    public RectTransform monsterHitbox;

    public void Randomize()
    {
        RectTransform mine = GetComponent<RectTransform>();
        if (monsterHitbox!=null)
        {
            monsterHitbox.anchoredPosition = new Vector2 ( Random.Range(-100f, 100f) * mine.rect.width * MaxDelta / 200, monsterHitbox.anchoredPosition.y);
        }
    }
}
