using UnityEngine;

/// <summary>
/// Weapon class
/// </summary>
public class Weapon : MonoBehaviour
{
    public string weaponName;
    public float attackDamage;
    public float attackCooldown;
    public ProjectileLauncher[] launchers;
    private float lastLaunchTime;

    /// <summary>
    /// Launches projectile from each launcher and applies damage
    /// </summary>
    public void LaunchProjectile()
    {
        if(Time.time > lastLaunchTime + attackCooldown)
        foreach(var launcher in launchers)
        {
            lastLaunchTime = Time.time;
            //launcher.Launch();
            launcher.Launch(attackDamage);
        }
    }
}
