using UnityEngine;
using System.Collections;

public enum CharacterState
{
	IDLE,
	WALK,
	RUN,
	FIGHT
}

public enum CharacterMove
{
	TRUE,FALSE
}

public class PlayerControl : MonoBehaviour {
	private float speed;
	private float walk_speed;
	private float back_speed;
	private float rotate_speed;
	private float jump_height;
	private float gravity;
	private float smooth = 5.0f;
	private GameObject character;
	private GameObject mainCamera;
	private GameObject playerCamera;
	private CharacterState characterState;
	private CharacterMove characterMove;
	private CharacterController controller;

	void Awake (){
		speed = PlayerConfiguration.SPEED;
		walk_speed = PlayerConfiguration.WALK_SPEED;
		back_speed = PlayerConfiguration.BACK_SPEED;
		rotate_speed = PlayerConfiguration.ROTATE_SPEED;
		jump_height = PlayerConfiguration.JUMP_HEIGHT;
		gravity = PlayerConfiguration.GRAVITY;
		character = GameObject.Find("Player/Character");
		playerCamera = GameObject.Find("Player/PlayerCamera");
		mainCamera = GameObject.Find("Camera");
		characterState = CharacterState.RUN;
		controller = GetComponent<CharacterController>();
	}

	private Vector3 moveDirection = Vector3.zero;

	// Update is called once per frame
	void Update () {
		float v = Input.GetAxis("Vertical");
		float h = Input.GetAxis("Horizontal");
		bool fire1 = (Input.GetAxis("Fire1") == 1.0f);
		bool fire2 = (Input.GetAxis("Fire2") == 1.0f);
		bool jump = (Input.GetAxis("Jump") == 1.0f);
		bool isRotate = false;

		//camera trends track
		mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, playerCamera.transform.position, Time.deltaTime * smooth);
		mainCamera.transform.forward = Vector3.Lerp(mainCamera.transform.forward, playerCamera.transform.forward, Time.deltaTime * smooth);

		//character move
		if (controller.isGrounded) {
			// We are grounded, so recalculate
			// move direction directly from axes
			moveDirection = new Vector3(0, 0, v);
			moveDirection = transform.TransformDirection(moveDirection);
			if(characterState == CharacterState.RUN)
			{
				moveDirection *= speed;
			}
			else if(characterState == CharacterState.WALK)
			{
				moveDirection *= walk_speed;
			}

			if (Input.GetButton ("Jump")) {
				moveDirection.y = jump_height;
			}
		}
		moveDirection.y -= gravity * Time.deltaTime; // Apply gravity
		controller.Move(moveDirection * Time.deltaTime);// Move the controller

		//animation
		if(fire1 || fire2)
		{
			//attack

		}
		else if(jump)
		{
			//character.animation.CrossFade("Jump_NoBlade");
		}
		else if(v > 0.1 || v < -0.1 || h > 0.1 || h < -0.1)
		{
			//move
			ChangeStateTo(CharacterState.RUN);

			if(Input.GetKeyDown(KeyCode.LeftShift)||Input.GetKey(KeyCode.LeftShift))
			{
				ChangeStateTo(CharacterState.WALK);
			}
			if(Input.GetKeyUp(KeyCode.LeftShift))
			{
				ChangeStateTo(CharacterState.RUN);
			}

			if(h > 0.1 || h < -0.1)
			{
				//is rotate
				isRotate = true;
			}

			if(v >= 0)
			{
				if(!isRotate)
				{
					if(characterState == CharacterState.RUN)
						character.animation.CrossFade("Run00");
					else if(characterState == CharacterState.WALK)
						character.animation.CrossFade("Walk");
					else if(characterState == CharacterState.FIGHT)
						character.animation.CrossFade("Run");
				}
				else
				{
					if((characterState == CharacterState.RUN))
					{
						if(h > 0) character.animation.CrossFade("R_Run00");
						if(h < 0) character.animation.CrossFade("L_Run00");
					}
					else if((characterState == CharacterState.WALK))
					{
						if(h > 0) character.animation.CrossFade("R_Walk");
						if(h < 0) character.animation.CrossFade("L_Walk");
					}
					else if((characterState == CharacterState.FIGHT))
					{
						if(h > 0) character.animation.CrossFade("R_Run");
						if(h < 0) character.animation.CrossFade("L_Run");
					}
				}
			}
			else
			{
				if(characterState == CharacterState.RUN)
					character.animation.CrossFade("B_Run00");
				else if(characterState == CharacterState.WALK)
					character.animation.CrossFade("B_Walk");
				else if(characterState == CharacterState.FIGHT)
					character.animation.CrossFade("B_Run");
			}

			if(isRotate)
			{
				//character rotate
				transform.Rotate(0, h * rotate_speed * Time.deltaTime, 0, Space.World);
			}
		}
		else
		{
			//idle
			ChangeStateTo(CharacterState.IDLE);
			character.animation.CrossFade("Idle");
		}
	}

	public void ChangeStateTo(CharacterState state)
	{
		characterState = state;
	}
}
