using UnityEngine;
using System.Collections;
public enum CharacterMove
{
	TRUE,FALSE
}
//*********************************
//该脚本已经弃用
//*********************************

public class PlayerControl : MonoBehaviour {
	private float speed;
	private float fight_speed;
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
	private bool AnimationFlag = false;
	private float fightModeCD = PlayerConfiguration.FIGHT_MODE_CD;
	private float animationTime;
	public GameObject[] enemyList;

	private PlayerState playerState;

	//debug use
	void OnGUI () {

	}


	void Awake (){
		speed = PlayerConfiguration.SPEED;
		fight_speed = PlayerConfiguration.FIGHT_SPEED;
		walk_speed = PlayerConfiguration.WALK_SPEED;
		back_speed = PlayerConfiguration.BACK_SPEED;
		rotate_speed = PlayerConfiguration.ROTATE_SPEED;
		jump_height = PlayerConfiguration.JUMP_HEIGHT;
		gravity = PlayerConfiguration.GRAVITY;
		character = GameObject.Find("Player/Character");
		playerCamera = GameObject.Find("Player/PlayerCamera");
		mainCamera = GameObject.Find("Main Camera");
		characterState = CharacterState.RUN;
		controller = GetComponent<CharacterController>();

		playerState = PlayerState.Alive;
	}

	private Vector3 moveDirection = Vector3.zero;

	// Update is called once per frame
	void Update () {
		if(GlobalObject.player.GetState() == PlayerState.Death) {
			if(this.playerState == PlayerState.Alive)
			{
				this.playerState = PlayerState.Death;
				PlayDeathAnimation();
			}
			return;
		}

		GetAllEnemy();

		float v = Input.GetAxis("Vertical");
		float h = Input.GetAxis("Horizontal");
		bool fire1 = (Input.GetAxis("Fire1") == 1.0f);
		bool fire2 = (Input.GetAxis("Fire2") == 1.0f);
		bool jump = (Input.GetAxis("Jump") == 1.0f);
		bool isRotate = false;

		CameraTrendsTrack();
		FightModeCD();
		AnimationFlagHandle();

		MoveControl(v);

		#region animations and rotates
		if(AnimationFlag) {return;}//don't receive any handle if a complete need to play

		if(fire1 || fire2)
		{
			/*
			//attack
			if(fire1)
				Attack();
			else if(fire2)
				WeightAttack();
			*/
		}
		else if(jump)
		{
			//character.animation.CrossFade("Jump_NoBlade");
		}
		else if(v > 0.1 || v < -0.1 || h > 0.1 || h < -0.1)
		{
			//move
			if(characterState != CharacterState.FIGHT)
			{
				if(Input.GetKeyDown(KeyCode.LeftShift)||Input.GetKey(KeyCode.LeftShift))
				{
					ChangeStateTo(CharacterState.WALK);
				}
				else
				{
					ChangeStateTo(CharacterState.RUN);
				}
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
			if(characterState == CharacterState.FIGHT)
			{
				character.animation.CrossFade("AttackStandy");
			}
			else
			{
				ChangeStateTo(CharacterState.IDLE);
				character.animation.CrossFade("Idle");
			}
		}
		#endregion
	}

	private void ChangeStateTo(CharacterState state)
	{
		characterState = state;
	}

	private void CameraTrendsTrack()
	{
		mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, playerCamera.transform.position, Time.deltaTime * smooth);
		mainCamera.transform.forward = Vector3.Lerp(mainCamera.transform.forward, playerCamera.transform.forward, Time.deltaTime * smooth);
	}

	private void Attack()
	{
		ChangeStateTo(CharacterState.FIGHT);
		if(!character.animation.IsPlaying("Attack"))
		{
			PlayCompleteAnimation("Attack");
		}
	}

	private void WeightAttack()
	{
		ChangeStateTo(CharacterState.FIGHT);
		if(!character.animation.IsPlaying("Attack00"))
		{
			PlayCompleteAnimation("Attack00");
		}
	}

	private void FightModeCD()
	{
		if(characterState != CharacterState.FIGHT) {return;}

		fightModeCD -= Time.deltaTime;
		if(fightModeCD <= 0)
		{
			ChangeStateTo(CharacterState.RUN);
			fightModeCD = PlayerConfiguration.FIGHT_MODE_CD;
		}
	}

	private void MoveControl(float v)
	{
		//can't move character when attack
		if(AnimationFlag) 
		{return;}

		//character move
		if (controller.isGrounded) {
			// We are grounded, so recalculate
			// move direction directly from axes
			moveDirection = new Vector3(0, 0, v);
			moveDirection = transform.TransformDirection(moveDirection);
			if(v >= 0)
			{
				switch(characterState)
				{
				case CharacterState.RUN : moveDirection *= speed;break;
				case CharacterState.FIGHT : moveDirection *= fight_speed;break;
				case CharacterState.WALK : moveDirection *= walk_speed;break;
				default : moveDirection *= speed;break;
				}
			}
			else
			{
				moveDirection *= back_speed;
			}

			if (Input.GetButton ("Jump")) {
				moveDirection.y = jump_height;

				if(characterState == CharacterState.IDLE)
				{
					PlayCompleteAnimation("Jump_NoBlade");
					return;
				}
				else if(characterState == CharacterState.FIGHT)
				{
					PlayCompleteAnimation("jump");
					return;
				}
			}
		}

		moveDirection.y -= gravity * Time.deltaTime; // Apply gravity
		//Debug.Log(moveDirection.z);
		controller.Move(moveDirection * Time.deltaTime);// Move the controller
	}

	private void AnimationFlagHandle()
	{
		if(AnimationFlag)
		{
			animationTime -= Time.deltaTime;
			if(animationTime <= 0){AnimationFlag = false;}
		}
	}

	private void PlayCompleteAnimation(string name,int mode = 1)
	{
		AnimationFlag = true;
		character.animation.CrossFade(name);
		switch(mode)
		{
		case 1 : animationTime = character.animation.GetClip(name).length - PlayerConfiguration.COMPLETE_ANIMATION_SURPLUS;break;
		default : animationTime = character.animation.GetClip(name).length;break;
		}
	}

	private void PlayDeathAnimation()
	{
		character.animation.CrossFade("Death");
	}

	private void GetAllEnemy()
	{
		enemyList = GameObject.FindGameObjectsWithTag("Enemy");
	}
}
