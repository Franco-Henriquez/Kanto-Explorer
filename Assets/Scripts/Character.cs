using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

	public void Back()
	{
	Animator anim = GetComponent<Animator>();
	anim.SetTrigger("PikaBack");
	}


	void Update()
	{
		//Debug code to show current coordinates of GameObject this is attached to when pressing letter p
		if (Input.GetKeyDown(KeyCode.P)) 
		{
			Debug.Log(transform.position);
		}

	}


}
