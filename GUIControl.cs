using UnityEngine;
using System.Collections;

public class GUIControl : MonoBehaviour {

	public int life;
	public int maxLife;
	public int energy;
	public int maxEnergy;

	private Rect lifeBar = new Rect(50, 10, Screen.width / 2, 20);
	private Rect energyBar = new Rect(50, 40, Screen.width / 2, 20);

	void Awake () {
		life = GlobalObject.player.GetLife();
		maxLife = GlobalObject.player.GetMaxLife();
		energy = GlobalObject.player.GetEnergy();
		maxEnergy = GlobalObject.player.GetMaxEnergy();
	}

	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		UpdateData();

		float lifeRatio = (float)life/maxLife;
		lifeBar.width = Screen.width/2 * lifeRatio;

		float energyRatio = (float)energy/maxEnergy;
		energyBar.width = Screen.width/2 * energyRatio;
	}

	void OnGUI () {
		//GUI.Box(new Rect(0,0,Screen.width,Screen.height),"This is a title");
		GUI.Label(new Rect(10,10,40,20),"生命值");
		GUI.Label(new Rect(10,40,40,20),"能量值");

		GUI.Box(lifeBar,life + " / " + maxLife);
		GUI.Box(energyBar,energy + " / " + maxEnergy);
	}

	void UpdateData()
	{
		life = GlobalObject.player.GetLife();
		energy = GlobalObject.player.GetEnergy();

		if(life <= 0)
		{
			Debug.Log("Death!!!!");
		}
	}
}
