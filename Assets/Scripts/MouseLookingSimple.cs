using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLookingSimple : MonoBehaviour {
//this script moves the object that it is attached to.
//set some platform depencies to work only in WEBGL - such as joystick (instead of the cursor) controlling the camera
#if UNITY_WEBGL || UNITY_EDITOR
	protected Joybutton joybutton;
#endif

	//Vector2 rotation = new Vector2(0, 0);
		public float speed = 3;
		public bool clampVerticalRotation = true;
		public float sensitivityX = 15F;
		public float sensitivityY = 15F;
		public float minimumX = -90F;
		public float maximumX = 90F;
		public float minimumY = -60F;
		public float maximumY = 60F;
		float rotationY = 0F;
		float rotationX = 0f;

	void Start()
	{
		// start the game with the cursor locked

		#if UNITY_STANDALONE_WIN
				LockCursor(true);
		#endif

		#if UNITY_WEBGL || UNITY_EDITOR
				joybutton = FindObjectOfType<Joybutton>();
		#endif

		// get a reference to the character's transform (which this script should be attached to)
		//character = gameObject.transform;

		// get a reference to the main camera's transform
		//cameraTransform = Camera.main.transform;

		// get the location rotation of the character and the camera
		//m_CharacterTargetRot = character.localRotation;
		//m_CameraTargetRot = cameraTransform.localRotation;
	}
	void Update()
		{
		// if ESCAPE key is pressed, then unlock the cursor
		if (Input.GetButtonDown("Cancel"))
		{
			LockCursor(false);
		}

		//Simple rotation code
		//rotation.y += Input.GetAxis("Mouse X");
		//rotation.x += -Input.GetAxis("Mouse Y");
		//transform.eulerAngles = (Vector2)rotation * speed;

#if UNITY_WEBGL || UNITY_EDITOR
		if (joybutton.Pressed)
		{
#endif
		//complex rotation code
		rotationX += Input.GetAxis("Mouse X") * sensitivityX;
			rotationX = Mathf.Clamp(rotationX, minimumX, maximumX);
			rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
			rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);
			transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
#if UNITY_WEBGL || UNITY_EDITOR
		}
#endif
	}

	private void LockCursor(bool isLocked)
	{
		if (isLocked)
		{
			// make the mouse pointer invisible
			Cursor.visible = false;

			// lock the mouse pointer within the game area
			Cursor.lockState = CursorLockMode.Locked;
		}
		else
		{
			// make the mouse pointer visible
			Cursor.visible = true;

			// unlock the mouse pointer so player can click on other windows
			Cursor.lockState = CursorLockMode.None;
		}
	}
	}

