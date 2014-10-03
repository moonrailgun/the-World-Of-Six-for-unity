using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	GameObject player;
	float guardDistance;
	float attackDistance;
	float moveSpeed;
	float rotateSpeed;
	float smooth = 5.0f;

	Vector3 startPoint;

	bool attackFlag = false;
	bool distanceFlag = false;

	// Use this for initialization
	void Awake () {
		guardDistance = 20.0f;
		attackDistance = 1.0f;
		moveSpeed = PlayerConfiguration.SPEED;
		rotateSpeed = PlayerConfiguration.ROTATE_SPEED;

		startPoint = transform.position;
	}

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		player = GameObject.FindWithTag("Player");

		float distance = Vector3.Distance(transform.position , player.transform.position);
		if(distance < guardDistance)
		{
			/*
			if(distance > attackDistance && !attackFlag)
			{
				//stage 1:rotate to player
				attackFlag = RotateToPlayerWithFrames(1.0f);
				//walk
				transform.Translate(0, 0, moveSpeed * Time.deltaTime * 0.1f);
			}
			else if(distance > attackDistance && attackFlag)
			{
				//stage 2: run to player
				transform.Translate(0, 0, moveSpeed * Time.deltaTime);
			}
			else
			{
				//stage 3:face to player and attack
				transform.LookAt(player.transform.position);
				distanceFlag = true;
			}

			if(attackFlag)
			{
				if(distanceFlag && Vector3.Distance(transform.position,player.transform.position) > attackDistance)
				{ attackFlag=false;distanceFlag = false;}
			}
			*/
		}
		else
		{
			transform.position = Vector3.Lerp(transform.position, startPoint, Time.deltaTime * smooth);
		}
	}

	private bool RotateToPlayerWithFrames(float a)
	{
		Vector3 targetVector = player.transform.position - transform.position;
		float angle = Vector3.Angle(targetVector, transform.forward);

		if(angle > a)
		{
			if((player.transform.position - transform.position).x > 0){
				transform.Rotate(0,rotateSpeed * Time.deltaTime,0,Space.Self);
			}
			else
			{
				transform.Rotate(0,-rotateSpeed * Time.deltaTime,0,Space.Self);
			}
			return false;
		}
		else
		{
			return true;
		}
	}
}