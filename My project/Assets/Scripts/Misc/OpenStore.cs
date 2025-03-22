using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenStore : MonoBehaviour
{
    public GameObject store_gameObject;
    public GameObject store_warning;
    public GameObject shop_Bg;
    public GameObject shop_Item;

    GameObject player_object;
    

    public List<Weapon> weaponsSold;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RandomItems();
        store_gameObject.SetActive(false);
        player_object = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector2.Distance(transform.position, player_object.transform.position);
        
        if(dist < 2){
            store_warning.SetActive(true);

            if(Input.GetKeyDown(KeyCode.E)){
                store_gameObject.SetActive(true);
            }
        }
        else{
            store_warning.SetActive(false);
            store_gameObject.SetActive(false);
        }
    }

    void RandomItems(){
        for(int i = 0; i < 3; i++){
            int random_number = Random.Range(0, weaponsSold.Count);

            GameObject new_shopItem = Instantiate(shop_Item, shop_Bg.transform);    
            new_shopItem.GetComponent<ShopItem>().w_ = weaponsSold[random_number];        
            new_shopItem.GetComponent<ShopItem>().Setup(weaponsSold[random_number]);
        }
    }
}
