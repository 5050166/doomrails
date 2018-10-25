// Bullet
using DG.Tweening;
using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed; //�ӵ������ٶ� 

    //  public float BulletNumber;//�ӵ����� ?????

    public float damage;
    public Vector3 Towards;

    //	public GameObject hitParticle; //��С���к��������Ч
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
        {//name����Ƿ������� ��Ҫִ�е�
            case "disablegameobject":
                GameObjectPool.Instance.CloseGameObjectImmediately(this.gameObject);
                StopCoroutine("disablegameobject");
                break;
         
        }
    }
    IEnumerator DisableGameObject(string name, float waittime)
    {
        yield return new WaitForSeconds(waittime); //�ȴ���ô�����Ժ�ʼִ�ж���
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

        this.transform.Translate(Towards.normalized * (speed * 2f) * Time.deltaTime);  //�����ƶ�

        Vector3 position = this.transform.localPosition;
        if (position.y > 1000f || position.x < -560 || position.x > 560f)
        {
            GameObjectPool.Instance.CloseGameObjectImmediately(this.gameObject);
        }

    }


    private void OnTriggerEnter2D(Collider2D col)  //����Ŀ���
    {
        if (!(col.gameObject.tag == "ball") && !(col.gameObject.tag == "big ball"))   //��ײ�����岻��С��
        {

            return;

        }

        //GameObject gameObject = Instantiate(hitParticle, this.transform.position, Quaternion.identity);  //�ӵ�����С���������Ч

    }
}
