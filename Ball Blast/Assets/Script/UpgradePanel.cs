// UpgradePanel
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanel : MonoBehaviour
{
    public Button atkSpeed;  //攻击速度按钮

    public Button damage;  //伤害按钮

    public Button money;   //增加金钱收益按钮

    public Button leavemoney;  //增加离线金币收益

    public Button buyButton;

    public Text nameText;  //按钮名的文本

    public Text coinText;

    public Text coin;

    private string upgradeName; //按钮名字

    public Sprite select; //选中状态图标

    public Sprite unSelect;  //未选中状态图标

    public float Damage;  //伤害值 

    public float Money; //钱

    public float LeaveMoney;//离线金钱



    private void OnEnable()
    {

        AtkspeedButton();  //默认进入游戏显示的是升级攻击速度的按钮 （先刷新按钮状态）

        upgradeName = "atk";    //通过名字区分是哪个按钮

        nameText.text = "Fire Speed\n" + ((int)PlayerPrefs.GetFloat("bullet per second")).ToString() + " bps";  //显示当前按钮的本内容


        if (PlayerPrefs.GetInt("bps money") < 1000)  //显示按钮上的钱
        {
            coinText.text = PlayerPrefs.GetInt("bps money").ToString();
        }
        else
        {
            coinText.text =((double)PlayerPrefs.GetInt("bps money")).ToShortString();

        }


        if (PlayerPrefs.GetInt("high score") >= PlayerPrefs.GetInt("bps money"))     //有足够的钱升级 按钮可交互
        {
            buyButton.interactable = true;
        }
        else
        {
            buyButton.interactable = false;
        }
        if (PlayerPrefs.GetInt("bullet per second") < 50) //取消速度 上限
        {
            buyButton.interactable = true;
        }
        else
        {
            buyButton.interactable = false;
        }
    }

    public void AtkspeedButton()
    {

        upgradeName = "atk";

        atkSpeed.GetComponent<Image>().overrideSprite = select;  //默认选中升级攻击速度的按钮 其余按钮图标都显示没有选中的状态
        damage.GetComponent<Image>().overrideSprite = unSelect;
        money.GetComponent<Image>().overrideSprite = unSelect;
        leavemoney.GetComponent<Image>().overrideSprite = unSelect;

        nameText.text = "Fire Speed\n" + ((int)PlayerPrefs.GetFloat("bullet per second")).ToString() + " bps";


        if (PlayerPrefs.GetInt("bps money") < 1000)
        {
            coinText.text = PlayerPrefs.GetInt("bps money").ToString();
        }
        else
        {
            coinText.text =((double)PlayerPrefs.GetInt("bps money")).ToShortString();

        }
        if (PlayerPrefs.GetInt("high score") < 1000)
        {
            coin.text = PlayerPrefs.GetInt("high score").ToString();
        }
        else
        {
            coin.text ="Money: " +((double)PlayerPrefs.GetInt("high score")).ToShortString();
        }


        if (PlayerPrefs.GetInt("high score") >= PlayerPrefs.GetInt("bps money"))
        {
        //    Debug.Log(PlayerPrefs.GetInt("coin"));

            buyButton.interactable = true;
        }
        else
        {
            buyButton.interactable = false;
        }
        if (PlayerPrefs.GetInt("bullet per second") <50)  //攻击速度上限限制
        {

            buyButton.interactable = true;
        }
        else
        {
            buyButton.interactable = false;
        }
    }

    public void DamageButton()
    {
        upgradeName = "dmg";
        Damage = Mathf.Round(PlayerPrefs.GetFloat("damage") * 10f); //四舍五入 取整

        damage.GetComponent<Image>().overrideSprite = select;
        atkSpeed.GetComponent<Image>().overrideSprite = unSelect;
        money.GetComponent<Image>().overrideSprite = unSelect;
        leavemoney.GetComponent<Image>().overrideSprite = unSelect;

        nameText.text = "Damage\n" + (Damage * 10f).ToString() + "%";    //*10是按百分比显示

        if (PlayerPrefs.GetInt("damage money") < 1000)
        {
            coinText.text = PlayerPrefs.GetInt("damage money").ToString();
        }
        else
        {
            coinText.text = ((double)PlayerPrefs.GetInt("damage money")).ToShortString();
        }
        if (PlayerPrefs.GetInt("high score") < 1000)
        {
            coin.text = PlayerPrefs.GetInt("high score").ToString();
        }
        else
        {
            coin.text = "Money: " + ((double)PlayerPrefs.GetInt("high score")).ToShortString();
        }


        if (PlayerPrefs.GetInt("high score") >= PlayerPrefs.GetInt("damage money"))// coin 玩家金钱
        {
            buyButton.interactable = true;
        }
        else
        {
            buyButton.interactable = false;
        }
    }

    public void CoinButton()  //金钱收益升级
    {
        upgradeName = "coin";
        Money = Mathf.Round(PlayerPrefs.GetFloat("money") * 10f);  //获取数值

        money.GetComponent<Image>().overrideSprite = select;
        atkSpeed.GetComponent<Image>().overrideSprite = unSelect;
        damage.GetComponent<Image>().overrideSprite = unSelect;
        leavemoney.GetComponent<Image>().overrideSprite = unSelect;

        nameText.text = "Coin\n" + (Money * 10f).ToString() + "%";  //按百分比显示

        if (PlayerPrefs.GetInt("make money") < 1000)
        {
            coinText.text = PlayerPrefs.GetInt("make money").ToString();
        }
        else
        {
            coinText.text = ((double)PlayerPrefs.GetInt("make money")).ToShortString();
        }

        if (PlayerPrefs.GetInt("high score") < 1000)
        {
            coin.text = PlayerPrefs.GetInt("high score").ToString();
        }
        else
        {
            coin.text = "Money: " + ((double)PlayerPrefs.GetInt("high score")).ToShortString();
        }

        if (PlayerPrefs.GetInt("high score") >=PlayerPrefs.GetInt("make money"))
        {
            buyButton.interactable = true;

        }
        else
        {
            buyButton.interactable = false;
        }
    }
    //离线金钱收益升级
    public void LeaveCoinButton()
    {
        upgradeName = "leavecoin";  //离线钱

        LeaveMoney = Mathf.Round(PlayerPrefs.GetFloat("money2") * 10f);
        Debug.Log(LeaveMoney);

        leavemoney.GetComponent<Image>().overrideSprite = select; //将按钮图标换成选中状态
        money.GetComponent<Image>().overrideSprite = unSelect;
        atkSpeed.GetComponent<Image>().overrideSprite = unSelect;
        damage.GetComponent<Image>().overrideSprite = unSelect;
        nameText.text = "offline earnings\n" + (LeaveMoney * 10f).ToString() + "%";
        if (PlayerPrefs.GetInt("leave money") < 1000)
        {
            coinText.text = PlayerPrefs.GetInt("leave money").ToString();
        }
        else
        {
            coinText.text = ((double)PlayerPrefs.GetInt("leave money")).ToShortString();
        }

        if (PlayerPrefs.GetInt("high score") < 1000)
        {
            coin.text = PlayerPrefs.GetInt("high score").ToString();
        }
        else
        {
            coin.text = "Money: " + ((double)PlayerPrefs.GetInt("high score")).ToShortString();
        }

        if (PlayerPrefs.GetInt("high score") >=PlayerPrefs.GetInt("leave money"))
        {
            buyButton.interactable = true;
        }
        else
        {
            buyButton.interactable = false;
        }

    }

    public void BuyButton() //购买按钮
    {
        if (upgradeName == "atk" && PlayerPrefs.GetInt("high score") >= PlayerPrefs.GetInt("bps money"))
        {
            PlayerprefController.AddFloatValue("bullet per second",0.2f); //增加升级后效果
            PlayerprefController.AddIntValue("high score", -PlayerPrefs.GetInt("bps money"));//当前金钱减去  升级所需要的钱（读取的xx）
            PlayerprefController.AddIntValue("bps money", PlayerPrefs.GetInt("bps money") / 15);  //升级的金钱每次都会增长十分之一


            nameText.text = "Fire Speed\n" +((int)PlayerPrefs.GetFloat("bullet per second")).ToString()+ " bps";

            if (PlayerPrefs.GetInt("bps money") < 1000)
            {
                coinText.text = PlayerPrefs.GetInt("bps money").ToString();

            }
            else
            {
                coinText.text = ((double)PlayerPrefs.GetInt("bps money")).ToShortString();
            }
            //  coinText.text = PlayerPrefs.GetInt("bps money").ToString();

            if (PlayerPrefs.GetInt("high score") < 1000)
            {
                coin.text = PlayerPrefs.GetInt("high score").ToString();
            }
            else
            {
                coin.text = "Money: " + ((double)PlayerPrefs.GetInt("high score")).ToShortString();
            }

            //  coin.text = PlayerPrefs.GetInt("coin").ToString();

        }
        else if (upgradeName == "dmg" && PlayerPrefs.GetInt("high score") >= PlayerPrefs.GetInt("damage money"))
        {
            PlayerprefController.AddFloatValue("damage", 0.1f); //每次升级增加0.1伤害？
            Damage = Mathf.Round(PlayerPrefs.GetFloat("damage") * 10f);
            PlayerprefController.AddIntValue("high score", -PlayerPrefs.GetInt("damage money"));
            PlayerprefController.AddIntValue("damage money", PlayerPrefs.GetInt("damage money") / 10);


            nameText.text = "Damage\n" + (Damage * 10f).ToString() + "%";

            //	coinText.text = PlayerPrefs.GetInt("damage money").ToString();
            if (PlayerPrefs.GetInt("damage money") < 1000)
            {
                coinText.text = PlayerPrefs.GetInt("damage money").ToString();
            }
            else
            {
                coinText.text = ((double)PlayerPrefs.GetInt("damage money")).ToShortString();
            }
            if (PlayerPrefs.GetInt("high score") < 1000)
            {
                coin.text = PlayerPrefs.GetInt("high score").ToString();
            }
            else
            {
                coin.text = "Money: " + ((double)PlayerPrefs.GetInt("high score")).ToShortString();
            }

        }
        else if (upgradeName == "coin" && PlayerPrefs.GetInt("high score") >= PlayerPrefs.GetInt("make money"))  //金钱收益升级
        {
            PlayerprefController.AddFloatValue("money", 0.2f);   //每升一级增加0.1的收入效果
            Debug.Log(PlayerPrefs.GetFloat("money"));
            Money = Mathf.Round(PlayerPrefs.GetFloat("money") * 10f);
            PlayerprefController.AddIntValue("high score", -PlayerPrefs.GetInt("make money"));
            PlayerprefController.AddIntValue("make money", PlayerPrefs.GetInt("make money") / 10);

            nameText.text = "Coin\n" + (Money * 10f).ToString() + "%";
            if (PlayerPrefs.GetInt("make money") < 1000)
            {
                coinText.text = PlayerPrefs.GetInt("make money").ToString();
            }
            else
            {
                coinText.text = ((double)PlayerPrefs.GetInt("make money")).ToShortString();
            }



            if (PlayerPrefs.GetInt("high score") < 1000)
            {
                coin.text = PlayerPrefs.GetInt("high score").ToString();
            }
            else
            {
                coin.text = "Money: " + ((double)PlayerPrefs.GetInt("high score")).ToShortString();
            }

        }
        //离线金钱收益升级
        else if (upgradeName == "leavecoin" && PlayerPrefs.GetInt("high score") >= PlayerPrefs.GetInt("leave money"))
        {
            PlayerprefController.AddFloatValue("money2", 0.1f);

            LeaveMoney = Mathf.Round(PlayerPrefs.GetFloat("money2") * 10f);

            PlayerprefController.AddIntValue("high score", -PlayerPrefs.GetInt("leave money"));

            PlayerprefController.AddIntValue("leave money", PlayerPrefs.GetInt("leave money") / 10);

            nameText.text = "offline earnings\n" + (LeaveMoney * 10f).ToString() + "%";



            if (PlayerPrefs.GetInt("leave money") < 1000)
            {

                coinText.text = PlayerPrefs.GetInt("leave money").ToString();

            }
            else
            {
                coinText.text = ((double)PlayerPrefs.GetInt("leave money")).ToShortString();

            }

            if (PlayerPrefs.GetInt("high score") < 1000)
            {
                coin.text = PlayerPrefs.GetInt("high score").ToString();
            }
            else
            {
                coin.text = "Money: " + ((double)PlayerPrefs.GetInt("high score")).ToShortString();
            }

        }
    }

    
}
