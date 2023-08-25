using UnityEngine;
using System.Collections;
using UnityEngine.UI;    //Allows us to use User Interface code.
//using UnityEngine.Input.OVRInput;
//using UnityEngine.Input;

public class ControllerVR : MonoBehaviour {

	//JoyStick Only Vars
	protected Joystick joystick;


	// public variables
	public float moveSpeed = 3.0f;
	public float gravity = 9.81f;

	private CharacterController myWASDController;
	private CharacterController mySTICKController;

	Animator anim;

	// variable used for screen touch camera movement
	private Vector2 touchOrigin = -Vector2.one;    //Used to store location of screen touch origin for mobile controls.
//#if UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_EDITOR || UNITY_WEBGL //comment this out because these varables are being called.
	private double right_x = .45f;
	private double left_x = -.45f;
	private float up_z = .45f; //max is 0.99
	private float down_z = -.45f;
//#endif



	// Use this for initialization
	void Start () {
		// store a reference to the CharacterController component on this gameObject
		// it is much more efficient to use GetComponent() once in Start and store
		// the result rather than continually use etComponent() in the Update function
		myWASDController = gameObject.GetComponent<CharacterController>();
		mySTICKController = gameObject.GetComponent<CharacterController>();
		anim = gameObject.GetComponent<Animator>();

		joystick = FindObjectOfType<Joystick>();




	}

