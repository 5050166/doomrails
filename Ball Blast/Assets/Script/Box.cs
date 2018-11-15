using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//初始化载入所有的格子并配置炮台基本信息

public class Box : MonoBehaviour
{
    public int Price;//格子解锁价格

    public string Number = "d";//格子存储信息   1:

    public bool IsHaveWheel;

    //数字 下标 0，1，2，3，4，5   存的值代表格子路面的状态



    private void Start()
    {
        DataManager.Instance.loadbox(); //载入box信息
        MainMenuUI.Instance.IsFirstPlayGame();//是否是初次进入游戏
        LoadAllBox();  //载入box

        if (ES3.FileExists("SaveData.es3"))
        {
            LoadAllFrame(); //载入所有的框架
        }
    }

    public void LoadAllBox()
    {
        switch (this.transform.GetComponent<Box>().Number)
        {
            case " ":
                this.transform.GetChild(0).gameObject.SetActive(true);
                transform.GetChild(2).gameObject.SetActive(false);
                break;
            case "1":
                if (transform.GetComponent<Box>().IsHaveWheel)
                {
                    transform.GetChild(1).gameObject.SetActive(true);  //显示轮子
                }
                transform.gameObject.tag = "gun1";
                this.transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(2).gameObject.SetActive(true);
                transform.GetChild(3).gameObject.SetActive(true);
                transform.GetChild(3).gameObject.GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[0];
                transform.GetChild(3).gameObject.GetComponent<Image>().SetNativeSize();

                transform.GetChild(3).localScale = new Vector3(0.6f, 0.6f);
                transform.SetParent(transform.parent.parent);
                break;
            case "2":
                if (transform.GetComponent<Box>().IsHaveWheel)
                {
                    transform.GetChild(1).gameObject.SetActive(true);  //显示轮子
                }

                transform.gameObject.tag = "gun2";
                this.transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(2).gameObject.SetActive(true);
                transform.GetChild(3).gameObject.SetActive(true);
                transform.GetChild(3).gameObject.GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[1];
                transform.GetChild(3).gameObject.GetComponent<Image>().SetNativeSize();

                transform.GetChild(3).localScale = new Vector3(0.6f, 0.6f);
                transform.SetParent(transform.parent.parent);
                break;
            case "3":
                if (transform.GetComponent<Box>().IsHaveWheel)
                {
                    transform.GetChild(1).gameObject.SetActive(true);  //显示轮子
                }
                transform.gameObject.tag = "gun3";
                this.transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(2).gameObject.SetActive(true);
                transform.GetChild(3).gameObject.SetActive(true);
                transform.GetChild(3).gameObject.GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[2];
                transform.GetChild(3).gameObject.GetComponent<Image>().SetNativeSize();

                transform.GetChild(3).localScale = new Vector3(0.6f, 0.6f);
                transform.SetParent(transform.parent.parent);
                break;
            case "4":
                if (transform.GetComponent<Box>().IsHaveWheel)
                {
                    transform.GetChild(1).gameObject.SetActive(true);  //显示轮子
                }
                transform.gameObject.tag = "gun4";
                this.transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(2).gameObject.SetActive(true);
                transform.GetChild(3).gameObject.SetActive(true);
                transform.GetChild(3).gameObject.GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[3];
                transform.GetChild(3).gameObject.GetComponent<Image>().SetNativeSize();

                transform.GetChild(3).gameObject.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.4f);
                transform.GetChild(3).gameObject.AddComponent<RoteCannon>();
                transform.GetChild(3).localScale = new Vector3(0.6f, 0.6f);
                transform.SetParent(transform.parent.parent);
                break;
            case "5":
                if (transform.GetComponent<Box>().IsHaveWheel)
                {
                    transform.GetChild(1).gameObject.SetActive(true);  //显示轮子
                }
                transform.gameObject.tag = "gun5";
                this.transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(2).gameObject.SetActive(true);
                transform.GetChild(3).gameObject.SetActive(true);
                transform.GetChild(3).gameObject.GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[4];
                transform.GetChild(3).gameObject.GetComponent<Image>().SetNativeSize();

                transform.GetChild(3).localScale = new Vector3(0.6f, 0.6f);
                transform.SetParent(transform.parent.parent);
                break;
            case "6":
                if (transform.GetComponent<Box>().IsHaveWheel)
                {
                    transform.GetChild(1).gameObject.SetActive(true);  //显示轮子
                }
                transform.gameObject.tag = "gun6";
                this.transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(2).gameObject.SetActive(true);
                transform.GetChild(3).gameObject.SetActive(true);
                transform.GetChild(3).gameObject.GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[5];
                transform.GetChild(3).gameObject.GetComponent<Image>().SetNativeSize();

                transform.GetChild(3).localScale = new Vector3(0.6f, 0.6f);
                transform.SetParent(transform.parent.parent);
                break;
            case "7":
                if (transform.GetComponent<Box>().IsHaveWheel)
                {
                    transform.GetChild(1).gameObject.SetActive(true);  //显示轮子
                }
                transform.gameObject.tag = "gun7";
                this.transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(2).gameObject.SetActive(true);
                transform.GetChild(3).gameObject.SetActive(true);
                transform.GetChild(3).gameObject.GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[6];
                transform.GetChild(3).gameObject.GetComponent<Image>().SetNativeSize();

                transform.GetChild(3).localScale = new Vector3(0.6f, 0.6f);
                transform.SetParent(transform.parent.parent);
                break;
            case "8":
                if (transform.GetComponent<Box>().IsHaveWheel)
                {
                    transform.GetChild(1).gameObject.SetActive(true);  //显示轮子
                }
                transform.gameObject.tag = "gun8";
                this.transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(2).gameObject.SetActive(true);
                transform.GetChild(3).gameObject.SetActive(true);
                transform.GetChild(3).gameObject.GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[7];
                transform.GetChild(3).gameObject.GetComponent<Image>().SetNativeSize();

                transform.GetChild(3).localScale = new Vector3(0.6f, 0.6f);
                transform.SetParent(transform.parent.parent);
                break;
            case "9":
                if (transform.GetComponent<Box>().IsHaveWheel)
                {
                    transform.GetChild(1).gameObject.SetActive(true);  //显示轮子
                }
                transform.gameObject.tag = "gun9";
                this.transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(2).gameObject.SetActive(true);
                transform.GetChild(3).gameObject.SetActive(true);
                transform.GetChild(3).gameObject.GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[8];
                transform.GetChild(3).gameObject.GetComponent<Image>().SetNativeSize();

                transform.GetChild(3).localScale = new Vector3(0.6f, 0.6f);
                transform.SetParent(transform.parent.parent);
                break;
            case "10":
                if (transform.GetComponent<Box>().IsHaveWheel)
                {
                    transform.GetChild(1).gameObject.SetActive(true);  //显示轮子
                }
                transform.gameObject.tag = "gun10";
                this.transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(2).gameObject.SetActive(true);
                transform.GetChild(3).gameObject.SetActive(true);
                transform.GetChild(3).gameObject.GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[9];
                transform.GetChild(3).gameObject.GetComponent<Image>().SetNativeSize();

                transform.GetChild(3).localScale = new Vector3(0.6f, 0.6f);
                transform.SetParent(transform.parent.parent);
                break;
            case "11":
                if (transform.GetComponent<Box>().IsHaveWheel)
                {
                    transform.GetChild(1).gameObject.SetActive(true);  //显示轮子
                }
                transform.gameObject.tag = "gun11";
                this.transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(2).gameObject.SetActive(true);
                transform.GetChild(3).gameObject.SetActive(true);
                transform.GetChild(3).gameObject.GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[10];
                transform.GetChild(3).gameObject.GetComponent<Image>().SetNativeSize();

                transform.GetChild(3).localScale = new Vector3(0.6f, 0.6f);
                transform.SetParent(transform.parent.parent);
                break;
            case "12":
                if (transform.GetComponent<Box>().IsHaveWheel)
                {
                    transform.GetChild(1).gameObject.SetActive(true);  //显示轮子
                }
                transform.gameObject.tag = "gun12";
                this.transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(2).gameObject.SetActive(true);
                transform.GetChild(3).gameObject.SetActive(true);
                transform.GetChild(3).gameObject.GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[11];
                transform.GetChild(3).gameObject.GetComponent<Image>().SetNativeSize();

                transform.GetChild(3).localScale = new Vector3(0.6f, 0.6f);
                transform.SetParent(transform.parent.parent);
                break;
            case "13":
                if (transform.GetComponent<Box>().IsHaveWheel)
                {
                    transform.GetChild(1).gameObject.SetActive(true);  //显示轮子
                }
                transform.gameObject.tag = "gun13";
                this.transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(2).gameObject.SetActive(true);
                transform.GetChild(3).gameObject.SetActive(true);
                transform.GetChild(3).gameObject.GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[12];
                transform.GetChild(3).gameObject.GetComponent<Image>().SetNativeSize();

                transform.GetChild(3).localScale = new Vector3(0.6f, 0.6f);
                transform.SetParent(transform.parent.parent);
                break;
            case "14":
                if (transform.GetComponent<Box>().IsHaveWheel)
                {
                    transform.GetChild(1).gameObject.SetActive(true);  //显示轮子
                }
                transform.gameObject.tag = "gun14";
                this.transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(2).gameObject.SetActive(true);
                transform.GetChild(3).gameObject.SetActive(true);
                transform.GetChild(3).gameObject.GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[13];
                transform.GetChild(3).gameObject.GetComponent<Image>().SetNativeSize();

                transform.GetChild(3).localScale = new Vector3(0.6f, 0.6f);
                transform.SetParent(transform.parent.parent);
                break;
            case "15":
                if (transform.GetComponent<Box>().IsHaveWheel)
                {
                    transform.GetChild(1).gameObject.SetActive(true);  //显示轮子
                }
                transform.gameObject.tag = "gun15";
                this.transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(2).gameObject.SetActive(true);
                transform.GetChild(3).gameObject.SetActive(true);
                transform.GetChild(3).gameObject.GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[14];
                transform.GetChild(3).gameObject.GetComponent<Image>().SetNativeSize();

                transform.GetChild(3).localScale = new Vector3(0.6f, 0.6f);
                transform.SetParent(transform.parent.parent);
                break;
            case "16":
                if (transform.GetComponent<Box>().IsHaveWheel)
                {
                    transform.GetChild(1).gameObject.SetActive(true);  //显示轮子
                }
                transform.gameObject.tag = "gun16";
                this.transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(2).gameObject.SetActive(true);
                transform.GetChild(3).gameObject.SetActive(true);
                transform.GetChild(3).gameObject.GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[15];
                transform.GetChild(3).gameObject.GetComponent<Image>().SetNativeSize();
                transform.GetChild(3).gameObject.AddComponent<RoteCannon>();

                transform.GetChild(3).localScale = new Vector3(0.6f, 0.6f);
                transform.SetParent(transform.parent.parent);

                break;
            case "17":
                if (transform.GetComponent<Box>().IsHaveWheel)
                {
                    transform.GetChild(1).gameObject.SetActive(true);  //显示轮子
                }
                transform.gameObject.tag = "gun17";
                this.transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(2).gameObject.SetActive(true);
                transform.GetChild(3).gameObject.SetActive(true);
                transform.GetChild(3).gameObject.GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[16];
                transform.GetChild(3).gameObject.GetComponent<Image>().SetNativeSize();
                transform.GetChild(3).gameObject.AddComponent<RoteCannon>();

                transform.GetChild(3).localScale = new Vector3(0.6f, 0.6f);
                transform.SetParent(transform.parent.parent);
                break;
            case "18":
                if (transform.GetComponent<Box>().IsHaveWheel)
                {
                    transform.GetChild(1).gameObject.SetActive(true);  //显示轮子
                }
                transform.gameObject.tag = "gun18";
                this.transform.GetChild(0).gameObject.SetActive(false);  //锁
                transform.GetChild(2).gameObject.SetActive(true);
                transform.GetChild(3).gameObject.SetActive(true);
                transform.GetChild(3).gameObject.GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[17];
                transform.GetChild(3).gameObject.GetComponent<Image>().SetNativeSize();

                transform.GetChild(3).localScale = new Vector3(0.6f, 0.6f);
                transform.SetParent(transform.parent.parent);
                break;
            case "19":
                //  Debug.Log("ha1");
                if (transform.GetComponent<Box>().IsHaveWheel)
                {
                    transform.GetChild(1).gameObject.SetActive(true);  //显示轮子
                }
                transform.gameObject.tag = "gun19";
                transform.GetChild(3).gameObject.GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[18];
                this.transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(2).gameObject.SetActive(true); //框架已经显示啦
                transform.SetParent(transform.parent.parent);
                break;

            case "20": //零件+20攻击速度
                if (transform.GetComponent<Box>().IsHaveWheel)
                {
                    transform.GetChild(1).gameObject.SetActive(true);  //显示轮子
                }
                //  BuffManager.Instance.ShootSpeedBuff = 0.2f;
                transform.gameObject.tag = "gun20";
                this.transform.GetChild(0).gameObject.SetActive(false);  //锁
                transform.GetChild(2).gameObject.SetActive(true);
                transform.GetChild(3).gameObject.SetActive(true);
                transform.GetChild(3).GetChild(0).gameObject.SetActive(false);//关闭开火功能
                transform.GetChild(3).gameObject.GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[19];
                transform.GetChild(3).gameObject.GetComponent<Image>().SetNativeSize();
                transform.GetChild(3).localScale = new Vector3(0.6f, 0.6f);
                transform.SetParent(transform.parent.parent);
                break;
            case "21": //零件+20攻击速度
                if (transform.GetComponent<Box>().IsHaveWheel)
                {
                    transform.GetChild(1).gameObject.SetActive(true);  //显示轮子
                }
                transform.gameObject.tag = "gun21";
                this.transform.GetChild(0).gameObject.SetActive(false);  //锁
                transform.GetChild(2).gameObject.SetActive(true);
                transform.GetChild(3).gameObject.SetActive(true);
                transform.GetChild(3).GetChild(0).gameObject.SetActive(false);//关闭开火功能
                transform.GetChild(3).gameObject.GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[20];
                transform.GetChild(3).gameObject.GetComponent<Image>().SetNativeSize();
                transform.GetChild(3).localScale = new Vector3(0.6f, 0.6f);
                transform.SetParent(transform.parent.parent);
                break;
            case "22": //零件+20攻击速度
                if (transform.GetComponent<Box>().IsHaveWheel)
                {
                    transform.GetChild(1).gameObject.SetActive(true);  //显示轮子
                }

                transform.gameObject.tag = "gun22";
                this.transform.GetChild(0).gameObject.SetActive(false);  //锁
                transform.GetChild(2).gameObject.SetActive(true);
                transform.GetChild(3).gameObject.SetActive(true);
                transform.GetChild(3).GetChild(0).gameObject.SetActive(false);//关闭开火功能
                transform.GetChild(3).gameObject.GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[21];
                transform.GetChild(3).gameObject.GetComponent<Image>().SetNativeSize();
                transform.GetChild(3).localScale = new Vector3(0.6f, 0.6f);
                transform.SetParent(transform.parent.parent);
                break;
            case "23": //零件+20攻击速度
                if (transform.GetComponent<Box>().IsHaveWheel)
                {
                    transform.GetChild(1).gameObject.SetActive(true);  //显示轮子
                }

                transform.gameObject.tag = "gun23";
                this.transform.GetChild(0).gameObject.SetActive(false);  //锁
                transform.GetChild(2).gameObject.SetActive(true);
                transform.GetChild(3).gameObject.SetActive(true);
                transform.GetChild(3).GetChild(0).gameObject.SetActive(false);//关闭开火功能
                transform.GetChild(3).gameObject.GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[22];
                transform.GetChild(3).gameObject.GetComponent<Image>().SetNativeSize();
                transform.GetChild(3).localScale = new Vector3(0.6f, 0.6f);
                transform.SetParent(transform.parent.parent);
                break;
            case "24":
                if (transform.GetComponent<Box>().IsHaveWheel)
                {
                    transform.GetChild(1).gameObject.SetActive(true);  //显示轮子
                }
                transform.gameObject.tag = "gun24";
                transform.GetChild(3).gameObject.GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[23];
                this.transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(2).gameObject.SetActive(true); //框架已经显示啦
                transform.SetParent(transform.parent.parent);
                break;
            case "25":
                if (transform.GetComponent<Box>().IsHaveWheel)
                {
                    transform.GetChild(1).gameObject.SetActive(true);  //显示轮子
                }
                transform.gameObject.tag = "gun25";
                transform.GetChild(3).gameObject.GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[24];
                this.transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(2).gameObject.SetActive(true); //框架已经显示啦
                transform.SetParent(transform.parent.parent);
                break;
            case "26": //奶酪
                if (transform.GetComponent<Box>().IsHaveWheel)
                {
                    transform.GetChild(1).gameObject.SetActive(true);  //显示轮子
                }
                transform.gameObject.tag = "gun26";
                this.transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(2).gameObject.SetActive(true);
                transform.GetChild(3).gameObject.SetActive(true);
                transform.GetChild(3).gameObject.GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[25];
                transform.GetChild(3).gameObject.GetComponent<Image>().SetNativeSize();

                transform.GetChild(3).localScale = new Vector3(0.6f, 0.6f);
                transform.SetParent(transform.parent.parent);
                break;
            case "27":
                if (transform.GetComponent<Box>().IsHaveWheel)
                {
                    transform.GetChild(1).gameObject.SetActive(true);  //显示轮子
                }
                transform.gameObject.tag = "gun27";
                this.transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(2).gameObject.SetActive(true);
                transform.GetChild(3).gameObject.SetActive(true);
                transform.GetChild(3).gameObject.GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[26];
                transform.GetChild(3).gameObject.GetComponent<Image>().SetNativeSize();
                transform.GetChild(3).gameObject.AddComponent<RoteCannon>();
                transform.GetChild(3).localScale = new Vector3(0.6f, 0.6f);
                transform.SetParent(transform.parent.parent);
                break;
            case "28":
                if (transform.GetComponent<Box>().IsHaveWheel)
                {
                    transform.GetChild(1).gameObject.SetActive(true);  //显示轮子
                }
                transform.gameObject.tag = "gun28";
                this.transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(2).gameObject.SetActive(true);
                transform.GetChild(3).gameObject.SetActive(true);
                transform.GetChild(3).gameObject.GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[27];
                transform.GetChild(3).gameObject.GetComponent<Image>().SetNativeSize();

                transform.GetChild(3).localScale = new Vector3(0.6f, 0.6f);
                transform.SetParent(transform.parent.parent);
                break;
            case "29":
                if (transform.GetComponent<Box>().IsHaveWheel)
                {
                    transform.GetChild(1).gameObject.SetActive(true);  //显示轮子
                }
                transform.gameObject.tag = "gun29";
                this.transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(2).gameObject.SetActive(true);
                transform.GetChild(3).gameObject.SetActive(true);
                transform.GetChild(3).gameObject.GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[28];
                transform.GetChild(3).gameObject.GetComponent<Image>().SetNativeSize();
                transform.GetChild(3).gameObject.AddComponent<RoteCannon>();
                transform.GetChild(3).localScale = new Vector3(0.6f, 0.6f);
                transform.SetParent(transform.parent.parent);
                break;
            case "30":
                if (transform.GetComponent<Box>().IsHaveWheel)
                {
                    transform.GetChild(1).gameObject.SetActive(true);  //显示轮子
                }
                transform.gameObject.tag = "gun30";
                this.transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(2).gameObject.SetActive(true);
                transform.GetChild(3).gameObject.SetActive(true);
                transform.GetChild(3).gameObject.GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[29];
                transform.GetChild(3).gameObject.GetComponent<Image>().SetNativeSize();

                transform.GetChild(3).localScale = new Vector3(0.6f, 0.6f);
                transform.SetParent(transform.parent.parent);
                break;
            case "31":
                if (transform.GetComponent<Box>().IsHaveWheel)
                {
                    transform.GetChild(1).gameObject.SetActive(true);  //显示轮子
                }
                transform.gameObject.tag = "gun31";
                this.transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(2).gameObject.SetActive(true);
                transform.GetChild(3).gameObject.SetActive(true);
                transform.GetChild(3).gameObject.GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[30];
                transform.GetChild(3).gameObject.GetComponent<Image>().SetNativeSize();

                transform.GetChild(3).localScale = new Vector3(0.6f, 0.6f);
                transform.SetParent(transform.parent.parent);
                break;
            case "32":
                if (transform.GetComponent<Box>().IsHaveWheel)
                {
                    transform.GetChild(1).gameObject.SetActive(true);  //显示轮子
                }
                transform.gameObject.tag = "gun32";
                this.transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(2).gameObject.SetActive(true);
                transform.GetChild(3).gameObject.SetActive(true);
                transform.GetChild(3).gameObject.GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[31];
                transform.GetChild(3).gameObject.GetComponent<Image>().SetNativeSize();
                transform.GetChild(3).gameObject.AddComponent<RoteCannon>();
                transform.GetChild(3).localScale = new Vector3(0.6f, 0.6f);
                transform.SetParent(transform.parent.parent);
                break;
            case "33":
                if (transform.GetComponent<Box>().IsHaveWheel)
                {
                    transform.GetChild(1).gameObject.SetActive(true);  //显示轮子
                }
                transform.gameObject.tag = "gun33";
                this.transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(2).gameObject.SetActive(true);
                transform.GetChild(3).gameObject.SetActive(true);
                transform.GetChild(3).gameObject.GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[32];
                transform.GetChild(3).gameObject.GetComponent<Image>().SetNativeSize();

                transform.GetChild(3).localScale = new Vector3(0.6f, 0.6f);
                transform.SetParent(transform.parent.parent);
                break;
            case "34":
                if (transform.GetComponent<Box>().IsHaveWheel)
                {
                    transform.GetChild(1).gameObject.SetActive(true);  //显示轮子
                }
                transform.gameObject.tag = "gun34";
                this.transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(2).gameObject.SetActive(true);
                transform.GetChild(3).gameObject.SetActive(true);
                transform.GetChild(3).gameObject.GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[33];
                transform.GetChild(3).gameObject.GetComponent<Image>().SetNativeSize();

                transform.GetChild(3).localScale = new Vector3(0.6f, 0.6f);
                transform.SetParent(transform.parent.parent);
                break;
            case "35":
                if (transform.GetComponent<Box>().IsHaveWheel)
                {
                    transform.GetChild(1).gameObject.SetActive(true);  //显示轮子
                }
                transform.gameObject.tag = "gun35";
                this.transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(2).gameObject.SetActive(true);
                transform.GetChild(3).gameObject.SetActive(true);
                transform.GetChild(3).gameObject.GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[34];
                transform.GetChild(3).gameObject.GetComponent<Image>().SetNativeSize();

                transform.GetChild(3).localScale = new Vector3(0.6f, 0.6f);
                transform.SetParent(transform.parent.parent);
                break;
            case "36":
                if (transform.GetComponent<Box>().IsHaveWheel)
                {
                    transform.GetChild(1).gameObject.SetActive(true);  //显示轮子
                }
                transform.gameObject.tag = "gun36";
                this.transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(2).gameObject.SetActive(true);
                transform.GetChild(3).gameObject.SetActive(true);
                transform.GetChild(3).gameObject.GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[35];
                transform.GetChild(3).gameObject.GetComponent<Image>().SetNativeSize();

                transform.GetChild(3).localScale = new Vector3(0.6f, 0.6f);
                transform.SetParent(transform.parent.parent);
                break;
            case "37":
                if (transform.GetComponent<Box>().IsHaveWheel)
                {
                    transform.GetChild(1).gameObject.SetActive(true);  //显示轮子
                }
                transform.gameObject.tag = "gun37";
                this.transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(2).gameObject.SetActive(true);
                transform.GetChild(3).gameObject.SetActive(true);
                transform.GetChild(3).gameObject.GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[36];
                transform.GetChild(3).gameObject.GetComponent<Image>().SetNativeSize();

                transform.GetChild(3).localScale = new Vector3(0.6f, 0.6f);
                transform.SetParent(transform.parent.parent);
                break;
            case "38":
                if (transform.GetComponent<Box>().IsHaveWheel)
                {
                    transform.GetChild(1).gameObject.SetActive(true);  //显示轮子
                }
                transform.gameObject.tag = "gun38";
                this.transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(2).gameObject.SetActive(true);
                transform.GetChild(3).gameObject.SetActive(true);
                transform.GetChild(3).gameObject.GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[37];
                transform.GetChild(3).gameObject.GetComponent<Image>().SetNativeSize();
                transform.GetChild(3).gameObject.AddComponent<RoteCannon>();
                transform.GetChild(3).localScale = new Vector3(0.6f, 0.6f);
                transform.SetParent(transform.parent.parent);
                break;
            case "39":
                transform.GetChild(0).gameObject.SetActive(false);
                transform.gameObject.tag = "gun39";
                break;
            default:
                break;
        }

    }


    public void CheckBox()  //通过tag判断格子状况
    {

        this.transform.GetComponent<Box>().Number = this.transform.gameObject.tag.Substring(3);
        #region
        //switch (transform.gameObject.tag)
        //{
        //    case "gun1":
        //        transform.GetComponent<Box>().Number = "1";
        //        break;
        //    case "gun2":
        //        transform.GetComponent<Box>().Number = "2";
        //        break;
        //    case "gun3":
        //        transform.GetComponent<Box>().Number = "3";
        //        break;
        //    case "gun4":
        //        transform.GetComponent<Box>().Number = "4";
        //        break;
        //    case "gun5":
        //        transform.GetComponent<Box>().Number = "5";
        //        break;
        //    case "gun6":
        //        transform.GetComponent<Box>().Number = "6";
        //        break;
        //    case "gun7":
        //        transform.GetComponent<Box>().Number = "7";
        //        break;
        //    case "gun8":
        //        transform.GetComponent<Box>().Number = "8";
        //        break;
        //    case "gun9":
        //        transform.GetComponent<Box>().Number = "9";
        //        break;
        //    case "gun10":
        //        transform.GetComponent<Box>().Number = "10";
        //        break;
        //    case "gun11":
        //        transform.GetComponent<Box>().Number = "11";
        //        break;
        //    case "gun12":
        //        transform.GetComponent<Box>().Number = "12";
        //        break;
        //    case "gun13":
        //        transform.GetComponent<Box>().Number = "13";
        //        break;
        //    case "gun14":
        //        transform.GetComponent<Box>().Number = "14";
        //        break;
        //    case "gun15":
        //        transform.GetComponent<Box>().Number = "15";
        //        break;
        //    case "gun16":
        //        transform.GetComponent<Box>().Number = "16";
        //        break;
        //    case "gun17":
        //        transform.GetComponent<Box>().Number = "17";
        //        break;
        //    case "gun18":
        //        transform.GetComponent<Box>().Number = "18";
        //        break;
        //    case "gun19":
        //        transform.GetComponent<Box>().Number = "19";
        //        break;
        //    case "gun20":
        //        transform.GetComponent<Box>().Number = "20";
        //        break;
        //    case "gun21":
        //        transform.GetComponent<Box>().Number = "21";
        //        break;
        //    case "gun22":
        //        transform.GetComponent<Box>().Number = "22";
        //        break;
        //    case "gun23":
        //        transform.GetComponent<Box>().Number = "23";
        //        break;
        //    case "gun24":
        //        transform.GetComponent<Box>().Number = "24";
        //        break;
        //    case "gun25":
        //        transform.GetComponent<Box>().Number = "25";
        //        break;
        //    case "gun26":
        //        transform.GetComponent<Box>().Number = "26";
        //        break;

        //    default:
        //        break;
        //}
        #endregion
    }



    public void LoadAllFrame() //载入格子
    {

        DataManager.Instance.LoadAllFrame();
        for (int i = 0; i < DataManager.Instance.frame.Length; i++)
        {

            switch (DataManager.Instance.frame[i])
            {
                //读取载入框架信息 无论存的是什么都可以读哦
                case "gun19":
                    DataManager.Instance.bo[i].transform.GetChild(2).name = DataManager.Instance.frame[i];
                    DataManager.Instance.bo[i].transform.GetChild(2).GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[18];
                    break;
                case "gun24":
                    DataManager.Instance.bo[i].transform.GetChild(2).name = DataManager.Instance.frame[i];
                    DataManager.Instance.bo[i].transform.GetChild(2).GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[24];
                    break;
                case "gun25":
                    DataManager.Instance.bo[i].transform.GetChild(2).name = DataManager.Instance.frame[i];
                    DataManager.Instance.bo[i].transform.GetChild(2).GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[23];
                    break;
                default:
                    break;
            }




        }



    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            CheckBox();
        }

    }






}
