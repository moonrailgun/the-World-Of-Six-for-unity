using UnityEngine;
using System.Collections;

public class ButtonControl : MonoBehaviour {
	public GUIStyle attackButton;
	public GUIStyle togglekButton;
	GameObject player;
	GameObject camera;

	float viewWidth,viewHeight;

	void Awake () {
		player = GameObject.Find("Player");
		camera = GameObject.Find("Camera");
		viewWidth = Screen.width;
		viewHeight = Screen.height;
	}

	// Use this for initialization
	void Start () {
		if(ConfigurationSetting.CONTROL_METHOD != PlayerControlMethod.TouchScreen) {this.enabled = false; }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI () {
		if(GlobalObject.player.GetState() == PlayerState.Death) {return; }

		if(GUI.Button(new Rect(viewWidth-200,viewHeight - 200,128,128),"",attackButton))
		{
			//attack
			player.SendMessage("Attack");
		}

		if(GUI.Button(new Rect(viewWidth - 200, 20, 128, 128),"切换",togglekButton)){
			Debug.Log("adadsafa");
			camera.SendMessage("ToggleShowedEnemy");
		}
	}
}
