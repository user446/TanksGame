using UnityEngine;

public class Weapon : MonoBehaviour
{
    public string weaponName;
    public float attackDamage;
    public float attackCooldown;
    public ProjectileLauncher[] launchers;
    private float lastLaunchTime;

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
