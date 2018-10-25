// Number
using UnityEngine;
using UnityEngine.UI;

public class Number : MonoBehaviour
{
    public static Number instance;



    private Text tm;

    private int hpNumber;  //生命值数字显示

    private void Awake()
    {
      
        tm = this.transform.GetComponent<Text>();
    }

    public void ChangeValue(int value)//更改hp值
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

    public void SetHpText(int hp)  //设置hp文字
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

    public string ConvertToK(int number)//将大数字转换为K
    {
        float num = number / 1000f;
        float num2 = (num - (float)(number / 1000)) * 10f;
        return (number / 1000).ToString("D") + "." + ((int)num2).ToString() + "k";
    }





}
