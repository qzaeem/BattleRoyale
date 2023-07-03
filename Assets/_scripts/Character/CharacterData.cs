using UnityEngine;

public abstract record CharacterData
{
    public float health;
    public float attackDamage;
}

public record PlayerData : CharacterData
{
    public string name;
    public int experience;
    public int level;
    public int colorIndex;
    public int playerIndex;
}

public record EnemyData : CharacterData
{

}
