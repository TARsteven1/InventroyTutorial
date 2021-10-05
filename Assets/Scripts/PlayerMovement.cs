using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;

public class PlayerMovement : MonoBehaviourPun
{
    Rigidbody2D rb;
    Collider2D coll;
    Animator anim;
    public GameObject bag;
    public bool isOpen = false;
    public float speed;
    Vector2 movement;

    public TextMeshProUGUI PlayerName;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        //PlayerName = transform.GetChild(1).GetChild(0).GetComponent<TextMeshPro>();

        if (photonView.IsMine)
        {
            PlayerName.text = PhotonNetwork.NickName;
        }
        else
        {
            PlayerName.text = photonView.Owner.NickName;
        }

    }

    private void Update()
    {
        if (!photonView.IsMine && PhotonNetwork.IsConnected)
            return;
        
            Movement();
            SwitchAnim();
            OnOpenBag();             
    }

    void Movement()//移动
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        rb.MovePosition(rb.position + movement * speed * Time.deltaTime);

    }

    void SwitchAnim()//切换动画
    {
        if (movement != Vector2.zero)//保证Horizontal归0时，保留movment的值来切换idle动画的blend tree
        {
            anim.SetFloat("horizontal", movement.x);
            anim.SetFloat("vertical", movement.y);
        }
        anim.SetFloat("speed", movement.magnitude);//magnitude 也可以用 sqrMagnitude 具体可以参考Api 默认返回值永远>=0
    }
    void OnOpenBag( )
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            bag.SetActive(!bag.activeSelf);
            InventroyManage.RefreshItem();
           
        }
        

    }
}