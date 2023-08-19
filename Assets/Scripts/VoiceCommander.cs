
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
using UnityEngine.Windows.Speech;
#endif
public class VoiceCommander : MonoBehaviour
{

#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN

	[SerializeField]
	public string[] m_Keywords = new string[] { "hey", "pikachu", "thunderbolt" };
	public ConfidenceLevel confidence = ConfidenceLevel.Low;
	public float speed = 1;
	public GameObject thunderObject;
	public GameObject piObject;
	public float secs = 3;

	protected string word = "Pikachu";

	private KeywordRecognizer m_Recognizer;

	void Start()
	{

		if (m_Keywords != null)
		{


			m_Recognizer = new KeywordRecognizer(m_Keywords, confidence);
			m_Recognizer.OnPhraseRecognized += OnPhraseRecognized;
			m_Recognizer.Start();
		}
	}

	private void OnPhraseRecognized(PhraseRecognizedEventArgs args)
	{
		StringBuilder builder = new StringBuilder();
		builder.AppendFormat("{0} ({1}){2}", args.text, args.confidence, Environment.NewLine);
		builder.AppendFormat("\tTimestamp: {0}{1}", args.phraseStartTime, Environment.NewLine);
		builder.AppendFormat("\tDuration: {0} seconds{1}", args.phraseDuration.TotalSeconds, Environment.NewLine);
		Debug.Log(builder.ToString());

		
		if (piObject.gameObject.activeSelf) //checks if pikachu has been called first, to use other actions.
		{
			print("Pikachu has your attention!");
			if (args.text == "thunderbolt")
			{
				thunderObject.gameObject.SetActive(true);
				StartCoroutine(LateCallTbolt()); //this starts a co routine that will de activate thunderbolt
			}

		}

		if (args.text == "hey")
		{
			if (args.text == "pikachu")
			{
				//get pikachu's attention!
				piObject.gameObject.SetActive(true);
				StartCoroutine(LateCallPika()); //this starts a co routine that will de activate the above animatin game object

			}

		}



	}

	IEnumerator LateCallPika()
	{

		yield return new WaitForSeconds(secs);

		piObject.gameObject.SetActive(false);

	}

	IEnumerator LateCallTbolt()
	{

		yield return new WaitForSeconds(secs);

		thunderObject.gameObject.SetActive(false);

	}

	private void OnApplicationQuit()
	{
		if (m_Recognizer != null && m_Recognizer.IsRunning)
		{
			m_Recognizer.OnPhraseRecognized -= OnPhraseRecognized;
			m_Recognizer.Stop();
		}
	}
#endif

}
