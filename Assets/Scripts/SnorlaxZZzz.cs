using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]

public class SnorlaxZZzz : MonoBehaviour {
	Animator anim1;
	Animator anim2;
	Animator anim3;
	Animator anim4;
	Animator snorlaxAnimation;
	Renderer mRenderedComponent1;
	Renderer mRenderedComponent2;
	Renderer mRenderedComponent3;
	Renderer mRenderedComponent4;
	Animator animComponent1;
	Animator animComponent2;
	Animator animComponent3;
	Animator animComponent4;
	GameObject animObject1;
	GameObject animObject2;
	GameObject animObject3;
	GameObject animObject4;
	GameObject snorlaxObject;
	public AudioClip wakingSnorlaxAudio;


	Component animComp;
	// Use this for initialization
	void Start () {
		animObject1 = GameObject.Find("ZZzz1");
		animObject2 = GameObject.Find("ZZzz2");
		animObject3 = GameObject.Find("ZZzz3");
		animObject4 = GameObject.Find("ZZzz4");
		snorlaxObject = GameObject.Find("Snorlax");

		//this saves the animation the animation we found in the anim var
		anim1 = animObject1.gameObject.GetComponent<Animator>();
		anim2 = animObject2.gameObject.GetComponent<Animator>();
		anim3 = animObject3.gameObject.GetComponent<Animator>();
		anim4 = animObject4.gameObject.GetComponent<Animator>();
		snorlaxAnimation = snorlaxObject.gameObject.GetComponent<Animator>();




		//this saves the specific animation name component from the ZZzz gameobject and stores it in animComponent - maybe we don't need this?
		animComponent1 = animObject1.GetComponent<Animator>();
		animComponent2 = animObject2.GetComponent<Animator>();
		animComponent3 = animObject3.GetComponent<Animator>();
		animComponent4 = animObject4.GetComponent<Animator>();
		mRenderedComponent1 = animObject1.GetComponent<MeshRenderer>();
		mRenderedComponent2 = animObject2.GetComponent<MeshRenderer>();
		mRenderedComponent3 = animObject3.GetComponent<MeshRenderer>();
		mRenderedComponent4 = animObject4.GetComponent<MeshRenderer>();

	}

	IEnumerator OnTriggerEnter(Collider CollideTrigger)
	{
		Debug.Log("Enter Triggered: SnorlaxZZzz.cs...");
		if (CollideTrigger.gameObject.CompareTag("Player"))
		{
			Debug.Log("ZZzz Started!");

			//this will enable the ZZzz gameobject (why we disabled? because we want to hide the text completely unless we get close to it.
			//animObject.SetActive(true); // doesn't seem to work...

			//this enables the animation component that is inside of the ZZzz GameObject
			anim1.SetTrigger("ZZzzTrigger");
			mRenderedComponent1.enabled = true;
			animComponent1.enabled = true;

			anim2.SetTrigger("ZZzzTrigger");
			mRenderedComponent2.enabled = true;
			animComponent2.enabled = true;

			anim3.SetTrigger("ZZzzTrigger");
			mRenderedComponent3.enabled = true;
			animComponent3.enabled = true;

			anim4.SetTrigger("ZZzzTrigger");
			mRenderedComponent4.enabled = true;
			animComponent4.enabled = true;
			//if player collides with the object this script is attached to. Then we start the ZZzz trigger set in the animator on Snorlax

		}

		if (CollideTrigger.gameObject.CompareTag("Attack"))
		{
			Debug.Log("Snorlax was Attacked!");

			//this will enable the ZZzz gameobject (why we disabled? because we want to hide the text completely unless we get close to it.
			//animObject.SetActive(true); // doesn't seem to work...

			//this enables the animation component that is inside of the ZZzz GameObject
			snorlaxAnimation.SetTrigger("Snorlax Bellydrum");
			AudioSource snorlaxDefaultAudio = snorlaxObject.gameObject.GetComponent<AudioSource>();

			//snorlaxDefaultAudio.Play();
			yield return new WaitForSeconds(snorlaxDefaultAudio.clip.length);
			snorlaxDefaultAudio.clip = wakingSnorlaxAudio;
			snorlaxDefaultAudio.Play();


			//if player collides with the object this script is attached to. Then we start the ZZzz trigger set in the animator on Snorlax

		}

	}

	void OnTriggerExit(Collider CollideTrigger)
	{
		Debug.Log("Exit Triggered: SnorlaxZZzz.cs...");
		if (CollideTrigger.gameObject.CompareTag("Player"))
		{
			Debug.Log("ZZzz exiting!");

			//this will enable the ZZzz gameobject (why we disabled? because we want to hide the text completely unless we get close to it.
			//animObject.SetActive(true); // doesn't seem to work...

			//this enables the animation component that is inside of the ZZzz GameObject
			anim1.ResetTrigger("ZZzzTrigger");
			mRenderedComponent1.enabled = false;
			animComponent1.enabled = false;

			anim2.ResetTrigger("ZZzzTrigger");
			mRenderedComponent2.enabled = false;
			animComponent2.enabled = false;

			anim3.ResetTrigger("ZZzzTrigger");
			mRenderedComponent3.enabled = false;
			animComponent3.enabled = false;

			anim4.ResetTrigger("ZZzzTrigger");
			mRenderedComponent4.enabled = false;
			animComponent4.enabled = false;

			snorlaxAnimation.ResetTrigger("Snorlax Bellydrum");

			//if player collides with the object this script is attached to. Then we start the ZZzz trigger set in the animator on Snorlax

		}

		if (CollideTrigger.gameObject.CompareTag("Attack"))
		{
			//snorlaxAnimation.ResetTrigger("Snorlax Bellydrum");
		}

	}

	// Update is called once per frame
	void Update () {
		
	}
}
