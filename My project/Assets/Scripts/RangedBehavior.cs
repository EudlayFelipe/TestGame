using UnityEngine;

public class RangedBehavior : MonoBehaviour
{
    public GameObject enemy_projectile;

    bool can_shoot = true;
    float cooldown_;

    GameObject player_obj;

    AudioSource shootSource;
    AudioClip shootClip;

    EntityStats ranged_stats;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        shootSource = GetComponent<AudioSource>();
        player_obj = GameObject.FindGameObjectWithTag("Player");
        ranged_stats = GetComponent<EntityStats>();
    }

    // Update is called once per frame
     void Update()
    {
        Shoot();            
    }

   

    void Shoot() // atira
    {
        if(can_shoot)
        {
            shootSource.PlayOneShot(shootClip);
            GameObject enemy_projectile_Instance = Instantiate(enemy_projectile, transform.position, Quaternion.identity);
            enemy_projectile_Instance.GetComponent<BulletDamage>().bullet_damage = ranged_stats.attack_damage;
            enemy_projectile_Instance.GetComponent<BulletDamage>().bullet_lifespan = ranged_stats.attack_life;

            Vector2 enemy_projectile_direction = player_obj.transform.position - transform.position;
            enemy_projectile_direction.Normalize();

            enemy_projectile_Instance.GetComponent<Rigidbody2D>().AddForce(enemy_projectile_direction * ranged_stats.attack_range, ForceMode2D.Impulse);

            can_shoot = false;
            cooldown_ = 0;            
        }

        Cooldown();
    }

    void Cooldown()
    {
        if(cooldown_ > ranged_stats.attack_speed && !can_shoot)
        {   
            can_shoot = true;
        }
        else{
            cooldown_ += Time.deltaTime;
        }
    }

}
