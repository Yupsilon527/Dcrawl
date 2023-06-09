using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Attack", menuName = "Df8r/Attack")]
public class ScriptableAttack : ScriptableObject
{
    public string TooltipDescription;
    public string AbilitySoundName; //SFX ability sound used for individual abilities
    public int AttackValue = 0;
    public int DefenseValue = 0;
    public int AttackCount = 0;
    public int ActionPoints = 0;
}
