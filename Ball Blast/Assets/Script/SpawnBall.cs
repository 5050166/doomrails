// SpawnBall
using System.Collections;
using UnityEngine;

public class SpawnBall : MonoBehaviour
{
    public GameObject[] ballprefab; //С��Ԥ���� 

    public GameObject[] bigBalls;   //�洢�����Ĵ���

    public GameObject[] smallBalls; //�洢������С��

    public static SpawnBall instance;

    public float offSet;

    public float Deadball;

    public int ball;   //��ǰ������С�������

    public int difficultNumber;   // ��ǰ�ؿ�С������

    public Transform BallBox;

    public int maxlife;

    public int minlife;

    public float RefreshTime; //����ˢ��ʱ��

    public float balltime;

    public int currentlevelball; //�Ѿ������˶��ٸ�1��С��

    public GameObject ballfromeballleft;

    public GameObject ballfromeballright;

    public GameObject ballfromespawn;

    private void Awake()
    {
       
        instance = this;
        BallBox = GameObject.Find("BallBox").transform; //�����С��
        
    }

    private void OnEnable() //�����Ժ�
    {
        StopCoroutine("BallSpawner");

        switch (GameMod.Instance.GameMods)
        {
            case "LevelMod":
                difficultNumber = 10 + MainUI.Instance.upmenu.transform.parent.childCount + (int)(DataManager.Instance.getCurrentLevel() * 3f);
                break;
            case "TimeMod":
                RefreshTime = 3f;
                maxlife = 45;
                minlife = 35;
                StartCoroutine("Raise2");
                break;
            case "DeathMod":
                RefreshTime = 3f;
                maxlife = 45;
                minlife = 35;
                StartCoroutine("Raise");

                break;
            default:
                break;
        }

        StartCoroutine("BallSpawner");

        //��ͬģʽ�ĵ������ɸ�����ͬ

    }

    public void SpawnBallFromBall(Vector3 position, int hp, int level)    //С�����
    {
        if (GameMod.Instance.GameMods == "DeathMod")
        {
            ballfromeballleft = Instantiate(ballprefab[Random.Range(5, 8)], position, Quaternion.identity);
            ballfromeballleft.transform.SetParent(BallBox);
            ballfromeballright = Instantiate(ballprefab[Random.Range(5, 8)], position, Quaternion.identity);
            ballfromeballright.transform.SetParent(BallBox);
        }
        else
        {
            ballfromeballleft = Instantiate(ballprefab[Random.Range(0, 5)], position, Quaternion.identity);
            ballfromeballleft.transform.SetParent(BallBox);
            ballfromeballright = Instantiate(ballprefab[Random.Range(0, 5)], position, Quaternion.identity);
            ballfromeballright.transform.SetParent(BallBox);
        }

        ballfromeballleft.GetComponent<Ball>().SetHp(hp);                //��ȡ����ֵ
        ballfromeballright.GetComponent<Ball>().SetHp(hp);
        ballfromeballleft.GetComponent<Ball>().SetLevel(level);          //��ȡ�ȼ�
        ballfromeballright.GetComponent<Ball>().SetLevel(level);
        ballfromeballleft.GetComponent<Ball>().rotateSpeed = -30f;         //һ������ת һ������ת
        ballfromeballright.GetComponent<Ball>().rotateSpeed = 30f;
        ballfromeballleft.GetComponent<Ball>().directionHorizontal = 1f;   //һ�������˶�  һ�������˶�
        ballfromeballright.GetComponent<Ball>().directionHorizontal = -1f;
        ballfromeballleft.GetComponent<Ball>().ballType = "from ball";          //�����з��Ѷ���
        ballfromeballright.GetComponent<Ball>().ballType = "from ball";
        ballfromeballleft.SetActive(true);
        ballfromeballright.SetActive(true);
    }

    public int GetBallNumber() //��ȡС������
    {
        return difficultNumber; //һ��С�������

    }

    public void SpawnBallFromSpawner(float min, float max)
    {
        int[] array = new int[2] //�趨С��Ĳ���x����
		{
           -1,
          1
        };

        if (GameMod.Instance.GameMods == "DeathMod")
        {
            ballfromespawn = Instantiate(ballprefab[Random.Range(5, 8)], new Vector3((float)array[UnityEngine.Random.Range(0, array.Length)] * 10000f, -2000f), Quaternion.identity);
        }
        else
        {
            ballfromespawn = Instantiate(ballprefab[Random.Range(0, 5)], new Vector3((float)array[UnityEngine.Random.Range(0, array.Length)] * 10000f, -2000f), Quaternion.identity);
        }
      

        ballfromespawn.transform.SetParent(BallBox);
       

        ballfromespawn.SetActive(true);

        ballfromespawn.GetComponent<Ball>().ballType = "from spawner";

        if (ballfromespawn.transform.position.x > 0f)
        {
            ballfromespawn.GetComponent<Ball>().directionHorizontal = -1f;
        }
        else if (ballfromespawn.transform.position.x < 0f)
        {
            ballfromespawn.GetComponent<Ball>().directionHorizontal = 1f;
        }


        if (GameMod.Instance.GameMods == "LevelMod" && DataManager.Instance.getCurrentLevel() < 5)
        {
            ballfromespawn.GetComponent<Ball>().level = 1;
        }
        else
        {
            ballfromespawn.GetComponent<Ball>().level = Random.Range(1, 5);
        }

        if (GameMod.Instance.GameMods == "TimeMod" && GameMod.Instance.time < 8) //ʱ��ģʽ
        {
            ballfromespawn.GetComponent<Ball>().level = 1;
        }
        else if (GameMod.Instance.time > 8 && GameMod.Instance.time < 15)
        {
            ballfromespawn.GetComponent<Ball>().level = Random.Range(1, 3);

        }
        else if (GameMod.Instance.time > 15 && GameMod.Instance.time < 25)
        {
            ballfromespawn.GetComponent<Ball>().level = Random.Range(1, 4);

        }
        else if (GameMod.Instance.time > 25 && GameMod.Instance.time < 36)
        {
            ballfromespawn.GetComponent<Ball>().level = Random.Range(1, 5);

        }


        if (GameMod.Instance.GameMods == "DeathMod")
        {
            ballfromespawn.GetComponent<Ball>().level = Random.Range(1, 5);
        }

        //ֻҪʱ��������ô���ȼ���С��
        switch (ballfromespawn.GetComponent<Ball>().level)
        {
            case 1:
                currentlevelball += 1;
                break;
            case 2:
                currentlevelball += 3;
                break;
            case 3:
                currentlevelball += 3;
                break;
            case 4:
                currentlevelball += 15;
                break;

            default:
                break;
        }

        ballfromespawn.GetComponent<Ball>().hpNumber = UnityEngine.Random.Range(min, max);
    }

