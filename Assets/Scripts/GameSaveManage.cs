using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameSaveManage : MonoBehaviour {
	public Inventroy myInventroy;


	public void SaveGame()
    {
		Debug.Log(Application.persistentDataPath);
        if (!Directory.Exists(Application.persistentDataPath+"/gamedata"))
        {
			Directory.CreateDirectory(Application.persistentDataPath + "/gamedata");
        }
        BinaryFormatter formatter = new BinaryFormatter();//二进制转换

        FileStream file = File.Create(Application.persistentDataPath + "/gamedata/Inventroy.txt");
        var Json = JsonUtility.ToJson(myInventroy);
        formatter.Serialize(file, Json);//将Json写入文件

        file.Close();//写入完毕保存关闭编辑

    }

	public void LoadGame()
    {
        BinaryFormatter bf = new BinaryFormatter();//二进制转换
        if (File.Exists(Application.persistentDataPath + "/gamedata/Inventroy.txt"))
        {
           
            FileStream file = File.Open(Application.persistentDataPath + "/gamedata/Inventroy.txt",FileMode.Open);
            JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), myInventroy);//反序列化
            file.Close();
        }
    }
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

}
