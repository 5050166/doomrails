using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLock : MonoBehaviour {
    public bool unlocked = false;
    public int Price;  //价格
    public string Number = "0"; //改炮台状态  是解锁还是锁定状态  （默认是锁定状态）  1 ,0 

    private static ItemLock instance;

    public static ItemLock Instance
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
        LoadAllItemBox();
    }


    public void LoadAllItemBox()//载入盒子
    {

        if (this.transform.GetComponent<ItemLock>().Number == "0" || this.transform.GetComponent<ItemLock>().Number == "#")
        {
            //未解锁的装备
            this.transform.GetChild(1).gameObject.SetActive(true);

        }
        else if (this.transform.GetComponent<ItemLock>().Number == "1")
        {
            this.transform.GetChild(1).gameObject.SetActive(false);
        }



    }

}