    private IEnumerator BallSpawner()   //����С��  ÿ��ģʽӦ���в�ͬ
    {
        switch (GameMod.Instance.GameMods)
        {
            case "LevelMod":
                if (DataManager.Instance.getCurrentLevel() < 5)
                {
                    balltime = 2f;
                }
                else
                {
                    balltime = balltime - DataManager.Instance.getCurrentLevel();
                    if (balltime < 4f)
                    {
                        balltime = 3.5f;
                    }
                }

                int i = 0;
                while (true)
                {
                    //   Debug.LogError("im sorry ");
                    if (currentlevelball <= difficultNumber && !Game_Controller.isPaused)    //û����ͣ ��ʼ��Ϸ��
                                                                                             //������С��ﵽÿһ�ص��趨�����󲻻��������С��  difficultNumber ����ÿһ��С�������
                    {
                        SpawnBallFromSpawner(8 * (DataManager.Instance.getCurrentLevel() + 1) * 3f,
                         8 * (DataManager.Instance.getCurrentLevel() + 1) * 3.5f);  //������С��
                        AudioManager.Instance.source.PlayOneShot(AudioManager.Instance.EnemyRefresh);

                    }
                    else if (currentlevelball == difficultNumber)
                    {
                        this.transform.gameObject.SetActive(false);

                    }
                    i++;
                    yield return new WaitForSeconds(balltime); //�趨С�����ɼ��
                }

            case "TimeMod":
                //ʱ��ģʽ����С��ķ�ʽ
                while (true)
                {
                    AudioManager.Instance.source.PlayOneShot(AudioManager.Instance.EnemyRefresh);
                    SpawnBallFromSpawner(minlife, maxlife);

                    if (RefreshTime < 1.5f)
                    {
                        RefreshTime = 0.9f;
                    }

                    yield return new WaitForSeconds(RefreshTime);
                }

            case "DeathMod":
                while (true)
                {
                    AudioManager.Instance.source.PlayOneShot(AudioManager.Instance.EnemyRefresh);
                    SpawnBallFromSpawner(minlife, maxlife);
                    if (RefreshTime < 1f)
                    {
                        RefreshTime = 0.55f;
                    }
                    yield return new WaitForSeconds(RefreshTime);
                }

            default:
                break;
        }

    }

    public IEnumerator Raise() //����ģʽ
    {
        while (true)
        {
            RefreshTime -= 0.5f;
            minlife += 30;
            maxlife += 45;
       
            yield return new WaitForSeconds(2f);//ÿ������Ѷȼ�ǿһ��
        }
    }

    public IEnumerator Raise2() //ʱ��ģʽ
    {
        while (true)
        {
            RefreshTime -= 0.5f;
            minlife +=30;
            maxlife +=45;

            yield return new WaitForSeconds(2f);
        }
    }
    #region
    //��ȡ��ǰ�������ж��ٸ�С��



    //    this.count = bigBalls.Length + smallBalls.Length;  //��ǰ������С������
    //    Debug.Log(count);

    //    for (int i = 0; i < this.bigBalls.Length; i++)
    //    {
    //        count += this.bigBalls[i].GetComponent<Ball>().countValue;  //����countValue��ֵ�����жϳ�����Ǵ�����С��
    //    }
    //    int count2 = 0;
    //    for (int j = 0; j < this.smallBalls.Length; j++)
    //    {
    //        count2 += this.smallBalls[j].GetComponent<Ball>().countValue;
    //    }
    //    this.count = count + count2;
    //    Debug.Log(count);
    //    this.StartCoroutine(this.CountValue());
    //    yield break;
    //}

    //private IEnumerator RaiseDifficult()  //�Ѷ����������Ը��ݵȼ��ؿ�������
    //{
    //       yield return new WaitForSeconds(7f);   //�ֱ��� ÿ10������һ��
    //       this.difficultNumber++;
    //       this.StartCoroutine(this.RaiseDifficult());
    //       yield break;
    //   }

    #endregion


}
