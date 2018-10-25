using System;
using UnityEngine;


public class EdgeControll : MonoBehaviour
{
    public static EdgeControll Instance;


    public GameObject left;


    public GameObject right;


    public float posx0 { get; private set; }


    public float posx1 { get; private set; }

    private void Awake()
    {
        EdgeControll.Instance = this;
    }

    private void Start()
    {
        Vector3 vector = Camera.main.WorldToScreenPoint(new Vector3(0f, 0f, 0f));
        this.posx0 = vector.x;  //-0.5
        this.posx1 = -vector.x; //0.5
       // this.left.transform.localPosition = new Vector3(vector.x - 580f, 0f, 0f);
       // this.right.transform.localPosition = new Vector3(vector.x + 580f, 0f, 0f);
    }
}
