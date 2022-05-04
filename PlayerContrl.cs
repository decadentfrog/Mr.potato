using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContrl : MonoBehaviour
{
    // Start is called before the first frame update
    public float MoveForce = 100.0f;//移动力
    public float MaxSpeed = 5;//每秒移动五个单位
    public Rigidbody2D HeroBody;//获取Hero的2d刚体
    public bool bFaceRight = true;//判断角色是否面朝右方
    [HideInInspector]
    public bool bJump = false;
    public float JumpForce = 100;
    public Transform mGroundCheck;
    void Start()
    {
        HeroBody = GetComponent<Rigidbody2D>();//获取Hero的2d刚体
        mGroundCheck = transform.Find("GroundCheck");
    }

    // Update is called once per frame
    void Update()
    {//水平移动
        float h = Input.GetAxis("Horizontal");//获取水平输入方向
        if(Mathf.Abs(HeroBody.velocity.x)<MaxSpeed)
        {
            HeroBody.AddForce(Vector2.right * h * MoveForce);
        }

        if(Mathf.Abs(HeroBody.velocity.x) > MaxSpeed)
        {
            HeroBody.velocity = new Vector2(Mathf.Sign(HeroBody.velocity.x) * MaxSpeed, HeroBody.velocity.y);//Mathf.Sign返回速度的方向
        }
        //转身
        if(h>0 && !bFaceRight )
        {
            flip();
        }
        else if(h<0 && bFaceRight )
        {
            flip();
        }
         void flip()
        {
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
            bFaceRight = !bFaceRight;
        }
        //跳跃
        if (Physics2D.Linecast(transform.position, mGroundCheck.position, 1<<LayerMask.NameToLayer("Ground")))
        {
            if(Input.GetButtonDown("Jump"))
                {
                bJump = true;
            }
        }
    }
     void FixedUpdate()
    {
        if (bJump)
        {
            HeroBody.AddForce(Vector2.up * JumpForce);
            bJump = false;
        }
    }
     void Awake()
    {
        
    }
}
