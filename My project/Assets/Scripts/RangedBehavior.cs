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

    Rigidbody2D rb_ranged;

    public float fixedYPosition;
    public float enemy_speed;
    private Transform player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        shootSource = GetComponent<AudioSource>();
        player_obj = GameObject.FindGameObjectWithTag("Player");
        player = GameObject.FindGameObjectWithTag("Player").transform;
        ranged_stats = GetComponent<EntityStats>();
        rb_ranged = GetComponent<Rigidbody2D>();
        fixedYPosition = transform.position.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Shoot();            
        FollowPlayer();       
    }

    void FollowPlayer()
    {
        if (player != null)
        {
            Vector3 targetPosition = new Vector3(player.position.x, fixedYPosition, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, enemy_speed * Time.deltaTime);
        }
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player"){
            collision.gameObject.GetComponent<EntityStats>().RemoveHp(gameObject.GetComponent<EntityStats>().attack_damage);
            SpawnManager.Instance.n_monsters_left--;
            Destroy(gameObject);
        }
    }

}
