using UnityEngine;

/// <summary>
/// Projectile launcher class
/// Holds a prefab of a projectile to launch
/// and speed with what it should be launched
/// </summary>
public class ProjectileLauncher : MonoBehaviour
{
    [SerializeField]
    public GameObject projectile_prefab;
    public float onLaunchSpeed = 100;

    /// <summary>
    /// Function to launch a projectile.
    /// Applies velocity to a pojectile.
    /// </summary>
    public void Launch()
    {
        var new_projectile = Instantiate(projectile_prefab, transform.position, transform.rotation);
        new_projectile.GetComponent<Rigidbody2D>().velocity = transform.up * onLaunchSpeed;
    }

    /// <summary>
    /// Function to launch projectile with applied damage
    /// </summary>
    /// <param name="withDamage">Damage to apply</param>
    public void Launch(float withDamage)
    {
        var new_projectile = Instantiate(projectile_prefab, transform.position, transform.rotation);
        new_projectile.GetComponent<Rigidbody2D>().velocity = transform.up * onLaunchSpeed;
        new_projectile.GetComponent<Projectile>().damage = withDamage; 
    }
}
