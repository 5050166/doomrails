﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataManager : MonoBehaviour   //不继承自mon 可以不用挂在场景物体中
{
    private string PREFS_BOX = "BOX";

    private string PREFS_ITEMBOX = "ITEMBOX";   //商店

    public GameObject go;   //盒子存放

    GameObject itembox;

    public List<Box> bo = new List<Box>();//用来存储所有的box

    public List<Box> boxes = new List<Box>();

    public List<ItemLock> ItemBox = new List<ItemLock>();  //存储所有的ItemBox

    public List<Vector3> boposition = new List<Vector3>();

    public string[] frame;

    public DateTime Offlinetime;

    private static DataManager instance;

    public static DataManager Instance
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


    public void Awake()
    {
        instance = this;
        AddBox();
    }
    private void Start()
    {
        frame = new string[8];
        if (ES3.FileExists("SaveData.es3"))
        {
            LoadAllFrame();
        }

    }

    public void AddBox() //添加所有盒子
    {
        go = GameObject.Find("player/upmenu").gameObject;
        for (int i = 0; i < go.transform.childCount; i++)
        {
            bo.Add(go.transform.GetChild(i).GetComponent<Box>());
            boposition.Add(bo[i].transform.localPosition);  //并没考虑到格子移出去的情况所以需要重新遍历一遍
            //Debug.Log(i);
        }
    }

    public void SaveAllFrame()
    {
        //存储所有的框架 记录下名字

        for (int i = 0; i < frame.Length; i++)
        {
            frame[i] = bo[i].transform.GetChild(2).GetComponent<Image>().sprite.name;
            //Debug.Log(frame[i]);
        }

        ES3.Save<string[]>("frame", frame);
    }

    public void LoadAllFrame()
    {
        if (ES3.KeyExists("frame"))
        {
            frame = ES3.Load<string[]>("frame");

        }
    }

    public void savebox()
    {
        ES3.Save<List<Box>>("box", bo);
    }

    public void loadbox()
    {
        if (ES3.FileExists("SaveData.es3"))
        {
            if (ES3.KeyExists("box"))
            {
                boxes = ES3.Load<List<Box>>("box");

            }
            else //没有存储
            {
                MainMenuUI.Instance.gun1.GetComponent<Box>().Number = "1";
                MainMenuUI.Instance.gun1.tag = "gun1";
                MainMenuUI.Instance.gun2.GetComponent<Box>().Number = "1";
                MainMenuUI.Instance.gun2.tag = "gun1";
                MainMenuUI.Instance.gun3.GetComponent<Box>().Number = "39";
                MainMenuUI.Instance.gun3.tag = "gun39";
                MainMenuUI.Instance.gun4.GetComponent<Box>().Number = "39";
                MainMenuUI.Instance.gun4.tag = "gun39";
            }
        }

        if (boxes.Count > 0)
        {
            //读取信息
            for (int i = 0; i < go.transform.childCount; i++)
            {
                Debug.Log(bo[i].Number);
                bo[i].Number = boxes[i].Number;
            }
        }
        else
        {
            for (int i = 0; i < go.transform.childCount; i++)
            {
                bo[i].Number = " ";
            }//没有获取到存档的时候为防止出错 

            MainMenuUI.Instance.gun1.GetComponent<Box>().Number = "1";
            MainMenuUI.Instance.gun1.tag = "gun1";
            MainMenuUI.Instance.gun2.GetComponent<Box>().Number = "1";
            MainMenuUI.Instance.gun2.tag = "gun1";
            MainMenuUI.Instance.gun3.GetComponent<Box>().Number = "39";
            MainMenuUI.Instance.gun3.tag = "gun39";
            MainMenuUI.Instance.gun4.GetComponent<Box>().Number = "39";
            MainMenuUI.Instance.gun4.tag = "gun39";
        }


    }

    public void setCurrentLevel(int level)
    {
        PlayerPrefs.SetInt("Level", level);   //存
    }

    public int getCurrentLevel()
    {
        return PlayerPrefs.GetInt("Level");       //读
    }

    // public void SetSaveItemBox(List<ItemLock> boxes) //存box信息
    //{
    //    string text = string.Empty;
    //    foreach (ItemLock item in boxes)
    //    {
    //        string texte2 = text;
    //        text = string.Concat(new object[] {
    //            texte2,item.Number,"#"

    //        });

    //    }
    //    if (text.Length > 0)
    //    {
    //        text = text.Substring(0, text.Length - 1);
    //    }

    //    PlayerPrefs.SetString(PREFS_ITEMBOX, text); //存储信息
    //}

    //public List<string> GetItemBox()//读取box信息
    //{
    //    List<string> list = new List<string>();
    //    string @string = PlayerPrefs.GetString(this.PREFS_ITEMBOX);
    //    if (@string == null || @string.Length == 0)
    //    {
    //        return new List<string>();
    //    }
    //    string[] arry = @string.Split(new char[] {
    //        '#'
    //    });
    //    for (int i = 0; i < arry.Length; i++)
    //    {
    //        string a = arry[i];
    //        //Debug.Log(a);
    //        list.Add(a);
    //    }
    //    return list;

    //}



    //public void SetSaveBox(List<Box> boxes) //存box信息
    //{
    //    string text = string.Empty;
    //    foreach (Box item in boxes)
    //    {
    //        string texte2 = text;
    //        text = string.Concat(new object[] {
    //            texte2,item.Number,"#"

    //        });

    //    }
    //    if (text.Length > 0)
    //    {
    //        text = text.Substring(0, text.Length - 1);
    //    }

    //    PlayerPrefs.SetString(PREFS_BOX, text); //存储信息
    //    Debug.Log(text);

    //}

    //public List<string> GetBox()//读取box信息
    //{
    //    List<string> list = new List<string>();
    //    string @string = PlayerPrefs.GetString(this.PREFS_BOX);
    //    if (@string == null || @string.Length == 0)
    //    {
    //        return new List<string>();
    //    }
    //    string[] arry = @string.Split(new char[] {
    //        '#'
    //    });
    //    for (int i = 0; i < arry.Length; i++)
    //    {
    //        string a = arry[i];
    //        list.Add(a);
    //    }
    //    return list;


    //}

    //public void LoadBox() //读取box number
    //{
    //    List<string> box = GetBox();
    //    if (box.Count > 0)
    //    {//读取信息
    //        for (int i = 0; i < go.transform.childCount; i++)
    //        {
    //            bo[i].Number = box[i];
    //        }
    //    }
    //    else
    //    {
    //        for (int i = 0; i < go.transform.childCount; i++)
    //        {
    //            bo[i].Number = " ";
    //        }
    //    }
    //}

    public string FormatTwoTime(int totalSeconds)//显示分钟
    {
        int minutes = totalSeconds / 60;

        string mm = minutes < 10f ? "0" + minutes : minutes.ToString();
        int seconds = (totalSeconds - (minutes * 60));
        string ss = seconds < 10 ? "0" + seconds : seconds.ToString();
        return string.Format("{0}:{1}", mm, ss);
    }

    public string FormatTwoTimeForhour(int totalSeconds)  //显示小时
    {
        int hours = totalSeconds / 3600;
        string hh = hours < 10 ? "0" + hours : hours.ToString();
        int minutes = (totalSeconds - hours * 3600) / 60;
        string mm = minutes < 10f ? "0" + minutes : minutes.ToString();
        int seconds = totalSeconds - hours * 3600 - minutes * 60;
        string ss = seconds < 10 ? "0" + seconds : seconds.ToString();
        return string.Format("{0}:{1}:{2}", hh, mm, ss);
    }

    public void SaveOfflineTime()//存储退出时间
    {
        ES3.Save<DateTime>("offtime", DateTime.Now);

    }

    private void OnApplicationQuit()
    {
        savebox();
        SaveAllFrame();


        if (EndUI.Instance.cansave)
        {
            EndUI.Instance.SaveDeathModTIme();
        }
        else
        {
            ES3.Save<int>("DeathModTime", GameMod.Instance.number);
        }
        SaveOfflineTime();

    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            savebox();

            SaveAllFrame();

            if (EndUI.Instance.cansave)
            {
                EndUI.Instance.SaveDeathModTIme();
            }
            else
            {
                ES3.Save<int>("DeathModTime", GameMod.Instance.number);
            }
            SaveOfflineTime();//存储离线时间

        }
        else
        {
            //恢复游戏时检测时间
            if (GameMod.Instance.number < 0)
            {
                StopCoroutine(GameMod.Instance.ColdTimes());
                GameMod.Instance.ColdTime.text = "coin x 3";
                GameMod.Instance.DeathModButton.interactable = true;

            }



        }

    }


}





