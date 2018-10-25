// Ball
using System;
using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    private float currentHp;                   //小球当前生命

    private bool canMove;                    //小球生成 是否可以移动

    private bool canRotate;                 //是否可以旋转 

    public int damage;

    public int level;                        //小球等级

    private float scale;                         //小球大小  分裂变小

    public int countValue;

    public int ballLayer;

    public float x;                          //小球X

    public float y;

    private Image sr;

    private GameObject textNumber;             //生命值显示

    private float xValue;                        //

    private float yMin;

    private float yMax;

    private float time;

    //  public Color[] ballColor;                     //小球颜色

    public GameObject go1;
    public Vector3 scal;

    public Transform ballbox;

    public GameObject coin;

    private Transform canva;

    protected float time4movedown = 2f;  //小球1.8秒掉下来

    public float hpNumber { get; set; }

    public float rotateSpeed { get; set; }

    public string ballType { get; set; }

    public float directionHorizontal { get; set; }
    public Vector3 ball;
    private void Start()
    {

        canMove = false;
        canRotate = false;       

        sr = this.GetComponent<Image>();

        textNumber = this.transform.GetChild(0).gameObject;
        textNumber.GetComponent<Number>().SetHpText((int)hpNumber); //设定生命值
        SetBall();   //设置ball属性

        FirstMovement();

        canva = GameObject.Find("player").transform;
        ballbox = GameObject.Find("BallBox").transform;
    }

    private void Update()
    {
        Rotate();

        OnBallDead();

        if (this.transform.localPosition.y <= yMin) //到达最低点的时候
        {
            canMove = true;
        }
        if (Game_Controller.isPaused) //玩家死了删除小球
        {

            Destroy(this.gameObject);
        }
    }

    public void Rotate()
    {
        if (canRotate)
        {
            Vector3 a = new Vector3(0f, 0f, rotateSpeed);
            this.transform.Rotate(a * Time.deltaTime);
        }
    }

    public void OnBallDead()//小球被灭了
    {


        if (currentHp < 1f)
        {

            if (level > 1)
            {
                if (hpNumber > 1f)
                {//分裂小球 生命值一半 等级减一
                 // CameraShake.instance.ShakeIt(9f, 1.5f);
                    SpawnBall.instance.SpawnBallFromBall(this.transform.position, (int)hpNumber / 2, level - 1);
                }
                else
                {
                    //  CameraShake.instance.ShakeIt(9f, 1.5f);
                    SpawnBall.instance.SpawnBallFromBall(this.transform.position, 1, level - 1);   //大ball生命值为1时分裂
                }
            }

            switch (this.level)
            {
                case 1:
                    CameraShake.instance.ShakeIt(2f, 0.3f);
                    break;
                case 2:
                    CameraShake.instance.ShakeIt(4f, 0.3f);
                    break;
                case 3:
                    CameraShake.instance.ShakeIt(7f, 0.3f);
                    break;
                case 4:
                    CameraShake.instance.ShakeIt(9f, 0.3f);
                    break;
            }

            GameObject go = Instantiate(Game_Controller.Instance.stonedie, this.transform.localPosition, Quaternion.identity);
            go.transform.SetParent(this.transform.parent);
            go.transform.localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y - 50f, this.transform.localPosition.z);
            go.transform.localScale = this.transform.localScale * 1.15f;


            SpawnCoin(); //掉金币
            AudioManager.Instance.source.PlayOneShot(AudioManager.Instance.KillEnemy);
            Destroy(gameObject);
        }
    }

    public void SpawnCoin()
    {
        int num = UnityEngine.Random.Range(0, 100);
        //  Debug.LogError(num);
        if (num < 75)
        {
            float size = UnityEngine.Random.Range(0.35f, 0.5f);
            go1 = Instantiate(coin, this.transform.localPosition, Quaternion.identity);
            go1.transform.SetParent(ballbox);
            go1.transform.localPosition = this.transform.localPosition; //金币和小球位置保持一致
            go1.transform.GetComponent<Image>().SetNativeSize();

            go1.transform.localScale = new Vector3(size, size, size); //这里随机大小

        }
    }
    private void OnDestroy()
    {
        SpawnBall.instance.Deadball++;
        MainMenuUI.Instance.slider.value = SpawnBall.instance.Deadball / SpawnBall.instance.difficultNumber;
    }

    public void SetBall()  //
    {
        this.currentHp = this.hpNumber;
        switch (this.level)
        {
            case 1:
                this.scale = 0.3f;

                this.xValue = -490f;  //设置为localposition     -540-----540
                this.yMin = -690f;  //   -540---0---540
                this.yMax = 500f;
                this.countValue = 1;//小球等级
                break;
            case 2:
                this.scale = 0.36f;

                this.xValue = -510f;
                this.yMin = -690f;
                this.yMax = 500;
                this.countValue = 2;
                break;
            case 3:
                this.scale = 0.4f;

                this.xValue = -530f;
                this.yMin = -690f;
                this.yMax = 560f;
                this.countValue = 8;
                this.gameObject.tag = "big ball";
                break;
            case 4:
                this.scale = 0.6f;
                this.xValue = -430f;
                this.yMin = -690f;
                this.yMax = 500f;
                this.countValue = 8;
                this.gameObject.tag = "big ball";
                break;
        }

        this.time = Mathf.Sqrt(((2f * (this.yMax - this.yMin)) / 100f) / Custom_Physics.Galileo(this.time4movedown)) - 0.5f;  //重力*时间

        //设置小球大小
        this.transform.localScale = new Vector3(this.scale, this.scale, this.scale);

    }

    private void OnTriggerEnter2D(Collider2D col)  //小球撞上子弹
    {
        if (col.gameObject.tag == "bullet")
        {
            GameObject go = Instantiate(Game_Controller.Instance.stoneExp, this.transform.position, Quaternion.identity);
            go.transform.SetParent(this.transform.parent);
            go.transform.localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y - 20f, -10f);
            go.transform.eulerAngles = new Vector3(-270f, -90, 90f);
            go.transform.localScale = new Vector3(0.2f, 0.2f, 1f);


            AudioManager.Instance.source.PlayOneShot(AudioManager.Instance.HitEnemy);
            this.currentHp -= col.GetComponent<Bullet>().damage;  //扣血受伤害
            this.textNumber.GetComponent<Number>().ChangeValue((int)col.GetComponent<Bullet>().damage);

            //子弹击中特效

            if (col.name.Contains("Bullet 13"))
            {
                GameObject rocket = Instantiate(Game_Controller.Instance.Rocket, this.transform.position, Quaternion.identity);
                rocket.transform.SetParent(this.transform.parent);
                rocket.transform.localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y, -10f);
                rocket.transform.eulerAngles = new Vector3(-90f, 0, 0f);
                rocket.transform.localScale = new Vector3(2.8f, 2.8f, 2.8f);

                AudioManager.Instance.source.PlayOneShot(AudioManager.Instance.Rocket);
            }

            else if (col.name.Contains("Bullet 7"))
            {
                GameObject watermelon = Instantiate(Game_Controller.Instance.watermelon, this.transform.position, Quaternion.identity);
                watermelon.transform.SetParent(this.transform.parent);
                watermelon.transform.localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y, -10f);
                watermelon.transform.eulerAngles = new Vector3(-90f, 0, 0f);
                watermelon.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);

                AudioManager.Instance.source.PlayOneShot(AudioManager.Instance.watermelon);
            }

            else if (col.name.Contains("Bullet 12"))
            {
                GameObject smoke = Instantiate(Game_Controller.Instance.smoke, this.transform.position, Quaternion.identity);
                smoke.transform.SetParent(this.transform.parent);
                smoke.transform.localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y, -10f);
                smoke.transform.eulerAngles = new Vector3(-90f, 0, 0f);
                smoke.transform.localScale = new Vector3(1f, 1f, 1f);

                AudioManager.Instance.source.PlayOneShot(AudioManager.Instance.Rocket);
            }

            else if (col.name.Contains("Bullet 10"))
            {
                GameObject icecream = Instantiate(Game_Controller.Instance.icecream, this.transform.position, Quaternion.identity);
                icecream.transform.SetParent(this.transform.parent);
                icecream.transform.localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y, -10f);
                icecream.transform.eulerAngles = new Vector3(-90f, 0, 0f);
                icecream.transform.localScale = new Vector3(0.55f, 0.55f, 0.55f);

                AudioManager.Instance.source.PlayOneShot(AudioManager.Instance.icecream);
            }

            else if (col.name.Contains("Bullet 20"))
            {

                GameObject icecream = Instantiate(Game_Controller.Instance.Homemade, this.transform.position, Quaternion.identity);
                icecream.transform.SetParent(this.transform.parent);
                icecream.transform.localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y, -10f);
                icecream.transform.eulerAngles = new Vector3(-90f, 0, 0f);
                icecream.transform.localScale = new Vector3(2.6f, 2.6f, 2.6f);
                AudioManager.Instance.source.PlayOneShot(AudioManager.Instance.Rocket);
            }

            else if (col.name.Contains("Bullet 23"))
            {

                GameObject icecream = Instantiate(Game_Controller.Instance.BigRocket, this.transform.position, Quaternion.identity);
                icecream.transform.SetParent(this.transform.parent);
                icecream.transform.localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y, -10f);
                icecream.transform.eulerAngles = new Vector3(-90f, 0, 0f);
                icecream.transform.localScale = new Vector3(2.6f, 2.6f,2.6f);
                AudioManager.Instance.source.PlayOneShot(AudioManager.Instance.Rocket);
            }

            else if (col.name.Contains("Bullet 31"))
            {

                GameObject icecream = Instantiate(Game_Controller.Instance.purple, this.transform.position, Quaternion.identity);
                icecream.transform.SetParent(this.transform.parent);
                icecream.transform.localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y, -10f);
                icecream.transform.eulerAngles = new Vector3(-90f, 0, 0f);
                icecream.transform.localScale = new Vector3(2f, 2f, 2f);
                AudioManager.Instance.source.PlayOneShot(AudioManager.Instance.Amethyst);
            }

            else if (col.name.Contains("Bullet 18")) //水滴跑
            {

                GameObject icecream = Instantiate(Game_Controller.Instance.watergun, this.transform.position, Quaternion.identity);
                icecream.transform.SetParent(this.transform.parent);
                icecream.transform.localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y, -10f);
                icecream.transform.eulerAngles = new Vector3(-90f, 0, 0f);
                icecream.transform.localScale = new Vector3(1f, 1f, 1f);
               
                AudioManager.Instance.source.PlayOneShot(AudioManager.Instance.Water);
            }
            else if (col.name.Contains("Bullet 5")) //西红柿炮
            {
                GameObject tomato = Instantiate(Game_Controller.Instance.tomato, this.transform.position, Quaternion.identity);
                tomato.transform.SetParent(this.transform.parent);
                tomato.transform.localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y, -10f);
                tomato.transform.eulerAngles = new Vector3(-90f, 0, 0f);
                tomato.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);

                AudioManager.Instance.source.PlayOneShot(AudioManager.Instance.TomatoGun);
            }
            else if (col.name.Contains("Bullet 19")) //奶酪
            {
                GameObject Cheese = Instantiate(Game_Controller.Instance.cheese, this.transform.position, Quaternion.identity);
                Cheese.transform.SetParent(this.transform.parent);
                Cheese.transform.localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y, -10f);
                Cheese.transform.eulerAngles = new Vector3(-90f, 0, 0f);
                Cheese.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);

                AudioManager.Instance.source.PlayOneShot(AudioManager.Instance.Cheese);
            }
            else if (col.name.Contains("Bullet 21")) //针管
            {
                GameObject medic = Instantiate(Game_Controller.Instance.medic, this.transform.position, Quaternion.identity);
                medic.transform.SetParent(this.transform.parent);
                medic.transform.localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y, -10f);
                medic.transform.eulerAngles = new Vector3(-90f, 0, 0f);
                medic.transform.localScale = new Vector3(1.5f,1.5f, 1.5f);

                AudioManager.Instance.source.PlayOneShot(AudioManager.Instance.NeedleGUn);
            }
            else if (col.name.Contains("Bullet 29")) //电磁炮
            {
                GameObject electro = Instantiate(Game_Controller.Instance.electromagnetic, this.transform.position, Quaternion.identity);
                electro.transform.SetParent(this.transform.parent);
                electro.transform.localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y, -10f);
                electro.transform.eulerAngles = new Vector3(-90f, 0, 0f);
                electro.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);

                AudioManager.Instance.source.PlayOneShot(AudioManager.Instance.ElectromagneticGun);
            }

            if (col.name.Contains("Bullet 8") || col.name.Contains("Bullet 15") || col.name.Contains("Bullet 17")|| col.name.Contains("Bullet 30")
                || col.name.Contains("Bullet 22") || col.name.Contains("Bullet 24") || col.name.Contains("Bullet 27")|| col.name.Contains("Bullet 28"))
       
            {
                //不用销毁子弹 啥也不做
            }
            else
            {
                GameObjectPool.Instance.CloseGameObjectImmediately(col.gameObject);  //销毁子弹
            }

        }

        else if (col.gameObject.tag == "ground")//如果撞到了地面
        {

            int num = this.level;

            if (num == 4)//最大的石头
            {
                CameraShake.instance.ShakeIt(9f, 1f);
            }
            else if (num == 3)
            {
                CameraShake.instance.ShakeIt(7f, 1f);

            }
            else if (num == 2)
            {
                CameraShake.instance.ShakeIt(4f, 1f);
            }
            else if (num == 1)
            {
                CameraShake.instance.ShakeIt(2f, 1f);
            }
        }

        else if (col.gameObject.tag == "Player") //玩家死亡
        {

            //if (Game_Controller.secondChance)  //点击了继续游戏（续命）
            //{
            // MainMenuUI.instance.OpenSecondChanceUI();
            // Destroy(this.gameObject);
            //}
            //else
            //{ 

            if (UnityEngine.Random.Range(1, 100) < BuffSystem.Instance.Framenumber)//进行死亡判定
            {
                  GameMod.Instance.Isplayerdie = false; //玩家没死

                GameObject go = Instantiate(Prefabmanager.Instance.miss, this.transform.localPosition, Quaternion.identity);
                go.transform.localScale = Vector3.zero;
                Destroy(go, 1f);
                go.transform.SetParent(canva);
                go.transform.localPosition = canva.transform.GetChild(0).transform.localPosition;
                go.transform.DOScale(1f, 0.5f);
            }
            else 
            {
                //if (GameMod.Instance.GameMods == "TimeMod")
                //{
                //    if (GameMod.Instance.time > PlayerPrefs.GetFloat("HighTime")) //存储最长存活时间
                //    {
                //        GameMod.Instance.CurrentTime.text = "Max Time:" + ((Mathf.Round(GameMod.Instance.time * 1000)) / 1000).ToString();
                //        PlayerPrefs.SetFloat("HighTime", Mathf.Round(GameMod.Instance.time * 1000)/ 1000);
                //    }
                //}
                GameMod.Instance.Isplayerdie = true;
                Game_Controller.isEnd = false;
                AudioManager.Instance.source.PlayOneShot(AudioManager.Instance.Playerdie);
                GameObject go = Instantiate(Game_Controller.Instance.playerdieExp, PlayerController.Instance.transform.localPosition, Quaternion.identity); //创建特效
                go.transform.SetParent(PlayerController.Instance.transform.parent);
                go.transform.localPosition = PlayerController.Instance.transform.localPosition;

                go.transform.localScale = PlayerController.Instance.transform.localScale * 0.7f;
                for (int i = 0; i < DataManager.Instance.bo.Count; i++)
                {
                    DataManager.Instance.bo[i].gameObject.AddComponent<Rigidbody2D>();
                    DataManager.Instance.bo[i].GetComponent<BoxCollider2D>().isTrigger = false;
                }
                GameMod.Instance.can = false;
                //Game_Controller.isEnd = true;
                MainMenuUI.Instance.OpenEndUI();
                AudioManager.Instance.source.PlayOneShot(AudioManager.Instance.GameFail);


            }

            //MainUI.Instance.buyui.SetActive(true);
            //MainUI.Instance.gameObject.transform.DOScaleY(1f, 0.7f);
            //MainUI.Instance.weaponitem.DOScaleY(1f, 0.7f);
            //Background.Instance.IsFinish = false;
            //   MainMenuUI.instance.OpenEndUI();
            //}
        }
    }


    public void SetHp(int hp)
    {
        hpNumber = (float)hp;
        currentHp = hpNumber;
    }

    public void SetLevel(int _level)  //给小球分配等级 共有4个等级
    {
        level = _level;
    }



    public void MoveLeft()//向左移动
    {
        LeanTween.moveLocalX(gameObject, -this.xValue, 2.5f).setOnComplete(new Action(this.MoveRight));  //执行完向右移动
    }

    public void MoveRight() //向右移动
    {
        LeanTween.moveLocalX(gameObject, this.xValue, 2.5f).setOnComplete(new Action(this.MoveLeft));  //执行完向左移动
    }


    public void MoveUp()//向上移动
    {
        LeanTween.moveLocalY(gameObject, this.yMax, this.time).setEase(LeanTweenType.easeOutQuad).setOnComplete(new Action(this.MoveDown));
    }


    public void MoveDown()//向下移动
    {
        this.time = Mathf.Sqrt(((2f * (this.transform.localPosition.y - this.yMin)) / 100f) / Custom_Physics.Galileo(this.time4movedown)) - 0.5f;  //重力*时间


        LeanTween.moveLocalY(gameObject, yMin, this.time).setEase(LeanTweenType.easeInQuad).setOnComplete(new Action(this.MoveUp));
        //Debug.LogError(time);
    }

    public void MoveDown1()   //小球移动模式
    {
        this.canRotate = true;
        this.rotateSpeed = 30f * this.directionHorizontal * -1f;
        if (this.directionHorizontal == 1f)
        { //右下移动
            this.MoveRight();
            this.MoveDown();
        }
        else if (this.directionHorizontal == -1f)
        { //左下移动
            this.MoveLeft();
            this.MoveDown();
        }
    }

    public void MoveDown2() //向下移动
    {
        //计算从最高点向最低点的时间没有问题

        //Debug.LogError(transform.localPosition.y);
        //计算当前位置到最低的位置有问题
        float num = Mathf.Sqrt(((2f * (transform.localPosition.y - this.yMin) / 100f)) / Custom_Physics.Galileo(this.time4movedown)) - 0.5f; //小球当前位置距离
        //Debug.LogError(num);

        LeanTween.moveLocalY(gameObject, this.yMin, num).setOnComplete(new Action(this.MoveUp));
    }


    public void OnComplete(string name)
    {
        switch (name)
        {
            case "up":
                LeanTween.moveY(gameObject, transform.position.y + 1f, 0.5f);
                StopCoroutine("WaitSomeTimeToFinishTween");
                break;
            case "down":
                MoveDown2();
                StopCoroutine("WaitSomeTimeToFinishTween");
                break;
        }
    }

    IEnumerator WaitSomeTimeToFinishTween(string name, float waittime)
    {
        yield return new WaitForSeconds(waittime);
        OnComplete(name);
    }





    public void FirstMovement()  //第一次移动
    {
        #region
        // this.transform.GetComponent<Image>().enabled = true;
        //if (this.ballType == "from ball")
        //{
        //    this.transform.GetComponent<Image>().enabled = true;
        //    if (this.directionHorizontal == 1f)
        //    {
        //        LeanTween.moveLocalX(gameObject, this.directionHorizontal * this.transform.localPosition.x, 2.5f).setOnComplete(new Action(this.MoveLeft));
        //    }
        //    else if (this.directionHorizontal == -1f)
        //    {
        //        LeanTween.moveLocalX(gameObject, this.directionHorizontal * this.transform.localPosition.x, 2.54f).setOnComplete(new Action(this.MoveRight));
        //    }
        //    LeanTween.moveLocalY(gameObject, transform.localPosition.y, 0.5f).setOnComplete(new Action(this.MoveDown2));
        //}
        #endregion

        if (this.ballType == "from ball")
        {
            this.transform.GetComponent<Image>().enabled = true;

            if (this.directionHorizontal == 1f)
            {
                LeanTween.moveLocalX(gameObject, this.directionHorizontal * this.xValue, 2.74f).setOnComplete(new Action(this.MoveLeft));
            }
            else if (this.directionHorizontal == -1f)
            {
                LeanTween.moveLocalX(gameObject, this.directionHorizontal * this.xValue, 2.74f).setOnComplete(new Action(this.MoveRight));
            }
            this.canRotate = true;
            this.rotateSpeed = 30f * this.directionHorizontal * -1f;

            LeanTween.moveY(gameObject, transform.position.y + 0.05f, 0.3f).setOnComplete(new Action(this.MoveDown2)).setEase(LeanTweenType.easeOutQuad);

        }



        else if (this.ballType == "from spawner")
        {

            Vector3 to = this.directionHorizontal * new Vector3(1f, 0f, 0f);
            // Debug.Log(to);

            if (to.x < 0f)
            {
                to.x = xValue - 50f;
                to.y = yMax;
            }
            else
            {
                to.x = -xValue - 50f;
                to.y = yMax;
            }
            this.transform.GetComponent<Image>().enabled = true;
            LeanTween.moveLocal(this.gameObject, to, 2.5f).setOnComplete(new Action(this.MoveDown1));

            //     Debug.Log(to);
        }
    }

    public float LeftAndRightTime()
    {
        float num = this.xValue * 2f / EdgeControll.Instance.posx1;
        float num2 = 0f;
        if (this.directionHorizontal == 1f)
        {
            num2 = this.xValue - transform.localPosition.x;
        }
        else if (this.directionHorizontal == -1f)
        {
            num2 = Mathf.Abs(-this.xValue - transform.localPosition.x);
        }
        return num2 / num;
    }


    //public void FirstMovement(float direction)
    //{
    //    if (this.ballType == "from ball")  //从球里面分裂出来的移动方法
    //    {
    //        this.transform.DOLocalMoveX(direction * (this.xValue), 2.5f);

    //        transform.DOLocalMoveY(transform.localPosition.y + 100f, 0.2f).SetEase(Ease.OutQuad);
    //        //    StartCoroutine(MoveDown(1f));
    //        MoveDown1();
    //    }
    //    else if (this.ballType == "from spawner")   //从小球生成出来的移动方法
    //    {

    //        this.transform.DOLocalMoveX(directionHorizontal * (this.xValue), 2f);
    //        //  StartCoroutine(MoveDown(1f));
    //        MoveDown2();

    //    }
    //} 

    //private IEnumerator MoveDown(float time)  //模拟重力 向下的效果
    //{

    //    yield return new WaitForSeconds(time); //
    //    this.canRotate = true;
    //    this.rotateSpeed = this.directionHorizontal * -1f * 10f;
    //    //设定下落时间（速度）
    //    float dir = this.transform.localPosition.y - this.yMin; //距离
    //    float t = Mathf.Sqrt(2f * dir / 168.9f);
    //    Debug.Log(t);
    //    this.transform.DOLocalMoveY(yMin - 100f, t - 1f).SetEase(Ease.InQuad);
    //    yield break;
    //}
}