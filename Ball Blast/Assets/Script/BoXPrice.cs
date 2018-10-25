using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoXPrice : MonoBehaviour {
    public int Price;//炮的价格
    public int damage;
    public float speed;
    public float bulletspeed;
    public string property; //属性
    private static BoXPrice instance;

    public static BoXPrice Instance
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
}
