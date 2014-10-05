using UnityEngine;
using System.Collections;

public class Player : IPlayer {
	string PlayerName;
	Sex sex;
	int level;
	int exp;
	int life;
	int energy;
	int power;
	int agility;
	int wit;
	int propertyPoint;
	int skillPoint;

	int maxLife;
	int maxEnergy;

	public Player()
	{
		Init();
	}

	public void Init(){
		PlayerName = "";
		sex = Sex.Female;
		level = 1;
		exp = 0;
		life = 100;
		maxLife = 100;
		energy = 100;
		maxEnergy = 100;
		power = 10;
		agility = 10;
		wit = 10;
		propertyPoint = 0;
		skillPoint = 0;
	}
	public int GetLife(){
		return life;
	}
	public int GetMaxLife(){
		return maxLife;
	}
	public int GetEnergy(){
		return energy;
	}
	public int GetMaxEnergy(){
		return maxEnergy;
	}
	public void SetName(string name){
		this.PlayerName = name;
	}
	public void SetSex(Sex sex){
		this.sex = sex;
	}
	public void ModifyLife(int changeValue){
		this.life += changeValue;
		if(this.life < 0) {this.life = 0;}
		else if(this.life > this.maxLife) {this.life = this.maxLife;}
	}
	public void ModifyEnergy(int changeValue){
		this.energy += changeValue;
		if(this.energy < 0) {this.energy = 0;}
		else if(this.energy > this.maxEnergy) {this.energy = this.maxEnergy;}
	}
	public void ModifyValue(Property changeProperty, int modifyValue){
		switch(changeProperty)
		{
		case Property.Agility:this.agility = modifyValue;break;
		case Property.Power:this.power = modifyValue;break;
		case Property.Wit:this.wit = modifyValue;break;
		}
	}
	public void UpLevel(int needExp){
		if(this.exp < needExp){return ; }
		this.level++;
		this.exp -= needExp;
		this.maxLife += PlayerConfiguration.LEVEL_UP_MAX_LEFT_ADD;
		this.maxEnergy += PlayerConfiguration.LEVEL_UP_MAX_ENERTY_ADD;
		this.life = this.maxLife;
		this.energy = this.maxEnergy;
		this.propertyPoint += PlayerConfiguration.LEVEL_UP_PROPERTY_POINT;
		this.skillPoint += PlayerConfiguration.LEVEL_UP_SKILL_POINT;
	}
	public void AddExp(int value) {
		this.exp += value;
	}
	public void CheckLevel() {
		//here write the rule of level
	}
	public void Damage(int damageValue) {
		ModifyLife(-damageValue);
	}
}
