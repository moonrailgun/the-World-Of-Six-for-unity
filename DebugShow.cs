using UnityEngine;
using System.Collections;

public class DebugShow : MonoBehaviour {

	int smooth = 12;
	Vector3 playerPoint;
	Vector3 leftEndPoint;
	Vector3 rightEndPoint;
	Vector3[] slerpPoint;
	float length = 20f;

	Vector3 leftEnd = new Vector3(-1.73f, 0, 2f);
	Vector3 rightEnd = new Vector3(1.73f, 0, 2f);

	// Use this for initialization
	void Start () {
		slerpPoint = new Vector3[smooth+1];

		leftEnd = leftEnd.normalized;
		rightEnd = rightEnd.normalized;
		slerpPoint[smooth] = rightEnd;
	}
	
	// Update is called once per frame
	void Update () {
		playerPoint = transform.position;
		//draw forward
		Debug.DrawRay(playerPoint, transform.TransformDirection(Vector3.forward * length), Color.green);

		leftEndPoint = transform.TransformDirection(leftEnd) * length;
		rightEndPoint = transform.TransformDirection(rightEnd) * length;

		Debug.DrawRay(playerPoint,leftEndPoint,Color.grey);
		Debug.DrawRay(playerPoint,rightEndPoint,Color.grey);

		for(int i = 0;i < smooth;i++)
		{
			float k = (float)i/smooth;
			slerpPoint[i] = Vector3.Slerp(leftEnd, rightEnd,k).normalized;
			Debug.Log(slerpPoint[i].x+","+slerpPoint[i].y+","+slerpPoint[i].z);
		}

		for(int i=0;i<smooth;i++)
		{
			Debug.DrawLine(transform.TransformDirection(slerpPoint[i] * length) + playerPoint,transform.TransformDirection(slerpPoint[i+1] * length) + playerPoint, Color.grey);
			if(i == smooth- 1)
			{
				Debug.Log("create once over!");
			}
		}
	}
}