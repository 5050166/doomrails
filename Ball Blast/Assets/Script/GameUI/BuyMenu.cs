using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyMenu : MonoBehaviour {

    public GameObject MessageBox;
    public Text message;
    public GameObject go; //当前选中物体
    public GameObject box; 

    private void Start()
    {
        MessageBox = this.transform.parent.GetChild(1).gameObject;
        message = this.transform.parent.GetChild(1).GetChild(2).GetComponent<Text>();
    }


    public void Unlock()
    {
        if (PlayerPrefs.GetInt("coin") >box.GetComponent<ItemLock>().Price)
        { //当前金钱大于解锁价格

            int num = PlayerPrefs.GetInt("coin") - box.GetComponent<ItemLock>().Price;
            Debug.Log(num);
            PlayerPrefs.SetInt("coin", num);  //存储金钱
            MainUI.Instance.UpdateCoinText();

            ItemLock.Instance.unlocked = true; //解锁

            StartCoroutine("Check");
            MessageBox.SetActive(false);

        }
        else
        {
            message.text = "Sorry,No Enough Money ";
        }

    }


    public void Nobuttom()
    {

        ItemLock.Instance.unlocked = false;
      MessageBox.SetActive(false);
    }


    IEnumerator Check()
    {
        yield return new WaitUntil(unlock); //直到某个数值为XX
        box.transform.GetChild(1).gameObject.SetActive(false);
        box.GetComponent<ItemLock>().Number="1"; //解锁了方格 

    }
    public bool unlock() {
        return ItemLock.Instance.unlocked;
    }



    public void Openbuymenu()
    {

        if (MessageBox.activeSelf) //消息框开启了 就把他关闭
        {
            MessageBox.SetActive(false);
        }
        else
        {
            go = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject; //获取当前ui下点击的
          
            box = go.transform.parent.gameObject;  //这个按钮
            Debug.Log(box.GetComponent<ItemLock>().Price);
          
            message.text = "Cost " + box.GetComponent<ItemLock>().Price + " Unlock New Parts";
            MessageBox.SetActive(true);
        }


    }
}
