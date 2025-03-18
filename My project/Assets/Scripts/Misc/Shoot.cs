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

    


    bool can_shoot = true;

    PlayerMovement playerMovement;   

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        player_stats = gameObject.GetComponent<EntityStats>();
        playerMovement = FindAnyObjectByType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        ShootBullet();
        RotateGun();
    }

    void RotateGun()
    {           
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(mousePos.y - gun.gameObject.transform.position.y, mousePos.x - gun.gameObject.transform.position.x) * Mathf.Rad2Deg;

        if(!playerMovement.isFacingRight){
            angle += 180f;
        }

        gun.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));              
    }

    void ShootBullet()
    {
        if(Input.GetMouseButton(0) && can_shoot)
        {
            GameObject bullet_Instance = Instantiate(bullet, spawnBullet.position, Quaternion.identity);
            bullet_Instance.GetComponent<BulletDamage>().bullet_damage = player_stats.attack_damage;

            Vector2 bullet_direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            bullet_direction.Normalize();

            bullet_Instance.GetComponent<Rigidbody2D>().AddForce(bullet_direction * bullet_speed, ForceMode2D.Impulse);

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
