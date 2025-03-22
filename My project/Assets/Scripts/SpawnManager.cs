using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public List<GameObject> spawn_points;

    public List<GameObject> enemies;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("SpawnEnemy", 5f, 2f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnEnemy(){
        foreach(GameObject sp in spawn_points)
        {
            int RandomEnemy = Random.Range(0,enemies.Count);
            Instantiate(enemies[RandomEnemy], sp.transform.position, Quaternion.identity);
        }
    }  
   
}
