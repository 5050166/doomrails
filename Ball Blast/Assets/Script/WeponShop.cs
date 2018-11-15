using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeponShop : MonoBehaviour
{
    public Transform Canvas;

    public GameObject ParentObject;//武器存放在这里,会自动显示 

    public GameObject WeaponPrefab;  //武器预制体

    public GameObject SelectedObject;

    public GameObject DataMenu; //gun信息框

    public GameObject mask;    //遮蔽

    public Image Bullet;      //子弹图标

    public Image weapon;     //武器图标

    public Text weapontext; //武器文本

    public int weaponnumber; //武器編號 對應子彈編號

    public Text WeaponPrice; //武器价格

    public Button BuyButton; //购买的按钮

    public float tweentime;

    public AudioSource buybutton;

    private static WeponShop instance;

    public static WeponShop Instance
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
    void Start()
    {

        LoadAllWeapon();
        Buybutton();  //注册按钮事件
        addclicklisten();

    }

    void OnTweenComplete(string name)
    {
        switch (name)
        {//name这个是方法名称 需要执行的
            case "Openwindow":
                DataMenu.gameObject.SetActive(true);
                StopCoroutine("Inactive");
                break;
            case "Closewindow":

                DataMenu.gameObject.SetActive(false);
                StopCoroutine("Inactive");
                break;
        }
    }
    IEnumerator Inactive(string name, float waittime)
    {
        yield return new WaitForSeconds(waittime); //等待这么多秒以后开始执行动画
        OnTweenComplete(name);
    }

    public void addclicklisten()
    {
        DataMenu.GetComponent<Button>().onClick.AddListener(delegate ()
        {
            //Debug.Log(1);
            if (DataMenu.transform.localScale.y == 1)
            {
                DataMenu.transform.DOScale(0f, tweentime);

                mask.transform.DOScale(0f, 0f);
                StartCoroutine(Inactive("Closewindow", tweentime));
            }
        });

    }

    public void LoadAllWeapon()  //载入所有的武器
    {
        for (int i = 0; i < Prefabmanager.Instance.sprites.Length; i++)//炮的数量
        {
            GameObject go = Instantiate(WeaponPrefab, this.transform.position, Quaternion.identity);
            go.gameObject.name = "gun" + (i + 1);
            go.AddComponent<BoXPrice>(); //记录价格
            switch (go.gameObject.name)
            {
                case "gun1":  //普通炮
                    go.GetComponent<BoXPrice>().Price = 50;
                    go.GetComponent<BoXPrice>().damage = 10;
                    go.GetComponent<BoXPrice>().speed = 0.5f;
                    go.GetComponent<BoXPrice>().bulletspeed = 2;
                    go.GetComponent<BoXPrice>().property = "NONE";
                    break;
                case "gun2": //绿炮
                    go.GetComponent<BoXPrice>().Price =100;
                    go.GetComponent<BoXPrice>().damage =13;
                    go.GetComponent<BoXPrice>().speed = 0.5f;
                    go.GetComponent<BoXPrice>().bulletspeed = 0.8f;
                    go.GetComponent<BoXPrice>().property = "NONE";
                    break;
                case "gun3": //星星炮
                    go.GetComponent<BoXPrice>().Price = 200;
                    go.GetComponent<BoXPrice>().damage =16;
                    go.GetComponent<BoXPrice>().speed = 0.45f;
                    go.GetComponent<BoXPrice>().bulletspeed = 2;
                    go.GetComponent<BoXPrice>().property = "DOUBLE BULLET";
                    break;
                case "gun4": //旋转炮
                    go.GetComponent<BoXPrice>().Price =350;
                    go.GetComponent<BoXPrice>().damage = 30;
                    go.GetComponent<BoXPrice>().speed = 0.5f;
                    go.GetComponent<BoXPrice>().bulletspeed = 0.7f;
                    go.GetComponent<BoXPrice>().property = "45°and-45°ROTATION";
                    break;
                case "gun5":  //西红柿炮
                    go.GetComponent<BoXPrice>().Price =550;
                    go.GetComponent<BoXPrice>().damage = 35;
                    go.GetComponent<BoXPrice>().speed = 0.5f;
                    go.GetComponent<BoXPrice>().bulletspeed = 0.8f;
                    go.GetComponent<BoXPrice>().property = "SELF ROTATION";
                    break;
                case "gun6":  //方块炮
                    go.GetComponent<BoXPrice>().Price =660;
                    go.GetComponent<BoXPrice>().damage = 40;
                    go.GetComponent<BoXPrice>().speed = 0.5f;
                    go.GetComponent<BoXPrice>().bulletspeed = 0.9f;
                    go.GetComponent<BoXPrice>().property = "NONE";
                    break;
                case "gun7": //西瓜炮
                    go.GetComponent<BoXPrice>().Price =900;
                    go.GetComponent<BoXPrice>().damage = 80;
                    go.GetComponent<BoXPrice>().speed =1f;
                    go.GetComponent<BoXPrice>().bulletspeed = 0.5f;
                    go.GetComponent<BoXPrice>().property = "NONE";
                    break;
                case "gun8": //激光炮
                    go.GetComponent<BoXPrice>().Price = 1100;
                    go.GetComponent<BoXPrice>().damage = 25;
                    go.GetComponent<BoXPrice>().speed = 1.5f;
                    go.GetComponent<BoXPrice>().bulletspeed = 999;
                    go.GetComponent<BoXPrice>().property = "PENETRATE";
                    break;
                case "gun9": //蝙蝠炮
                    go.GetComponent<BoXPrice>().Price = 1499;
                    go.GetComponent<BoXPrice>().damage = 35;
                    go.GetComponent<BoXPrice>().speed = 0.5f;
                    go.GetComponent<BoXPrice>().bulletspeed = 1f;
                    go.GetComponent<BoXPrice>().property = "TRACKING";
                    break;
                case "gun10": //冰淇凌
                    go.GetComponent<BoXPrice>().Price =1700;
                    go.GetComponent<BoXPrice>().damage =60;
                    go.GetComponent<BoXPrice>().speed = 0.35f;
                    go.GetComponent<BoXPrice>().bulletspeed = 0.7f;
                    go.GetComponent<BoXPrice>().property = "NONE";
                    break;
                case "gun11": //弓箭
                    go.GetComponent<BoXPrice>().Price = 2000;
                    go.GetComponent<BoXPrice>().damage = 20;
                    go.GetComponent<BoXPrice>().speed = 0.2f;
                    go.GetComponent<BoXPrice>().bulletspeed = 2;
                    go.GetComponent<BoXPrice>().property = "NONE";
                    break;
                case "gun12": //黑袍
                    go.GetComponent<BoXPrice>().Price = 2600;
                    go.GetComponent<BoXPrice>().damage =40;
                    go.GetComponent<BoXPrice>().speed = 0.7f;
                    go.GetComponent<BoXPrice>().bulletspeed = 1.1f;
                    go.GetComponent<BoXPrice>().property = "NONE";
                    break;
                case "gun13": //小火箭
                    go.GetComponent<BoXPrice>().Price =3100;
                    go.GetComponent<BoXPrice>().damage = 50;
                    go.GetComponent<BoXPrice>().speed = 0.7f;
                    go.GetComponent<BoXPrice>().bulletspeed = 0.7f;
                    go.GetComponent<BoXPrice>().property = "NONE";
                    break;
                case "gun14": //闪电
                    go.GetComponent<BoXPrice>().Price = 4000;
                    go.GetComponent<BoXPrice>().damage = 15;
                    go.GetComponent<BoXPrice>().speed = 0.1f;
                    go.GetComponent<BoXPrice>().bulletspeed = 1;
                    go.GetComponent<BoXPrice>().property = "NONE";
                    break;
                case "gun15": //喷火器
                    go.GetComponent<BoXPrice>().Price = 4800;
                    go.GetComponent<BoXPrice>().damage =65;
                    go.GetComponent<BoXPrice>().speed = 1f;
                    go.GetComponent<BoXPrice>().bulletspeed = 0.5f;
                    go.GetComponent<BoXPrice>().property = "PENETRATE";

                    break;
                case "gun16":  //齿轮
                    go.GetComponent<BoXPrice>().Price =5200;
                    go.GetComponent<BoXPrice>().damage =130;
                    go.GetComponent<BoXPrice>().speed = 0.5f;
                    go.GetComponent<BoXPrice>().bulletspeed = 0.7f;
                    go.GetComponent<BoXPrice>().property = "45°and - 45°ROTATION ";
                    break;
                case "gun17": //音波
                    go.GetComponent<BoXPrice>().Price =6100;
                    go.GetComponent<BoXPrice>().damage =65;
                    go.GetComponent<BoXPrice>().speed = 0.4f;
                    go.GetComponent<BoXPrice>().bulletspeed = 0.6f;
                    go.GetComponent<BoXPrice>().property = "PENETRATE";
                    break;
                case "gun18": //水滴
                    go.GetComponent<BoXPrice>().Price =7600;
                    go.GetComponent<BoXPrice>().damage =170;
                    go.GetComponent<BoXPrice>().speed = 0.8f;
                    go.GetComponent<BoXPrice>().bulletspeed = 0.9f;
                    go.GetComponent<BoXPrice>().property = "SLOW SHOOT SPEED HIGH DAMAGE";
                    break;
                case "gun19":
                    go.GetComponent<BoXPrice>().Price =100;
                    go.GetComponent<BoXPrice>().property = "1% missing all attack";
                    break;
                case "gun20":
                    go.GetComponent<BoXPrice>().Price =3000;
                    go.GetComponent<BoXPrice>().property = "ALL WEAPON ATTACK SPEED +10%";
                    break;
                case "gun21":
                    go.GetComponent<BoXPrice>().Price = 8000;
                    go.GetComponent<BoXPrice>().property = "ALL WEAPON ATTACK SPEED +20%";
                    break;
                case "gun22":
                    go.GetComponent<BoXPrice>().Price = 3000;
                    go.GetComponent<BoXPrice>().property = "ALL WEAPON DAMAGE +10%";
                    break;
                case "gun23":
                    go.GetComponent<BoXPrice>().Price = 8000;
                    go.GetComponent<BoXPrice>().property = "ALL WEAPON DAMAGE +20%";
                    break;
                case "gun24":
                    go.GetComponent<BoXPrice>().Price = 1000;
                    go.GetComponent<BoXPrice>().property = "2% missing all attack";
                    break;
                case "gun25":
                    go.GetComponent<BoXPrice>().Price =2000;
                    go.GetComponent<BoXPrice>().property = "3% missing all attack";
                    break;
                case "gun26": //奶酪
                    go.GetComponent<BoXPrice>().Price = 8400;
                    go.GetComponent<BoXPrice>().damage = 130;
                    go.GetComponent<BoXPrice>().speed = 0.5f;
                    go.GetComponent<BoXPrice>().bulletspeed = 1;
                    go.GetComponent<BoXPrice>().property = "NONE";
                    break;
                case "gun27": //土炮
                    go.GetComponent<BoXPrice>().Price =10000;
                    go.GetComponent<BoXPrice>().damage =200;
                    go.GetComponent<BoXPrice>().speed = 0.5f;
                    go.GetComponent<BoXPrice>().bulletspeed = 0.5f;
                    go.GetComponent<BoXPrice>().property = "NONE";
                    break;
                case "gun28": //针筒
                    go.GetComponent<BoXPrice>().Price =12000;
                    go.GetComponent<BoXPrice>().damage =100;
                    go.GetComponent<BoXPrice>().speed = 0.3f;
                    go.GetComponent<BoXPrice>().bulletspeed = 1;
                    go.GetComponent<BoXPrice>().property = "NONE";
                    break;
                case "gun29": //风扇
                    go.GetComponent<BoXPrice>().Price =15000;
                    go.GetComponent<BoXPrice>().damage =80;
                    go.GetComponent<BoXPrice>().speed = 0.6f;
                    go.GetComponent<BoXPrice>().bulletspeed =1;
                    go.GetComponent<BoXPrice>().property = "PENETRATE";
                    break;
                case "gun30": //火箭筒
                    go.GetComponent<BoXPrice>().Price =20000;
                    go.GetComponent<BoXPrice>().damage = 250;
                    go.GetComponent<BoXPrice>().speed =0.6f;
                    go.GetComponent<BoXPrice>().bulletspeed = 1.5f;
                    go.GetComponent<BoXPrice>().property = "NONE";
                    break;
                case "gun31": //飞刀
                    go.GetComponent<BoXPrice>().Price = 30000;
                    go.GetComponent<BoXPrice>().damage = 95;
                    go.GetComponent<BoXPrice>().speed = 0.8f;
                    go.GetComponent<BoXPrice>().bulletspeed =0.5f;
                    go.GetComponent<BoXPrice>().property = "PENETRATE";
                    break;
                case "gun32": //转盘
                    go.GetComponent<BoXPrice>().Price =40000;
                    go.GetComponent<BoXPrice>().damage =350;
                    go.GetComponent<BoXPrice>().speed =0.5f;
                    go.GetComponent<BoXPrice>().bulletspeed = 0.5f;
                    go.GetComponent<BoXPrice>().property = "60°and - 60°ROTATION ";
                    break;
                case "gun33"://加特林
                    go.GetComponent<BoXPrice>().Price =50000;
                    go.GetComponent<BoXPrice>().damage =120;
                    go.GetComponent<BoXPrice>().speed = 0.11f;
                    go.GetComponent<BoXPrice>().bulletspeed = 3;
                    go.GetComponent<BoXPrice>().property = "HIGH SPEED";
                    break;
                case "gun34": //三角
                    go.GetComponent<BoXPrice>().Price = 60000;
                    go.GetComponent<BoXPrice>().damage = 110;
                    go.GetComponent<BoXPrice>().speed = 0.3f;
                    go.GetComponent<BoXPrice>().bulletspeed =0.8f;
                    go.GetComponent<BoXPrice>().property = "PENETRATE";
                    break;
                case "gun35": //月牙
                    go.GetComponent<BoXPrice>().Price =70000;
                    go.GetComponent<BoXPrice>().damage = 200;
                    go.GetComponent<BoXPrice>().speed = 1f;
                    go.GetComponent<BoXPrice>().bulletspeed = 2;
                    go.GetComponent<BoXPrice>().property = "NONE";
                    break;
                case "gun36"://电磁
                    go.GetComponent<BoXPrice>().Price =80000;
                    go.GetComponent<BoXPrice>().damage = 200;
                    go.GetComponent<BoXPrice>().speed = 0.35f;
                    go.GetComponent<BoXPrice>().bulletspeed = 2;
                    go.GetComponent<BoXPrice>().property = "NONE";
                    break;
                case "gun37": //大激光
                    go.GetComponent<BoXPrice>().Price =90000;
                    go.GetComponent<BoXPrice>().damage =125;
                    go.GetComponent<BoXPrice>().speed = 0.5f;
                    go.GetComponent<BoXPrice>().bulletspeed = 2;
                    go.GetComponent<BoXPrice>().property = "PENETRATE";
                    break;
                case "gun38": //紫水晶
                    go.GetComponent<BoXPrice>().Price =100000;
                    go.GetComponent<BoXPrice>().damage =200;
                    go.GetComponent<BoXPrice>().speed =0.1f;
                    go.GetComponent<BoXPrice>().bulletspeed =0.3f;
                    go.GetComponent<BoXPrice>().property = "HIGH SHOOT SPPED HIGH DAMAGE";
                    break;
                default:
                    break;
            }

            go.transform.SetParent(Canvas);
            go.transform.localScale = Vector3.one;
            go.transform.SetParent(ParentObject.transform);

            go.gameObject.transform.GetChild(0).GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[i];  //设置图片
            go.gameObject.transform.GetChild(0).GetComponent<Image>().SetNativeSize();
            go.gameObject.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(delegate ()          //给商店武器图标添加点击事件
            {
                SelectedWeapon();
                if (DataMenu.transform.localScale.y == 0) //y轴等于0就是关闭着的
                {
                    DataMenu.SetActive(true);
                    DataMenu.transform.DOScale(1f, tweentime); //播放动画并设置时间
                    mask.transform.DOScale(1f, 0f);
                    StartCoroutine(Inactive("Openwindow", tweentime));
                    //子彈     
                    string a = string.Concat(SelectedObject.transform.parent.name.Split(new char[] { 'g', 'u', 'n' }));
                    weaponnumber = int.Parse(a);

                    if (weaponnumber < 19)
                    {
                        Bullet.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
                        if (weaponnumber == 3 || weaponnumber == 5 || weaponnumber == 6 || weaponnumber == 7 || weaponnumber == 10 || weaponnumber == 16 || weaponnumber == 17)
                        {

                            Bullet.GetComponent<Image>().sprite = Prefabmanager.Instance.bulletboxs.GetChild(weaponnumber - 1).GetChild(0).GetComponent<Image>().sprite;
                        }
                        else
                        {
                            Bullet.GetComponent<Image>().sprite = Prefabmanager.Instance.bulletboxs.GetChild(weaponnumber - 1).GetComponent<Image>().sprite; //子弹图像
                        }
                    }
                    else if (weaponnumber > 18 && weaponnumber < 26)
                    {
                        Bullet.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0f);//变为透明
                    }
                    else if (weaponnumber > 25)
                    {
                        Bullet.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
                        if (weaponnumber == 29||weaponnumber==27 || weaponnumber == 26 || weaponnumber == 31)
                        {
                            Bullet.GetComponent<Image>().sprite = Prefabmanager.Instance.bulletboxs.GetChild(weaponnumber -8).GetChild(0).GetComponent<Image>().sprite;
                        }
                        else
                        {
                            Bullet.GetComponent<Image>().sprite = Prefabmanager.Instance.bulletboxs.GetChild(weaponnumber - 8).GetComponent<Image>().sprite; //子弹图像
                        }

                    }
               
                    Bullet.GetComponent<Image>().SetNativeSize();
                    weapon.GetComponent<Image>().sprite = SelectedObject.GetComponent<Image>().sprite;
                    weapon.GetComponent<Image>().SetNativeSize();
                    weapontext.text = "DPS:" + SelectedObject.transform.parent.GetComponent<BoXPrice>().damage
                                       + "  SHOOTSPEED:" + SelectedObject.transform.parent.GetComponent<BoXPrice>().speed + " BULLETSPEED:" +
                                       SelectedObject.transform.parent.GetComponent<BoXPrice>().bulletspeed + "\n" +
                                       SelectedObject.transform.parent.GetComponent<BoXPrice>().property;  //價格 攻擊力 攻擊速度 子彈屬性  
                }
            });
            go.transform.GetChild(0).localScale = new Vector3(1f, 1f, 1f);
        }
    }

    public void SelectedWeapon()
    {
        //获取选中的那个商店物品
        SelectedObject = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject; //获取选中的物体
        WeaponPrice.transform.parent.gameObject.SetActive(true);
        WeaponPrice.text = "X " + SelectedObject.transform.parent.GetComponent<BoXPrice>().Price;
        Debug.Log(SelectedObject);
        buybutton.PlayOneShot(AudioManager.Instance.Click);
    }

    public void Buybutton()
    {
        BuyButton.onClick.AddListener(delegate ()   //按钮添加点击事件
        {
            if (SelectedObject != null) //是否选中了某个武器
            {
                Debug.Log("Buy weapon ");
                //购买武器的方法

                if (PlayerPrefs.GetInt("coin") >= SelectedObject.transform.parent.GetComponent<BoXPrice>().Price)   //这个是已经点击了的动作
                {
                    int num = PlayerPrefs.GetInt("coin") - SelectedObject.transform.parent.GetComponent<BoXPrice>().Price; //买炮扣钱

                    PlayerPrefs.SetInt("coin", num);
                    MainMenuUI.Instance.UpdateCurrentCoin();
                    buybutton.PlayOneShot(AudioManager.Instance.BuyWeapon); //播放购买成功的音乐

                    //点击--背包添加所选中的炮台(SelectedObject)
                    for (int i = 0; i < WeaponShowitem.Instance.WeaponIco.Length; i++) //背包所有格子图片
                    {
                        GameObject go = WeaponShowitem.Instance.WeaponIco[i]; //第i格的图片
                        if (go.transform.GetChild(0).GetComponent<Image>().color.a == 0)
                        {
                            go.transform.GetChild(0).GetComponent<Image>().sprite = SelectedObject.GetComponent<Image>().sprite;
                            go.transform.GetChild(0).GetComponent<Image>().color = Color.white;
                            go.transform.GetChild(0).GetComponent<Image>().SetNativeSize();
                            go.transform.GetChild(0).gameObject.name = SelectedObject.transform.parent.name;
                            go.transform.GetChild(0).GetComponent<Image>().raycastTarget = true;
                            switch (go.transform.GetChild(0).gameObject.name)
                            {
                                case "gun1":
                                    go.transform.GetChild(0).localScale = new Vector3(0.7f, 0.7f);
                                    break;
                                case "gun2":
                                    go.transform.GetChild(0).localScale = new Vector3(0.7f, 0.7f);
                                    break;
                                case "gun3":
                                    go.transform.GetChild(0).localScale = new Vector3(0.7f, 0.7f);
                                    break;
                                case "gun4":
                                    go.transform.GetChild(0).localScale = new Vector3(0.7f, 0.7f);
                                    break;
                                case "gun5":
                                    go.transform.GetChild(0).localScale = new Vector3(0.7f, 0.7f);
                                    break;
                                case "gun6":
                                    go.transform.GetChild(0).localScale = new Vector3(0.7f, 0.7f);
                                    break;
                                case "gun7":
                                    go.transform.GetChild(0).localScale = new Vector3(0.7f, 0.7f);
                                    break;
                                case "gun8":
                                    go.transform.GetChild(0).localScale = new Vector3(0.7f, 0.7f);
                                    break;
                                case "gun9":
                                    go.transform.GetChild(0).localScale = new Vector3(0.7f, 0.7f);
                                    break;
                                case "gun10":
                                    go.transform.GetChild(0).localScale = new Vector3(0.7f, 0.7f);
                                    break;
                                case "gun11":
                                    go.transform.GetChild(0).localScale = new Vector3(0.7f, 0.7f);
                                    break;
                                case "gun12":
                                    go.transform.GetChild(0).localScale = new Vector3(0.7f, 0.7f);
                                    break;
                                case "gun13":
                                    go.transform.GetChild(0).localScale = new Vector3(0.7f, 0.7f);
                                    break;
                                case "gun14":
                                    go.transform.GetChild(0).localScale = new Vector3(0.7f, 0.7f);
                                    break;
                                case "gun15":
                                    go.transform.GetChild(0).localScale = new Vector3(0.7f, 0.7f);
                                    break;
                                case "gun16":
                                    go.transform.GetChild(0).localScale = new Vector3(0.7f, 0.7f);
                                    break;
                                case "gun17":
                                    go.transform.GetChild(0).localScale = new Vector3(0.7f, 0.7f);
                                    break;
                                case "gun18":
                                    go.transform.GetChild(0).localScale = new Vector3(0.7f, 0.7f);
                                    break;
                                case "gun19":
                                    go.transform.GetChild(0).localScale = new Vector3(0.7f, 0.7f);
                                    break;
                                case "gun20":
                                    go.transform.GetChild(0).localScale = new Vector3(0.7f, 0.7f);
                                    break;
                                case "gun21":
                                    go.transform.GetChild(0).localScale = new Vector3(0.7f, 0.7f);
                                    break;
                                case "gun22":
                                    go.transform.GetChild(0).localScale = new Vector3(0.7f, 0.7f);
                                    break;
                                case "gun23":
                                    go.transform.GetChild(0).localScale = new Vector3(0.7f, 0.7f);
                                    break;
                                case "gun24":
                                    go.transform.GetChild(0).localScale = new Vector3(0.7f, 0.7f);
                                    break;
                                case "gun25":
                                    go.transform.GetChild(0).localScale = new Vector3(0.7f, 0.7f);
                                    break;
                                default:
                                    go.transform.GetChild(0).localScale = new Vector3(0.7f, 0.7f);
                                    break;
                            }
                            go.transform.GetChild(0).gameObject.tag = "gun";
                            break;
                        }
                    }
                }
                else //钱不够
                {
                    buybutton.PlayOneShot(AudioManager.Instance.BuildFail);
                }
            }
            else
            {
                buybutton.PlayOneShot(AudioManager.Instance.BuildFail);
                Debug.Log("empty");
            }

        });
    }

}
