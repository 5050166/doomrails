using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rote : MonoBehaviour {
	public float z=0;
	public float x=0;
	public float y=0;
	public float speed;
	// Use this for initialization
	void Start () {
       
    }
	
	// Update is called once per frame
	void Update () {
		
		transform.Rotate(new Vector3 (/*x*speed*Time.deltaTime, y*speed*Time.deltaTime, z*speed*Time.deltaTime*/0,0,10*speed));
	}
}
