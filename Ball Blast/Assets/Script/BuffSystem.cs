using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffSystem : MonoBehaviour
{  //在玩家点击开始游戏钱 把所有炮台的buff配置好

    public float ShootBuff;  //攻速buff
    public float DamageBuff;  //伤害buff

    public GameObject coin;
    public GameObject TextPrefab;
    public int weapnumber; //武器数量

    [HideInInspector]
    public Color speed;
    [HideInInspector]
    public Color damage;


    [SerializeField]
    private float num20;
    [SerializeField]
    private float num21;
    [SerializeField]
    private float num22;
    [SerializeField]
    private float num23;

    public List<string> AllBox;

    public int MetalFrame;  //金属框架

    public int AlloyFrame;  //合金框架

    public int Framenumber;

    private static BuffSystem instance;
    public static BuffSystem Instance
    {
        get
        {
            return instance;
        }

        set
        {
            instance = value;
        }
    }


    private void Awake()
    {

        instance = this;
    }

    private void Start()
    {

        speed = new Color(255f, 202f, 0);
        damage = new Color(0f, 266f, 255f);
   

    }



    public void restnumber()
    {
        num20 = 0;
        num21 = 0;
        num22 = 0;
        num23 = 0;
        weapnumber = 0;
        AlloyFrame = 0;
        MetalFrame = 0;

    }

    public void Setbuff()
    {
        foreach (var item in Game_Controller.Instance.bo)
        {
            //Debug.Log(item.gameObject.name);
            switch (item.gameObject.tag)
            {
                case "gun19":
                    break;
                case "gun20":
                    num20++;
                    break;
                case "gun21":
                    num21++;
                    break;
                case "gun22":
                    num22++;
                    break;
                case "gun23":
                    num23++;
                    break;

            }
        }

        for (int i = 0; i < DataManager.Instance.frame.Length; i++)
        {
            switch (DataManager.Instance.frame[i])
            {
                case "gun24":
                    AlloyFrame++;
                    break;
                case "gun25":
                    MetalFrame++;
                    break;
            }
        }
        SetAllBuff();
    }

    public void RefreshAllBox() //跟新武器攻击力和攻击速度
    {//获取格子 判断格子状态
        foreach (var item in Game_Controller.Instance.bo)
        {
            item.transform.GetChild(3).GetChild(0).gameObject.SetActive(true);
            switch (item.gameObject.tag)
            {
                case "gun1":
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().ShootSpeed = 0.5f - 0.5f * ShootBuff;
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().bullet = Prefabmanager.Instance.bullets[0];
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().bullet.GetComponent<Bullet>().speed = 2f;//设定子弹的速度
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().bullet.GetComponent<Bullet>().damage = 5f + 5f * DamageBuff;
                    weapnumber++;
                    break;
                case "gun2":

                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().ShootSpeed = 0.4f - 0.4f * ShootBuff;
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().bullet = Prefabmanager.Instance.bullets[1];
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().bullet.GetComponent<Bullet>().speed = 0.8f;//设定子弹的速度
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().bullet.GetComponent<Bullet>().damage = 8f + 8f * DamageBuff;
                    weapnumber++;
                    break;
                case "gun3":

                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().ShootSpeed = 0.45f - 0.45f * ShootBuff;
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().bullet = Prefabmanager.Instance.bullets[2];
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().bullet.GetComponent<Bullet>().speed = 2f;//设定子弹的速度
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().bullet.GetComponent<Bullet>().damage = 11f + 11f * DamageBuff;
                    weapnumber++;
                    break;
                case "gun4":

                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().ShootSpeed = 0.5f - 0.5f * ShootBuff;
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().bullet = Prefabmanager.Instance.bullets[3];
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().bullet.GetComponent<Bullet>().speed = 0.7f;//设定子弹的速度
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().bullet.GetComponent<Bullet>().damage = 20f + 20f * DamageBuff;
                    weapnumber++;
                    break;
                case "gun5":
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().ShootSpeed = 0.5f - 0.5f * ShootBuff;
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().bullet = Prefabmanager.Instance.bullets[4];
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().bullet.GetComponent<Bullet>().speed = 0.8f;//设定子弹的速度
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().bullet.GetComponent<Bullet>().damage = 20f + 20f * DamageBuff;
                    weapnumber++;
                    break;
                case "gun6":
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().ShootSpeed = 0.5f - 0.5f * ShootBuff;
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().bullet = Prefabmanager.Instance.bullets[5];
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().bullet.GetComponent<Bullet>().speed = 0.9f;//设定子弹的速度
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().bullet.GetComponent<Bullet>().damage = 30f + 30f * DamageBuff;
                    weapnumber++;
                    break;
                case "gun7":
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().ShootSpeed = 1f - 1f * ShootBuff;
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().bullet = Prefabmanager.Instance.bullets[6];
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().bullet.GetComponent<Bullet>().speed = 0.5f;//设定子弹的速度
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().bullet.GetComponent<Bullet>().damage = 80f + 80f * DamageBuff;
                    weapnumber++;
                    break;
                case "gun8":
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().ShootSpeed = 1.5f - 1.5f * ShootBuff;
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().bullet = Prefabmanager.Instance.bullets[7];
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().bullet.GetComponent<Bullet>().speed = 0f;//设定子弹的速度
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().bullet.GetComponent<Bullet>().damage = 25f + 25f * DamageBuff;
                    weapnumber++;
                    break;
                case "gun9":
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().ShootSpeed = 0.5f - 0.5f * ShootBuff;
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().bullet = Prefabmanager.Instance.bullets[8];
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().bullet.GetComponent<Bullet>().speed = 1f;//设定子弹的速度
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().bullet.GetComponent<Bullet>().damage = 35f + 35f * DamageBuff;
                    weapnumber++;
                    break;
                case "gun10":
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().ShootSpeed = 0.35f - 0.35f * ShootBuff;
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().bullet = Prefabmanager.Instance.bullets[9];
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().bullet.GetComponent<Bullet>().speed = 0.7f;//设定子弹的速度
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().bullet.GetComponent<Bullet>().damage = 60f + 60f * DamageBuff;
                    weapnumber++;
                    break;
                case "gun11":
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().ShootSpeed = 0.2f - 0.2f * ShootBuff;
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().bullet = Prefabmanager.Instance.bullets[10];
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().bullet.GetComponent<Bullet>().speed = 2f;//设定子弹的速度
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().bullet.GetComponent<Bullet>().damage = 25f + 25f * DamageBuff;
                    weapnumber++;
                    break;
                case "gun12":
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().ShootSpeed = 0.7f - 0.7f * ShootBuff;
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().bullet = Prefabmanager.Instance.bullets[11];
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().bullet.GetComponent<Bullet>().speed = 1.1f;//设定子弹的速度
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().bullet.GetComponent<Bullet>().damage =75f +75f * DamageBuff;
                    weapnumber++;
                    break;
                case "gun13":
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().ShootSpeed = 0.7f - 0.7f * ShootBuff;
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().bullet = Prefabmanager.Instance.bullets[12];
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().bullet.GetComponent<Bullet>().speed = 0.7f;//设定子弹的速度
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().bullet.GetComponent<Bullet>().damage =50f + 50f * DamageBuff;
                    weapnumber++;
                    break;
                case "gun14":
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().ShootSpeed = 0.1f - 0.1f * ShootBuff;
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().bullet = Prefabmanager.Instance.bullets[13];
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().bullet.GetComponent<Bullet>().speed = 1f;//设定子弹的速度
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().bullet.GetComponent<Bullet>().damage = 20f + 20f * DamageBuff;
                    weapnumber++;
                    break;
                case "gun15":
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().ShootSpeed = 1f - 1f * ShootBuff;
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().bullet = Prefabmanager.Instance.bullets[14];
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().bullet.GetComponent<Bullet>().speed = 0.5f;//设定子弹的速度
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().bullet.GetComponent<Bullet>().damage = 70f + 70f * DamageBuff;
                    weapnumber++;
                    break;
                case "gun16":
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().ShootSpeed =0.5f - 0.5f * ShootBuff;
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().bullet = Prefabmanager.Instance.bullets[15];
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().bullet.GetComponent<Bullet>().speed = 0.7f;//设定子弹的速度
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().bullet.GetComponent<Bullet>().damage = 130f +130f * DamageBuff;
                    weapnumber++;
                    break;
                case "gun17":
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().ShootSpeed = 0.4f - 0.4f * ShootBuff;
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().bullet = Prefabmanager.Instance.bullets[16];
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().bullet.GetComponent<Bullet>().speed = 0.6f;//设定子弹的速度
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().bullet.GetComponent<Bullet>().damage = 100f +100f * DamageBuff;
                    weapnumber++;
                    break;
                case "gun18":
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().ShootSpeed = 0.8f - 0.8f * ShootBuff;
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().bullet = Prefabmanager.Instance.bullets[17];
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().bullet.GetComponent<Bullet>().speed =0.9f;//设定子弹的速度
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().bullet.GetComponent<Bullet>().damage =170f + 170f * DamageBuff;
                    weapnumber++;
                    break;

                case "gun20":
                    var color = item.transform.GetChild(2).GetComponent<ParticleSystem>().main;
                    color.startColor = new ParticleSystem.MinMaxGradient(speed);

                    item.transform.GetChild(2).GetComponent<ParticleSystem>().Play();

                    item.transform.GetChild(3).GetChild(0).gameObject.SetActive(false);
                    break;
                case "gun21":

                    var color1 = item.transform.GetChild(2).GetComponent<ParticleSystem>().main;
                    color1.startColor = new ParticleSystem.MinMaxGradient(speed);
                    item.transform.GetChild(2).GetComponent<ParticleSystem>().Play();

                    item.transform.GetChild(3).GetChild(0).gameObject.SetActive(false);
                    break;
                case "gun22":

                    var color2 = item.transform.GetChild(2).GetComponent<ParticleSystem>().main;
                    color2.startColor = new ParticleSystem.MinMaxGradient(damage);
                    item.transform.GetChild(2).GetComponent<ParticleSystem>().Play();
                    item.transform.GetChild(3).GetChild(0).gameObject.SetActive(false);
                    break;
                case "gun23":

                    var color3 = item.transform.GetChild(2).GetComponent<ParticleSystem>().main;
                    color3.startColor = new ParticleSystem.MinMaxGradient(damage);
                    item.transform.GetChild(2).GetComponent<ParticleSystem>().Play();
                    item.transform.GetChild(3).GetChild(0).gameObject.SetActive(false);
                    break;

                case "gun24":
                    item.transform.GetChild(3).GetChild(0).gameObject.SetActive(false);
                    break;
                case "gun25":
                    item.transform.GetChild(3).GetChild(0).gameObject.SetActive(false);
                    break;

                case "gun26":
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().ShootSpeed =0.5f -0.5f * ShootBuff;
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().bullet = Prefabmanager.Instance.bullets[18];
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().bullet.GetComponent<Bullet>().speed = 1f;//设定子弹的速度
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().bullet.GetComponent<Bullet>().damage = 200f + 200f * DamageBuff;
                    weapnumber++;
                    break;
                case "gun27":
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().ShootSpeed =0.5f - 0.5f * ShootBuff;
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().bullet = Prefabmanager.Instance.bullets[19];
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().bullet.GetComponent<Bullet>().speed = 0.5f;//设定子弹的速度
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().bullet.GetComponent<Bullet>().damage =250f + 250f * DamageBuff;
                    weapnumber++;
                    break;
                case "gun28":
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().ShootSpeed = 0.3f -0.3f * ShootBuff;
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().bullet = Prefabmanager.Instance.bullets[20];
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().bullet.GetComponent<Bullet>().speed = 1f;//设定子弹的速度
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().bullet.GetComponent<Bullet>().damage =200f + 200f * DamageBuff;
                    weapnumber++;
                    break;
                case "gun29":
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().ShootSpeed = 0.6f - 0.6f * ShootBuff;
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().bullet = Prefabmanager.Instance.bullets[21];
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().bullet.GetComponent<Bullet>().speed = 1f;//设定子弹的速度
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().bullet.GetComponent<Bullet>().damage =130f +130f * DamageBuff;
                    weapnumber++;
                    break;
                case "gun30":
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().ShootSpeed =0.6f -0.6f * ShootBuff;
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().bullet = Prefabmanager.Instance.bullets[22];
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().bullet.GetComponent<Bullet>().speed = 0.3f;//设定子弹的速度
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().bullet.GetComponent<Bullet>().damage = 300f + 300f * DamageBuff;
                    weapnumber++;
                    break;
                case "gun31":
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().ShootSpeed = 0.5f -0.5f * ShootBuff;
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().bullet = Prefabmanager.Instance.bullets[23];
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().bullet.GetComponent<Bullet>().speed = 0.5f;//设定子弹的速度
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().bullet.GetComponent<Bullet>().damage = 150f + 150f * DamageBuff;
                    weapnumber++;
                    break;
                case "gun32":
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().ShootSpeed =0.5f - 0.5f * ShootBuff;
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().bullet = Prefabmanager.Instance.bullets[24];
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().bullet.GetComponent<Bullet>().speed = 0.5f;//设定子弹的速度
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().bullet.GetComponent<Bullet>().damage =350f + 350f * DamageBuff;
                    weapnumber++;
                    break;
                case "gun33":
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().ShootSpeed =0.2f -0.2f * ShootBuff;
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().bullet = Prefabmanager.Instance.bullets[25];
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().bullet.GetComponent<Bullet>().speed = 3f;//设定子弹的速度
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().bullet.GetComponent<Bullet>().damage = 200f + 200f * DamageBuff;
                    weapnumber++;
                    break;
                case "gun34":
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().ShootSpeed = 0.3f - 0.3f * ShootBuff;
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().bullet = Prefabmanager.Instance.bullets[26];
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().bullet.GetComponent<Bullet>().speed = 0.8f;//设定子弹的速度
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().bullet.GetComponent<Bullet>().damage = 190f + 190f * DamageBuff;
                    weapnumber++;
                    break;
                case "gun35":
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().ShootSpeed =1f - 1f * ShootBuff;
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().bullet = Prefabmanager.Instance.bullets[27];
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().bullet.GetComponent<Bullet>().speed = 0.5f;//设定子弹的速度
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().bullet.GetComponent<Bullet>().damage =200f +200f * DamageBuff;
                    weapnumber++;
                    break;
                case "gun36":
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().ShootSpeed = 0.35f - 0.35f * ShootBuff;
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().bullet = Prefabmanager.Instance.bullets[28];
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().bullet.GetComponent<Bullet>().speed = 2f;//设定子弹的速度
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().bullet.GetComponent<Bullet>().damage = 470f +470f * DamageBuff;
                    weapnumber++;
                    break;
                case "gun37":
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().ShootSpeed =1.5f -1.5f * ShootBuff;
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().bullet = Prefabmanager.Instance.bullets[29];
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().bullet.GetComponent<Bullet>().speed = 0f;//设定子弹的速度
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().bullet.GetComponent<Bullet>().damage = 200f +200f * DamageBuff;
                    weapnumber++;
                    break;
                case "gun38":
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().ShootSpeed = 0.1f -0.1f * ShootBuff;
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().bullet = Prefabmanager.Instance.bullets[30];
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().bullet.GetComponent<Bullet>().speed = 0.3f;//设定子弹的速度
                    item.transform.GetChild(3).GetChild(0).GetComponent<Gun>().bullet.GetComponent<Bullet>().damage =500f + 500f * DamageBuff;
                    weapnumber++;
                    break;
                default:
                    item.transform.GetChild(3).GetChild(0).gameObject.SetActive(false);
                    break;
            }     
        }
    }
    
    public void SetAllBuff()
    {   
        ShootBuff = num20 * 0.15f + num21 * 0.25f;   //几个0.2 和几个0.35   减小开火间隔  速度加成
        if (ShootBuff >= 1)
        {
            ShootBuff = 0.9f;
        }
        DamageBuff = num22 * 0.2f + num23 * 0.35f;

        Framenumber = MetalFrame * 2 + 3 * AlloyFrame;

    }



}
