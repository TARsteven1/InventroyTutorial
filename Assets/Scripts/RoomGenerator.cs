using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomGenerator : MonoBehaviour
{
    public enum Direction { up,down,left,right };
    public Direction direction;

    [Header("房间信息")]
    public GameObject roomPrefab;
    public int roomNumber;
    public Color startColor, endColor;
    private GameObject endRoom;

    [Header("位置控制")]
    public Transform generatorPoint;
    public float xOffset;
    public float yOffset;
    public LayerMask roomLayer;
    public int maxStep;

    public List<Room> rooms = new List<Room>();

    List<GameObject> farRooms = new List<GameObject>();
    List<GameObject> lessFarRooms = new List<GameObject>();
    List<GameObject> oneWayRooms = new List<GameObject>();

    public WallType wallType;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < roomNumber; i++)
        {
            rooms.Add(Instantiate(roomPrefab, generatorPoint.position, Quaternion.identity).GetComponent<Room>());

            //改变point位置
            ChangePointPos();
        }
        //Debug.Log(rooms.Count);
        rooms[0].GetComponent<SpriteRenderer>().color = startColor;
        //rooms[roomNumber-1].GetComponent<SpriteRenderer>().color = endColor;
        endRoom = rooms[0].gameObject;
        foreach (var room in rooms)
        {
            //if (room.transform.position.sqrMagnitude>endRoom.transform.position.sqrMagnitude)
            //{
            //    endRoom = room.gameObject;
            //}
            SetipRoom(room, room.transform.position);
        }
        FindEndRoom();
        endRoom.GetComponent<SpriteRenderer>().color = endColor;

    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.anyKeyDown)
        //{
        //    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //}
    }
    void ChangePointPos()
    {
        do
        {
            direction = (Direction)Random.Range(0, 4);

            switch (direction)
            {
                case Direction.up:
                    generatorPoint.position += new Vector3(0, yOffset, 0);
                    break;
                case Direction.down:
                    generatorPoint.position += new Vector3(0, -yOffset, 0);
                    break;
                case Direction.left:
                    generatorPoint.position += new Vector3(xOffset, 0, 0);
                    break;
                case Direction.right:
                    generatorPoint.position += new Vector3(-xOffset, 0, 0);
                    break;
                default:
                    break;
            }
        } while (Physics2D.OverlapCircle(generatorPoint.position,0.2f,roomLayer));
       
    }
    public void SetipRoom(Room newRoom ,Vector3 roomPosition)
    {
        newRoom.roomUp = Physics2D.OverlapCircle(roomPosition + new Vector3(0, yOffset, 0), 0.2f, roomLayer);
        newRoom.roomDown = Physics2D.OverlapCircle(roomPosition + new Vector3(0, -yOffset, 0), 0.2f, roomLayer);
        newRoom.roomLeft = Physics2D.OverlapCircle(roomPosition + new Vector3(-xOffset, 0, 0), 0.2f, roomLayer);
        newRoom.roomRight = Physics2D.OverlapCircle(roomPosition + new Vector3(xOffset, 0, 0), 0.2f, roomLayer);
        newRoom.UpdateRoom(xOffset,yOffset);

        switch (newRoom.doorNumber)
        {
            case 1:
                if (newRoom.roomUp)
                    Instantiate(wallType.singleUp, roomPosition, Quaternion.identity);
                if (newRoom.roomDown)
                    Instantiate(wallType.singleDown, roomPosition, Quaternion.identity);
                if (newRoom.roomLeft)
                    Instantiate(wallType.singleLeft, roomPosition, Quaternion.identity);
                if (newRoom.roomRight)
                    Instantiate(wallType.singleRight, roomPosition, Quaternion.identity);
                break;
            case 2:
                if (newRoom.roomLeft && newRoom.roomUp)
                    Instantiate(wallType.doubleLU, roomPosition, Quaternion.identity);
                if (newRoom.roomLeft && newRoom.roomRight)
                    Instantiate(wallType.doubleLR, roomPosition, Quaternion.identity);
                if (newRoom.roomLeft && newRoom.roomDown)
                    Instantiate(wallType.doubleLD, roomPosition, Quaternion.identity);
                if (newRoom.roomUp && newRoom.roomRight)
                    Instantiate(wallType.doubleUR, roomPosition, Quaternion.identity);
                if (newRoom.roomUp && newRoom.roomDown)
                    Instantiate(wallType.doubleUD, roomPosition, Quaternion.identity);
                if (newRoom.roomRight && newRoom.roomDown)
                    Instantiate(wallType.doubleRD, roomPosition, Quaternion.identity);
                break;
            case 3:
                if (newRoom.roomLeft && newRoom.roomUp && newRoom.roomRight)
                    Instantiate(wallType.tripleLUR, roomPosition, Quaternion.identity);
                if (newRoom.roomLeft && newRoom.roomRight && newRoom.roomDown)
                    Instantiate(wallType.tripleLRD, roomPosition, Quaternion.identity);
                if (newRoom.roomDown && newRoom.roomUp && newRoom.roomRight)
                    Instantiate(wallType.tripleURD, roomPosition, Quaternion.identity);
                if (newRoom.roomLeft && newRoom.roomUp && newRoom.roomDown)
                    Instantiate(wallType.tripleLUD, roomPosition, Quaternion.identity);
                break;
            case 4:
                if (newRoom.roomLeft && newRoom.roomUp && newRoom.roomRight && newRoom.roomDown)
                    Instantiate(wallType.fourDoors, roomPosition, Quaternion.identity);
                break;
            //default:
            //    break;
        }
    }
    public void FindEndRoom()
    {
        for (int i = 0; i < rooms.Count; i++)
        {
            if (rooms[i].stepToStart>maxStep)
            {
                maxStep = rooms[i].stepToStart;
            }
        }
        foreach (var room in rooms)
        {
            if (room.stepToStart==maxStep)
            {
                farRooms.Add(room.gameObject);
            }
            if (room.stepToStart==maxStep-1)
            {
                lessFarRooms.Add(room.gameObject);
            }
        }
        for (int i = 0; i < farRooms.Count; i++)
        {
            if (farRooms[i].GetComponent<Room>().doorNumber==1)
            {
                oneWayRooms.Add(farRooms[i]);
            }
        }
        for (int i = 0; i < lessFarRooms.Count; i++)
        {
            if (lessFarRooms[i].GetComponent<Room>().doorNumber == 1)
            {
                oneWayRooms.Add(lessFarRooms[i]);
            }
        }
        if (oneWayRooms.Count!=0)
        {
            endRoom = oneWayRooms[Random.Range(0, oneWayRooms.Count)];
        }
        else
        {
            endRoom = farRooms[Random.Range(0, farRooms.Count)];
        }
    }
}
[System.Serializable]
public class WallType
{
    public GameObject singleLeft, singleRight, singleUp, singleDown,
        doubleLU, doubleLR, doubleLD, doubleUR, doubleUD, doubleRD,
        tripleLUR, tripleLUD, tripleURD, tripleLRD,
        fourDoors;
}
