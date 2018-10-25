using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Islock : MonoBehaviour {
    private static Islock instance;
    public static Islock Instance
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
    public GameObject locker;

    public void Awake()
    {
        Instance = this;
    }
    void Start () {
        locker = this.transform.GetChild(0).gameObject;
    }
   
	void Update () {
		
	}
}
