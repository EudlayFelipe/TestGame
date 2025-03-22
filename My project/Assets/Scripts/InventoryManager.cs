using System.Collections.Generic;
using System.ComponentModel.Design;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance {get; private set;}

    public GameObject inv_background;
    public GameObject inv_slot;

    int selected_slot = 0;

    public List<Weapon> inventory_;

    EntityStats player_stats;

    [Header("Gold")]
    public int gold_coins;
    public Text gold_text;

    int actvive_slot;

    void Awake()
    {
        if(Instance != null && Instance != this){
            Destroy(this);
        }else{
            Instance = this;
        }
    }

    void Start()
    {               
        player_stats = GameObject.FindGameObjectWithTag("Player").GetComponent<EntityStats>();
        SelectWeapon(1);
    }

    
    void Update()
    {
        InventorySelection();
    }

    void RefreshInventory()
    {
        gold_text.text = gold_coins.ToString();

        //Assim que um item é selecionado, A lista inteira é destruida, evitando duplicatas
        GameObject[] destroy_slots = GameObject.FindGameObjectsWithTag("Slot");
        foreach ( GameObject go in destroy_slots){
            Destroy(go);
        } 


        int hotKey_ = 1;

        foreach (Weapon w in inventory_)
        {
            GameObject slot_instance = Instantiate(inv_slot, inv_background.transform); // instancia no bg
            
            if(w == null){
                slot_instance.GetComponent<Image>().enabled = false;               

            }
            else
            {
                slot_instance.GetComponent<Image>().enabled = true;
                slot_instance.GetComponentInChildren<Image>().sprite = w.weapon_icon;
                slot_instance.GetComponentInChildren<Image>().color = Color.gray;
                if(selected_slot == hotKey_)                                             // Muda a cor do item selecionado
                {
                    slot_instance.GetComponentInChildren<Image>().color = Color.white;
                }
            }

            
            

            slot_instance.GetComponentInChildren<Text>().text = hotKey_.ToString();
            hotKey_ ++;

        }
    }

    void SelectWeapon(int hotKey_)
    {
        Weapon selected_weapon = inventory_[hotKey_ - 1];
        actvive_slot = hotKey_ - 1;
        player_stats.attack_damage = selected_weapon.weapon_damage;
        player_stats.attack_speed = selected_weapon.weapon_speed;
        player_stats.attack_range = selected_weapon.weapon_range;
        player_stats.attack_life = selected_weapon.weapon_life;

        selected_slot = hotKey_;
        RefreshInventory();
    }

    void InventorySelection(){
        if(Input.GetKeyDown(KeyCode.Alpha1)){
            SelectWeapon(1);
        }
        if(Input.GetKeyDown(KeyCode.Alpha2)){
            SelectWeapon(2);
        }
        if(Input.GetKeyDown(KeyCode.Alpha3)){
            SelectWeapon(3);
        }
    }
    
    public void AddGold(int g){
        gold_coins += g;
        RefreshInventory();
    }


    public void DiscardWeapon(){
        if(actvive_slot != 0){
            inventory_[actvive_slot] = null;
            SelectWeapon(1);
            RefreshInventory();
        }
    }
}
