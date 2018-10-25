using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, ICanvasRaycastFilter
{
    private static Transform canvas; // canvas
    public Transform tools;

    private Transform nowParent; //物品 是格子的子物体，nowParent记录的是当前物品属于哪个格子
                                 //（移动物体 将移动的物体放入目标位置的子物体 然后变更position）
    private bool isRaycastValid = true; //默认射线不可穿透物品

    private Camera canvasCamera = null;

    private Vector3 tranposition;

    public Transform player;

    public Sprite sb;

    public int coc;

    public string sbname;

    public GameObject Clone; //生成的物体;

    public GameObject wheel;//轮子

    //public string name;
    /// <summary>
    /// <summary>
    /// 0：默认  1：框架 2：灰色炮台 3：空格子  4：黄色炮台  5：绿色炮台
    /// </summary>  
    private void Awake()
    {
       
        if (canvas == null)
        {
            canvas = GameObject.Find("Canvas").transform; //获取Canvas组件
        }
        player = GameObject.Find("player").transform;

    }

    void Start()
    {    
        tools = GameObject.Find("Canvas").transform.GetChild(2);
    }
    public void OnBeginDrag(PointerEventData eventData) //开始拖拽
    {
        AudioManager.Instance.source.PlayOneShot(AudioManager.Instance.ClickSound);

        //当鼠标在对象按下并拖拽时，该对象每帧响应一次此事件


        canvasCamera = canvas.GetComponentInChildren<Camera>();

        //当前物体在哪个格子下放着； 也就是说移动物体就是放在另一个物体之下
        nowParent = transform.parent;

        tranposition = transform.localPosition;

        if (transform.tag == "hammer")
        {
            Clone = Instantiate(this.transform.gameObject, transform.localPosition, Quaternion.identity);
            Clone.transform.localScale = Vector3.one;
            //可以拖拽的物体必须在Canvas下
            Clone.transform.SetParent(canvas);
            Clone.transform.localScale = Vector3.one;
            Clone.transform.localPosition = this.transform.localPosition;
        }
        //Debug.LogError("test");
        transform.SetParent(canvas); //移动canvas下显示

        //ui穿透 ，设置为可以穿透（必须通过射线即时获取position）
        isRaycastValid = false;
        //【拖拽物体移动的时候鼠标下是有物体一直跟随遮挡的，如果不穿透就获取不到放置位置（OnEndDrag中判断是空格子，物体，还是无效位置）】

    }
    public void OnDrag(PointerEventData eventData) //拖拽中
    {   //鼠标左键按住拖拽的时候，物体跟着鼠标移动.
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent as RectTransform, Input.mousePosition, Camera.main, out pos);

        this.transform.localPosition = new Vector3(pos.x, pos.y);


    }
    //默认都是lockBox 
    //扣钱只能是在安装成功的时候扣钱

    public void OnEndDrag(PointerEventData eventData) //拖拽结束（判断位置巴拉巴拉）
    {   //获取拖拽结束后 所在点的物体
        GameObject go = eventData.pointerCurrentRaycast.gameObject;

        this.transform.localScale = Vector3.one;

        if (go != null && transform.gameObject.tag != "hammer") //拖拽终点必须有物体，并且物体为box？
        {
            if (go.transform.tag == "gun39" && (transform.gameObject.name == "gun19" || transform.gameObject.name == "gun24" || transform.gameObject.name == "gun25")) //空格子做东西
            {
                if (go.GetComponent<Box>().IsHaveWheel)
                {
                    go.transform.GetChild(1).gameObject.SetActive(true);
                }
                go.transform.GetChild(2).gameObject.SetActive(true);
                transform.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
                transform.SetParent(nowParent);
                transform.localPosition = nowParent.position;// 物体回到原来的格子上去        
                go.transform.SetParent(player);
                switch (transform.gameObject.name)
                {
                    case "gun19":
                        go.GetComponent<Box>().Number = "19"; //必须建的框架
                        go.transform.tag = "gun19";
                        go.transform.GetChild(2).GetComponent<Image>().sprite = transform.GetComponent<Image>().sprite;
                        break;
                    case "gun24":
                        go.GetComponent<Box>().Number = "24"; //必须建的框架
                        go.transform.tag = "gun24";
                        go.transform.GetChild(2).GetComponent<Image>().sprite = transform.GetComponent<Image>().sprite;
                        break;
                    case "gun25":
                        go.GetComponent<Box>().Number = "25"; //必须建的框架
                        go.transform.tag = "gun25";
                        go.transform.GetChild(2).GetComponent<Image>().sprite = transform.GetComponent<Image>().sprite;
                        break;
                    default:
                        break;
                }
                AudioManager.Instance.source.PlayOneShot(AudioManager.Instance.Bulid);
                isRaycastValid = true;
            }

            else if ((go.transform.parent.gameObject.tag == "gun19" || go.transform.parent.gameObject.tag == "gun24" || go.transform.parent.gameObject.tag == "gun25") || go.transform.parent.gameObject.tag.Contains("gun")) //不是炮就是框架  进到这里就已经是解锁了
            {
                //go的第四个子物体（3）是关闭着的 证明只有框架没东西
                if (transform.gameObject.name != "gun19") //要建造的东西不是框架 而是炮台
                {
                    Debug.LogError(go.transform.name);
                    Debug.LogError(transform.gameObject.name);

                    sb = go.transform.parent.GetChild(3).GetComponent<Image>().sprite;

                    AudioManager.Instance.source.PlayOneShot(AudioManager.Instance.Bulid);
                  
                    switch (transform.gameObject.name)
                    {

                        case "gun1":
                            if (go.transform.parent.GetChild(3).GetComponent<RoteCannon>())
                            {
                                go.transform.parent.GetChild(3).DOKill();
                                go.transform.parent.GetChild(3).transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                                go.transform.parent.GetChild(3).transform.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
                                Destroy(go.transform.parent.GetChild(3).GetComponent<RoteCannon>());
                            }
                            go.transform.parent.GetChild(2).GetComponent<ParticleSystem>().Stop();
                            go.transform.parent.GetChild(3).GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[0];
                            go.transform.parent.GetChild(3).GetComponent<Image>().SetNativeSize();
                            go.transform.parent.GetChild(3).transform.localScale = new Vector3(0.6f, 0.6f);

                            go.transform.parent.GetComponent<Box>().Number = "1";
                            go.transform.parent.gameObject.tag = "gun1";
                            break;
                        case "gun2":
                            if (go.transform.parent.GetChild(3).GetComponent<RoteCannon>())
                            {
                                go.transform.parent.GetChild(3).DOKill();
                                go.transform.parent.GetChild(3).transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                                go.transform.parent.GetChild(3).transform.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
                                Destroy(go.transform.parent.GetChild(3).GetComponent<RoteCannon>());
                            }
                            go.transform.parent.GetChild(2).GetComponent<ParticleSystem>().Stop();
                            go.transform.parent.GetChild(3).GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[1];
                            go.transform.parent.GetChild(3).GetComponent<Image>().SetNativeSize();
                            go.transform.parent.GetChild(3).transform.localScale = new Vector3(0.6f, 0.6f);
                            go.transform.parent.GetChild(3).GetChild(0).GetComponent<Gun>().bullet = Prefabmanager.Instance.bullets[1];

                            go.transform.parent.GetComponent<Box>().Number = "2";
                            go.transform.parent.gameObject.tag = "gun2";
                            break;
                        case "gun3":
                            if (go.transform.parent.GetChild(3).GetComponent<RoteCannon>())
                            {
                                go.transform.parent.GetChild(3).DOKill();
                                go.transform.parent.GetChild(3).transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                                go.transform.parent.GetChild(3).transform.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
                                Destroy(go.transform.parent.GetChild(3).GetComponent<RoteCannon>());
                            }
                            go.transform.parent.GetChild(2).GetComponent<ParticleSystem>().Stop();
                            go.transform.parent.GetChild(3).GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[2];
                            go.transform.parent.GetChild(3).GetComponent<Image>().SetNativeSize();
                            go.transform.parent.GetChild(3).transform.localScale = new Vector3(0.6f, 0.6f);

                            go.transform.parent.GetComponent<Box>().Number = "3";
                            go.transform.parent.gameObject.tag = "gun3";
                            break;
                        case "gun4":
                            if (go.transform.parent.GetChild(3).GetComponent<RoteCannon>())
                            {
                                go.transform.parent.GetChild(3).DOKill();
                                go.transform.parent.GetChild(3).transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                                go.transform.parent.GetChild(3).transform.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
                                Destroy(go.transform.parent.GetChild(3).GetComponent<RoteCannon>());
                            }
                            go.transform.parent.GetChild(2).GetComponent<ParticleSystem>().Stop();
                            go.transform.parent.GetChild(3).GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[3];
                            go.transform.parent.GetChild(3).GetComponent<Image>().SetNativeSize();
                            go.transform.parent.GetChild(3).GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.4f);
                            go.transform.parent.GetChild(3).gameObject.AddComponent<RoteCannon>();
                            go.transform.parent.GetChild(3).transform.localScale = new Vector3(0.6f, 0.6f);

                            go.transform.parent.GetComponent<Box>().Number = "4";
                            go.transform.parent.gameObject.tag = "gun4";
                            break;
                        case "gun5":
                            if (go.transform.parent.GetChild(3).GetComponent<RoteCannon>())
                            {
                                go.transform.parent.GetChild(3).DOKill();
                                go.transform.parent.GetChild(3).transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                                go.transform.parent.GetChild(3).transform.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
                                Destroy(go.transform.parent.GetChild(3).GetComponent<RoteCannon>());
                            }
                            go.transform.parent.GetChild(2).GetComponent<ParticleSystem>().Stop();
                            go.transform.parent.GetChild(3).GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[4];
                            go.transform.parent.GetChild(3).GetComponent<Image>().SetNativeSize();

                            go.transform.parent.GetChild(3).transform.localScale = new Vector3(0.6f, 0.6f);

                            go.transform.parent.GetComponent<Box>().Number = "5";
                            go.transform.parent.gameObject.tag = "gun5";
                            break;
                        case "gun6":
                            if (go.transform.parent.GetChild(3).GetComponent<RoteCannon>())
                            {
                                go.transform.parent.GetChild(3).DOKill();
                                go.transform.parent.GetChild(3).transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                                go.transform.parent.GetChild(3).transform.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
                                Destroy(go.transform.parent.GetChild(3).GetComponent<RoteCannon>());
                            }
                            go.transform.parent.GetChild(2).GetComponent<ParticleSystem>().Stop();
                            go.transform.parent.GetChild(3).GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[5];
                            go.transform.parent.GetChild(3).GetComponent<Image>().SetNativeSize();
                            go.transform.parent.GetChild(3).transform.localScale = new Vector3(0.6f, 0.6f);

                            go.transform.parent.GetComponent<Box>().Number = "6";
                            go.transform.parent.gameObject.tag = "gun6";
                            break;
                        case "gun7":
                            if (go.transform.parent.GetChild(3).GetComponent<RoteCannon>())
                            {
                                go.transform.parent.GetChild(3).DOKill();
                                go.transform.parent.GetChild(3).transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                                go.transform.parent.GetChild(3).transform.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
                                Destroy(go.transform.parent.GetChild(3).GetComponent<RoteCannon>());
                            }
                            go.transform.parent.GetChild(2).GetComponent<ParticleSystem>().Stop();
                            go.transform.parent.GetChild(3).GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[6];
                            go.transform.parent.GetChild(3).GetComponent<Image>().SetNativeSize();
                            go.transform.parent.GetChild(3).transform.localScale = new Vector3(0.6f, 0.6f);

                            go.transform.parent.GetComponent<Box>().Number = "7";
                            go.transform.parent.gameObject.tag = "gun7";
                            break;
                        case "gun8":
                            if (go.transform.parent.GetChild(3).GetComponent<RoteCannon>())
                            {
                                go.transform.parent.GetChild(3).DOKill();
                                go.transform.parent.GetChild(3).transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                                go.transform.parent.GetChild(3).transform.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
                                Destroy(go.transform.parent.GetChild(3).GetComponent<RoteCannon>());
                            }
                            go.transform.parent.GetChild(2).GetComponent<ParticleSystem>().Stop();
                            go.transform.parent.GetChild(3).GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[7];
                            go.transform.parent.GetChild(3).GetComponent<Image>().SetNativeSize();
                            go.transform.parent.GetChild(3).transform.localScale = new Vector3(0.6f, 0.6f);

                            go.transform.parent.GetComponent<Box>().Number = "8";
                            go.transform.parent.gameObject.tag = "gun8";
                            break;
                        case "gun9":
                            if (go.transform.parent.GetChild(3).GetComponent<RoteCannon>())
                            {
                                go.transform.parent.GetChild(3).DOKill();
                                go.transform.parent.GetChild(3).transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                                go.transform.parent.GetChild(3).transform.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
                                Destroy(go.transform.parent.GetChild(3).GetComponent<RoteCannon>());
                            }
                            go.transform.parent.GetChild(2).GetComponent<ParticleSystem>().Stop();
                            go.transform.parent.GetChild(3).GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[8];
                            go.transform.parent.GetChild(3).GetComponent<Image>().SetNativeSize();
                            go.transform.parent.GetChild(3).transform.localScale = new Vector3(0.6f, 0.6f);

                            go.transform.parent.GetComponent<Box>().Number = "9";
                            go.transform.parent.gameObject.tag = "gun9";
                            break;
                        case "gun10":
                            if (go.transform.parent.GetChild(3).GetComponent<RoteCannon>())
                            {
                                go.transform.parent.GetChild(3).DOKill();
                                go.transform.parent.GetChild(3).transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                                go.transform.parent.GetChild(3).transform.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
                                Destroy(go.transform.parent.GetChild(3).GetComponent<RoteCannon>());
                            }
                            go.transform.parent.GetChild(2).GetComponent<ParticleSystem>().Stop();
                            go.transform.parent.GetChild(3).GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[9];
                            go.transform.parent.GetChild(3).GetComponent<Image>().SetNativeSize();
                            go.transform.parent.GetChild(3).transform.localScale = new Vector3(0.6f, 0.6f);

                            go.transform.parent.GetComponent<Box>().Number = "10";
                            go.transform.parent.gameObject.tag = "gun10";
                            break;
                        case "gun11":
                            if (go.transform.parent.GetChild(3).GetComponent<RoteCannon>())
                            {
                                go.transform.parent.GetChild(3).DOKill();
                                go.transform.parent.GetChild(3).transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                                go.transform.parent.GetChild(3).transform.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
                                Destroy(go.transform.parent.GetChild(3).GetComponent<RoteCannon>());
                            }
                            go.transform.parent.GetChild(2).GetComponent<ParticleSystem>().Stop();
                            go.transform.parent.GetChild(3).GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[10];
                            go.transform.parent.GetChild(3).GetComponent<Image>().SetNativeSize();
                            go.transform.parent.GetChild(3).transform.localScale = new Vector3(0.6f, 0.6f);

                            go.transform.parent.GetComponent<Box>().Number = "11";
                            go.transform.parent.gameObject.tag = "gun11";

                            break;
                        case "gun12":
                            if (go.transform.parent.GetChild(3).GetComponent<RoteCannon>())
                            {
                                go.transform.parent.GetChild(3).DOKill();
                                go.transform.parent.GetChild(3).transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                                go.transform.parent.GetChild(3).transform.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
                                Destroy(go.transform.parent.GetChild(3).GetComponent<RoteCannon>());
                            }
                            go.transform.parent.GetChild(2).GetComponent<ParticleSystem>().Stop();
                            go.transform.parent.GetChild(3).GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[11];
                            go.transform.parent.GetChild(3).GetComponent<Image>().SetNativeSize();
                            go.transform.parent.GetChild(3).transform.localScale = new Vector3(0.6f, 0.6f);

                            go.transform.parent.GetComponent<Box>().Number = "12";
                            go.transform.parent.gameObject.tag = "gun12";
                            break;
                        case "gun13":
                            if (go.transform.parent.GetChild(3).GetComponent<RoteCannon>())
                            {
                                go.transform.parent.GetChild(3).DOKill();
                                go.transform.parent.GetChild(3).transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                                go.transform.parent.GetChild(3).transform.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
                                Destroy(go.transform.parent.GetChild(3).GetComponent<RoteCannon>());
                            }
                            go.transform.parent.GetChild(2).GetComponent<ParticleSystem>().Stop();
                            go.transform.parent.GetChild(3).GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[12];
                            go.transform.parent.GetChild(3).GetComponent<Image>().SetNativeSize();
                            go.transform.parent.GetChild(3).transform.localScale = new Vector3(0.6f, 0.6f);

                            go.transform.parent.GetComponent<Box>().Number = "13";
                            go.transform.parent.gameObject.tag = "gun13";
                            break;
                        case "gun14":
                            if (go.transform.parent.GetChild(3).GetComponent<RoteCannon>())
                            {
                                go.transform.parent.GetChild(3).DOKill();
                                go.transform.parent.GetChild(3).transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                                go.transform.parent.GetChild(3).transform.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
                                Destroy(go.transform.parent.GetChild(3).GetComponent<RoteCannon>());
                            }
                            go.transform.parent.GetChild(2).GetComponent<ParticleSystem>().Stop();
                            go.transform.parent.GetChild(3).GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[13];
                            go.transform.parent.GetChild(3).GetComponent<Image>().SetNativeSize();
                            go.transform.parent.GetChild(3).transform.localScale = new Vector3(0.6f, 0.6f);

                            go.transform.parent.GetComponent<Box>().Number = "14";
                            go.transform.parent.gameObject.tag = "gun14";
                            break;
                        case "gun15":
                            if (go.transform.parent.GetChild(3).GetComponent<RoteCannon>())
                            {
                                go.transform.parent.GetChild(3).DOKill();
                                go.transform.parent.GetChild(3).transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                                go.transform.parent.GetChild(3).transform.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
                                Destroy(go.transform.parent.GetChild(3).GetComponent<RoteCannon>());
                            }
                            go.transform.parent.GetChild(2).GetComponent<ParticleSystem>().Stop();
                            go.transform.parent.GetChild(3).GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[14];
                            go.transform.parent.GetChild(3).GetComponent<Image>().SetNativeSize();
                            go.transform.parent.GetChild(3).transform.localScale = new Vector3(0.6f, 0.6f);

                            go.transform.parent.GetComponent<Box>().Number = "15";
                            go.transform.parent.gameObject.tag = "gun15";
                            break;

                        case "gun16":

                            if (go.transform.parent.GetChild(3).GetComponent<RoteCannon>())
                            {
                                go.transform.parent.GetChild(3).DOKill();
                                go.transform.parent.GetChild(3).transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                                go.transform.parent.GetChild(3).transform.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
                                Destroy(go.transform.parent.GetChild(3).GetComponent<RoteCannon>());
                            }
                            go.transform.parent.GetChild(2).GetComponent<ParticleSystem>().Stop();
                            go.transform.parent.GetChild(3).GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[15];
                            go.transform.parent.GetChild(3).GetComponent<Image>().SetNativeSize();
                            go.transform.parent.GetChild(3).transform.localScale = new Vector3(0.6f, 0.6f);
                            go.transform.parent.GetChild(3).gameObject.AddComponent<RoteCannon>();
                            go.transform.parent.GetComponent<Box>().Number = "16";
                            go.transform.parent.gameObject.tag = "gun16";
                            break;

                        case "gun17":
                            if (go.transform.parent.GetChild(3).GetComponent<RoteCannon>())
                            {
                                go.transform.parent.GetChild(3).DOKill();
                                go.transform.parent.GetChild(3).transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                                go.transform.parent.GetChild(3).transform.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
                                Destroy(go.transform.parent.GetChild(3).GetComponent<RoteCannon>());
                            }
                            go.transform.parent.GetChild(2).GetComponent<ParticleSystem>().Stop();
                            go.transform.parent.GetChild(3).GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[16];
                            go.transform.parent.GetChild(3).GetComponent<Image>().SetNativeSize();
                            go.transform.parent.GetChild(3).transform.localScale = new Vector3(0.6f, 0.6f);
                            go.transform.parent.GetChild(3).gameObject.AddComponent<RoteCannon>();

                            go.transform.parent.GetComponent<Box>().Number = "17";
                            go.transform.parent.gameObject.tag = "gun17";
                            break;
                        case "gun18":
                            if (go.transform.parent.GetChild(3).GetComponent<RoteCannon>())
                            {
                                go.transform.parent.GetChild(3).DOKill();
                                go.transform.parent.GetChild(3).transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                                go.transform.parent.GetChild(3).transform.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
                                Destroy(go.transform.parent.GetChild(3).GetComponent<RoteCannon>());
                            }
                            go.transform.parent.GetChild(2).GetComponent<ParticleSystem>().Stop();
                            go.transform.parent.GetChild(3).GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[17];
                            go.transform.parent.GetChild(3).GetComponent<Image>().SetNativeSize();
                            go.transform.parent.GetChild(3).transform.localScale = new Vector3(0.6f, 0.6f);

                            go.transform.parent.GetComponent<Box>().Number = "18";
                            go.transform.parent.gameObject.tag = "gun18";
                            break;

                        case "gun20":
                            if (go.transform.parent.GetChild(3).GetComponent<RoteCannon>())
                            {
                                go.transform.parent.GetChild(3).DOKill();
                                go.transform.parent.GetChild(3).transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                                go.transform.parent.GetChild(3).transform.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
                                Destroy(go.transform.parent.GetChild(3).GetComponent<RoteCannon>());
                            }
                            go.transform.parent.GetChild(2).GetComponent<ParticleSystem>().Stop();
                            go.transform.parent.GetChild(3).GetChild(0).gameObject.SetActive(false);//关闭开火功能
                            go.transform.parent.GetChild(3).GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[19];
                            go.transform.parent.GetChild(3).GetComponent<Image>().SetNativeSize();
                            go.transform.parent.GetChild(3).transform.localScale = new Vector3(0.6f, 0.6f);
                            go.transform.parent.GetComponent<Box>().Number = "20";
                            go.transform.parent.gameObject.tag = "gun20";
                            break;
                        case "gun21":
                            if (go.transform.parent.GetChild(3).GetComponent<RoteCannon>())
                            {
                                go.transform.parent.GetChild(3).DOKill();
                                go.transform.parent.GetChild(3).transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                                go.transform.parent.GetChild(3).transform.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
                                Destroy(go.transform.parent.GetChild(3).GetComponent<RoteCannon>());
                            }
                          
                            go.transform.parent.GetChild(2).GetComponent<ParticleSystem>().Stop();
                            go.transform.parent.GetChild(3).GetChild(0).gameObject.SetActive(false);//关闭开火功能
                            go.transform.parent.GetChild(3).GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[20];
                            go.transform.parent.GetChild(3).GetComponent<Image>().SetNativeSize();
                            go.transform.parent.GetChild(3).transform.localScale = new Vector3(0.6f, 0.6f);
                            go.transform.parent.GetComponent<Box>().Number = "21";
                            go.transform.parent.gameObject.tag = "gun21";
                            break;

                        case "gun22":
                            if (go.transform.parent.GetChild(3).GetComponent<RoteCannon>())
                            {
                                go.transform.parent.GetChild(3).DOKill();
                                go.transform.parent.GetChild(3).transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                                go.transform.parent.GetChild(3).transform.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
                                Destroy(go.transform.parent.GetChild(3).GetComponent<RoteCannon>());
                            }
                          
                            go.transform.parent.GetChild(2).GetComponent<ParticleSystem>().Stop();
                            go.transform.parent.GetChild(3).GetChild(0).gameObject.SetActive(false);//关闭开火功能
                            go.transform.parent.GetChild(3).GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[21];
                            go.transform.parent.GetChild(3).GetComponent<Image>().SetNativeSize();
                            go.transform.parent.GetChild(3).transform.localScale = new Vector3(0.6f, 0.6f);
                            go.transform.parent.GetComponent<Box>().Number = "22";
                            go.transform.parent.gameObject.tag = "gun22";
                            break;
                        case "gun23":
                            if (go.transform.parent.GetChild(3).GetComponent<RoteCannon>())
                            {
                                go.transform.parent.GetChild(3).DOKill();
                                go.transform.parent.GetChild(3).transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                                go.transform.parent.GetChild(3).transform.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
                                Destroy(go.transform.parent.GetChild(3).GetComponent<RoteCannon>());
                            }
                            go.transform.parent.GetChild(2).GetComponent<ParticleSystem>().Stop();
                            go.transform.parent.GetChild(3).GetChild(0).gameObject.SetActive(false);//关闭开火功能
                            go.transform.parent.GetChild(3).GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[22];
                            go.transform.parent.GetChild(3).GetComponent<Image>().SetNativeSize();
                            go.transform.parent.GetChild(3).transform.localScale = new Vector3(0.6f, 0.6f);                      
                            go.transform.parent.GetComponent<Box>().Number = "23";
                            go.transform.parent.gameObject.tag = "gun23";
                            break;

                        case "gun24":
                            sb = go.transform.parent.GetChild(2).GetComponent<Image>().sprite;
                            go.transform.parent.GetChild(2).GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[23];
                            break;
                        case "gun25":
                            sb = go.transform.parent.GetChild(2).GetComponent<Image>().sprite;
                            go.transform.parent.GetChild(2).GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[24];
                            break;
                        case "gun26":
                            if (go.transform.parent.GetChild(3).GetComponent<RoteCannon>())
                            {
                                go.transform.parent.GetChild(3).DOKill();
                                go.transform.parent.GetChild(3).transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                                go.transform.parent.GetChild(3).transform.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
                                Destroy(go.transform.parent.GetChild(3).GetComponent<RoteCannon>());
                            }
                            go.transform.parent.GetChild(2).GetComponent<ParticleSystem>().Stop();
                            go.transform.parent.GetChild(3).GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[25];
                            go.transform.parent.GetChild(3).GetComponent<Image>().SetNativeSize();
                            go.transform.parent.GetChild(3).transform.localScale = new Vector3(0.6f, 0.6f);

                            go.transform.parent.GetComponent<Box>().Number = "26";
                            go.transform.parent.gameObject.tag = "gun26";
                            break;
                        case "gun27":
                            if (go.transform.parent.GetChild(3).GetComponent<RoteCannon>())
                            {
                                go.transform.parent.GetChild(3).DOKill();
                                go.transform.parent.GetChild(3).transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                                go.transform.parent.GetChild(3).transform.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
                                Destroy(go.transform.parent.GetChild(3).GetComponent<RoteCannon>());
                            }
                            go.transform.parent.GetChild(2).GetComponent<ParticleSystem>().Stop();
                            go.transform.parent.GetChild(3).GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[26];
                            go.transform.parent.GetChild(3).GetComponent<Image>().SetNativeSize();
                            go.transform.parent.GetChild(3).transform.localScale = new Vector3(0.6f, 0.6f);
                            go.transform.parent.GetChild(3).gameObject.AddComponent<RoteCannon>();
                            go.transform.parent.GetComponent<Box>().Number = "27";
                            go.transform.parent.gameObject.tag = "gun27";
                            break;
                        case "gun28":
                            if (go.transform.parent.GetChild(3).GetComponent<RoteCannon>())
                            {
                                go.transform.parent.GetChild(3).DOKill();
                                go.transform.parent.GetChild(3).transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                                go.transform.parent.GetChild(3).transform.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
                                Destroy(go.transform.parent.GetChild(3).GetComponent<RoteCannon>());
                            }
                            go.transform.parent.GetChild(2).GetComponent<ParticleSystem>().Stop();
                            go.transform.parent.GetChild(3).GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[27];
                            go.transform.parent.GetChild(3).GetComponent<Image>().SetNativeSize();
                            go.transform.parent.GetChild(3).transform.localScale = new Vector3(0.6f, 0.6f);

                            go.transform.parent.GetComponent<Box>().Number = "28";
                            go.transform.parent.gameObject.tag = "gun28";
                            break;
                        case "gun29":
                            if (go.transform.parent.GetChild(3).GetComponent<RoteCannon>())
                            {
                                go.transform.parent.GetChild(3).DOKill();
                                go.transform.parent.GetChild(3).transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                                go.transform.parent.GetChild(3).transform.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
                                Destroy(go.transform.parent.GetChild(3).GetComponent<RoteCannon>());
                            }
                            go.transform.parent.GetChild(2).GetComponent<ParticleSystem>().Stop();
                            go.transform.parent.GetChild(3).GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[28];
                            go.transform.parent.GetChild(3).GetComponent<Image>().SetNativeSize();
                            go.transform.parent.GetChild(3).transform.localScale = new Vector3(0.6f, 0.6f);
                            go.transform.parent.GetChild(3).gameObject.AddComponent<RoteCannon>();
                            go.transform.parent.GetComponent<Box>().Number = "29";
                            go.transform.parent.gameObject.tag = "gun29";
                            break;
                        case "gun30":
                            if (go.transform.parent.GetChild(3).GetComponent<RoteCannon>())
                            {
                                go.transform.parent.GetChild(3).DOKill();
                                go.transform.parent.GetChild(3).transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                                go.transform.parent.GetChild(3).transform.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
                                Destroy(go.transform.parent.GetChild(3).GetComponent<RoteCannon>());
                            }
                            go.transform.parent.GetChild(2).GetComponent<ParticleSystem>().Stop();
                            go.transform.parent.GetChild(3).GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[29];
                            go.transform.parent.GetChild(3).GetComponent<Image>().SetNativeSize();
                            go.transform.parent.GetChild(3).transform.localScale = new Vector3(0.6f, 0.6f);

                            go.transform.parent.GetComponent<Box>().Number = "30";
                            go.transform.parent.gameObject.tag = "gun30";
                            break;
                        case "gun31":
                            if (go.transform.parent.GetChild(3).GetComponent<RoteCannon>())
                            {
                                go.transform.parent.GetChild(3).DOKill();
                                go.transform.parent.GetChild(3).transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                                go.transform.parent.GetChild(3).transform.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
                                Destroy(go.transform.parent.GetChild(3).GetComponent<RoteCannon>());
                            }
                            go.transform.parent.GetChild(2).GetComponent<ParticleSystem>().Stop();
                            go.transform.parent.GetChild(3).GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[30];
                            go.transform.parent.GetChild(3).GetComponent<Image>().SetNativeSize();
                            go.transform.parent.GetChild(3).transform.localScale = new Vector3(0.6f, 0.6f);

                            go.transform.parent.GetComponent<Box>().Number = "31";
                            go.transform.parent.gameObject.tag = "gun31";
                            break;
                        case "gun32":
                            if (go.transform.parent.GetChild(3).GetComponent<RoteCannon>())
                            {
                                go.transform.parent.GetChild(3).DOKill();
                                go.transform.parent.GetChild(3).transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                                go.transform.parent.GetChild(3).transform.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
                                Destroy(go.transform.parent.GetChild(3).GetComponent<RoteCannon>());
                            }
                            go.transform.parent.GetChild(2).GetComponent<ParticleSystem>().Stop();
                            go.transform.parent.GetChild(3).GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[31];
                            go.transform.parent.GetChild(3).GetComponent<Image>().SetNativeSize();
                            go.transform.parent.GetChild(3).transform.localScale = new Vector3(0.6f, 0.6f);
                            go.transform.parent.GetChild(3).gameObject.AddComponent<RoteCannon>();
                            go.transform.parent.GetComponent<Box>().Number = "32";
                            go.transform.parent.gameObject.tag = "gun32";
                            break;
                        case "gun33":
                            if (go.transform.parent.GetChild(3).GetComponent<RoteCannon>())
                            {
                                go.transform.parent.GetChild(3).DOKill();
                                go.transform.parent.GetChild(3).transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                                go.transform.parent.GetChild(3).transform.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
                                Destroy(go.transform.parent.GetChild(3).GetComponent<RoteCannon>());
                            }
                            go.transform.parent.GetChild(2).GetComponent<ParticleSystem>().Stop();
                            go.transform.parent.GetChild(3).GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[32];
                            go.transform.parent.GetChild(3).GetComponent<Image>().SetNativeSize();
                            go.transform.parent.GetChild(3).transform.localScale = new Vector3(0.6f, 0.6f);

                            go.transform.parent.GetComponent<Box>().Number = "33";
                            go.transform.parent.gameObject.tag = "gun33";
                            break;
                        case "gun34":
                            if (go.transform.parent.GetChild(3).GetComponent<RoteCannon>())
                            {
                                go.transform.parent.GetChild(3).DOKill();
                                go.transform.parent.GetChild(3).transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                                go.transform.parent.GetChild(3).transform.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
                                Destroy(go.transform.parent.GetChild(3).GetComponent<RoteCannon>());
                            }
                            go.transform.parent.GetChild(2).GetComponent<ParticleSystem>().Stop();
                            go.transform.parent.GetChild(3).GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[33];
                            go.transform.parent.GetChild(3).GetComponent<Image>().SetNativeSize();
                            go.transform.parent.GetChild(3).transform.localScale = new Vector3(0.6f, 0.6f);

                            go.transform.parent.GetComponent<Box>().Number = "34";
                            go.transform.parent.gameObject.tag = "gun34";
                            break;
                        case "gun35":
                            if (go.transform.parent.GetChild(3).GetComponent<RoteCannon>())
                            {
                                go.transform.parent.GetChild(3).DOKill();
                                go.transform.parent.GetChild(3).transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                                go.transform.parent.GetChild(3).transform.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
                                Destroy(go.transform.parent.GetChild(3).GetComponent<RoteCannon>());
                            }
                            go.transform.parent.GetChild(2).GetComponent<ParticleSystem>().Stop();
                            go.transform.parent.GetChild(3).GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[34];
                            go.transform.parent.GetChild(3).GetComponent<Image>().SetNativeSize();
                            go.transform.parent.GetChild(3).transform.localScale = new Vector3(0.6f, 0.6f);

                            go.transform.parent.GetComponent<Box>().Number = "35";
                            go.transform.parent.gameObject.tag = "gun35";
                            break;
                        case "gun36":
                            if (go.transform.parent.GetChild(3).GetComponent<RoteCannon>())
                            {
                                go.transform.parent.GetChild(3).DOKill();
                                go.transform.parent.GetChild(3).transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                                go.transform.parent.GetChild(3).transform.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
                                Destroy(go.transform.parent.GetChild(3).GetComponent<RoteCannon>());
                            }
                            go.transform.parent.GetChild(2).GetComponent<ParticleSystem>().Stop();
                            go.transform.parent.GetChild(3).GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[35];
                            go.transform.parent.GetChild(3).GetComponent<Image>().SetNativeSize();
                            go.transform.parent.GetChild(3).transform.localScale = new Vector3(0.6f, 0.6f);

                            go.transform.parent.GetComponent<Box>().Number = "36";
                            go.transform.parent.gameObject.tag = "gun36";
                            break;
                        case "gun37":
                            if (go.transform.parent.GetChild(3).GetComponent<RoteCannon>())
                            {
                                go.transform.parent.GetChild(3).DOKill();
                                go.transform.parent.GetChild(3).transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                                go.transform.parent.GetChild(3).transform.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
                                Destroy(go.transform.parent.GetChild(3).GetComponent<RoteCannon>());
                            }
                            go.transform.parent.GetChild(2).GetComponent<ParticleSystem>().Stop();
                            go.transform.parent.GetChild(3).GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[36];
                            go.transform.parent.GetChild(3).GetComponent<Image>().SetNativeSize();
                            go.transform.parent.GetChild(3).transform.localScale = new Vector3(0.6f, 0.6f);

                            go.transform.parent.GetComponent<Box>().Number = "37";
                            go.transform.parent.gameObject.tag = "gun37";
                            break;
                        case "gun38":
                            if (go.transform.parent.GetChild(3).GetComponent<RoteCannon>())
                            {
                                go.transform.parent.GetChild(3).DOKill();
                                go.transform.parent.GetChild(3).transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                                go.transform.parent.GetChild(3).transform.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
                                Destroy(go.transform.parent.GetChild(3).GetComponent<RoteCannon>());
                            }
                            go.transform.parent.GetChild(2).GetComponent<ParticleSystem>().Stop();
                            go.transform.parent.GetChild(3).GetComponent<Image>().sprite = Prefabmanager.Instance.sprites[37];
                            go.transform.parent.GetChild(3).GetComponent<Image>().SetNativeSize();
                            go.transform.parent.GetChild(3).transform.localScale = new Vector3(0.6f, 0.6f);
                            go.transform.parent.GetChild(3).gameObject.AddComponent<RoteCannon>();
                            go.transform.parent.GetComponent<Box>().Number = "38";
                            go.transform.parent.gameObject.tag = "gun38";
                            break;
                        default:
                            break;
                    }
                  
                    if (go.transform.parent.GetChild(3).gameObject.activeSelf)  //打开着的证明有炮台
                    {
                        if (go.transform.parent.GetChild(3).GetChild(0).gameObject.activeSelf == false && go.transform.parent.tag != "gun20"
                            && go.transform.parent.tag != "gun21" && go.transform.parent.tag != "gun22" && go.transform.parent.tag != "gun23") //target关闭着 并且不是gun20
                        {
                            go.transform.parent.GetChild(3).GetChild(0).gameObject.SetActive(true);
                        }
                        transform.GetComponent<Image>().sprite = sb;
                        transform.name = sb.name;
                        go.transform.parent.SetParent(player);//目的：跟着玩家移动         
                        transform.SetParent(nowParent);
                        transform.localPosition = nowParent.position;
                        transform.GetComponent<Image>().SetNativeSize();
                        transform.localScale = new Vector3(0.7f, 0.7f);
                        isRaycastValid = true;
                    }
                    else
                    {
                        if (transform.gameObject.name == "gun19")
                        {
                            Debug.Log("ss1");
                            transform.GetComponent<Image>().sprite = sb;
                            transform.name = sb.name;

                        }
                        else if (transform.gameObject.name == "gun24")
                        {
                            Debug.Log("ss2");
                            transform.GetComponent<Image>().sprite = sb;
                            transform.name = sb.name;

                        }
                        else if (transform.gameObject.name == "gun25")
                        {
                            Debug.Log("ss3");
                            transform.GetComponent<Image>().sprite = sb;
                            transform.name = sb.name;

                        }
                        else
                        {
                            go.transform.parent.GetChild(3).gameObject.SetActive(true);
                            transform.GetComponent<Image>().sprite = null;
                            transform.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
                            transform.name = "Image";

                        }
                    }
                }

                AudioManager.Instance.source.PlayOneShot(AudioManager.Instance.BuildFail); //操作失败
                transform.SetParent(nowParent);
                transform.localPosition = nowParent.position;// 物体回到原来的格子上去          
                transform.localScale = new Vector3(0.7f, 0.7f);
                isRaycastValid = true;
            }
            else
            {

                if (transform.name != "Image")
                {

                    Game_Controller.Instance.tips.transform.DOScaleY(1.1f, 0.3f).OnComplete(() => StartCoroutine(Inactive("close", 1f)));
                }

              //  AudioManager.Instance.source.PlayOneShot(AudioManager.Instance.BuildFail); //操作失败
                Debug.Log(go.name);
                transform.SetParent(nowParent);
                transform.localPosition = nowParent.position;// 物体回到原来的格子上去

                switch (transform.name)
                {
                    case "gun1":
                        transform.localScale = new Vector3(0.7f, 0.7f);
                        break;
                    case "gun2":
                        transform.localScale = new Vector3(0.7f, 0.7f);
                        break;
                    case "gun3":
                        transform.localScale = new Vector3(0.7f, 0.7f);
                        break;
                    case "gun4":
                        transform.localScale = new Vector3(0.7f, 0.7f);
                        break;
                    case "gun5":
                        transform.localScale = new Vector3(0.7f, 0.7f);
                        break;
                    case "gun6":
                        transform.localScale = new Vector3(0.7f, 0.7f);
                        break;
                    case "gun7":
                        transform.localScale = new Vector3(0.7f, 0.7f);
                        break;
                    case "gun8":
                        transform.localScale = new Vector3(0.7f, 0.7f);
                        break;
                    case "gun9":
                        transform.localScale = new Vector3(0.7f, 0.7f);
                        break;
                    case "gun10":
                        transform.localScale = new Vector3(0.7f, 0.7f);
                        break;
                    case "gun11":
                        transform.localScale = new Vector3(0.7f, 0.7f);
                        break;
                    case "gun12":
                        transform.localScale = new Vector3(0.7f, 0.7f);
                        break;
                    case "gun13":
                        transform.localScale = new Vector3(0.7f, 0.7f);
                        break;
                    case "gun14":
                        transform.localScale = new Vector3(0.7f, 0.7f);
                        break;
                    case "gun15":
                        transform.localScale = new Vector3(0.7f, 0.7f);
                        break;
                    case "gun16":
                        transform.localScale = new Vector3(0.7f, 0.7f);
                        break;
                    case "gun17":
                        transform.localScale = new Vector3(0.7f, 0.7f);
                        break;
                    case "gun18":
                        transform.localScale = new Vector3(0.7f, 0.7f);
                        break;
                    case "gun19":
                        transform.localScale = new Vector3(0.7f, 0.7f);
                        break;
                    default:
                        transform.localScale = new Vector3(0.7f, 0.7f);
                        break;
                }
                isRaycastValid = true;
            }
        }
        else if (transform.gameObject.tag == "hammer") //选中拖动物体是🔨
        {
           
            if (go != null && go.transform.gameObject.tag == "gun")
            {
             //   Debug.LogError(go.name);

                if (go.transform.parent.childCount == 4) //删的是box上的组件
                {
                    go.transform.parent.GetChild(3).DOKill(); //移除当前删除炮台上任何动画效果
                    go.transform.parent.GetChild(3).transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                    go.transform.parent.GetChild(3).transform.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
                    Destroy(go.transform.parent.GetChild(3).GetComponent<RoteCannon>());
                    go.transform.parent.GetChild(3).gameObject.SetActive(false);     //关闭炮台
                    go.transform.parent.gameObject.tag = "gun19";
                    go.transform.parent.gameObject.GetComponent<Box>().Number = "19";
                }
                else if (go.transform.parent.childCount == 1)
                {
                    go.transform.parent.GetChild(0).name = "Image";
                    //  go.transform.parent.GetChild(0).GetComponent<Image>().sprite = null;
                    go.transform.parent.GetChild(0).GetComponent<Image>().color = new Color(0, 0, 0, 0);
                    go.transform.tag = "Untagged";

                }

                //卖炮加钱 二分之一;
                //coc = go.transform.GetComponent<BoXPrice>().Price / 2;
                switch (go.transform.GetComponent<Image>().sprite.name)
                {
                    case "gun1":
                        coc =25;
                        PlayerprefController.AddIntValue("coin", 25);
                        break;
                    case "gun2":
                        coc =50;
                        PlayerprefController.AddIntValue("coin", 50);
                        break;
                    case "gun3":
                        coc =180;
                        PlayerprefController.AddIntValue("coin",100);
                        break;
                    case "gun4":
                        coc =215;
                        PlayerprefController.AddIntValue("coin",175);
                        break;
                    case "gun5":
                        coc = 275;
                        PlayerprefController.AddIntValue("coin",275);
                        break;
                    case "gun6":
                        coc =330;
                        PlayerprefController.AddIntValue("coin", 330);
                        break;
                    case "gun7":
                        coc = 450;
                        PlayerprefController.AddIntValue("coin", 450);
                        break;
                    case "gun8":
                        coc = 550;
                        PlayerprefController.AddIntValue("coin", 550);
                        break;
                    case "gun9":
                        coc =750;
                        PlayerprefController.AddIntValue("coin", 750);
                        break;
                    case "gun10":
                        coc = 850;
                        PlayerprefController.AddIntValue("coin", 850);
                        break;
                    case "gun11":
                        coc =1000;
                        PlayerprefController.AddIntValue("coin", 1000);
                        break;
                    case "gun12":
                        coc = 1300;
                        PlayerprefController.AddIntValue("coin", 1300);
                        break;
                    case "gun13":
                        coc =1550;
                        PlayerprefController.AddIntValue("coin", 1550);
                        break;
                    case "gun14":
                        coc = 2000;
                        PlayerprefController.AddIntValue("coin",2000);
                        break;
                    case "gun15":
                        coc = 2400;
                        PlayerprefController.AddIntValue("coin", 2400);
                        break;
                    case "gun16":
                        coc = 2600;
                        PlayerprefController.AddIntValue("coin", 2600);
                        break;
                    case "gun17":
                        coc = 3500;
                        PlayerprefController.AddIntValue("coin", 3500);
                        break;
                    case "gun18":
                        coc = 2800;
                        PlayerprefController.AddIntValue("coin", 2800);
                        break;
                    case "gun19":
                        coc = 50;
                        PlayerprefController.AddIntValue("coin", 50);
                        break;
                    case "gun20":
                        coc = 1500;
                        PlayerprefController.AddIntValue("coin", 1500);
                        break;
                    case "gun21":
                        coc =4000;
                        PlayerprefController.AddIntValue("coin", 4000);
                        break;
                    case "gun22":
                        coc = 1500;
                        PlayerprefController.AddIntValue("coin", 1500);
                        break;
                    case "gun23":
                        coc = 40000;
                        PlayerprefController.AddIntValue("coin", 4000);
                        break;
                    case "gun24":
                        coc =500;
                        PlayerprefController.AddIntValue("coin",500);
                        break;
                    case "gun25":
                        coc = 1000;
                        PlayerprefController.AddIntValue("coin",1000);
                        break;
                    case "gun26":
                        coc = 4200;
                        PlayerprefController.AddIntValue("coin",4200);
                        break;
                    case "gun27":
                        coc = 5000;
                        PlayerprefController.AddIntValue("coin", 5000);
                        break;
                    case "gun28":
                        coc = 6000;
                        PlayerprefController.AddIntValue("coin", 6000);
                        break;
                    case "gun29":
                        coc =7500;
                        PlayerprefController.AddIntValue("coin", 7500);
                        break;
                    case "gun30":
                        coc = 10000;
                        PlayerprefController.AddIntValue("coin", 10000);
                        break;
                    case "gun31":
                        coc = 15000;
                        PlayerprefController.AddIntValue("coin", 15000);
                        break;
                    case "gun32":
                        coc = 20000;
                        PlayerprefController.AddIntValue("coin", 20000);
                        break;
                    case "gun33":
                        coc = 25000;
                        PlayerprefController.AddIntValue("coin",25000);
                        break;
                    case "gun34":
                        coc = 30000;
                        PlayerprefController.AddIntValue("coin", 30000);
                        break;
                    case "gun35":
                        coc = 35000;
                        PlayerprefController.AddIntValue("coin", 35000);
                        break;
                    case "gun36":
                        coc =40000;
                        PlayerprefController.AddIntValue("coin", 40000);
                        break;
                    case "gun37":
                        coc = 45000;
                        PlayerprefController.AddIntValue("coin", 45000);
                        break;
                    case "gun38":
                        coc = 50000;
                        PlayerprefController.AddIntValue("coin",50000);
                        break;

                    default:
                    
                        break;
                }
                AudioManager.Instance.source.PlayOneShot(AudioManager.Instance.SellWeapon);



                //卖东西金币生成
                if (go.transform.parent.childCount == 4)
                {
                    GameObject coin = Instantiate(BuffSystem.Instance.coin, go.transform.localPosition, Quaternion.identity);
                    coin.transform.SetParent(go.transform.parent.parent);
                    coin.transform.localPosition = go.transform.localPosition;
                    coin.transform.DOLocalMove(new Vector3(-490f, 1540f, 0f), 0.5f).SetEase(Ease.InQuad);
                    coin.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
                    Destroy(coin, 1f);

                    GameObject coin2 = Instantiate(BuffSystem.Instance.coin, go.transform.localPosition, Quaternion.identity);
                    coin2.transform.SetParent(go.transform.parent.parent);
                    coin2.transform.localPosition = go.transform.localPosition;
                    coin2.transform.DOLocalMove(new Vector3(-490f, 1540f, 0f), 0.5f).SetEase(Ease.InOutQuad);
                    coin2.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
                    Destroy(coin2, 1f);

                    GameObject coin3 = Instantiate(BuffSystem.Instance.coin, go.transform.localPosition, Quaternion.identity);
                    coin3.transform.SetParent(go.transform.parent.parent);
                    coin3.transform.localPosition = go.transform.localPosition;
                    coin3.transform.DOLocalMove(new Vector3(-490f, 1540f, 0f), 0.5f);
                    coin3.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
                    Destroy(coin3, 1f);

                    GameObject TextPrefab = Instantiate(BuffSystem.Instance.TextPrefab, go.transform.localPosition, Quaternion.identity);
                    TextPrefab.transform.SetParent(go.transform.parent.parent);
                    TextPrefab.transform.localPosition = new Vector3(go.transform.localPosition.x, go.transform.localPosition.y + 100f, go.transform.localPosition.z);
                    TextPrefab.transform.localScale = Vector3.one;
                    TextPrefab.GetComponent<CoinNumber>().setCoinadd(coc);
                }
                else if (go.transform.parent.childCount == 1)
                {
                    GameObject coin = Instantiate(BuffSystem.Instance.coin, go.transform.localPosition, Quaternion.identity);
                    coin.transform.SetParent(go.transform.parent.parent.parent.parent);
                    coin.transform.position = go.transform.position;
                    coin.transform.DOLocalMove(new Vector3(-472f, 1700f, 0f), 0.5f).SetEase(Ease.InQuad);
                    coin.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
                    Destroy(coin, 1f);

                    GameObject coin2 = Instantiate(BuffSystem.Instance.coin, go.transform.localPosition, Quaternion.identity);
                    coin2.transform.SetParent(go.transform.parent.parent.parent.parent);
                    coin2.transform.position = go.transform.position;
                    coin2.transform.DOLocalMove(new Vector3(-472f, 1700f, 0f), 0.5f).SetEase(Ease.InOutQuad);
                    coin2.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
                    Destroy(coin2, 1f);

                    GameObject coin3 = Instantiate(BuffSystem.Instance.coin, go.transform.localPosition, Quaternion.identity);
                    coin3.transform.SetParent(go.transform.parent.parent.parent.parent);
                    coin3.transform.position = go.transform.position;
                    coin3.transform.DOLocalMove(new Vector3(-472f, 1700, 0f), 0.5f);
                    coin3.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
                    Destroy(coin3, 1f);

                    GameObject TextPrefab = Instantiate(BuffSystem.Instance.TextPrefab, go.transform.localPosition, Quaternion.identity);
                    TextPrefab.transform.SetParent(go.transform.parent.parent.parent.parent);
                    TextPrefab.transform.position = new Vector3(go.transform.position.x, go.transform.position.y, go.transform.position.z);
                    TextPrefab.transform.localScale = Vector3.one;
                    TextPrefab.GetComponent<CoinNumber>().setCoinadd(coc);

                }


                //  Debug.Log(go.transform.parent.tag);
                MainUI.Instance.UpdateCoinText();
                //  Debug.Log(go.transform.parent.tag);
                Destroy(Clone.gameObject);
                transform.SetParent(nowParent);
                this.transform.localPosition = tranposition;
                isRaycastValid = true;
            }
            else
            {
                Destroy(Clone.gameObject);
                transform.SetParent(nowParent);
                this.transform.localPosition = tranposition;
                isRaycastValid = true;
            }
        }
        else
        {
            Debug.Log("????");
            transform.SetParent(nowParent);
            transform.localPosition = nowParent.position;// 物体回到原来的格子上去

            switch (transform.name)
            {

                case "gun1":
                    transform.localScale = new Vector3(0.7f, 0.7f);
                    break;
                case "gun2":
                    transform.localScale = new Vector3(0.7f, 0.7f);
                    break;
                case "gun3":
                    transform.localScale = new Vector3(0.7f, 0.7f);
                    break;
                case "gun4":
                    transform.localScale = new Vector3(0.7f, 0.7f);
                    break;
                case "gun5":
                    transform.localScale = new Vector3(0.7f, 0.7f);
                    break;
                case "gun6":
                    transform.localScale = new Vector3(0.7f, 0.7f);
                    break;
                case "gun7":
                    transform.localScale = new Vector3(0.7f, 0.7f);
                    break;
                case "gun8":
                    transform.localScale = new Vector3(0.7f, 0.7f);
                    break;
                case "gun9":
                    transform.localScale = new Vector3(0.7f, 0.7f);
                    break;
                case "gun10":
                    transform.localScale = new Vector3(0.7f, 0.7f);
                    break;
                case "gun11":
                    transform.localScale = new Vector3(0.7f, 0.7f);
                    break;
                case "gun12":
                    transform.localScale = new Vector3(0.7f, 0.7f);
                    break;
                case "gun13":
                    transform.localScale = new Vector3(0.7f, 0.7f);
                    break;
                case "gun14":
                    transform.localScale = new Vector3(0.7f, 0.7f);
                    break;
                case "gun15":
                    transform.localScale = new Vector3(0.7f, 0.7f);
                    break;
                case "gun16":
                    transform.localScale = new Vector3(0.7f, 0.7f);
                    break;
                case "gun17":
                    transform.localScale = new Vector3(0.7f, 0.7f);
                    break;
                case "gun18":
                    transform.localScale = new Vector3(0.7f, 0.7f);
                    break;
                case "gun20":
                    transform.localScale = new Vector3(0.7f, 0.7f);
                    break;
                case "gun21":
                    transform.localScale = new Vector3(0.7f, 0.7f);
                    break;
                case "gun22":
                    transform.localScale = new Vector3(0.7f, 0.7f);
                    break;
                case "gun23":
                    transform.localScale = new Vector3(0.7f, 0.7f);
                    break;
                case "gun24":
                    transform.localScale = new Vector3(0.7f, 0.7f);
                    break;
                case "gun25":
                    transform.localScale = new Vector3(0.7f, 0.7f);
                    break;


                default:
                    transform.localScale = new Vector3(0.7f, 0.7f);
                    break;
                
            }

            isRaycastValid = true;
        }
    }

    public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)  //ui组件是否可以被穿透，为false可以被穿透
    {
        return isRaycastValid;
    }


    void OnTweenComplete(string name)
    {
        switch (name)
        {//name这个是方法名称 需要执行的
            case "close":
                Game_Controller.Instance.tips.transform.DOScaleY(0f, 0.3f);
                StopCoroutine("Inactive");
                break;

        }
    }
    IEnumerator Inactive(string name, float waittime)
    {
        yield return new WaitForSeconds(waittime); //等待这么多秒以后开始执行动画
        OnTweenComplete(name);

    }

}
