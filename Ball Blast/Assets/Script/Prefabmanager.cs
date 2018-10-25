using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prefabmanager : MonoBehaviour {
    
    public GameObject[] bullets;  //子弹存放管理器

    public Transform bulletboxs;  //存放子弹预制 
   
    public Sprite[] sprites;

    public GameObject miss;

    // public List<Sprite> Sprite = new List<Sprite>();

    //public Sprite gun1;
    //public Sprite gun2;
    //public Sprite gun3;
    //public Sprite gun4;
    //public Sprite gun5;
    //public Sprite gun6;
    //public Sprite gun7;
    //public Sprite gun8;
    //public Sprite gun9;
    //public Sprite gun10;
    //public Sprite gun11;
    //public Sprite gun12;
    //public Sprite gun13;
    //public Sprite gun14;
    //public Sprite gun15;
    //public Sprite gun16;
    //public Sprite gun17;
    //public Sprite gun18;


    private static Prefabmanager instance;

    public static Prefabmanager Instance
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
        bulletboxs = GameObject.FindGameObjectWithTag("bulletbox").transform;
        bullets = new GameObject[bulletboxs.childCount];
        // Debug.Log(bulletboxs.transform.childCount);

        for (int i = 0; i < bulletboxs.childCount; i++)
        {
            bullets[i] = bulletboxs.GetChild(i).gameObject;
        }

    }

    void Start () {

       
       
    }


}
