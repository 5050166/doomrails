using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 游戏对象池 用于频繁创建销毁游戏物体时提高性能 
/// </summary>
public class GameObjectPool : MonoSingleton<GameObjectPool>
{
    //对象池字典
    private Dictionary<string, List<GameObject>> gameObjectPool = new Dictionary<string, List<GameObject>>();

    /// <summary>
    /// 创建游戏对象
    /// </summary>
    /// <param name="obj"> 需要创建的游戏对象 </param>
    /// <param name="pos"> 所创建对象目标位置 </param>
    /// <param name="direction"> 所创建对象目标旋转 </param>
    /// <returns></returns>
    public GameObject CreateObject(GameObject obj, Vector3 pos, Quaternion direction)
    {
        //Debug.LogError(obj.name + ":" + gameObjectPool.Count + ":" + gameObjectPool.ContainsKey(obj.name));
        if (gameObjectPool.ContainsKey(obj.name))  //如果在对象池（字典中）中找到了
        {
            List<GameObject> objs = gameObjectPool[obj.name];
            List<GameObject> result = objs.FindAll(e => e.activeSelf == false);
            if (result.Count != 0)
            {
                GameObject target = result[0];
                target.transform.localPosition= pos;
                target.transform.rotation = direction;
                Reset(target);
                target.SetActive(true);
                target.transform.SetAsLastSibling();
                return target;
            }
            else
            {
                GameObject target = GameObject.Instantiate(obj, pos, direction) as GameObject;
                objs.Add(target);
                return target;
            }
        }
        else //对象池（字典中）没有找到要找的内容   加入到对象池中
        {
            List<GameObject> objs = new List<GameObject>();
            gameObjectPool.Add(obj.name, objs);
            return CreateObject(obj, pos, direction);
        }
    }
    /// <summary>
    /// 设置创建物体的父物体（创建的物体都会在这个物体之下）
    /// </summary>
    /// <param name="obj">目标物体</param>
    /// <param name="parent">目标父物体</param>
    /// <returns></returns>
    public GameObject CreateObject(GameObject obj, Transform parent)
    {
        GameObject result = CreateObject(obj, parent.localPosition, parent.rotation);
        result.transform.SetParent(parent);
        return result;
    }

    /// <summary>
    /// 调用物体复位接口方法
    /// </summary>
    /// <param name="obj"></param>
    private void Reset(GameObject obj)
    {
        IReset target = obj.GetComponent<IReset>();
        if (target != null)
            target.ResetStatus();
    }

    public void CloseGameObjectWithKey(string key)
    {
        if (gameObjectPool.ContainsKey(key))
            foreach (var item in gameObjectPool[key])
            {
                CloseGameObjectImmediately(item);
            }
    }
    /// <summary>
    /// 立即回收物
    /// </summary>
    /// <param name="obj">目标物体</param>
    public void CloseGameObjectImmediately(GameObject obj)
    {
        obj.SetActive(false);
    }

    /// <summary>
    /// 延迟回收物体
    /// </summary>
    /// <param name="obj">目标物体</param>
    /// <param name="delayTime">延迟时间</param>
    public void CloseGameObjectDelay(GameObject obj, float delayTime)
    {
        StartCoroutine(CloseDelay(obj, delayTime));
    }
    private IEnumerator CloseDelay(GameObject obj, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        CloseGameObjectImmediately(obj);
    }

    /// <summary>
    /// 清空对象池中某种物体缓存
    /// </summary>
    /// <param name="key">物体名</param>
    public void RemoveKey(GameObject obj)
    {
        if (gameObjectPool.ContainsKey(obj.name))
        {
            foreach (var item in gameObjectPool[obj.name])
            {
                Destroy(item.gameObject);
            }
            gameObjectPool.Remove(obj.name); 

        }
    }

    /// <summary>
    /// 清空当前对象池所有缓存
    /// </summary>
    public void RemoveAll()
    {
        foreach (var item in gameObjectPool.Values)
        {
            foreach (var value in item)
            {
                Destroy(value.gameObject);
            }
        }
        gameObjectPool.Clear();
        gameObjectPool = new Dictionary<string, List<GameObject>>();

    }
}

/// <summary>
/// 复位接口
/// </summary>
public interface IReset
{
    void ResetStatus();
}
