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
    public GameObject gameEndPanel; // Painel de fim de jogo

    [Header("Lists")]
    public List<GameObject> spawn_points;
    public List<WaveScriptable> waves_list;
    public GameObject flyingEnemyPrefab; // Prefab do inimigo voador

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
            
            if(wave_ >= waves_list.Count){
                ThanksForPlayingDemo();
                return;
            }

            UpdateWaveHUD();
            next_wave_button.SetActive(true);
            return;
        }

        if(n_monsters_spawned < waves_list[wave_].n_monsters && spawn_cooldown_count <= 0)
        {
            foreach(GameObject sp in spawn_points){

                   if (waves_list[wave_].monster.Count > 0)
                {
                    GameObject enemyToSpawn;
                    
                    // Verifica se o spawn é o terceiro e se o inimigo voador está na lista da wave
                    if (spawn_points.IndexOf(sp) == 2 && waves_list[wave_].monster.Contains(flyingEnemyPrefab)) 
                    {
                        enemyToSpawn = flyingEnemyPrefab;
                    }
                    else
                    {
                        int randomIndex = Random.Range(0, waves_list[wave_].monster.Count);
                        enemyToSpawn = waves_list[wave_].monster[randomIndex];
                    }

                    Instantiate(enemyToSpawn, sp.transform.position, Quaternion.identity);
                    n_monsters_spawned++;
                    spawn_cooldown_count = spawn_cooldown;
                }
            
            }
            #region BackupSpawnEnemy2
            /*
            foreach(GameObject sp in spawn_points){

                if(waves_list[wave_].monster.Count > 0){

                    int RandomIndex = Random.Range(0, waves_list[wave_].monster.Count);
                    GameObject enemyToSpawn = waves_list[wave_].monster[RandomIndex];


                    Instantiate(enemyToSpawn, sp.transform.position, Quaternion.identity);
                    n_monsters_spawned++;                    
                    spawn_cooldown_count = spawn_cooldown;
                }
            
            }
            */
            #endregion
            
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


    void ThanksForPlayingDemo(){
        gameEndPanel.SetActive(true);
        Time.timeScale = 0;
    }
}
