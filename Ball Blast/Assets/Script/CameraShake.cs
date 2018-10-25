using System;
using UnityEngine;


public class CameraShake : MonoBehaviour
{
   
    public static CameraShake instance;

    
    private float time;  //震动时间


    private float amount; //震动幅度

    private void Awake()
    {
     instance = this;
    }


    public void ShakeIt(float _amount, float _duration)
    {
        this.time = _duration;
        this.amount = _amount;
    }


    private void Update()
    {
        if (this.time > 0f)
        {
            this.time -= Time.deltaTime;
            float x = UnityEngine.Random.Range(-this.amount, this.amount);    //随机值x
            float y = UnityEngine.Random.Range(-this.amount, this.amount);    //随机值y
           this.transform.localPosition = new Vector3(x, y, -100f);
         //  Handheld.Vibrate();

        }
        else
        {
            this.transform.localPosition = new Vector3(0f, 0f, -100f);
        }
    }

}
