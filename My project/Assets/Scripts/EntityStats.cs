using UnityEngine;

public class EntityStats : MonoBehaviour
{
    public float max_Hp;
    public float hp;
    public float base_speed;
    public float attack_speed;
    public float attack_damage;
    public float attack_range;

    public ParticleSystem blood_particle;

   

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
            ParticleSystem blood = Instantiate(blood_particle, transform.position, Quaternion.identity);            
            blood.Play();
            Destroy(blood, blood.main.duration);
            Destroy(this.gameObject);
        }
    }
    


}
