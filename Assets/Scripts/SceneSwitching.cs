using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitching : MonoBehaviour
{

	private Vector3 pikaPosition; //we tell the game that pikaPostition is a Vector3
	Scene m_Scene; //we tell them game that m_Scene is a Scene (probably called from using UnityEngine.SceneManagement.
	string currentScene; // we tell the game that currentScene is a string (aka words)
	GameObject player; //we tell the game that player is a GameObject 

	void Awake()
    {
		Debug.Log("Awake Triggered: SceneTesting.cs...");
		//Grab the LastScene name that was created using SetString
		string lastScene = PlayerPrefs.GetString("LastScene", null);
		//Debug will literally show that m_Scene is "UnityEngine.SceneManagement.Scene" why?
		m_Scene = SceneManager.GetActiveScene();
		//Here we make it so that we get the actual name of the scene from m_Scene
		currentScene = m_Scene.name;
		//Here we try to find a gameobject with the tag of Player and we set it to a variable called player.
		player = GameObject.FindGameObjectWithTag("Player");
		Debug.Log("Active Scene Name:" + currentScene);
		Debug.Log("Previous Scene Name:" + lastScene);
		//DontDestroyOnLoad(transform);

		if (lastScene == "Lab" && currentScene == "PalletTown")
		{


			//pikaPosition = new Vector3(PlayerPrefs.GetFloat("X"), PlayerPrefs.GetFloat("Y"), PlayerPrefs.GetFloat("Z"));
			pikaPosition = new Vector3(3.5f, 0.6f, 0.4f);
			player.transform.position = pikaPosition;
			PlayerPrefs.SetString("LastScene", currentScene);

			PlayerPrefs.Save();
			Debug.Log("Updated new Player Prefs");

		}
		if (lastScene == "PalletTown" && currentScene == "Lab")
		{


			//pikaPosition = new Vector3(PlayerPrefs.GetFloat("X"), PlayerPrefs.GetFloat("Y"), PlayerPrefs.GetFloat("Z"));
			pikaPosition = new Vector3(0.0f, 0.6f, -10.3f);
			player.transform.position = pikaPosition;
			Debug.Log("transform: " + pikaPosition);
			PlayerPrefs.SetString("LastScene", currentScene);

			PlayerPrefs.Save();
			Debug.Log("Updated new Player Prefs");

		}

	}

	//// called first
	//void OnEnable()
	//{
	//    Debug.Log("OnEnable called");
	//    SceneManager.sceneLoaded += OnSceneLoaded;
	//}

	//// called second
	//void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	//{
	//    Debug.Log("OnSceneLoaded: " + scene.name);
	//    Debug.Log(mode);
	//}

	//// called third
	//void Start()
	//{
	//    Debug.Log("Start");
	//}

	// called when the game is terminated
	void OnDisable()
	{
		m_Scene = SceneManager.GetActiveScene();
		currentScene = m_Scene.name;

		//Flush all Player Preferences only when we turn off the app and the current scene is pallettown
		//note*** this seems to also delete all prefs when switching scenes?
		if (Application.isEditor && currentScene == "PalletTown")
		{
			PlayerPrefs.DeleteAll();
			Debug.Log("Deleting all Player Prefs");
		}
	}
}