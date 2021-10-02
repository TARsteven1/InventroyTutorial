using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleMap : MonoBehaviour
{
    public GameObject PlayerPoint;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(PlayerPoint.transform.position.x, PlayerPoint.transform.position.y,transform.position.z);
    }
}
