// Bullet
using DG.Tweening;
using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed; //子弹飞行速度 

    //  public float BulletNumber;//子弹数量 ?????

    public float damage;
    public Vector3 Towards;

    //	public GameObject hitParticle; //击小球中后的粒子特效
    private void Start()
    {
        if (this.transform.name.Contains("Bullet 8")|| this.transform.name.Contains("Bullet 30"))
        {
            Destroy(this.gameObject, 0.6f);
            transform.DOScaleX(1.42f, 0.2f).OnComplete(() => transform.DOScaleX(0, 0.25f));
            //  StartCoroutine(DisableGameObject("disablegameobject", 1f));     
        }
    }

    void OnTweenComplete(string name)
    {
        switch (name)
        {//name这个是方法名称 需要执行的
            case "disablegameobject":
                GameObjectPool.Instance.CloseGameObjectImmediately(this.gameObject);
                StopCoroutine("disablegameobject");
                break;
         
        }
    }
    IEnumerator DisableGameObject(string name, float waittime)
    {
        yield return new WaitForSeconds(waittime); //等待这么多秒以后开始执行动画
        OnTweenComplete(name);

    }

    private void Update()
    {
  
        if (transform.name.Contains("Bullet 9"))
        {

        }
        else
        {
            BulletBehavior();
        }
    }

    public virtual void BulletBehavior()
    {

        this.transform.Translate(Towards.normalized * (speed * 2f) * Time.deltaTime);  //向上移动

        Vector3 position = this.transform.localPosition;
        if (position.y > 1000f || position.x < -560 || position.x > 560f)
        {
            GameObjectPool.Instance.CloseGameObjectImmediately(this.gameObject);
        }

    }


    private void OnTriggerEnter2D(Collider2D col)  //击中目标后
    {
        if (!(col.gameObject.tag == "ball") && !(col.gameObject.tag == "big ball"))   //碰撞的物体不是小球
        {

            return;

        }

        //GameObject gameObject = Instantiate(hitParticle, this.transform.position, Quaternion.identity);  //子弹命中小球产生的特效

    }
}
