using UnityEngine;
using FirstGearGames.SmoothCameraShaker;
public class BulletDamage : MonoBehaviour
{
    public float bullet_damage;  

    public ShakeData shootShakeData;

    public ParticleSystem groundColl_Particle;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {        
        Destroy(gameObject, .5f);        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy"){
            collision.gameObject.GetComponent<EntityStats>().hp -= bullet_damage;
            CameraShakerHandler.Shake(shootShakeData);
            Destroy(gameObject);
        }

        if(collision.gameObject.tag == "Ground"){
            Destroy(this.gameObject);
            ParticleSystem particleInstance = Instantiate(groundColl_Particle, transform.position, Quaternion.identity);
            particleInstance.Play();
            Destroy(particleInstance, 3f);
        }
    }
}
