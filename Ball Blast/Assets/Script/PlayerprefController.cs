// PlayerprefController
using System;
using UnityEngine;

public class PlayerprefController : MonoBehaviour  //储存游戏中的各项数值
{
    private void Awake()
    {
 
        if (!PlayerPrefs.HasKey("coin"))    //是否存在key（XXX）;
        {
            PlayerPrefs.SetInt("coin", 0);   //金币
        }
        if (!PlayerPrefs.HasKey("Level"))
        {
            PlayerPrefs.SetInt("Level", 0);   //关卡 
        }
        if (!PlayerPrefs.HasKey("high score"))
        {
            PlayerPrefs.SetInt("high score", 0);  //最高分
        }
        if (!PlayerPrefs.HasKey("new"))
        {
            PlayerPrefs.SetInt("new", 0);   //是否显示过新手引导
        }
        if (!PlayerPrefs.HasKey("first"))   //是否显示过
        {
            PlayerPrefs.SetInt("first", 0);
        }
        if (!PlayerPrefs.HasKey("HighTime"))
        {
            PlayerPrefs.SetInt("HighTime", 0);
        }
        if (!PlayerPrefs.HasKey("OfflineTime")) //退出游戏的时间
        {
            PlayerPrefs.SetInt("OfflineTime", 0);
        }
        if (!PlayerPrefs.HasKey("removead"))
        {
            PlayerPrefs.SetInt("removead", 0);
        }
        if (!PlayerPrefs.HasKey("life"))
        {
            PlayerPrefs.SetInt("life",10);
        }
        if (!PlayerPrefs.HasKey("gif"))
        {
            PlayerPrefs.SetInt("gif",0); //是否已经开了礼物 0 没开 1 开过了
        }

    }

    public static void AddIntValue(string str, int value) //添加整数数据
    {
        int value2 = PlayerPrefs.GetInt(str) + value;
        PlayerPrefs.SetInt(str, value2);
    }

    public static void AddFloatValue(string str, float value)  //添加浮点数据
    {
        float value2 = PlayerPrefs.GetFloat(str) + value;
        PlayerPrefs.SetFloat(str, value2);
    }
}
