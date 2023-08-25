using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LabVideoControl : MonoBehaviour, IPointerClickHandler {
	//public GameObject player;
	public GameObject mainCam;
	public UnityEngine.Video.VideoClip videoClip;

	// Use this for initialization
	//void Start () {
	void OnTriggerEnter(Collider vidPlayerTrigger)
		{
		Debug.Log("PLAY VID");

			//mainCam = GameObject.Find("Main Camera");//this wont work for VR.
			mainCam = GameObject.Find("CenterEyeAnchor");
			GameObject mapCamera = GameObject.Find("Map Camera");
			GameObject videoPlayerObj = GameObject.Find("Video Player");

			var videoPlayer = videoPlayerObj.GetComponent<UnityEngine.Video.VideoPlayer>();
			var cameraAudio = mainCam.GetComponent<AudioSource>();



		if (vidPlayerTrigger.gameObject.CompareTag("Attack"))
		{
			Debug.Log("Player Trigger set to Attack?");

			videoPlayer.Play();

			if (!mapCamera.GetComponent<UnityEngine.Video.VideoPlayer>())
			{

				// VideoPlayer automatically targets the camera backplane when it is added
				// to a camera object, no need to change videoPlayer.targetCamera.
				var videoPlayerMap = mapCamera.AddComponent<UnityEngine.Video.VideoPlayer>();
				videoPlayerMap.clip = videoClip;
				videoPlayerMap.renderMode = UnityEngine.Video.VideoRenderMode.CameraNearPlane;

				//videoPlayerMap.videoClip.originalPath = "/Videos/Pikachu Dance.mp4";
			}
			else
			{
				var videoPlayerMap = mapCamera.GetComponent<UnityEngine.Video.VideoPlayer>();
				videoPlayerMap.Play();
			}

			cameraAudio.Stop();
		}
		else
		{
		Debug.Log("Player Trigger mismatch");
		}
	}
		

	void OnTriggerExit(Collider vidPlayerTrigger)
	{
		Debug.Log("PAUSE VID");
		mainCam = GameObject.Find("CenterEyeAnchor");
		GameObject mapCamera = GameObject.Find("Map Camera");
		GameObject videoPlayerObj = GameObject.Find("Video Player");

		var videoPlayer = videoPlayerObj.GetComponent<UnityEngine.Video.VideoPlayer>();
		var cameraAudio = mainCam.GetComponent<AudioSource>();
		var videoPlayerMap = mapCamera.GetComponent<UnityEngine.Video.VideoPlayer>();

		if (vidPlayerTrigger.gameObject.CompareTag("Player"))
		{
			videoPlayer.Pause();
			//THE BELOW LINE CAUSES AN ERROR and because of the nature of C# any line after that (within the same void statement) is ignored.
			//Must add an IF statement to only pause the map video if the video is present.
			if (videoPlayerMap)
			{
				videoPlayerMap.Pause();
			}
			if (!cameraAudio.isPlaying)
			{

				cameraAudio.Play();
			}
		}
	}

	public virtual void OnPointerClick(PointerEventData eventData)
	{
		//I named my button the name of the scene i want to load
		Debug.Log("Switching level: " + eventData.pointerPress.name);
		GameObject mapCamera = GameObject.Find("Map Camera");
		var videoPlayerMap = mapCamera.GetComponent<UnityEngine.Video.VideoPlayer>();
		Destroy(videoPlayerMap);
	}

	//}

	// Update is called once per frame
	void Update () {
		
	}
}
