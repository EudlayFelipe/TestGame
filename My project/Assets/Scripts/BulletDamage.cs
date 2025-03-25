using UnityEngine;
using FirstGearGames.SmoothCameraShaker;
public class BulletDamage : MonoBehaviour
{
    public float bullet_damage;  
    public bool isPlayer;

    public float bullet_lifespan = 1;
    public ShakeData shootShakeData;
    public ParticleSystem groundColl_Particle;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {        
        Destroy(gameObject, bullet_lifespan);        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if((collision.gameObject.tag == "Enemy" && isPlayer) || collision.gameObject.tag == "Player" && !isPlayer){            
            collision.gameObject.GetComponent<EntityStats>().RemoveHp(bullet_damage);
            
            Destroy(gameObject);
        }

        if(collision.gameObject.tag == "Ground"){
            Destroy(this.gameObject);
            ParticleSystem particleInstance = Instantiate(groundColl_Particle, transform.position, Quaternion.identity);
            particleInstance.Play();
            Destroy(particleInstance, particleInstance.main.duration);
           
        }
    }
}
