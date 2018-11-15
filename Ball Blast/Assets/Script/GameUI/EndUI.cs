// EndUI
using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class EndUI : MonoBehaviour
{
    public Color hidecolor = new Color(0f, 0f, 0f, 0f);
    public GameObject player;
    public double timeX = 0; //��ȴʱ��
    private static EndUI instance;
    public static EndUI Instance
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
    public float time = 0; //����ֵ�ָ�ʱ��
    private void Awake()
    {
        instance = this;
    }
    public int CD = 0;
    public bool cansave = false;//�Ƿ���Դ洢;

    public void AgainButton()
    {
        AudioManager.Instance.source.PlayOneShot(AudioManager.Instance.Click);
        for (int i = 0; i < DataManager.Instance.bo.Count; i++)
        {
            Destroy(DataManager.Instance.bo[i].GetComponent<Rigidbody2D>());
            DataManager.Instance.bo[i].GetComponent<BoxCollider2D>().isTrigger = true;
            var color = DataManager.Instance.bo[i].transform.GetChild(2).GetComponent<ParticleSystem>().main;
            color.startColor = new ParticleSystem.MinMaxGradient(hidecolor);
            DataManager.Instance.bo[i].transform.localPosition = DataManager.Instance.boposition[i];
            DataManager.Instance.bo[i].transform.rotation = Quaternion.Euler(0f, 0f, 0f); //���ýǶ�
        }

        this.player.transform.localPosition = new Vector3(0, -590f, 0);
        this.player.transform.GetComponent<BoxCollider2D>().offset = new Vector2(-50f, -70);

        switch (GameMod.Instance.GameMods)
        {
            case "LevelMod":
                if (PlayerPrefs.GetInt("removead") == 0)
                {
                    //if (Random.Range(0, 100) > 80)
                    //{
                    //    if (AdManager.Instance.rewardBasedVideo.IsLoaded())
                    //    {
                    //        AdManager.Instance.rewardBasedVideo.Show(); //չʾ�������

                    //    }
                    //}

                    if (Random.Range(0, 100) > 70)
                    {
                        if (AdManager.Instance.interstitial.IsLoaded())
                        {
                            AdManager.Instance.interstitial.Show(); //չʾ�������

                        }
                    }

                }


                GameMod.Instance.AdButton.transform.DOScale(1, 0.2f);
                Game_Controller.isPaused = true;   //��Ϸ�Ƿ����
                                                   // Game_Controller.isPaused = true;
                Game_Controller.isEnd = true; //���¿�ʼ����Ϸ ȥ������
                MainUI.Instance.spawnner.SetActive(false);
                MainUI.Instance.buyui.SetActive(true);
                MainUI.Instance.levelbox.gameObject.SetActive(true);
                LevelBox.Instance.CheckTheGrid();//����������box
                this.transform.DOScaleY(0f, 0.3f);
                MainUI.Instance.gameObject.transform.DOScaleY(1f, 0.3f);
                MainUI.Instance.weaponitem.DOScaleY(1f, 0f);
                MainUI.Instance.sold.transform.DOScaleY(1, 0.3f);
                Background.Instance.IsFinish = false;
                MainUI.Instance.upmenu.SetActive(true);
                MainUI.Instance.upmenu.transform.localPosition = Vector3.zero;
                MainMenuUI.Instance.SetProgressBar(); //�Ƴ�����
                SpawnBall.instance.Deadball = 0;
                MainMenuUI.Instance.slider.value = 0;
                GameMod.Instance.CurrentLevel.text = "Max  LV." + PlayerPrefs.GetInt("Level");
                GameMod.Instance.Homebutton.transform.DOScale(1, 0.2f);


                MainMenuUI.Instance.LoadGifBox();

                if (UnityEngine.Random.Range(0, 100) > 80)
                {
                    OpenShopmenu();
                }

                break;

            case "TimeMod":
                if (PlayerPrefs.GetInt("removead") == 0)
                {
                    //if (Random.Range(0, 100) > 80)
                    //{
                    //    if (AdManager.Instance.rewardBasedVideo.IsLoaded())
                    //    {
                    //        AdManager.Instance.rewardBasedVideo.Show(); //չʾ�������

                    //    }
                    //}

                    if (Random.Range(0, 100) >70)
                    {
                        if (AdManager.Instance.interstitial.IsLoaded())
                        {
                            AdManager.Instance.interstitial.Show(); //չʾ�������

                        }
                    }

                }
                GameMod.Instance.GameModBGM.Stop();
                GameMod.Instance.BGM.UnPause();

                Game_Controller.isPaused = true;
                Game_Controller.isEnd = true;
                MainUI.Instance.buyui.SetActive(true);

                GameMod.Instance.timetext.gameObject.SetActive(false);

                if (GameMod.Instance.time > PlayerPrefs.GetFloat("HighTime")) //�洢����ʱ��
                {
                    GameMod.Instance.CurrentTime.text = "Max Time:" + ((Mathf.Round(GameMod.Instance.time))).ToString() + "S";

                    PlayerPrefs.SetFloat("HighTime", Mathf.Round(GameMod.Instance.time * 1000) / 1000);
                }
                GameMod.Instance.time = 0f;
                MainUI.Instance.spawnner.SetActive(false); //��ʱ��ֱ�����ɵ���
                this.transform.DOScaleY(0f, 0.3f);
                MainUI.Instance.gameObject.transform.DOScaleY(1f, 0.3f);
                MainUI.Instance.weaponitem.DOScaleY(1f, 0f); //����
                MainUI.Instance.sold.transform.DOScaleY(1, 0.3f);
                MainUI.Instance.upmenu.SetActive(true);
                MainUI.Instance.upmenu.transform.localPosition = Vector3.zero;
                MainMenuUI.Instance.SetProgressBar(); //�Ƴ�����
                SpawnBall.instance.Deadball = 0;
                MainMenuUI.Instance.slider.value = 0;
                GameMod.Instance.AdButton.transform.DOScale(1, 0.2f);
                GameMod.Instance.transform.DOScale(1f, 0.3f);
                GameMod.Instance.transform.GetChild(0).GetChild(0).GetComponent<RectTransform>().DOAnchorPosX(0f, 0f);

                MainMenuUI.Instance.LoadGifBox();
                if (UnityEngine.Random.Range(0, 100) > 80)
                {
                    OpenShopmenu();
                }

                break;

            case "DeathMod":
                if (PlayerPrefs.GetInt("removead") == 0)
                {
                    //if (Random.Range(0, 100) > 80)
                    //{
                    //    if (AdManager.Instance.rewardBasedVideo.IsLoaded())
                    //    {
                    //        AdManager.Instance.rewardBasedVideo.Show(); //չʾ�������

                    //    }
                    //}
                    if (Random.Range(0, 100) > 70)
                    {
                        if (AdManager.Instance.interstitial.IsLoaded())
                        {
                            AdManager.Instance.interstitial.Show(); //չʾ�������

                        }
                    }

                }
                GameMod.Instance.GameModBGM.Stop();
                GameMod.Instance.BGM.UnPause();

                cansave = true;
                Game_Controller.isPaused = true;
                Game_Controller.isEnd = true;
                MainUI.Instance.buyui.SetActive(true);
                MainUI.Instance.spawnner.SetActive(false);   //��ʱ��ֱ�����ɵ���
                this.transform.DOScaleY(0f, 0.3f);
                MainUI.Instance.gameObject.transform.DOScaleY(1f, 0.3f);
                MainUI.Instance.weaponitem.DOScaleY(1f, 0f); //����
                MainUI.Instance.sold.transform.DOScaleY(1, 0.3f);
                MainUI.Instance.upmenu.SetActive(true);
                MainUI.Instance.upmenu.transform.localPosition = Vector3.zero;
                MainMenuUI.Instance.SetProgressBar(); //�Ƴ�����
                SpawnBall.instance.Deadball = 0;
                MainMenuUI.Instance.slider.value = 0;
                GameMod.Instance.AdButton.transform.DOScale(1, 0.2f);
                GameMod.Instance.transform.DOScale(1f, 0.3f);
                GameMod.Instance.transform.GetChild(0).GetChild(0).GetComponent<RectTransform>().DOAnchorPosX(0f, 0f);

                CD =360;
                GameMod.Instance.ClickTime = new TimeSpan(DateTime.Now.Ticks); //��¼����ʱ��ʱ��

                GameMod.Instance.ColdTime.transform.DOScale(1, 0f);
                StartCoroutine(ColdTime());
                //�ﵽ��Ӧ�ؿ�������û�е��
                MainMenuUI.Instance.LoadGifBox();


                if (UnityEngine.Random.Range(0, 100) > 80)
                {
                    OpenShopmenu();
                }

                break;
            default:
                break;
        }
    }

    public IEnumerator ColdTime()
    {
        while (true)
        {
            TimeSpan nowtime = new TimeSpan(DateTime.Now.Ticks);                //��¼��ǰʱ��
            timeX = nowtime.Subtract(GameMod.Instance.ClickTime).TotalSeconds; //��ǰʱ���ȥ���ʱ��
            //timeX�����˶��


            GameMod.Instance.ColdTime.text = DataManager.Instance.FormatTwoTime(CD - (int)timeX); //����ʱ
           

            if (CD - (int)timeX<=0)
            {
                GameMod.Instance.ColdTime.text = "coin x 3";
                break;
            }
            yield return new WaitForSeconds(1);
        }
        // GameMod.Instance.ColdTime.transform.DOScale(0, 0f);
        GameMod.Instance.DeathModButton.interactable = true;
        StopCoroutine(ColdTime());
    }
    public void SaveDeathModTIme()
    {
        ES3.Save<int>("DeathModTime", CD - (int)timeX);

    }

    public void OpenShopmenu()
    {

        //���̵����
        MainUI.Instance.PauseMod();


    }
    
}
