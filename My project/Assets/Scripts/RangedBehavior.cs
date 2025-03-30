using UnityEngine;

public class RangedBehavior : MonoBehaviour
{
    public GameObject enemy_projectile;

    private bool can_shoot = true;
    private float lastShotTime;

    private GameObject player_obj;
    private Transform player;

    private AudioSource shootSource;
    public AudioClip shootClip;

    private EntityStats ranged_stats;
    private Rigidbody2D rb_ranged;

    public float enemy_speed;
    public float groundDetectionDistance = 1.0f; // Distância para detectar o chão
    public Transform bullet_spawn_ranged;
    public LayerMask groundLayer; // Camada do chão

    void Start()
    {
        shootSource = GetComponent<AudioSource>();
        player_obj = GameObject.FindGameObjectWithTag("Player");
        player = player_obj?.transform;

        ranged_stats = GetComponent<EntityStats>();
        rb_ranged = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        FollowPlayer();
        FlipDirection();
        Shoot();
    }

    void FollowPlayer()
    {
        if (player != null)
        {
            float direction = player.position.x > transform.position.x ? 1f : -1f;
            Vector2 moveForce = new Vector2(direction * enemy_speed, rb_ranged.linearVelocity.y);

            // Verifica se há chão à frente para evitar cair em buracos
            RaycastHit2D groundInfo = Physics2D.Raycast(transform.position + new Vector3(direction * 0.5f, 0, 0), Vector2.down, groundDetectionDistance, groundLayer);
            
            if (groundInfo.collider != null)
            {
                rb_ranged.linearVelocity = moveForce;
            }
        }
    }

    void FlipDirection()
    {
        if (player != null)
        {
            if (player.position.x > transform.position.x)
                transform.localScale = new Vector3(1, 1, 1);
            else
                transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    void Shoot()
    {
        if (can_shoot && player != null)
        {
            if (shootClip && shootSource)
                shootSource.PlayOneShot(shootClip);

            GameObject projectileInstance = Instantiate(enemy_projectile, bullet_spawn_ranged.position, Quaternion.identity);
            BulletDamage bullet = projectileInstance.GetComponent<BulletDamage>();

            bullet.bullet_damage = ranged_stats.attack_damage;
            bullet.bullet_lifespan = ranged_stats.attack_life;

            Vector2 direction = (player.position - bullet_spawn_ranged.position).normalized;
            projectileInstance.GetComponent<Rigidbody2D>().linearVelocity = direction * ranged_stats.attack_range;

            can_shoot = false;
            lastShotTime = Time.time;
        }

        CheckCooldown();
    }

    void CheckCooldown()
    {
        if (!can_shoot && Time.time - lastShotTime >= ranged_stats.attack_speed)
        {
            can_shoot = true;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<EntityStats>().RemoveHp(ranged_stats.attack_damage);
            SpawnManager.Instance.n_monsters_left--;
            Destroy(gameObject);
        }
    }
}
