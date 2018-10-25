using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Showmenu : MonoBehaviour
{
    public Transform Uimenu;

    public void ShowUI()
    {
        StartCoroutine("ShowUi");
    }

    //for (int i = 0; i < Uimenu.childCount; i++)
    //{
    //    Uimenu.GetChild(i).DOScale(1f, 1f);
    //}
    //Uimenu.GetChild(0).DOScale(1f, 0.1f).OnComplete(() =>
    //Uimenu.GetChild(1).DOScale(1f, 0.1f).OnComplete(() =>
    //Uimenu.GetChild(2).DOScale(1f, 0.1f).OnComplete(() =>
    //Uimenu.GetChild(3).DOScale(1f, 0.1f).OnComplete(() =>
    // Uimenu.GetChild(4).DOScale(1f, 0.1f).OnComplete(() =>
    //  Uimenu.GetChild(5).DOScale(1f, 0.1f).OnComplete(() =>
    //  Uimenu.GetChild(6).DOScale(1f, 0.1f).OnComplete(() =>
    //  Uimenu.GetChild(7).DOScale(1f, 0.1f).OnComplete(() =>
    //  Uimenu.GetChild(8).DOScale(1f, 0.1f).OnComplete(() =>
    //   Uimenu.GetChild(9).DOScale(1f, 0.1f))))))))));
    //}

    IEnumerator ShowUi()
    {
        
        int i = 0;
        while (true)
        {
            Uimenu.GetChild(i).DOScale(1f, 0.05f);
            i += 1;
            if (i >= Uimenu.childCount)
            {
                StopCoroutine("ShowUi");
                MainUI.Instance.tool.SetActive(true);
                break;  //跳出停止了协程
            }
            yield return new WaitForSeconds(0.05f);  //等待0.05f以后
        }
    }

    public void CloseUI()
    {
        StartCoroutine("CloseUi");
    }

    IEnumerator CloseUi()
    {
        int s = Uimenu.childCount;
      //  Debug.Log(s);
        while (true)
        {
            Uimenu.GetChild(s-1).DOScale(0f, 0.05f);
            s -= 1;
            if (s <= 0)
            {
                StopCoroutine("CloseUi");
                MainUI.Instance.tool.SetActive(false);
                break;
            }
            yield return new WaitForSeconds(0.05f);
        }
    }


}
