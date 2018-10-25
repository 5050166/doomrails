using System;
using UnityEngine;


public class MissleBullet : MonoBehaviour
{
    public  GameObject[] ball; //存储获取的小球


    private Rigidbody2D rb;


  //  public float speed;


    public float rotateSpeed;//旋转速度



    private Vector3 target;


  //  public GameObject explosion;

    private void Awake()
    {
        this.rb = base.GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        this.ball = GameObject.FindGameObjectsWithTag("ball"); //获取所有小球
    }


    private void FixedUpdate()
    {
        this.ball = GameObject.FindGameObjectsWithTag("ball");
        if (GameObject.FindGameObjectsWithTag("ball").Length != 0)
        {
            this.target = this.ball[0].transform.position; //待定
        }
        else if (GameObject.FindGameObjectsWithTag("ball").Length == 0) //没有获取到敌人的时候就直接向前发射
        {
            this.target = base.transform.position + new Vector3(0f, 10f, 0f);
        }
        Vector2 v = this.target - this.transform.position;
        v.Normalize();//数值变小归一化
        float z = Vector3.Cross(v, base.transform.up).z;
        this.rb.angularVelocity = -z * this.rotateSpeed;
        this.rb.velocity = base.transform.up * this.transform.GetComponent<Bullet>().speed;
    }

    private void Update()
    {
        if (this.transform.localPosition.y>1000f||this.transform.localPosition.x<-560f||this.transform.localPosition.x>560f)
        {
            GameObjectPool.Instance.CloseGameObjectImmediately(this.gameObject);
        }
    }

    //  GameObjectPool.Instance.CloseGameObjectImmediately(this.gameObject);



    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "ball")
        { //{
        //    EffectSound.instance.PlayEffect("missleHit");   //播放导弹爆炸音效
        //    UnityEngine.Object.Instantiate<GameObject>(this.explosion, base.transform.position, Quaternion.identity);//生成导弹爆炸的特效
        }
    }


}
