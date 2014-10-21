using UnityEngine;
using System.Collections;

/// <summary>
/// Joystick event
/// </summary>
public class PlayerJoystickEvent : MonoBehaviour {
	GameObject character;

	void Awake () {
		character = GameObject.Find("Player/Character");
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
	void On_JoystickMove( MovingJoystick move){
		if (move.joystickName == "MoveJoystick"){
			float dis = Vector2.Distance(move.joystickAxis,Vector2.zero);
			if(dis <= 0.4) { GlobalObject.player.SetAnimationState(CharacterState.WALK);}
			else { GlobalObject.player.SetAnimationState(CharacterState.RUN);}

			if(move.joystickAxis.y >= 0)
			{
				if(GlobalObject.player.GetAnimationState() == CharacterState.WALK) {
					if(Mathf.Abs(move.joystickAxis.x)< 0.3) {
						character.animation.CrossFade("Walk");
					}
					else if(move.joystickAxis.x>0) {
						character.animation.CrossFade("R_Walk");
					}
					else if (move.joystickAxis.x<0) {
						character.animation.CrossFade("L_Walk");
					}
				}
				else if (GlobalObject.player.GetAnimationState() == CharacterState.RUN) {
					if(Mathf.Abs(move.joystickAxis.x)< 0.3) {
						character.animation.CrossFade("Run00");	
					}
					else if(move.joystickAxis.x>0) {
						character.animation.CrossFade("R_Run00");
					}
					else if (move.joystickAxis.x<0) {
						character.animation.CrossFade("L_Run00");
					}
				}
			}
			else
			{
				if(GlobalObject.player.GetAnimationState() == CharacterState.WALK) {character.animation.CrossFade("B_Walk");}
				else if(GlobalObject.player.GetAnimationState() == CharacterState.RUN) {character.animation.CrossFade("B_Run00");}
			}
		}
	}
}
