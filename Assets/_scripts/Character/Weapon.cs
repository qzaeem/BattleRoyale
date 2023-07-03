using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float attackDamage { get; set; }
    public string targetTag { get; set; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != targetTag) return;

        var character = other.GetComponent<Character>();
        character.ApplyDamage(attackDamage);
    }
}
