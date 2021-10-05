using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;

public class NetworkLauncher : MonoBehaviourPunCallbacks
{
    public GameObject LoginUI;
    public GameObject NameUI;
    public InputField NameField;
    public InputField RoomField;
    // Start is called before the first frame update
    void Start()
    {
        //链接服务器
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
        NameUI.SetActive(true);
            //base.OnConnectedToMaster();

        //创建房间
        //PhotonNetwork.JoinOrCreateRoom("Room", new Photon.Realtime.RoomOptions() { MaxPlayers = 20 },default);
    }
    public void OnCreateRoom()
    {
        // base.OnJoinedRoom();
        if (RoomField.text.Length<2)
            return;
        
        LoginUI.SetActive(false);
        RoomOptions roomOptions = new RoomOptions { MaxPlayers = 20 };
        PhotonNetwork.JoinOrCreateRoom(RoomField.text, roomOptions, default);
       // PhotonNetwork.Instantiate("Player",new Vector3(1,1,0),Quaternion.identity,0);
    }
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayeButton()
    {
        NameUI.SetActive(false);
        PhotonNetwork.NickName = NameField.text;
        LoginUI.SetActive(true);
    }
    
}
