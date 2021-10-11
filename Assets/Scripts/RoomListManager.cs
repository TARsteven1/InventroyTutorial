using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class RoomListManager : MonoBehaviourPunCallbacks
{
    public GameObject roomNamePrafab;
    public Transform gridLayout;
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        for (int i = 0; i < gridLayout.childCount; i++)
        {
            if (gridLayout.GetChild(i).gameObject.GetComponentInChildren<Text>().text==roomList[i].Name)
            {
                Destroy(gridLayout.GetChild(i).gameObject);
                if (roomList[i].PlayerCount==0)
                {
                    roomList.Remove(roomList[i]);
                }
            }
        }
        //base.OnRoomListUpdate(roomList);
        foreach (var room in roomList)
        {
            GameObject newRoom = Instantiate(roomNamePrafab, gridLayout.transform.position,Quaternion.identity);
            newRoom.GetComponentInChildren<Text>().text = room.Name+"("+room.PlayerCount+")";
            newRoom.transform.SetParent(gridLayout);
        }
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
