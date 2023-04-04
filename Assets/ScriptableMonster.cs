using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Monster", menuName = "Df8r/Monster")]
public class ScriptableMonster : ScriptableObject
{
    public int HealthValue = 5;
    public ScriptableAttack[] Attacks = new ScriptableAttack[0];
}
