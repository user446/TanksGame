using UnityEngine;

/// <summary>
/// Projectile class that destroys projectile after any collision outside Player prefab
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Projectile : MonoBehaviour
{
    public float damage;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag != "Player")
            Destroy(gameObject);
    }
}
