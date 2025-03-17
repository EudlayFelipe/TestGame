using UnityEngine;

public class EntityStats : MonoBehaviour
{
    public float max_Hp;
    public float hp;
    public float base_speed;
    public float attack_speed;
    public float attack_damage;
    public float attack_range;

    void Start()
    {
        hp = max_Hp;
    }
    
    void Update()
    {
        Death();
    }

    void Death()
    {
        if(hp <= 0 )
        {
            Destroy(this.gameObject);
        }
    }
    


}
