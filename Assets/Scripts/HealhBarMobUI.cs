using UnityEngine;
using UnityEngine.UI;

public class HealhBarMobUI : MonoBehaviour
{
    public Slider slider;
    public GameObject mob;
    void Start()
    {
        slider.maxValue = mob.GetComponent<Health>().maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(mob != null)
            slider.value = mob.GetComponent<Health>().currentHealth;
    }
}
