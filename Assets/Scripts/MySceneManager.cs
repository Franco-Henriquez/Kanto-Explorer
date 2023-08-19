using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour {
	[SerializeField] private string loadLevel;
	private Vector3 pikaPosition;
	Scene m_Scene;
	public GameObject player;
	public GameObject pikachu;

	void OnTriggerEnter(Collider ChangeScene)	{
		string currentScene = SceneManager.GetActiveScene().name;
		player = GameObject.Find("Player");
		Debug.Log("OnTriggerEnter Triggered: MySceneManager.cs...");

		if (ChangeScene.gameObject.CompareTag("Player"))
		{
			//if player collides with the object this script is attached to. Then we load the Scene stored in loadLevel
			SceneManager.LoadScene(loadLevel);
			m_Scene = SceneManager.GetActiveScene();

			//Check if the current Active Scene's name is the Lab
			if (m_Scene.name == "Lab")

			{
				PlayerPrefs.SetString("LastScene", currentScene);
				PlayerPrefs.SetFloat("X", ChangeScene.transform.position.x);
				PlayerPrefs.SetFloat("Y", ChangeScene.transform.position.y);
				PlayerPrefs.SetFloat("Z", ChangeScene.transform.position.z);
				PlayerPrefs.Save();
			}
			//Check if the current Active Scene's name is PalletTown
			if (m_Scene.name == "PalletTown")
			{
				PlayerPrefs.SetString("LastScene", currentScene);
				PlayerPrefs.SetFloat("X", ChangeScene.transform.position.x);
				PlayerPrefs.SetFloat("Y", ChangeScene.transform.position.y);
				PlayerPrefs.SetFloat("Z", ChangeScene.transform.position.z);
				PlayerPrefs.Save();
			}
		}

	}

	void Awake()
	{
		//DontDestroyOnLoad(transform); Do I need this??
		//DontDestroyOnLoad(cameraGame);
		//DontDestroyOnLoad(canvasGame);
	}



}
