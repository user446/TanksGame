﻿using UnityEngine;
using UnityEngine.UI;

public class WeaponInfoUI : MonoBehaviour
{
    public Text weaponInfo;
    private PlayerBase player;
    void Start()
    {
        player = GameManager.GetPlayer().GetComponent<PlayerBase>();
    }

    void Update()
    {
        if(player != null)
            weaponInfo.text = player.weapon_name;
    }
}