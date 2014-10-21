using UnityEngine;
using System.Collections;

public class GUIControl : MonoBehaviour {

	public int life;
	public int maxLife;
	public int energy;
	public int maxEnergy;

	private Rect lifeBar = new Rect(50, 10, Screen.width / 2, 20);
	private Rect energyBar = new Rect(50, 40, Screen.width / 2, 20);
	private Rect deathWindowRect;

	private PlayerControl playerControl;

	void Awake () {
		life = GlobalObject.player.GetLife();
		maxLife = GlobalObject.player.GetMaxLife();
		energy = GlobalObject.player.GetEnergy();
		maxEnergy = GlobalObject.player.GetMaxEnergy();

		deathWindowRect = new Rect(Screen.width/2 - 300, Screen.height/2 - 200, 600, 400);

		playerControl = GameObject.Find("Player").GetComponent<PlayerControl>();
	}

	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		float lifeRatio = (float)life/maxLife;
		lifeBar.width = Screen.width/2 * lifeRatio;

		float energyRatio = (float)energy/maxEnergy;
		energyBar.width = Screen.width/2 * energyRatio;

		if(Input.GetKeyDown(KeyCode.Tab))
		{
			ToggleShowedEnemy();
		}
	}

	private int i = -1;

	void OnGUI () {
		UpdateData();

		GUI.color = Color.white;
		//GUI.Box(new Rect(0,0,Screen.width,Screen.height),"This is a title");
		GUI.Label(new Rect(10,10,40,20),"生命值");
		GUI.Label(new Rect(10,40,40,20),"能量值");

		GUI.Box(lifeBar,"");
		GUI.Box(energyBar,"");
		GUI.Box(new Rect(50, 10, Screen.width / 2, 20),life + " / " + maxLife);
		GUI.Box(new Rect(50, 40, Screen.width / 2, 20),energy + " / " + maxEnergy);

		if(i >= 0 )
		{
			if(i >= playerControl.enemyList.Length) {i = 0;}
			GameObject enemy = playerControl.enemyList[i];
			string enemyName = enemy.GetComponent<Enemy>().GetEnemyName();

			GUI.Box(new Rect(Screen.width/2 - 50, 10, 100, 20),enemyName);
		}
	}

	void UpdateData()
	{
		life = GlobalObject.player.GetLife();
		energy = GlobalObject.player.GetEnergy();

		if(life <= 0)
		{
			if(GlobalObject.player.GetState() == PlayerState.Alive)
			{
				GlobalObject.player.Death();
			}

			GUI.color = Color.red;
			GUI.Window(5,deathWindowRect,DeathWindow,"你已经死亡");
		}
	}

	void DeathWindow(int windowID)
	{
		GUI.color = Color.red;
		if(GUI.Button(new Rect(deathWindowRect.width/2 - 100,deathWindowRect.height/2 - 50, 200, 100),"Restart"))
		{
			GlobalObject.player.Init();//data restart
			Application.LoadLevel(Application.loadedLevelName);
		}
	}

	void ToggleShowedEnemy () {
		i++;
	}
}