	// Update is called once per frame
	void Update () {



		/*#if UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_EDITOR || UNITY_WEBGL*//*
		int horizontal = 0;      //Used to store the horizontal move direction.
		int vertical = 0;        //Used to store the vertical move direction.

		//Get input from the input manager, round it to an integer and store in horizontal to set x axis move direction
		horizontal = (int)(Input.GetAxisRaw("Horizontal"));

		//Get input from the input manager, round it to an integer and store in vertical to set y axis move direction
		vertical = (int)(Input.GetAxisRaw("Vertical"));

		//Check if moving horizontally, if so set vertical to zero.
		if (horizontal != 0)
		{
			vertical = 0;
		}

		var rigidbody = GetComponent<Rigidbody>(); //this may not be needed anymore

		//Copied from Vector3 code below - this allows the control of the character with the virtual joystick
		*//* disabling because we are working with vr
		 * Vector3 movementStickZ = joystick.Vertical * Vector3.forward * moveSpeed * Time.deltaTime;
				Vector3 movementStickX = joystick.Horizontal * Vector3.right * moveSpeed * Time.deltaTime;
				Vector3 movementStick = transform.TransformDirection(movementStickZ + movementStickX);
				movementStick.y -= gravity * Time.deltaTime;
				mySTICKController.Move(movementStick); 

		 */

		/*#endif*/

		/*		object p = print("Left Controller Axis: " + Axis2D.SecondaryThumbstick);
		*//*		//rigidbody.velocity = new Vector3(joystick.Horizontal * 100f,
				//								rigidbody.velocity.y,
				//								joystick.Vertical * 100f);

				// Determine how much should move in the z-direction
				Vector3 movementZ = Input.GetAxis("Vertical") * Vector3.forward * moveSpeed * Time.deltaTime;

				// Determine how much should move in the x-direction
				Vector3 movementX = Input.GetAxis("Horizontal") * Vector3.right * moveSpeed * Time.deltaTime;

				// Convert combined Vector3 from local space to world space based on the position of the current gameobject (player)
				Vector3 movement = transform.TransformDirection(movementZ+movementX);

				// Apply gravity (so the object will fall if not grounded)
				movement.y -= gravity * Time.deltaTime;

				//Debug.Log ("Movement Vector = " + movement);

				// Actually move the character controller in the movement direction
				myWASDController.Move(movement);
		*/


		/*----------VR MOVEMENT EVENTS----------*/

		//Axis2D.SecondaryThumbstick
		//defining the right and left Stick Axes so that we can use it for IF statements and comparisons
		Vector2 rightStickAxis = OVRInput.Get(OVRInput.RawAxis2D.RThumbstick);
		Vector2 leftStickAxis = OVRInput.Get(OVRInput.RawAxis2D.LThumbstick);
		//var axes2 = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
		//Debug.Log ("Movement Right Controller Twist = " + rightStickAxis + " | x : " + rightStickAxis.x);
		//Debug.Log ("Movement Left Controller Twist = " + rightStickAxis + " | x : " + rightStickAxis.x);


		
		
		double rightStickAxisX = rightStickAxis.x;
		
		double leftStickAxisX = leftStickAxis.x;
		double leftStickAxisY = leftStickAxis.y;


		/*TO DO: Find universal reset for animation to save other resets
		 * new unity version has some improvements that could alliviate old workaround code for animations.
		 */
		if (rightStickAxisX < left_x || leftStickAxisX < left_x)
		{
			//Animator anim = GetComponent<Animator>();
			anim.ResetTrigger("PikaRight Idle");
			anim.ResetTrigger("PikaRight");
			anim.ResetTrigger("PikaForward Idle");
			anim.ResetTrigger("PikaForward");
			anim.ResetTrigger("PikaBack Idle");
			anim.ResetTrigger("PikaBack");
			anim.SetTrigger("PikaLeft");
		}
		if (rightStickAxisX > right_x || leftStickAxisX > right_x)
		{
			//Animator anim = GetComponent<Animator>();
			anim.ResetTrigger("PikaLeft Idle");
			anim.ResetTrigger("PikaLeft");
			anim.ResetTrigger("PikaForward Idle");
			anim.ResetTrigger("PikaForward");
			anim.ResetTrigger("PikaBack Idle");
			anim.ResetTrigger("PikaBack");
			anim.SetTrigger("PikaRight");
		}
		if (leftStickAxisY > up_z) 
		{
			//Animator anim = GetComponent<Animator>();
			anim.ResetTrigger("PikaBack Idle");
			anim.ResetTrigger("PikaBack");
			anim.ResetTrigger("PikaLeft Idle");
			anim.ResetTrigger("PikaLeft");
			anim.ResetTrigger("PikaRight Idle");
			anim.ResetTrigger("PikaRight");
			anim.SetTrigger("PikaForward");
		}

		if (leftStickAxisY < down_z) //Input.GetKeyDown(KeyCode.S) || 
		{
			//Animator anim = GetComponent<Animator>();
			anim.ResetTrigger("PikaForward Idle");
			anim.ResetTrigger("PikaForward");
			anim.ResetTrigger("PikaLeft Idle");
			anim.ResetTrigger("PikaLeft");
			anim.ResetTrigger("PikaRight Idle");
			anim.ResetTrigger("PikaRight");
			anim.SetTrigger("PikaBack");
		}

		/*#if UNITY_STANDALONE //use this for debuging only

				//Debug.Log("Movement is: " + joystick.Horizontal);


				if (joystick.Horizontal > right_x)
				{
					print("moving right");
				}
				if (joystick.Horizontal < left_x)
				{
					print("moving left");
				}

				if (joystick.Vertical > up_z)
				{
					print("moving up");
				}
				if (joystick.Vertical < down_z)
				{
					print("moving down");
				}
		#endif

				/*
				 NOTES SO FAR:
				ONLY THE LEFT CONTROLLER WORKS, RIGHT CONTROLLER DOESN'T DO ANYTHING.
				THE VR CAMERA MOVES EVERYTHING INSTEAD OF MOVING WELL... THE CAMERA ONLY? 
				CURRENTLY TROUBLESHOOTING THE OVRPLAYERCONTROLLER AS A CHILD OF PIKACHU - just enabled debug mode on the lower right of unity
				 */
		/*if (joystick.Vertical > up_z) // Input.GetKeyDown(KeyCode.W) ||
		{
			//Move the Rigidbody forwards constantly at speed you define (the blue arrow axis in Scene view)
			//Animator anim = GetComponent<Animator>();
			anim.ResetTrigger("PikaBack Idle");
			anim.ResetTrigger("PikaBack");
			anim.ResetTrigger("PikaLeft Idle");
			anim.ResetTrigger("PikaLeft");
			anim.ResetTrigger("PikaRight Idle");
			anim.ResetTrigger("PikaRight");
			anim.SetTrigger("PikaForward");
		}

		if (joystick.Vertical < down_z) //Input.GetKeyDown(KeyCode.S) || 
		{
			//Move the Rigidbody backwards constantly at the speed you define (the blue arrow axis in Scene view)
			//Animator anim = GetComponent<Animator>();
			anim.ResetTrigger("PikaForward Idle");
			anim.ResetTrigger("PikaForward");
			anim.ResetTrigger("PikaLeft Idle");
			anim.ResetTrigger("PikaLeft");
			anim.ResetTrigger("PikaRight Idle");
			anim.ResetTrigger("PikaRight");
			anim.SetTrigger("PikaBack");
		}
		if (Input.GetKeyUp(KeyCode.W))
		{
			//Move the Rigidbody forwards constantly at speed you define (the blue arrow axis in Scene view)
			//Animator anim = GetComponent<Animator>();
			anim.ResetTrigger("PikaForward");
			anim.SetTrigger("PikaForward Idle");

		}

		if (Input.GetKeyUp(KeyCode.S))
		{
			//Move the Rigidbody backwards constantly at the speed you define (the blue arrow axis in Scene view)
			//Animator anim = GetComponent<Animator>();
			anim.ResetTrigger("PikaBack");
			anim.SetTrigger("PikaBack Idle");
		}
		if (Input.GetKey(KeyCode.RightArrow))
		{
			//Rotate the sprite about the Y axis in the positive direction
		}

		if (Input.GetKey(KeyCode.LeftArrow))
		{
			//Rotate the sprite about the Y axis in the negative direction
		}

		if (joystick.Horizontal < left_x)//Input.GetKeyDown(KeyCode.A) || 
		{
			//Move the Rigidbody forwards constantly at speed you define (the blue arrow axis in Scene view)
			//Animator anim = GetComponent<Animator>();
			anim.ResetTrigger("PikaRight Idle");
			anim.ResetTrigger("PikaRight");
			anim.ResetTrigger("PikaForward Idle");
			anim.ResetTrigger("PikaForward");
			anim.ResetTrigger("PikaBack Idle");
			anim.ResetTrigger("PikaBack");
			anim.SetTrigger("PikaLeft");
		}

		if (joystick.Horizontal > right_x)//Input.GetKeyDown(KeyCode.D) || 
		{
			//Move the Rigidbody backwards constantly at the speed you define (the blue arrow axis in Scene view)
			//Animator anim = GetComponent<Animator>();
			anim.ResetTrigger("PikaLeft Idle");
			anim.ResetTrigger("PikaLeft");
			anim.ResetTrigger("PikaForward Idle");
			anim.ResetTrigger("PikaForward");
			anim.ResetTrigger("PikaBack Idle");
			anim.ResetTrigger("PikaBack");
			anim.SetTrigger("PikaRight");
		}
		if (Input.GetKeyUp(KeyCode.A))
		{
			//Move the Rigidbody forwards constantly at speed you define (the blue arrow axis in Scene view)
			//Animator anim = GetComponent<Animator>();
			anim.ResetTrigger("PikaLeft");
			anim.SetTrigger("PikaLeft Idle");

		}

		if (Input.GetKeyUp(KeyCode.D))
		{
			//Move the Rigidbody backwards constantly at the speed you define (the blue arrow axis in Scene view)
			//Animator anim = GetComponent<Animator>();
			anim.ResetTrigger("PikaRight");
			anim.SetTrigger("PikaRight Idle");
		}*/
	}
}
