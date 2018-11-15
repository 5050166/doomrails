// PlayerprefController
using System;
using UnityEngine;

public class PlayerprefController : MonoBehaviour  //������Ϸ�еĸ�����ֵ
{
    private void Awake()
    {
 
        if (!PlayerPrefs.HasKey("coin"))    //�Ƿ����key��XXX��;
        {
            PlayerPrefs.SetInt("coin", 0);   //���
        }
        if (!PlayerPrefs.HasKey("Level"))
        {
            PlayerPrefs.SetInt("Level", 0);   //�ؿ� 
        }
        if (!PlayerPrefs.HasKey("high score"))
        {
            PlayerPrefs.SetInt("high score", 0);  //��߷�
        }
        if (!PlayerPrefs.HasKey("new"))
        {
            PlayerPrefs.SetInt("new", 0);   //�Ƿ���ʾ����������
        }
        if (!PlayerPrefs.HasKey("first"))   //�Ƿ���ʾ��
        {
            PlayerPrefs.SetInt("first", 0);
        }
        if (!PlayerPrefs.HasKey("HighTime"))
        {
            PlayerPrefs.SetInt("HighTime", 0);
        }
        if (!PlayerPrefs.HasKey("OfflineTime")) //�˳���Ϸ��ʱ��
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
            PlayerPrefs.SetInt("gif",0); //�Ƿ��Ѿ��������� 0 û�� 1 ������
        }

    }

    public static void AddIntValue(string str, int value) //�����������
    {
        int value2 = PlayerPrefs.GetInt(str) + value;
        PlayerPrefs.SetInt(str, value2);
    }

    public static void AddFloatValue(string str, float value)  //��Ӹ�������
    {
        float value2 = PlayerPrefs.GetFloat(str) + value;
        PlayerPrefs.SetFloat(str, value2);
    }
}
