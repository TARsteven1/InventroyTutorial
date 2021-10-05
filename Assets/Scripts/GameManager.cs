using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class GameManager : MonoBehaviour
{
    public GameObject ReadyBtn;
    public Transform RelifePoint;
    public void ReadyToPlay()
    {
        ReadyBtn.SetActive(false);
        PhotonNetwork.Instantiate("Player", RelifePoint.transform.position, Quaternion.identity, 0);
       // PhotonNetwork.Instantiate("Player", new Vector3(1,1,0), Quaternion.identity, 0);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
