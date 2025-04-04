using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public static HUD Instance {get; private set;}
    public Slider hp_Bar;

    EntityStats player_stats;

    public GameObject damagePopUp;

    void Awake()
    {
        if(Instance != null && Instance != this){
            Destroy(this);
        }
        else{Instance = this;}
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player_stats = GameObject.FindGameObjectWithTag("Player").GetComponent<EntityStats>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerHP();
    }

    void PlayerHP()
    {
        hp_Bar.maxValue = player_stats.max_Hp;
        hp_Bar.value = player_stats.hp; 
    }
}
