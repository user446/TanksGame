using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player Base class
/// It's a root for all player based scripts
/// </summary>
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Armor))]
[RequireComponent(typeof(Combat))]
[RequireComponent(typeof(PlayerController))]
public class PlayerBase : MonoBehaviour
{
    public List<GameObject> possibleWeapons;
    public Animator track_left;
    public Animator track_right;
    private int currentWeaponIndex;
    private bool isMoving;
    public Health health;
    public Armor armor;
    public Combat combat;
    public Weapon weapon;
    public string weapon_name = "";
    private PlayerController controller;

    void Start()
    {
        InstallNextWeapon();

        health = GetComponent<Health>();
        armor = GetComponent<Armor>();
        combat = GetComponent<Combat>();
        controller = GetComponent<PlayerController>();

        //add calls on controller actions
        controller.onMoving += OnMoving;
        controller.onStopMoving += OnStopMoving;
        controller.onAttack += Attack;
        controller.onNextWeapon += InstallNextWeapon;
        controller.onPrevWeapon += InstallPreviousWeapon;

        health.onDeath += Death;    //add a new call upon player's death
    }

    void Update()
    {
        //stomp that enemy!
        if(isMoving)
        {
            combat.Attack();
        }
    }

    private void Death()
    {
        OnStopMoving();
        combat.enabled = false;
        controller.enabled = false;
        Destroy(gameObject);
    }

    private void OnMoving()
    {
        track_left.SetBool("isMoving", true);
        track_right.SetBool("isMoving", true);
        isMoving = true;
    }

    private void OnStopMoving()
    {
        track_left.SetBool("isMoving", false);
        track_right.SetBool("isMoving", false);
        isMoving = false;
    }

    private void Attack()
    {
        weapon.LaunchProjectile();
    }

    public void InstallNextWeapon()
    {
        ClearCurrentWeapon();
        currentWeaponIndex++;
        if(currentWeaponIndex > possibleWeapons.Count-1)
        {
            currentWeaponIndex = 0;
        }
        InstallWeapon();
    }

    public void InstallPreviousWeapon()
    {
        ClearCurrentWeapon();
        currentWeaponIndex--;
        if(currentWeaponIndex < 0)
        {
            currentWeaponIndex = possibleWeapons.Count-1;
        }
        InstallWeapon(); 
    }

    /// <summary>
    /// Function to instantiate a new weapon inside player prefab
    /// </summary>
    private void InstallWeapon()
    {
        var new_weapon = Instantiate(possibleWeapons[currentWeaponIndex], transform.position, transform.rotation);
        new_weapon.transform.parent = this.transform;
        weapon = FindObjectOfType<Weapon>(); 
        weapon_name = weapon.weaponName;
    }

    /// <summary>
    /// Function to clear previous weapon by destroying its prefab
    /// </summary>
    private void ClearCurrentWeapon()
    {
        if(weapon != null)
        {
            Destroy(weapon.gameObject);
        }

    }
}
