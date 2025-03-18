using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public float enemy_speed;

    GameObject player_object;
    Rigidbody2D rb_enemy;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb_enemy = GetComponent<Rigidbody2D>();
        player_object = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        FollowPlayer();
    }

    void FollowPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, player_object.transform.position, enemy_speed * Time.deltaTime) ;
    }
}
