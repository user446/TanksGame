using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class to control Health bar UI
/// </summary>
public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Text healthText;
    private GameObject player;
    void Start()
    {
        player = GameManager.GetPlayer();
        slider.maxValue = player.GetComponent<Health>().maxHealth;
        healthText.text = player.GetComponent<Health>().maxHealth.ToString();
    }

    /// <summary>
    /// Update slider and text values to show new health amount
    /// </summary>
    void Update()
    {
        if(player != null)
        {
            slider.value = player.GetComponent<Health>().currentHealth;
            healthText.text = player.GetComponent<Health>().currentHealth.ToString();
        }

    }
}
