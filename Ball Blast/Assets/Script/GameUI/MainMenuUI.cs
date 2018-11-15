// MainMenuUI  ��ʼ����
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    public GameObject endUI;   //�����Ľ���

    public GameObject spawner;   //С�����ɵ�

    public GameObject gun1;  //��ʼ��1

    public GameObject gun2;  //��ʼ��2

    public GameObject gun3;

    public GameObject gun4;

    public Transform coin1;

    public Transform coin2;

    public Transform coin3;

    public Transform coin4;

    public Transform coins;


    public Text coinText;    //���

    public Text Level;     //�ؿ�

    public Slider slider;

    public Text slidertext;  //slidertext

    public GameObject NextLevelUi;  //����ui���

    public GameObject bu;

    public GameObject finger; //������ָ    -714 -612

    public GameObject Gift;//�������

    public bool box;

    public Transform Canva;

    public List<int> gif = new List<int>();

    private static MainMenuUI instance;

    public int score { get; set; }   //����

    public int coin { get; set; }  //Ǯ

    public int level { get; set; } //�ȼ�

    public static MainMenuUI Instance
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
      
        Instance = this;
        DontDestroyOnLoad(this.transform);
        Canva = this.transform;
    }

    private void Start()
    {
 
        if (ES3.FileExists("SaveData.es3"))
        {
         gif = ES3.Load<List<int>>("gif");

        }
   
        LoadGifBox();
        StartCoroutine("Showboxtip");
        //��ӽ����ؿ�
 
    }
    public IEnumerator Showboxtip() {
        yield return new WaitUntil(Getcoins);

        PlayerController.Instance.boxtips.SetActive(true);

        PlayerController.Instance.boxtips.transform.DOScale(1.5f, 0.5f).SetLoops(-1, LoopType.Yoyo);
    }

    public bool Getcoins() {
        if (PlayerPrefs.GetInt("coin") > 299)
        {

            return true;
        }
        else
        {
            return false;
        }
    }




   

    private void Update()
    {
        if (GameMod.Instance.GameMods == "LevelMod")
        {
            if (this.slider.value == 1f)
            {
                if (MainUI.Instance.spawnner.activeSelf)
                {
                    MainUI.Instance.spawnner.SetActive(false); //ֹͣ����С��
                }
                if (Game_Controller.Instance.ballbox.childCount == 0 && Game_Controller.isEnd == false && GameMod.Instance.Isplayerdie == false) //����������С���Ҳ�������������������  ��������
                {
                    Game_Controller.Instance.goToNxtLevel(); //����
                   LoadGifBox();  //���ؼ�����

                    if (Random.Range(0, 100) > 70)
                    {
                        if (AdManager.Instance.interstitial.IsLoaded())
                        {
                            AdManager.Instance.interstitial.Show(); //չʾ�������

                        }
                    }
                }
            }
        }

    }

    //����ҵ�һ������Ϸ�Ļ�
    public void IsFirstPlayGame()
    {
        if (PlayerPrefs.GetInt("Level") == 0 && PlayerPrefs.GetInt("coin") == 0)
        {
            //��ǰ�ǵ�һ�ν�����Ϸ
            gun1.GetComponent<Box>().Number = "1";
            gun1.tag = "gun1";
            gun2.GetComponent<Box>().Number = "1";
            gun2.tag = "gun1";
            gun3.GetComponent<Box>().Number = "39";
            gun3.tag = "gun39";
            gun4.GetComponent<Box>().Number = "39";
            gun4.tag = "gun39";

            PlayerPrefs.SetInt("life", 80);

            gif.Add(1);
            gif.Add(10);
            gif.Add(20);
        
        }
        else
        {
            // Debug.Log("Welcome back");
        }
    }
    private void OnEnable()
    {
        Level.text = " Level:" + (PlayerPrefs.GetInt("Level")).ToString();//��ȡ��ǰ�ؿ�
        coinText.text = "X" + PlayerPrefs.GetInt("coin").ToString();
        Game_Controller.secondChance = true;
    }
    public int AddCoin() //��Ǯ
    {
        int a = Random.Range(2, 10);
        switch (GameMod.Instance.GameMods)
        {
            case "LevelMod":
                PlayerprefController.AddIntValue("coin", a);  //��ȡcoin+��ǰ�˺�  
                break;
            case "TimeMod":
                PlayerprefController.AddIntValue("coin", a);  //��ȡcoin+��ǰ�˺�  
                break;
            case "DeathMod":
                PlayerprefController.AddIntValue("coin", (int)(a * 1.5f));  //����ģʽ3����Ǯ? 
                break;
            default:
                PlayerprefController.AddIntValue("coin", a);
                break;
        }
        coinText.text = "X  " + ((double)PlayerPrefs.GetInt("coin")).ToShortString();
        return a;
    }
    public void PauseButton() //��ͣ��ť
    {
        Game_Controller.isPaused = true;
    }
    public void OpenEndUI() //�����˵�
    {
        //this.gameObject.SetActive(false);          //�رտ�ʼ����
        spawner.SetActive(false);                  //ֹͣ����С�򣡣��趨������
                                                   //Game_Controller.isEnd = true;            //��Ϸ������ͣ����F
        Game_Controller.isPaused = true;
        endUI.transform.DOScale(1f, 0.3f);   //��ui
    }
    public void OpenSecondChanceUI() //����F
    {
        Game_Controller.isPaused = true;      //�����˶�����������ͣ
                                              //  secondChanceUI.SetActive(true);
    }
    public void UpdateCurrentLevel(int level)  //���¹ؿ��ȼ�
    {
        Level.text = "Level: " + level;       //����ʵʱ�ȼ�
    }
    public void UpdateCurrentCoin()   //���½�Ǯui
    {
        coinText.text = "X" + PlayerPrefs.GetInt("coin").ToString();
    }
    public void CheckProgressBar() //��Ӽ���
    {
        slider.value = SpawnBall.instance.Deadball / SpawnBall.instance.difficultNumber;
    }
    public void ShowNextLevelUi()
    {
        NextLevelUi.gameObject.SetActive(true);
        NextLevelUi.transform.GetChild(0).DOScaleY(1f, 0.8f);
        NextLevelUi.transform.GetChild(1).DOScaleY(1f, 1f);
        NextLevelUi.transform.DOShakeScale(0.4f);
    }
    public void SetProgressBar()  //���ý�����
    {
        this.slider.value = 0f; //����������
        slider.onValueChanged.RemoveAllListeners();
        slider.gameObject.SetActive(false);
    }
    public void OpenGift() //������Ӻ�
    {

        int b = PlayerPrefs.GetInt("Level");

        for (int i = 0; i < gif.Count; i++)  //�����ȷ��ɾ��list�б��е�ĳ��Ԫ��
        {
            if (gif[i]==b)
            {
                gif.Remove(gif[i]);
            }
        }
        GameObject go = Instantiate(Game_Controller.Instance.giftopen, new Vector3( Gift.transform.position.x, Gift.transform.position.y+0.1f, Gift.transform.position.z), Quaternion.identity);
        AudioManager.Instance.source.PlayOneShot(AudioManager.Instance.addlife);
      
        
        
        //Ʈ��

        GameObject TextPrefab = Instantiate(BuffSystem.Instance.TextPrefab, new Vector3(Gift.transform.position.x+0.1f, Gift.transform.position.y + 0.1f, Gift.transform.position.z), Quaternion.identity);
        TextPrefab.transform.SetParent(Gift.transform.parent.transform);
        TextPrefab.transform.localScale = Vector3.one;

        TextPrefab.GetComponent<CoinNumber>().setnumber(15);

        //��15����
        PlayerprefController.AddIntValue("life", 15);
        MainUI.Instance.UpdateLifeText();
        Gift.SetActive(false);
       
    }


    public void LoadGifBox() //�����������
    {
        //�������� �ﵽ�ؿ� 2 10 20 
        int a = PlayerPrefs.GetInt("Level");
        if (gif.Contains(a))
        {
            GameObject go = Instantiate(Game_Controller.Instance.giftshow, Gift.transform.position, Quaternion.identity);
            //��ʾ����
            Gift.SetActive(true);
        }
    }

    public void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            ES3.Save<List<int>>("gif", gif);
        }
    }
    public void OnApplicationQuit()
    {
        ES3.Save<List<int>>("gif", gif);
    }

}
