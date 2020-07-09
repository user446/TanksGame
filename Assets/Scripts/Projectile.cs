using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag != "Player")
            Destroy(gameObject);
    }
}
