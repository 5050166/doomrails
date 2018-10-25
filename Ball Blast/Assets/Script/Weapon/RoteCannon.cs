using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoteCannon : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Roto();

    }

    public void Roto() //炮台转动 只可以调用一次  拖的时候检测一下 CanRote，  换炮 dokill    CanRote换false
    {

        transform.Rotate(new Vector3(0, 0, 45f));

        this.transform.DOLocalRotate(new Vector3(0, 0, -45f), 1.4f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
    }
}
