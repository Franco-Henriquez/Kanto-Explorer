using System.Collections.Generic;
using System.Linq;
using System.Collections;
using UnityEngine;
using System;
using System.Text;
#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
using UnityEngine.Windows.Speech;
#endif

[RequireComponent(typeof(AudioSource))]

public class VoiceCommanderBeta : MonoBehaviour {
#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN 
	[SerializeField]
	public ConfidenceLevel confidence = ConfidenceLevel.Low;
	public float speed = 1;
	public GameObject thunderObject;
	public GameObject piListenObject;
	public GameObject piConfusedObject;
	public float triggerSecs = 6;
	public float secs = 3;
	public bool triggerWord = false;
	public bool actionTriggered = false;
	public AudioSource audio1; 
	public AudioSource audio2; 

	AudioSource audioData;

	protected string word = "Pikachu";

	KeywordRecognizer keywordRecognizer;
Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();
	//Create keywords for keyword recognizer

	void Start()
	{
		audioData = GetComponent<AudioSource>();

		keywords.Add("pikachu", () =>
		{
			// action to be performed when this keyword is spoken
			Debug.Log("keyword: pikachu");

		});

		keywords.Add("pikchu", () =>
		{
			// action to be performed when this keyword is spoken
			Debug.Log("keyword: pikchu??");

		});

		keywords.Add("pikechu", () =>
		{
			// action to be performed when this keyword is spoken
			Debug.Log("keyword: pikechu??");

		});

		keywords.Add("thunderbolt", () =>
		{
			// action to be performed when this keyword is spoken
			Debug.Log("keyword: thunderbolt");

		});

		keywords.Add("hey", () =>
		{
			// action to be performed when this keyword is spoken
			Debug.Log("keyword: hey");
			triggerWord = true;
			audio2.Play(0);


		});


		keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray(), confidence);
				keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
				keywordRecognizer.Start();

	}
	private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
	{
		System.Action keywordAction;
		// if the keyword recognized is in our dictionary, call that Action.
		Debug.Log("Voice System Action triggered?");

		if (keywords.TryGetValue(args.text, out keywordAction))
		{
			keywordAction.Invoke();
			Debug.Log("Some action invoked?");

			if (piListenObject.gameObject.activeSelf) //checks if pikachu has been called first, to use other actions.
			{
				print("piListenObject is active?");
				if (args.text == "thunderbolt")
				{
					audioData.Play(0);
					actionTriggered = true;
					StartCoroutine(LateCallTbolt()); //this starts a co routine that will de activate thunderbolt
				}

			}


			//TO DO NEXT -- May need to create a HEY blank object just to enable then disable after a second.
			//So that we can then trigger the word 'pikachu'. Just like pikachu is the trigger word for thunderbolt listener


			if (args.text == "pikachu" || args.text == "pikechu" || args.text == "pikchu" && triggerWord)
			{
				//get pikachu's attention!
				print("Pikachu has your attention!");
				piListenObject.gameObject.SetActive(true);
				StartCoroutine(LateCallPika()); //this starts a co routine that will de activate the above animatin game object
				triggerWord = false;

			}

			//}

		}
		else
		{
			Debug.Log("Pikachu did not understand you :(");
			piConfusedObject.gameObject.SetActive(true);
			StartCoroutine(LateCallPika2());
		}




	}

		IEnumerator LateCallPika()
	{

		yield return new WaitForSeconds(triggerSecs);
		piListenObject.gameObject.SetActive(false);
		if (!actionTriggered)
		{
			piConfusedObject.gameObject.SetActive(true);
			StartCoroutine(LateCallPika2());
		}
		else
		{
			actionTriggered = false;
		}

	}

	IEnumerator LateCallPika2()
	{
		yield return new WaitForSeconds(triggerSecs);
		piConfusedObject.gameObject.SetActive(false);

	}

	IEnumerator LateCallTbolt()
	{
		yield return new WaitForSeconds(1);
		thunderObject.gameObject.SetActive(true);
		StartCoroutine(LateCallTbolt2());
	}

	IEnumerator LateCallTbolt2()
	{
		yield return new WaitForSeconds(secs);
		thunderObject.gameObject.SetActive(false);
	}

	private void OnApplicationQuit()
	{
		if (keywordRecognizer != null && keywordRecognizer.IsRunning)
		{
			keywordRecognizer.OnPhraseRecognized -= KeywordRecognizer_OnPhraseRecognized;
			keywordRecognizer.Stop();
		}
	}
#endif
}



