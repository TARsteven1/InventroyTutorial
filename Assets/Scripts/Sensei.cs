using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensei : MonoBehaviour
{
public GameObject Button;
public GameObject talkUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Button.activeSelf&&Input.GetKeyDown(KeyCode.R))
talkUI.SetActive(true);
    }
private void OnTrriggerEnter2D(Collider2D other){
        Button.SetActive(true);
    }
private void OnTrriggerExit2D(Collider2D other){
        Button.SetActive(false);
    }

}
