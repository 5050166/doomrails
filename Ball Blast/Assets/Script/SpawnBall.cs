// SpawnBall
using System.Collections;
using UnityEngine;

public class SpawnBall : MonoBehaviour
{
    public GameObject[] ballprefab; //小球预制体 

    public GameObject[] bigBalls;   //存储创建的大球

    public GameObject[] smallBalls; //存储创建的小球

    public static SpawnBall instance;

    public float offSet;

    public float Deadball;

    public int ball;   //当前场景内小球的数量

    public int difficultNumber;   // 当前关卡小球数量

    public Transform BallBox;

    public int maxlife;

    public int minlife;

    public float RefreshTime; //敌人刷新时间

    public float balltime;

    public int currentlevelball; //已经生成了多少个1级小球

    public GameObject ballfromeballleft;

    public GameObject ballfromeballright;

    public GameObject ballfromespawn;

    private void Awake()
    {
       
        instance = this;
        BallBox = GameObject.Find("BallBox").transform; //存放新小球
        
    }

    private void OnEnable() //激活以后
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

        //不同模式的敌人生成各不相同

    }

    public void SpawnBallFromBall(Vector3 position, int hp, int level)    //小球分裂
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

        ballfromeballleft.GetComponent<Ball>().SetHp(hp);                //获取生命值
        ballfromeballright.GetComponent<Ball>().SetHp(hp);
        ballfromeballleft.GetComponent<Ball>().SetLevel(level);          //获取等级
        ballfromeballright.GetComponent<Ball>().SetLevel(level);
        ballfromeballleft.GetComponent<Ball>().rotateSpeed = -30f;         //一个正旋转 一个反旋转
        ballfromeballright.GetComponent<Ball>().rotateSpeed = 30f;
        ballfromeballleft.GetComponent<Ball>().directionHorizontal = 1f;   //一个向右运动  一个向左运动
        ballfromeballright.GetComponent<Ball>().directionHorizontal = -1f;
        ballfromeballleft.GetComponent<Ball>().ballType = "from ball";          //从球中分裂而来
        ballfromeballright.GetComponent<Ball>().ballType = "from ball";
        ballfromeballleft.SetActive(true);
        ballfromeballright.SetActive(true);
    }

    public int GetBallNumber() //获取小球数量
    {
        return difficultNumber; //一级小球的总数

    }

    public void SpawnBallFromSpawner(float min, float max)
    {
        int[] array = new int[2] //设定小球的产生x坐标
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

        if (GameMod.Instance.GameMods == "TimeMod" && GameMod.Instance.time < 8) //时间模式
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

        //只要时生成了这么个等级的小球
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

    private IEnumerator BallSpawner()   //生成小球  每个模式应该有不同
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
                    if (currentlevelball <= difficultNumber && !Game_Controller.isPaused)    //没有暂停 开始游戏了
                                                                                             //场景内小球达到每一关的设定数量后不会继续生成小球  difficultNumber 就是每一关小球的数量
                    {
                        SpawnBallFromSpawner(8 * (DataManager.Instance.getCurrentLevel() + 1) * 3f,
                         8 * (DataManager.Instance.getCurrentLevel() + 1) * 3.5f);  //生成了小球
                        AudioManager.Instance.source.PlayOneShot(AudioManager.Instance.EnemyRefresh);

                    }
                    else if (currentlevelball == difficultNumber)
                    {
                        this.transform.gameObject.SetActive(false);

                    }
                    i++;
                    yield return new WaitForSeconds(balltime); //设定小球生成间隔
                }

            case "TimeMod":
                //时间模式生成小球的方式
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

    public IEnumerator Raise() //死亡模式
    {
        while (true)
        {
            RefreshTime -= 0.5f;
            minlife += 30;
            maxlife += 45;
       
            yield return new WaitForSeconds(2f);//每隔多久难度加强一次
        }
    }

    public IEnumerator Raise2() //时间模式
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
    //获取当前场景内有多少个小球



    //    this.count = bigBalls.Length + smallBalls.Length;  //当前场景中小球数量
    //    Debug.Log(count);

    //    for (int i = 0; i < this.bigBalls.Length; i++)
    //    {
    //        count += this.bigBalls[i].GetComponent<Ball>().countValue;  //根据countValue的值可以判断出这个是大球还是小球
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

    //private IEnumerator RaiseDifficult()  //难度增幅（可以根据等级关卡增幅）
    //{
    //       yield return new WaitForSeconds(7f);   //粗暴的 每10秒增幅一次
    //       this.difficultNumber++;
    //       this.StartCoroutine(this.RaiseDifficult());
    //       yield break;
    //   }

    #endregion


}
