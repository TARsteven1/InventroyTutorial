using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkLauncher : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        //链接服务器
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {    
            base.OnConnectedToMaster();

        //创建房间
        PhotonNetwork.JoinOrCreateRoom("Room", new Photon.Realtime.RoomOptions() { MaxPlayers = 20 },default);
    }
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        PhotonNetwork.Instantiate("Player",new Vector3(1,1,0),Quaternion.identity,0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
