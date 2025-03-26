using UnityEngine;
using UnityEngine.UI;

public class EnemyHPBar : MonoBehaviour
{
    Slider hp_slider;
    EntityStats entity_stats;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hp_slider = gameObject.GetComponentInChildren<Slider>();
        entity_stats = gameObject.GetComponentInParent<EntityStats>();
    }

    // Update is called once per frame
    void Update()
    {
        
        hp_slider.maxValue = entity_stats.max_Hp;
        hp_slider.value = entity_stats.hp;

    }
}
