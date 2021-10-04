using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensei : MonoBehaviour
{
    //private Collider2D coll;
    public GameObject Button;
    public GameObject talkUI;
    // Start is called before the first frame update
    void Start()
    {
        //coll = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Button.activeSelf&&Input.GetKeyDown(KeyCode.R))
          talkUI.SetActive(true);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag=="Player")
        {
            Button.SetActive(true);          
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Button.SetActive(false);
            
        }
    }

}
