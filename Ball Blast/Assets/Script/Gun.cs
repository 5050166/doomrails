using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    public GameObject bullet;  //子弹

    public float ShootSpeed; //开火间隔  值越小 开火速度越快   

    private bool isShooting; //是否可以开火

    public Transform target;   //子弹发射地点

    public float fireTime;    //开火时间

    public Transform canva;

    public Transform Bullet;

    private GameObject go;

    private void Awake()
    {
        canva = GameObject.Find("Canvas").transform;
        Bullet = GameObject.Find("BulletBox").transform;
        target = this.transform;
    }

    void Update()
    {
        if (fireTime < ShootSpeed)
        {
            fireTime += Time.deltaTime;   //当fireTime小于coolTime 的时候  firetime开始增加到fireTime

        }
        if (fireTime > ShootSpeed)
        {
            fireTime = ShootSpeed;

        }


        if (!Game_Controller.isPaused && !Game_Controller.isEnd)
        {
            if (Input.GetMouseButton(0)&& fireTime == ShootSpeed)    //只有当按下F键 且  fireTime和coolTime 相同的时候才开火    如果你的开火是自动的，把按下F键去掉就行了
            {
                fireTime = 0;

                Shoot();
            }
        }
    }
    public void Shoot()
    {

        Vector3 targetpos = this.transform.position - this.transform.parent.position; //得到子弹发射的方向

        if (this.transform.parent.parent.tag == "gun8"|| this.transform.parent.parent.tag == "gun37")
        {
            go = Instantiate(bullet, target.localPosition, Quaternion.identity);
            go.SetActive(true);
        }
        else
        {

            go = GameObjectPool.Instance.CreateObject(bullet, target.localPosition, Quaternion.identity);
        }

        go.transform.SetParent(target.transform);                                  // loclposition相对于父物体的坐标   

        go.transform.localPosition = target.localPosition;                        //记录下坐标 拿出来

        go.transform.SetParent(Bullet);

   
        go.transform.localPosition = new Vector3(go.transform.localPosition.x, go.transform.localPosition.y /*- 26f*/, go.transform.localPosition.z);
        if (this.transform.parent.parent.tag == "gun8"|| this.transform.parent.parent.tag == "gun37")
        {
            go.transform.localPosition = new Vector3(go.transform.localPosition.x, go.transform.localPosition.y- 26f, go.transform.localPosition.z);
            go.transform.localScale = new Vector3(0f, 22.6f, 1f);
            go.transform.SetParent(this.transform.parent.parent.parent);
            //go.transform.SetParent(Bullet);
        }
        else if (this.transform.parent.parent.tag == "gun35") //yueya
        {
            go.transform.localScale = new Vector3(2f, 2f, 2f);
        }
        else
        {
            go.transform.localScale = new Vector3(1f, 1f, 1f);
        }
        go.transform.GetComponent<Bullet>().Towards = targetpos;
    }
}

