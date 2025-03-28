using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{
    public static OptionsManager Instance {get; private set;}
    public GameObject options_obj;
    public List<AudioSource> audioSources;
    public Slider audio_slider;
    public Dropdown resolucion_dropdown;

    void Awake()
    {
        if(Instance != null && Instance != this){
            Destroy(this);
        }
        else{
            Instance = this;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            Time.timeScale = 0;
            options_obj.SetActive(true);
        }
    }

    public void ApplyOptions(){
        Time.timeScale = 1;
        options_obj.SetActive(false);

        foreach(AudioSource audio in audioSources){
            audio.volume = audio_slider.value;
        }
    }

    public void changeResolucion(){
        if(resolucion_dropdown.value == 0){
            Screen.SetResolution(1920,1080, false);
        }
         if(resolucion_dropdown.value == 1){
            Screen.SetResolution(1280,720, false);
        }
         if(resolucion_dropdown.value == 2){
            Screen.SetResolution(900,600, false);
        }
    }
}
