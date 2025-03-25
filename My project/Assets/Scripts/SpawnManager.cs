using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{

    public static SpawnManager Instance {get; private set; }

    [Header("Wave Control")]
    public TMP_Text wave_text;
    public int wave_;
    //public int n_monsters;    
    public int n_monsters_spawned;
    public int n_monsters_left;
    public float spawn_cooldown;
    float spawn_cooldown_count;
    bool canSpawnEnemies = false;
    public GameObject next_wave_button;

    [Header("Lists")]
    public List<GameObject> spawn_points;
    public List<WaveScriptable> waves_list;

    //public List<GameObject> enemies; BACKUP

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
        
    }

    
    void FixedUpdate()
    {
        if(canSpawnEnemies == true){SpawnEnemy();}
    }

    void UpdateWaveHUD(){        
        wave_text.text = "Wave: " + (wave_ + 1).ToString();
    }

    public void StartNewWave()
    {
        next_wave_button.SetActive(false);
        n_monsters_left = waves_list[wave_].n_monsters;
        n_monsters_spawned = 0;
        canSpawnEnemies = true;
    }

    void SpawnEnemy()
    {
        if(n_monsters_left <= 0){
            canSpawnEnemies = false;                       
            wave_++;
            UpdateWaveHUD();
            next_wave_button.SetActive(true);
            return;
        }

        if(n_monsters_spawned < waves_list[wave_].n_monsters && spawn_cooldown_count <= 0)
        {
            

            foreach(GameObject sp in spawn_points){

                if(waves_list[wave_].monster.Count > 0){

                    int RandomIndex = Random.Range(0, waves_list[wave_].monster.Count);
                    GameObject enemyToSpawn = waves_list[wave_].monster[RandomIndex];


                    Instantiate(enemyToSpawn, sp.transform.position, Quaternion.identity);
                    n_monsters_spawned++;                    
                    spawn_cooldown_count = spawn_cooldown;
                }
                
            }
            
            #region BackupSpawnEnemy
            /*
            foreach(GameObject sp in spawn_points)
             {
                int RandomEnemy = Random.Range(0,enemies.Count);
                Instantiate(enemies[RandomEnemy], sp.transform.position, Quaternion.identity);
                n_monsters_spawned++;
                spawn_cooldown_count = spawn_cooldown;
            }
            */
            #endregion
        }
        else{
            spawn_cooldown_count -= Time.deltaTime;
        }
        
    }  
   
}
