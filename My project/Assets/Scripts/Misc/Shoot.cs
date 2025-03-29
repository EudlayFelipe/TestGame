using System;
using System.IO.Compression;
using Unity.Mathematics;
using UnityEngine;


public class Shoot : MonoBehaviour
{
    EntityStats player_stats;
    float cooldown_;

    [Header("Bullet Config")]
    public Transform spawnBullet;
    public GameObject bullet;
    public float bullet_speed;
    public GameObject gun;

    [Header("SFX")]
    public AudioClip shootClip;
    public float weapon_sound_pitch;
    AudioSource shootSource;

    bool can_shoot = true;

    PlayerMovement playerMovement;   

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {        
        player_stats = gameObject.GetComponent<EntityStats>();
        playerMovement = FindAnyObjectByType<PlayerMovement>();
        shootSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ShootBullet();
        RotateGun();

        if(Input.GetKeyDown(KeyCode.G)){
            InventoryManager.Instance.DiscardWeapon();
        }
    }

    void RotateGun() // rotaciona a arma se o player estiver virado para esquerda ou para direita e dependendo da posição do mouse
    {           
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(mousePos.y - gun.gameObject.transform.position.y, mousePos.x - gun.gameObject.transform.position.x) * Mathf.Rad2Deg;

        if(!playerMovement.isFacingRight){
            angle += 180f;
        }        
        
        gun.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));                    
    }

    void ShootBullet() // atira
    {
        if(Input.GetMouseButton(0) && can_shoot)
        {
            shootSource.pitch = weapon_sound_pitch;
            shootSource.PlayOneShot(shootClip);
            GameObject bullet_Instance = Instantiate(bullet, spawnBullet.position, Quaternion.identity);
            bullet_Instance.GetComponent<BulletDamage>().bullet_damage = player_stats.attack_damage;
            bullet_Instance.GetComponent<BulletDamage>().bullet_lifespan = player_stats.attack_life;

            Vector2 bullet_direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            bullet_direction.Normalize();


            float rot_z = Mathf.Atan2(bullet_direction.y, bullet_direction.x) * Mathf.Rad2Deg;
            bullet_Instance.transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);


            bullet_Instance.GetComponent<Rigidbody2D>().AddForce(bullet_direction * player_stats.attack_range, ForceMode2D.Impulse);

            can_shoot = false;
            cooldown_ = 0;            
        }

        Cooldown();
    }

    void Cooldown()
    {
        if(cooldown_ > player_stats.attack_speed && !can_shoot)
        {   
            can_shoot = true;
        }
        else{
            cooldown_ += Time.deltaTime;
        }
    }

}
