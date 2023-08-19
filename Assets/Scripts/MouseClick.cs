using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseClick : MonoBehaviour, IPointerClickHandler {

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update()
	{

	}
	void OnMouseDown()
	{
		if (Input.GetMouseButtonDown(0))
			Debug.Log("Pressed primary button.");

		if (Input.GetMouseButtonDown(1))
			Debug.Log("Pressed secondary button.");

		if (Input.GetMouseButtonDown(2))
			Debug.Log("Pressed middle click.");
	}

	public virtual void OnPointerClick(PointerEventData eventData)
	{
		//I named my button the name of the scene i want to load
		Debug.Log("Clicked: " + eventData.pointerPress.name);
		GameObject mapCamera = GameObject.Find("Map Camera");
		var videoPlayerMap = mapCamera.GetComponent<UnityEngine.Video.VideoPlayer>();
		Destroy(videoPlayerMap);
	}

	//void UnityApiMouseEvents()
	//{
	//	RaycastHit hit;
	//	if (Physics.Raycast(hit))
	//	{
	//		if (hit.rigidbody != null)
	//			hit.rigidbody.gameObject.SendMessage("OnMouseDown");
	//		else
	//			hit.collider.SendMessage("OnMouseDown");
	//	}

	//}
}

