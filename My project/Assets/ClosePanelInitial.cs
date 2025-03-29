using UnityEngine;

public class ClosePanelInitial : MonoBehaviour
{

    public GameObject panel;
   
    public void ClosePanelButton(){
        panel.SetActive(false);
    }
}
