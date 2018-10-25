// TripleBullet
using UnityEngine;

public class TripleBullet : Bullet
{
    public GameObject bullet;

    public override void BulletBehavior()
    {
        base.BulletBehavior();
        Vector3 position = this.transform.position;
        if (position.y > -1.1f)
        {
            GameObjectPool.Instance.CreateObject(bullet, base.transform.position + new Vector3(0.15f, 0f), Quaternion.Euler(0f, 0f, -90f));
            GameObjectPool.Instance.CreateObject(bullet, base.transform.position + new Vector3(-0.15f, 0f), Quaternion.Euler(0f, 0f, -90f));
            GameObjectPool.Instance.CreateObject(bullet, base.transform.position, Quaternion.Euler(0f, 0f, -90f));
            GameObjectPool.Instance.CloseGameObjectImmediately(this.gameObject);
        }
    }
}
