// UpgradePanel
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanel : MonoBehaviour
{
    public Button atkSpeed;  //�����ٶȰ�ť

    public Button damage;  //�˺���ť

    public Button money;   //���ӽ�Ǯ���水ť

    public Button leavemoney;  //�������߽������

    public Button buyButton;

    public Text nameText;  //��ť�����ı�

    public Text coinText;

    public Text coin;

    private string upgradeName; //��ť����

    public Sprite select; //ѡ��״̬ͼ��

    public Sprite unSelect;  //δѡ��״̬ͼ��

    public float Damage;  //�˺�ֵ 

    public float Money; //Ǯ

    public float LeaveMoney;//���߽�Ǯ



    private void OnEnable()
    {

        AtkspeedButton();  //Ĭ�Ͻ�����Ϸ��ʾ�������������ٶȵİ�ť ����ˢ�°�ť״̬��

        upgradeName = "atk";    //ͨ�������������ĸ���ť

        nameText.text = "Fire Speed\n" + ((int)PlayerPrefs.GetFloat("bullet per second")).ToString() + " bps";  //��ʾ��ǰ��ť�ı�����


        if (PlayerPrefs.GetInt("bps money") < 1000)  //��ʾ��ť�ϵ�Ǯ
        {
            coinText.text = PlayerPrefs.GetInt("bps money").ToString();
        }
        else
        {
            coinText.text =((double)PlayerPrefs.GetInt("bps money")).ToShortString();

        }


        if (PlayerPrefs.GetInt("high score") >= PlayerPrefs.GetInt("bps money"))     //���㹻��Ǯ���� ��ť�ɽ���
        {
            buyButton.interactable = true;
        }
        else
        {
            buyButton.interactable = false;
        }
        if (PlayerPrefs.GetInt("bullet per second") < 50) //ȡ���ٶ� ����
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

        atkSpeed.GetComponent<Image>().overrideSprite = select;  //Ĭ��ѡ�����������ٶȵİ�ť ���ఴťͼ�궼��ʾû��ѡ�е�״̬
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
        if (PlayerPrefs.GetInt("bullet per second") <50)  //�����ٶ���������
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
        Damage = Mathf.Round(PlayerPrefs.GetFloat("damage") * 10f); //�������� ȡ��

        damage.GetComponent<Image>().overrideSprite = select;
        atkSpeed.GetComponent<Image>().overrideSprite = unSelect;
        money.GetComponent<Image>().overrideSprite = unSelect;
        leavemoney.GetComponent<Image>().overrideSprite = unSelect;

        nameText.text = "Damage\n" + (Damage * 10f).ToString() + "%";    //*10�ǰ��ٷֱ���ʾ

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


        if (PlayerPrefs.GetInt("high score") >= PlayerPrefs.GetInt("damage money"))// coin ��ҽ�Ǯ
        {
            buyButton.interactable = true;
        }
        else
        {
            buyButton.interactable = false;
        }
    }

    public void CoinButton()  //��Ǯ��������
    {
        upgradeName = "coin";
        Money = Mathf.Round(PlayerPrefs.GetFloat("money") * 10f);  //��ȡ��ֵ

        money.GetComponent<Image>().overrideSprite = select;
        atkSpeed.GetComponent<Image>().overrideSprite = unSelect;
        damage.GetComponent<Image>().overrideSprite = unSelect;
        leavemoney.GetComponent<Image>().overrideSprite = unSelect;

        nameText.text = "Coin\n" + (Money * 10f).ToString() + "%";  //���ٷֱ���ʾ

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
    //���߽�Ǯ��������
    public void LeaveCoinButton()
    {
        upgradeName = "leavecoin";  //����Ǯ

        LeaveMoney = Mathf.Round(PlayerPrefs.GetFloat("money2") * 10f);
        Debug.Log(LeaveMoney);

        leavemoney.GetComponent<Image>().overrideSprite = select; //����ťͼ�껻��ѡ��״̬
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

    public void BuyButton() //����ť
    {
        if (upgradeName == "atk" && PlayerPrefs.GetInt("high score") >= PlayerPrefs.GetInt("bps money"))
        {
            PlayerprefController.AddFloatValue("bullet per second",0.2f); //����������Ч��
            PlayerprefController.AddIntValue("high score", -PlayerPrefs.GetInt("bps money"));//��ǰ��Ǯ��ȥ  ��������Ҫ��Ǯ����ȡ��xx��
            PlayerprefController.AddIntValue("bps money", PlayerPrefs.GetInt("bps money") / 15);  //�����Ľ�Ǯÿ�ζ�������ʮ��֮һ


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
            PlayerprefController.AddFloatValue("damage", 0.1f); //ÿ����������0.1�˺���
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
        else if (upgradeName == "coin" && PlayerPrefs.GetInt("high score") >= PlayerPrefs.GetInt("make money"))  //��Ǯ��������
        {
            PlayerprefController.AddFloatValue("money", 0.2f);   //ÿ��һ������0.1������Ч��
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
        //���߽�Ǯ��������
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
