// Number
using UnityEngine;
using UnityEngine.UI;

public class Number : MonoBehaviour
{
    public static Number instance;



    private Text tm;

    private int hpNumber;  //����ֵ������ʾ

    private void Awake()
    {
      
        tm = this.transform.GetComponent<Text>();
    }

    public void ChangeValue(int value)//����hpֵ
    {
        hpNumber -= value;
        if (hpNumber < 1000)
        {
            tm.text = hpNumber.ToString();
        }
        else
        {
            tm.text = ConvertToK(hpNumber);
        }
    }

    public void SetHpText(int hp)  //����hp����
    {
        hpNumber = hp;
        if (hp < 1000)
        {
            tm.text = hpNumber.ToString();
        }
        else
        {
            tm.text = ConvertToK(hp);
        }
    }

    public string ConvertToK(int number)//��������ת��ΪK
    {
        float num = number / 1000f;
        float num2 = (num - (float)(number / 1000)) * 10f;
        return (number / 1000).ToString("D") + "." + ((int)num2).ToString() + "k";
    }





}
