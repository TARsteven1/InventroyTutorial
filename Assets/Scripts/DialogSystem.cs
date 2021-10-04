using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    [Header("UI组件")]
    public Text textLabel;
    public Image faceImage;

    [Header("文本文件")]
    public TextAsset textFile;
    public int index;
    [Header("头像")]
    public Sprite face01, face02;

    List<string> textList = new List<string>();
    bool textFinshed,cancelTyping;
    // Start is called before the first frame update
    void Awake()
    {
        GetTextFromFile(textFile);      
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)&&index==textList.Count)
        {
            gameObject.SetActive(false);
            index = 0;
            return;
        }
        //if (Input.GetKeyDown(KeyCode.Space)&&textFinshed)
        //{
        //    //textLabel.text = textList[index];
        //    //index++;
        //    StartCoroutine(SetTextUI());
        //}
        if (Input.GetKeyDown(KeyCode.Space) )
        {
            if (textFinshed&&!cancelTyping)
            {
                StartCoroutine(SetTextUI());
            }
            else if (!textFinshed)
            {
                cancelTyping = !cancelTyping;
            }
           
        }
    }
    private void OnEnable()
    {
        //textLabel.text = textList[index];
        //index++;
        StartCoroutine(SetTextUI());
    }
    void GetTextFromFile(TextAsset file)
    {
        textList.Clear();
        index = 0;
        var lineData = file.text.Split('\n');
        foreach (var line in lineData)
        {
            textList.Add(line);
        } 
    }
    IEnumerator  SetTextUI()
    {
        textFinshed = false;
        textLabel.text = "";
        switch (textList[index])
        {
            case "A\r":
                faceImage.sprite = face01;
                index++;
                break;
            case "B\r":
                faceImage.sprite = face02;
                index++;
                break;
            default:
                break;
        }
        //for (int i = 0; i < textList[index].Length; i++)
        //{
        //    textLabel.text += textList[index][i];
        //    yield return new WaitForSeconds(0.1f);
        //}
        int letter = 0;
        while (!cancelTyping&& letter< textList[index].Length-1)
        {
            textLabel.text += textList[index][letter];
            letter++;
            yield return new WaitForSeconds(0.1f);
        }
        textLabel.text = textList[index];
        cancelTyping = false;
       textFinshed = true;
        index++;
    }
}
