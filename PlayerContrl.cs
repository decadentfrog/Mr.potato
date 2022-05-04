using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContrl : MonoBehaviour
{
    // Start is called before the first frame update
    public float MoveForce = 100.0f;//�ƶ���
    public float MaxSpeed = 5;//ÿ���ƶ������λ
    public Rigidbody2D HeroBody;//��ȡHero��2d����
    public bool bFaceRight = true;//�жϽ�ɫ�Ƿ��泯�ҷ�
    [HideInInspector]
    public bool bJump = false;
    public float JumpForce = 100;
    public Transform mGroundCheck;
    void Start()
    {
        HeroBody = GetComponent<Rigidbody2D>();//��ȡHero��2d����
        mGroundCheck = transform.Find("GroundCheck");
    }

    // Update is called once per frame
    void Update()
    {//ˮƽ�ƶ�
        float h = Input.GetAxis("Horizontal");//��ȡˮƽ���뷽��
        if(Mathf.Abs(HeroBody.velocity.x)<MaxSpeed)
        {
            HeroBody.AddForce(Vector2.right * h * MoveForce);
        }

        if(Mathf.Abs(HeroBody.velocity.x) > MaxSpeed)
        {
            HeroBody.velocity = new Vector2(Mathf.Sign(HeroBody.velocity.x) * MaxSpeed, HeroBody.velocity.y);//Mathf.Sign�����ٶȵķ���
        }
        //ת��
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
        //��Ծ
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