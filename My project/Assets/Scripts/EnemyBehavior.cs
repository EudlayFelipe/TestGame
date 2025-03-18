using Unity.VisualScripting;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public float enemy_speed;

    GameObject player_object;
 
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        player_object = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        FollowPlayer();
    }

    void FollowPlayer()
    {
        Vector3 targetPos = new Vector3(player_object.transform.position.x, transform.position.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPos, enemy_speed * Time.deltaTime) ;

        if(player_object.transform.position.x > transform.position.x){
            transform.localScale = new Vector3(1, 1, 1);
        }
        else{
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
