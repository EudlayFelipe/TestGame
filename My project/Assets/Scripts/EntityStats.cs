using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Random = UnityEngine.Random;
public class EntityStats : MonoBehaviour
{
    public float max_Hp;
    public float hp;
    public float base_speed;
    public float attack_life;
    public float attack_speed;
    public float attack_damage;
    public float attack_range;

    public int gold_carry;

    public ParticleSystem blood_particle;

   

    void Start()
    {        
        hp = max_Hp;
    }
    
    void Update()
    {
        DamageBlink();
    }

    void Death()
    {
        if(hp <= 0 )
        {
            if(this.gameObject.tag != "Player"){               
                InventoryManager.Instance.AddGold(gold_carry);
                SpawnManager.Instance.n_monsters_left--;
            }

            ParticleSystem blood = Instantiate(blood_particle, transform.position, Quaternion.identity);            
            blood.Play();
            Destroy(blood, blood.main.duration);
            Destroy(this.gameObject);
        }
    }

    
    public void RemoveHp(float hp_to_remove){

        // Change color
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;

        GameObject new_pop_up =Instantiate(HUD.Instance.damagePopUp, this.gameObject.transform.position, Quaternion.identity);
        new_pop_up.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-1f,1f), 5), ForceMode2D.Impulse);
        new_pop_up.GetComponentInChildren<TextMeshProUGUI>().text = hp_to_remove.ToString();
        Destroy(new_pop_up, 1f);

        hp -= hp_to_remove;
        Death();
    }

    void DamageBlink()
    {
        if(gameObject.GetComponent<SpriteRenderer>().color == Color.white){

        }else{
            gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(gameObject.GetComponent<SpriteRenderer>().color, Color.white, 10 * Time.deltaTime);
        }
    }
}
