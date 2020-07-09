using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    [SerializeField]
    public GameObject projectile_prefab;
    public float onLaunchSpeed = 100;

    public void Launch()
    {
        var new_projectile = Instantiate(projectile_prefab, transform.position, transform.rotation);
        new_projectile.GetComponent<Rigidbody2D>().velocity = transform.up * onLaunchSpeed;
    }

    public void Launch(float withDamage)
    {
        var new_projectile = Instantiate(projectile_prefab, transform.position, transform.rotation);
        new_projectile.GetComponent<Rigidbody2D>().velocity = transform.up * onLaunchSpeed;
        new_projectile.GetComponent<Projectile>().damage = withDamage; 
    }
}
