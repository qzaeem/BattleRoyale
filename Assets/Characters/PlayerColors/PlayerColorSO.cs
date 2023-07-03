using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerColors", menuName = "ScriptableObjects/PlayerColors", order = 1)]
public class PlayerColorSO : ScriptableObject
{
    public float health;
    public float attackDamage;
    public int experience;
    public int level;
    public Color[] colors;
}
