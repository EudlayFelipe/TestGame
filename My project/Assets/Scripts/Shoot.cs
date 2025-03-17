using Unity.Mathematics;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    EntityStats player_stats;
    public Transform spawnBullet;
    public GameObject bullet;

    bool can_shoot = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player_stats = gameObject.GetComponent<EntityStats>();
    }

    // Update is called once per frame
    void Update()
    {
        ShootBullet();
    }

    void ShootBullet(){
        if(Input.GetMouseButtonDown(0)){
            Instantiate(bullet, spawnBullet.transform.position, Quaternion.identity);

            Vector2 bullet_direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            bullet_direction.Normalize();

            bullet.GetComponent<Rigidbody2D>().AddForce(bullet_direction * player_stats.attack_range, ForceMode2D.Impulse);
        }
    }
}
