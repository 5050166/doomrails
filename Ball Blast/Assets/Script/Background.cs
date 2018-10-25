using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public Transform layer1;
    public Transform layer2;
    public Transform layer3;
    public bool IsFinish = false; //背景显示完毕
    private static Background instance;

    public static Background Instance
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
        instance = this;
    }
    void Start()
    {
        layer3 = transform.GetChild(0);
        layer2 = transform.GetChild(1);
        layer1 = transform.GetChild(2);
        ShowBackground();

    }

    public void ShowBackground()  //显示背景 
    {
        layer1.DOLocalMoveX(-986f, 0.3f);
        layer2.DOLocalMoveX(-905f,0.3f);
        layer3.DOLocalMoveX(-957f, 0.3f);
        IsFinish = true; //显示完毕
    }

    public void Move()  //移动
    {
        if (IsFinish)
        {
            layer1.DOLocalMoveX(4382f, 50f).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
            layer2.DOLocalMoveX(5047f,300f).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
            layer3.DOLocalMoveX(4705f,400f).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
            IsFinish = false;
        }


    }


}
