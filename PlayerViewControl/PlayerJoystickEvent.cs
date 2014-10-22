using UnityEngine;
using System.Collections;

/// <summary>
/// Joystick event
/// </summary>
public class PlayerJoystickEvent : MonoBehaviour {
	GameObject character;
	GameObject player;

	void Awake () {
		character = GameObject.Find("Player/Character");
		player = GameObject.Find("Player");
	}

	void OnEnable(){
		EasyJoystick.On_JoystickMove += On_JoystickMove;
		EasyJoystick.On_JoystickMoveEnd += On_JoystickMoveEnd;
	}
	
	void OnDisable(){
		EasyJoystick.On_JoystickMove -= On_JoystickMove	;
		EasyJoystick.On_JoystickMoveEnd -= On_JoystickMoveEnd;
	}
		
	void OnDestroy(){
		EasyJoystick.On_JoystickMove -= On_JoystickMove;	
		EasyJoystick.On_JoystickMoveEnd -= On_JoystickMoveEnd;
	}
	
	
	void On_JoystickMoveEnd(MovingJoystick move){
		if (move.joystickName == "MoveJoystick"){
			character.animation.CrossFade("Idle");
			GlobalObject.player.SetAnimationState(CharacterState.IDLE);
		}
	}
	void On_JoystickMove(MovingJoystick move){
		if (move.joystickName == "MoveJoystick"){
			float dis = Vector2.Distance(move.joystickAxis,Vector2.zero);
			if(dis <= 0.4) { GlobalObject.player.SetAnimationState(CharacterState.WALK);}
			else { GlobalObject.player.SetAnimationState(CharacterState.RUN);}

			Vector3 target = new Vector3(character.transform.position.x + move.joystickAxis.x, character.transform.position.y, character.transform.position.z + move.joystickAxis.y);
			character.transform.LookAt(target);

			if(GlobalObject.player.GetAnimationState() == CharacterState.WALK) {
				character.animation.CrossFade("Walk");
			}
			else if(GlobalObject.player.GetAnimationState() == CharacterState.RUN) {
				character.animation.CrossFade("Run00");
			}
		}
	}
}
