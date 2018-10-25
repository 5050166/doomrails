using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponShowitem : MonoBehaviour
{
    public GameObject WeaponPrefab;

    public GameObject[] WeaponIco; //存储图标

    public string[] weapon;

    public GameObject Content;

    Color color = new Color(1f, 1f, 1f, 0f);
    Color co = new Color(1f, 1f, 1f, 1f);
    private static WeaponShowitem instance;

    public static WeaponShowitem Instance
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
      
        //自适应
        if (Screen.height == 1920f)
        {
            this.transform.localPosition = new Vector3(0f, -750f, 0f);
            this.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(1215f, 330f);
        }
        else if (Screen.height == 2160f)
        {
            this.transform.localPosition = new Vector3(0f, -766.7f, 0f);
            this.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(1080f, 313.3f);
        }


        instance = this;
    }
    void Start()
    {
        //将所有的格子加载到list中管理
        WeaponIco = new GameObject[30];
        weapon = new string[30];
        LoadAllWeaponBox();
        if(ES3.FileExists("SaveData.es3"))
        {
    
        LoadData();

        }

        if (System.String.IsNullOrEmpty(PlayerPrefs.GetString("firstplay")))//第一次进入游戏送框架
        {
            for (int i = 0; i < 4; i++)
            {
                Content.transform.GetChild(i).GetChild(0).name = "gun19";
                Content.transform.GetChild(i).GetChild(0).GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[18];

                Content.transform.GetChild(i).GetChild(0).GetComponent<Image>().color = co;
                Content.transform.GetChild(i).GetChild(0).GetComponent<Image>().raycastTarget = true;
                Content.transform.GetChild(i).GetChild(0).GetComponent<Image>().SetNativeSize();
                Content.transform.GetChild(i).GetChild(0).localScale = new Vector3(0.7f, 0.7f);
                Content.transform.GetChild(i).GetChild(0).tag = "gun";
            }
            PlayerPrefs.SetString("firstplay", "546");
        }

    }
    public void LoadAllWeaponBox()
    {
        for (int i = 0; i < 30; i++)
        {
            WeaponIco[i] = Content.transform.GetChild(i).gameObject;  //获取的时Content的子物体
            WeaponIco[i].transform.GetChild(0).gameObject.AddComponent<DragItem>();
            WeaponIco[i].transform.GetChild(0).GetComponent<Image>().color = color; //图片颜色设置为透明
            WeaponIco[i].transform.GetChild(0).GetComponent<Image>().raycastTarget = false;
        }

    }
    public void SaveData()
    {
        for (int i = 0; i < 30; i++)
        {

            weapon[i] = WeaponIco[i].transform.GetChild(0).name;
            Debug.Log(weapon[i]);
        }
        ES3.Save<string[]>("name", weapon);

    }
    public void LoadData()
    {
        weapon = ES3.Load<string[]>("name");
        for (int i = 0; i < 30; i++)
        {
            WeaponIco[i].transform.GetChild(0).name = weapon[i];
           

            switch (WeaponIco[i].transform.GetChild(0).name)
            {
                case "gun1":
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[0];
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().color = co;
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().SetNativeSize();
                    WeaponIco[i].transform.GetChild(0).localScale = new Vector3(0.7f, 0.7f);
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().raycastTarget = true;
                    WeaponIco[i].transform.GetChild(0).tag = "gun";
                    break;
                case "gun2":
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[1];
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().color = co;
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().SetNativeSize();
                    WeaponIco[i].transform.GetChild(0).localScale = new Vector3(0.7f, 0.7f);
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().raycastTarget = true;
                    WeaponIco[i].transform.GetChild(0).tag = "gun";
                    break;
                case "gun3":
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[2];
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().color = co;
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().SetNativeSize();
                    WeaponIco[i].transform.GetChild(0).localScale = new Vector3(0.7f, 0.7f);
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().raycastTarget = true;
                    WeaponIco[i].transform.GetChild(0).tag = "gun";
                    break;
                case "gun4":
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[3];
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().color = co;
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().SetNativeSize();
                    WeaponIco[i].transform.GetChild(0).localScale = new Vector3(0.7f, 0.7f);
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().raycastTarget = true;
                    WeaponIco[i].transform.GetChild(0).tag = "gun";
                    break;
                case "gun5":
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[4];
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().color = co;
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().SetNativeSize();
                    WeaponIco[i].transform.GetChild(0).localScale = new Vector3(0.7f, 0.7f);
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().raycastTarget = true;
                    WeaponIco[i].transform.GetChild(0).tag = "gun";
                    break;
                case "gun6":
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[5];
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().color = co;
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().SetNativeSize();
                    WeaponIco[i].transform.GetChild(0).localScale = new Vector3(0.7f, 0.7f);
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().raycastTarget = true;
                    WeaponIco[i].transform.GetChild(0).tag = "gun";
                    break;
                case "gun7":
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[6];
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().color = co;
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().SetNativeSize();
                    WeaponIco[i].transform.GetChild(0).localScale = new Vector3(0.7f, 0.7f);
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().raycastTarget = true;
                    WeaponIco[i].transform.GetChild(0).tag = "gun";
                    break;
                case "gun8":
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[7];
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().color = co;
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().SetNativeSize();
                    WeaponIco[i].transform.GetChild(0).localScale = new Vector3(0.7f, 0.7f);
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().raycastTarget = true;
                    WeaponIco[i].transform.GetChild(0).tag = "gun";
                    break;
                case "gun9":
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[8];
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().color = co;
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().SetNativeSize();
                    WeaponIco[i].transform.GetChild(0).localScale = new Vector3(0.7f, 0.7f);
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().raycastTarget = true;
                    WeaponIco[i].transform.GetChild(0).tag = "gun";
                    break;
                case "gun10":
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[9];
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().color = co;
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().SetNativeSize();
                    WeaponIco[i].transform.GetChild(0).localScale = new Vector3(0.7f, 0.7f);
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().raycastTarget = true;
                    WeaponIco[i].transform.GetChild(0).tag = "gun";
                    break;
                case "gun11":
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[10];
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().color = co;
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().SetNativeSize();
                    WeaponIco[i].transform.GetChild(0).localScale = new Vector3(0.7f, 0.7f);
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().raycastTarget = true;
                    WeaponIco[i].transform.GetChild(0).tag = "gun";
                    break;
                case "gun12":
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[11];
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().color = co;
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().SetNativeSize();
                    WeaponIco[i].transform.GetChild(0).localScale = new Vector3(0.7f, 0.7f);
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().raycastTarget = true;
                    WeaponIco[i].transform.GetChild(0).tag = "gun";
                    break;
                case "gun13":
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[12];
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().color = co;
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().SetNativeSize();
                    WeaponIco[i].transform.GetChild(0).localScale = new Vector3(0.7f, 0.7f);
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().raycastTarget = true;
                    WeaponIco[i].transform.GetChild(0).tag = "gun";
                    break;
                case "gun14":
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[13];
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().color = co;
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().SetNativeSize();
                    WeaponIco[i].transform.GetChild(0).localScale = new Vector3(0.7f, 0.7f);
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().raycastTarget = true;
                    WeaponIco[i].transform.GetChild(0).tag = "gun";
                    break;
                case "gun15":
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[14];
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().color = co;
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().SetNativeSize();
                    WeaponIco[i].transform.GetChild(0).localScale = new Vector3(0.7f, 0.7f);
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().raycastTarget = true;
                    WeaponIco[i].transform.GetChild(0).tag = "gun";
                    break;
                case "gun16":
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[15];
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().color = co;
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().SetNativeSize();
                    WeaponIco[i].transform.GetChild(0).localScale = new Vector3(0.7f, 0.7f);
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().raycastTarget = true;
                    WeaponIco[i].transform.GetChild(0).tag = "gun";
                    break;
                case "gun17":
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[16];
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().color = co;
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().SetNativeSize();
                    WeaponIco[i].transform.GetChild(0).localScale = new Vector3(0.7f, 0.7f);
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().raycastTarget = true;
                    WeaponIco[i].transform.GetChild(0).tag = "gun";
                    break;
                case "gun18":
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[17];
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().color = co;
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().SetNativeSize();
                    WeaponIco[i].transform.GetChild(0).localScale = new Vector3(0.7f, 0.7f);
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().raycastTarget = true;
                    WeaponIco[i].transform.GetChild(0).tag = "gun";
                    break;
                case "gun19":
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[18];
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().color = co;
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().SetNativeSize();
                    WeaponIco[i].transform.GetChild(0).localScale = new Vector3(0.7f, 0.7f);
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().raycastTarget = true;
                    WeaponIco[i].transform.GetChild(0).tag = "gun";
                    break;
                case "gun20":
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[19];
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().color = co;
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().SetNativeSize();
                    WeaponIco[i].transform.GetChild(0).localScale = new Vector3(0.7f, 0.7f);
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().raycastTarget = true;
                    WeaponIco[i].transform.GetChild(0).tag = "gun";
                    break;
                case "gun21":
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[20];
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().color = co;
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().SetNativeSize();
                    WeaponIco[i].transform.GetChild(0).localScale = new Vector3(0.7f, 0.7f);
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().raycastTarget = true;
                    WeaponIco[i].transform.GetChild(0).tag = "gun";
                    break;
                case "gun22":
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[21];
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().color = co;
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().SetNativeSize();
                    WeaponIco[i].transform.GetChild(0).localScale = new Vector3(0.7f, 0.7f);
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().raycastTarget = true;
                    WeaponIco[i].transform.GetChild(0).tag = "gun";
                    break;
                case "gun23":
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[22];
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().color = co;
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().SetNativeSize();
                    WeaponIco[i].transform.GetChild(0).localScale = new Vector3(0.7f, 0.7f);
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().raycastTarget = true;
                    WeaponIco[i].transform.GetChild(0).tag = "gun";
                    break;
                case "gun24":
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[23];
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().color = co;
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().SetNativeSize();
                    WeaponIco[i].transform.GetChild(0).localScale = new Vector3(0.7f, 0.7f);
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().raycastTarget = true;
                    WeaponIco[i].transform.GetChild(0).tag = "gun";
                    break;
                case "gun25":
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[24];
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().color = co;
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().SetNativeSize();
                    WeaponIco[i].transform.GetChild(0).localScale = new Vector3(0.7f, 0.7f);
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().raycastTarget = true;
                    WeaponIco[i].transform.GetChild(0).tag = "gun";
                    break;
                case "gun26":
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[25];
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().color = co;
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().SetNativeSize();
                    WeaponIco[i].transform.GetChild(0).localScale = new Vector3(0.7f, 0.7f);
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().raycastTarget = true;
                    WeaponIco[i].transform.GetChild(0).tag = "gun";
                    break;
                case "gun27":
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[26];
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().color = co;
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().SetNativeSize();
                    WeaponIco[i].transform.GetChild(0).localScale = new Vector3(0.7f, 0.7f);
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().raycastTarget = true;
                    WeaponIco[i].transform.GetChild(0).tag = "gun";
                    break;
                case "gun28":
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[27];
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().color = co;
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().SetNativeSize();
                    WeaponIco[i].transform.GetChild(0).localScale = new Vector3(0.7f, 0.7f);
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().raycastTarget = true;
                    WeaponIco[i].transform.GetChild(0).tag = "gun";
                    break;
                case "gun29":
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[28];
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().color = co;
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().SetNativeSize();
                    WeaponIco[i].transform.GetChild(0).localScale = new Vector3(0.7f, 0.7f);
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().raycastTarget = true;
                    WeaponIco[i].transform.GetChild(0).tag = "gun";
                    break;
                case "gun30":
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[29];
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().color = co;
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().SetNativeSize();
                    WeaponIco[i].transform.GetChild(0).localScale = new Vector3(0.7f, 0.7f);
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().raycastTarget = true;
                    WeaponIco[i].transform.GetChild(0).tag = "gun";
                    break;
                case "gun31":
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[30];
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().color = co;
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().SetNativeSize();
                    WeaponIco[i].transform.GetChild(0).localScale = new Vector3(0.7f, 0.7f);
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().raycastTarget = true;
                    WeaponIco[i].transform.GetChild(0).tag = "gun";
                    break;
                case "gun32":
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[31];
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().color = co;
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().SetNativeSize();
                    WeaponIco[i].transform.GetChild(0).localScale = new Vector3(0.7f, 0.7f);
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().raycastTarget = true;
                    WeaponIco[i].transform.GetChild(0).tag = "gun";

                    break;
                case "gun33":
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[32];
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().color = co;
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().SetNativeSize();
                    WeaponIco[i].transform.GetChild(0).localScale = new Vector3(0.7f, 0.7f);
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().raycastTarget = true;
                    WeaponIco[i].transform.GetChild(0).tag = "gun";
                    break;
                case "gun34":
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[33];
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().color = co;
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().SetNativeSize();
                    WeaponIco[i].transform.GetChild(0).localScale = new Vector3(0.7f, 0.7f);
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().raycastTarget = true;
                    WeaponIco[i].transform.GetChild(0).tag = "gun";
                    break;
                case "gun35":
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[34];
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().color = co;
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().SetNativeSize();
                    WeaponIco[i].transform.GetChild(0).localScale = new Vector3(0.7f, 0.7f);
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().raycastTarget = true;
                    WeaponIco[i].transform.GetChild(0).tag = "gun";
                    break;
                case "gun36":
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[35];
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().color = co;
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().SetNativeSize();
                    WeaponIco[i].transform.GetChild(0).localScale = new Vector3(0.7f, 0.7f);
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().raycastTarget = true;
                    WeaponIco[i].transform.GetChild(0).tag = "gun";
                    break;
                case "gun37":
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[36];
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().color = co;
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().SetNativeSize();
                    WeaponIco[i].transform.GetChild(0).localScale = new Vector3(0.7f, 0.7f);
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().raycastTarget = true;
                    WeaponIco[i].transform.GetChild(0).tag = "gun";
                    break;
                case "gun38":
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[37];
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().color = co;
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().SetNativeSize();
                    WeaponIco[i].transform.GetChild(0).localScale = new Vector3(0.7f, 0.7f);
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().raycastTarget = true;
                    WeaponIco[i].transform.GetChild(0).tag = "gun";
                    break;
                default:
                    WeaponIco[i].transform.GetChild(0).name = "Image";
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().color = color;
                    WeaponIco[i].transform.GetChild(0).GetComponent<Image>().raycastTarget = false;
                    break;
            }
        }


        //for (int i = 0; i < WeaponShowitem.Instance.WeaponIco.Length; i++) //背包所有格子图片
        //{
        //    GameObject go = WeaponShowitem.Instance.WeaponIco[i]; //第i格的图片
        //    if (go.transform.GetChild(0).GetComponent<Image>().color.a == 0)
        //    {
        //        go.transform.GetChild(0).GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[18];
        //        go.transform.GetChild(0).GetComponent<Image>().color = Color.white;
        //        go.transform.GetChild(0).GetComponent<Image>().SetNativeSize();
        //        go.transform.GetChild(0).gameObject.name = Prefabmanager.Instance.sprites[18].name;
        //        switch (go.transform.GetChild(0).gameObject.name)
        //        {
        //            case "gun1":
        //                go.transform.GetChild(0).localScale = new Vector3(0.7f, 0.7f);
        //                break;
        //            case "gun2":
        //                go.transform.GetChild(0).localScale = new Vector3(0.7f, 0.7f);
        //                break;
        //            case "gun3":
        //                go.transform.GetChild(0).localScale = new Vector3(0.7f, 0.7f);
        //                break;
        //            case "gun4":
        //                go.transform.GetChild(0).localScale = new Vector3(0.7f, 0.7f);
        //                break;
        //            case "gun5":
        //                go.transform.GetChild(0).localScale = new Vector3(0.7f, 0.7f);
        //                break;
        //            case "gun6":
        //                go.transform.GetChild(0).localScale = new Vector3(0.7f, 0.7f);
        //                break;
        //            case "gun7":
        //                go.transform.GetChild(0).localScale = new Vector3(0.7f, 0.7f);
        //                break;
        //            case "gun8":
        //                go.transform.GetChild(0).localScale = new Vector3(0.7f, 0.7f);
        //                break;
        //            case "gun9":
        //                go.transform.GetChild(0).localScale = new Vector3(0.7f, 0.7f);
        //                break;
        //            case "gun10":
        //                go.transform.GetChild(0).localScale = new Vector3(0.7f, 0.7f);
        //                break;
        //            case "gun11":
        //                go.transform.GetChild(0).localScale = new Vector3(0.7f, 0.7f);
        //                break;
        //            case "gun12":
        //                go.transform.GetChild(0).localScale = new Vector3(0.7f, 0.7f);
        //                break;
        //            case "gun13":
        //                go.transform.GetChild(0).localScale = new Vector3(0.7f, 0.7f);
        //                break;
        //            case "gun14":
        //                go.transform.GetChild(0).localScale = new Vector3(0.7f, 0.7f);
        //                break;
        //            case "gun15":
        //                go.transform.GetChild(0).localScale = new Vector3(0.7f, 0.7f);
        //                break;
        //            case "gun16":
        //                go.transform.GetChild(0).localScale = new Vector3(0.7f, 0.7f);
        //                break;
        //            case "gun17":
        //                go.transform.GetChild(0).localScale = new Vector3(0.7f, 0.7f);
        //                break;
        //            case "gun18":
        //                go.transform.GetChild(0).localScale = new Vector3(0.7f, 0.7f);
        //                break;
        //            case "gun19":
        //                go.transform.GetChild(0).localScale = new Vector3(0.7f, 0.7f);
        //                break;
        //            case "gun20":
        //                go.transform.GetChild(0).localScale = new Vector3(0.7f, 0.7f);
        //                break;
        //            case "gun21":
        //                go.transform.GetChild(0).localScale = new Vector3(0.7f, 0.7f);
        //                break;
        //            case "gun22":
        //                go.transform.GetChild(0).localScale = new Vector3(0.7f, 0.7f);
        //                break;
        //            case "gun23":
        //                go.transform.GetChild(0).localScale = new Vector3(0.7f, 0.7f);
        //                break;
        //            case "gun24":
        //                go.transform.GetChild(0).localScale = new Vector3(0.7f, 0.7f);
        //                break;
        //            case "gun25":
        //                go.transform.GetChild(0).localScale = new Vector3(0.7f, 0.7f);
        //                break;
        //            default:
        //                go.transform.GetChild(0).localScale = new Vector3(0.7f, 0.7f);
        //                break;
        //        }
        //        go.transform.GetChild(0).gameObject.tag = "gun";
        //        break;
        //    }
        //}
    


}

    private void OnApplicationPause(bool pause) //手机上的保存方法
    {
        if (pause)
        {
            SaveData();
        }
    }


    private void OnApplicationQuit()
    {
        SaveData();

    }

}