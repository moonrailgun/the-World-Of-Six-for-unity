using UnityEngine;
using System.Collections;

public class CameraTrendsTrack : MonoBehaviour {
	public float smooth = 10.0f;

	GameObject mainCamera;
	GameObject playerCamera;
	void Awake () {
		mainCamera = GameObject.FindWithTag("MainCamera");
		playerCamera = GameObject.Find("Player/PlayerCamera");
	}
	
	// Update is called once per frame
	void Update () {
		mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, playerCamera.transform.position, Time.deltaTime * smooth);
		mainCamera.transform.forward = Vector3.Lerp(mainCamera.transform.forward, playerCamera.transform.forward, Time.deltaTime * smooth);
	}
}
