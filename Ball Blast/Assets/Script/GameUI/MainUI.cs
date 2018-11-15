// MainUI
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class MainUI : MonoBehaviour
{
    public GameObject spawnner;//���ɵ�

    public GameObject sold;

    private static MainUI instance;
    public Text coinText;
    public Text lifeText;//����ֵ

    public Text message;
    public bool isunlock = false;
    public GameObject shakeloop;
    public GameObject buymenu;  //����������Ϣ�� messagebox
    public GameObject Mainui;
    public GameObject tool;
    public GameObject upmenu;
    public GameObject go;
    public GameObject box;
    public AudioSource audioSource;
    public static MainUI Instance
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
    public Transform weaponitem;
    public GameObject buyui;
    public float tweentime;
    public Transform levelbox;


    private void Start()
    {
        //  MainMenuUI.Instance.IsHidenOrShow();
        shakeloop = this.transform.GetChild(0).gameObject;

        UpdateCoinText(); //������½��
                          //��������ֵ��Ϣ

        audioSource = this.transform.GetComponent<AudioSource>();
        //���������
        shakeloop.transform.DOScale(new Vector3(0.75f, 0.75f, 0.75f), 0.7f).SetLoops(-1, LoopType.Yoyo);
    }
    public void OnComplete(string name)
    {
        switch (name)
        {
            case "closetool":
                tool.SetActive(false);
                StopCoroutine("WaitSomeTimeToFinishTween");
                break;
            case "Opentool":
                tool.SetActive(true);
                StopCoroutine("WaitSomeTimeToFinishTween");
                break;
        }
    }

    IEnumerator WaitSomeTimeToFinishTween(string name, float waittime)
    {
        yield return new WaitForSeconds(waittime);
        OnComplete(name);
    }

    private void OnDisable()
    {
        //���غ�����
    }
    private void Awake()
    {

        UiFade();

        Game_Controller.isPaused = true;   //����Ϸ����ֱ�ӿ�ʼ
        instance = this;
    }

    public void UpdateLifeText() //��������ֵ���ı�
    {
        lifeText.text = "X " + PlayerPrefs.GetInt("life").ToString();
    }



    public void UpdateCoinText() //���
    {
        coinText.text = "X " + PlayerPrefs.GetInt("coin").ToString();
    }
    public void PauseMod() //��ͣ����ģʽ
    {
        AudioManager.Instance.source.PlayOneShot(AudioManager.Instance.ClickSound);

        if (this.gameObject.transform.localScale.y > 0) //��⿪ʼui�Ŀ���״̬  ui���ؿ��Ŵ���һ��������Ϸ�ĵȴ���ʼ���棡
        {
            upmenu.SetActive(true);  //�򿪽���������

            if (tool.activeSelf)  //����ǿ��ŵ�   
            {
                WeponShop.Instance.WeaponPrice.transform.parent.gameObject.SetActive(false);
                tool.transform.DOScale(0, tweentime);  //�رս����˵�
                StartCoroutine(WaitSomeTimeToFinishTween("closetool", tweentime)); //����Э�̵ȴ��趨�Ķ���ʱ���Ժ��ڵ���ȡ��Э�� 
            }
            else  //����ǹ��ŵ�
            {
                tool.SetActive(true);
                tool.transform.DOScale(1, tweentime);

                StartCoroutine(WaitSomeTimeToFinishTween("Opentool", tweentime));
            }
            spawnner.gameObject.SetActive(false);//ֹͣ����
            //Game_Controller.isPaused = true;
            //Game_Controller.isEnd = true;  //��ͣ�ƶ��Ϳ���
        }
        else
        {
            upmenu.SetActive(true);
            //��Ϸû�н�������������Ե������ ���ڹ��ص�ʱ��
            if (tool.activeSelf)  //����ǿ��ŵ�   
            {
                tool.transform.DOScale(0, tweentime);  //�رս����˵�
                StartCoroutine(WaitSomeTimeToFinishTween("closetool", tweentime)); //����Э�̵ȴ��趨�Ķ���ʱ���Ժ��ڵ���ȡ��Э�� 
            }
            else  //����ǹ��ŵ�
            {
                Debug.LogError("1");
                tool.SetActive(true);
                tool.transform.DOScale(1, tweentime);

                StartCoroutine(WaitSomeTimeToFinishTween("Opentool", tweentime));
            }
        }
    }


    public void StartGame()//��ʼ��ť ҲӦ�ð�����Ϸģʽ������
    {
        switch (GameMod.Instance.GameMods)
        {
            case "LevelMod":
                //�ؿ�ģʽ
                if (PlayerPrefs.GetInt("life") > 0)
                {
                    buyui.SetActive(false);
                    GameMod.Instance.AdButton.transform.DOScale(0, 0.2f);
                    StartCoroutine(GameMod.Instance.wait());
                    GameMod.Instance.Homebutton.transform.DOScale(0, 0.2f);
                    levelbox.gameObject.SetActive(false);
                    Game_Controller.isEnd = false;
                    //��ʼ��ť
                    AudioManager.Instance.source.PlayOneShot(AudioManager.Instance.ClickSound);
                    BuffSystem.Instance.Setbuff();
                    BuffSystem.Instance.RefreshAllBox();
                    BuffSystem.Instance.restnumber();
                    Game_Controller.isPaused = false;  //�����ƶ�����
                    sold.transform.DOScaleY(0, 0.3f);                                      //���س��۰�ť
                    weaponitem.DOScaleY(0, 0f);                                         //������������
                    this.transform.DOScaleY(0f, 0.3f);                                   //������û�رն��ǵ�����scalֵ
                    upmenu.SetActive(false);
                    MainMenuUI.Instance.slider.gameObject.SetActive(true);             //��ʾ������
                    MainMenuUI.Instance.slidertext.text = "LV. " + PlayerPrefs.GetInt("Level");
                    spawnner.SetActive(true); //��ʼ���ɵ���
                    SpawnBall.instance.currentlevelball = 0;
                    GameMod.Instance.Isplayerdie = false;
                    MainMenuUI.Instance.slider.onValueChanged.AddListener(delegate   //��Ӽ���
                    {
                        MainMenuUI.Instance.CheckProgressBar();
                    });
                    Background.Instance.Move();


                    if (MainMenuUI.Instance.Gift.activeSelf) //���غ���
                    {
                        PlayerPrefs.SetInt("gif", 1);
                        MainMenuUI.Instance.Gift.SetActive(false);//���������ӻ��ǿ��ŵľ����غ���

                    }
                }
                else
                {
                    GameMod.Instance.nolife.transform.DOScale(1, 0.2f);
                    AudioManager.Instance.source.PlayOneShot(AudioManager.Instance.BuildFail);
                }

                break;

            case "TimeMod":
                //ʱ��ģʽ


                Game_Controller.isEnd = false;
                AudioManager.Instance.source.PlayOneShot(AudioManager.Instance.ClickSound);
                BuffSystem.Instance.Setbuff();
                BuffSystem.Instance.RefreshAllBox();
                BuffSystem.Instance.restnumber();
                Game_Controller.isPaused = false;  //�����ƶ�����

                buyui.SetActive(false);
                sold.transform.DOScaleY(0, 0.3f);                                      //���س��۰�ť
                weaponitem.DOScaleY(0, 0f);                                         //������������
                this.transform.DOScaleY(0f, 0.3f);                                   //������û�رն��ǵ�����scalֵ
                upmenu.SetActive(false);
                MainMenuUI.Instance.SetProgressBar();
                spawnner.SetActive(true); //��ʼ���ɵ���

                break;
            case "DeathMod":
                //����ģʽ

                Game_Controller.isEnd = false;
                AudioManager.Instance.source.PlayOneShot(AudioManager.Instance.ClickSound);
                BuffSystem.Instance.Setbuff();
                BuffSystem.Instance.RefreshAllBox();
                BuffSystem.Instance.restnumber();
                Game_Controller.isPaused = false;                                          //�����ƶ�����

                buyui.SetActive(false);
                sold.transform.DOScaleY(0, 0.3f);                                      //���س��۰�ť
                weaponitem.DOScaleY(0, 0f);                                         //������������
                this.transform.DOScaleY(0f, 0.3f);                                   //������û�رն��ǵ�����scalֵ
                upmenu.SetActive(false);
                spawnner.SetActive(true);                                                 //��ʼ���ɵ���

                break;
            default:
                break;
        }
        //  BuffSystem.Instance.Setbuff();
        //if (Background.Instance.IsFinish)
        //{
        //    Background.Instance.Move();//��ʼ�ƶ�����
        //}

    }

    public void Yesbutton()
    {
        if (PlayerPrefs.GetInt("coin") > box.GetComponent<Box>().Price)
        {
            audioSource.PlayOneShot(AudioManager.Instance.Click); //click 
            int num = PlayerPrefs.GetInt("coin") - box.GetComponent<Box>().Price;
            Debug.Log(num);
            PlayerPrefs.SetInt("coin", num);
            UpdateCoinText();

            isunlock = true;

            if (this.gameObject.activeSelf)
            {
                StartCoroutine(checkLock()); //�����ӵ�״̬
            }
            buymenu.SetActive(false);
        }
        else
        {
            //��ʾ��Ǯ����
            audioSource.PlayOneShot(AudioManager.Instance.BuildFail);
            message.text = "You Have No Enough Money....";
        }

    }
    bool Isunlock()
    {
        return isunlock;
    }


    public void Nobuttom()
    {
        isunlock = false;
        audioSource.PlayOneShot(AudioManager.Instance.Click);
        buymenu.transform.DOScale(0, 0.1f);
        buymenu.transform.DOScale(0, 0.1f).OnComplete(() => buymenu.SetActive(false));

    }

    public void Openbuymenu()
    {
        audioSource.PlayOneShot(AudioManager.Instance.Click); //click
        if (buymenu.activeSelf)
        {
            buymenu.transform.DOScale(0, 0.1f).OnComplete(() => buymenu.SetActive(false));

        }
        else
        {
            buymenu.SetActive(true);
            buymenu.transform.DOScale(1, 0.1f);
            go = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject; //��ȡ��ǰui�µ����
            box = go.transform.parent.gameObject;
            message.text = "Cost " + box.GetComponent<Box>().Price + " Unlock New Parts";
        }
    }
    IEnumerator checkLock()
    {
        yield return new WaitUntil(Isunlock); //ֱ��ĳ����ֵΪXX
        audioSource.PlayOneShot(AudioManager.Instance.BuyWeapon);
        go.SetActive(false);
        box.GetComponent<Box>().Number = "39"; //�����˷��� 
        box.gameObject.tag = "gun39"; //������
    }

    public void UiFade()
    {
        this.transform.GetChild(0).GetComponent<Text>().DOFade(1f, 1f).SetLoops(-1, LoopType.Yoyo);
    }

}
