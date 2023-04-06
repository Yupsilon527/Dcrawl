using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterTargetingComponent : MonoBehaviour
{
    public float MaxDelta = .25f;
    public RectTransform monsterHitbox;

    public float monsterAttackDuration = .5f;
    public GameObject monsterAttack;


    private void Awake()
    {
        monsterAttack?.SetActive(false);
    }
    public void MoveEnemyAround()
    {
        RectTransform mine = GetComponent<RectTransform>();
        if (monsterHitbox!=null)
        {
            monsterHitbox.anchoredPosition = new Vector2 ( Random.Range(-100f, 100f) * mine.rect.width * MaxDelta / 200, monsterHitbox.anchoredPosition.y);
        }
    }
    Coroutine AttackCoroutine;
    public void ShowMonsterAttack()
    {
        if (AttackCoroutine != null)
            StopCoroutine(AttackCoroutine);

        AttackCoroutine = StartCoroutine(ShowMonsterAttackForDuration(monsterAttackDuration));
    }
    IEnumerator ShowMonsterAttackForDuration( float dur)
    {
            monsterAttack?.SetActive(true);
        yield return new WaitForSecondsRealtime(dur);
        monsterAttack?.SetActive(false);
        AttackCoroutine = null;
    }
}
