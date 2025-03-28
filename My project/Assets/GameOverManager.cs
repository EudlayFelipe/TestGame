using UnityEngine;

public class GameOverManager : MonoBehaviour
{
     public GameObject panel_go;

     EntityStats player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        panel_go.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<EntityStats>();
    }

    // Update is called once per frame
    void Update()
    {
       if(player.hp <= 0){
        Time.timeScale = 0;
        panel_go.SetActive(true);
       }
    }
}
