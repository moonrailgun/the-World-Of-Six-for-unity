using UnityEngine;
using System.Collections;

public class AttackInputHandler : MonoBehaviour {
	GameObject character;
	float animationTime;
	public void Awake () {
		character = GameObject.Find("Player/Character");
	}
	public void Update () {
		animationTime -= Time.deltaTime;
	}

	public void CommonAttack () {
		float rand = Random.value;
		if(rand < 0.5) {
			PlayCompleteAnimation("Attack00",0);
		}
		else {
			PlayCompleteAnimation("Attack",0);
		}
	}
	public void Skill1 () {
		PlayCompleteAnimation("ComboAttack",0);
	}
	public void Skill2 () {
		PlayCompleteAnimation("Skill",0);
	}
	public void Skill3 () {
		PlayCompleteAnimation("Block",0);
	}
	private void PlayCompleteAnimation(string name,int mode = 1)
	{
		if(animationTime > 0) {return;}
		character.animation.CrossFade(name);
		switch(mode)
		{
		case 1 : animationTime = character.animation.GetClip(name).length - PlayerConfiguration.COMPLETE_ANIMATION_SURPLUS;break;
		default : animationTime = character.animation.GetClip(name).length;break;
		}
	}
}
