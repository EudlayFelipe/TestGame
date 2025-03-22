using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class ShopItem : MonoBehaviour
{
    public Weapon w_;

    public Text item_name_holder;
    public Text item_value_holder;
    public Image item_icon_holder;
    public Text item_info_holder;

    public Button shop_button;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Setup(w_);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Setup(Weapon w)
    {
        item_name_holder.text = w.weapon_name;
        item_value_holder.text = w.weapon_value.ToString();
        item_icon_holder.sprite = w.weapon_icon;
        item_info_holder.text = "Damage: "+ w.weapon_damage.ToString()+  "\nSpeed: " + w.weapon_speed.ToString() + "\nRange: " + w.weapon_range.ToString();

        if(InventoryManager.Instance.gold_coins < w.weapon_value){
            shop_button.interactable = false;
        }
        else{
            shop_button.interactable = true;
        }
    }

    public void BuyWeapon()
    {
        if(InventoryManager.Instance.inventory_[2] != null){

        }
        else{

            for (int i = 0; i < 3; i++){
                if(InventoryManager.Instance.inventory_[i] == null){
                    InventoryManager.Instance.inventory_[i] = w_;
                    break;
                }
            }


           // InventoryManager.Instance.inventory_.Add(w_);
            InventoryManager.Instance.AddGold(w_.weapon_value * -1);
            RefreshShop();
            Destroy(this.gameObject);
        }
    }

    void RefreshShop(){
        GameObject[] shop_buttons = GameObject.FindGameObjectsWithTag("ShopItem");

        foreach (GameObject go in shop_buttons){
            go.GetComponent<ShopItem>().Setup(go.GetComponent<ShopItem>().w_);
        }
    }
}
