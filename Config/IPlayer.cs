using System.Collections;

public enum Sex
{
	Male,Female
}

public enum Property
{
	Power,Agility,Wit
}

public interface IPlayer{
	void Init();
	void SetName(string name);
	void SetSex(Sex sex);
	void ModifyLife(int changeValue);
	void ModifyEnergy(int changeValue);
	void ModifyValue(Property changeProperty, int modifyValue);
	void UpLevel(int needExp);
	void AddExp(int value);
	void CheckLevel();
}
