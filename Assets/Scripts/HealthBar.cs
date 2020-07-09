using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    private GameObject player;
    void Start()
    {
        player = GameManager.GetPlayer();
        slider.maxValue = player.GetComponent<Health>().maxHealth;
    }

    void Update()
    {
        if(player != null)
            slider.value = player.GetComponent<Health>().currentHealth;
    }
}
