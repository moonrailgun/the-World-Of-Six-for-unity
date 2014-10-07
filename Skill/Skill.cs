using System.Collections;

public enum SkillType
{
	physics,magic,buff,debuff
}

public abstract class Skill{
	public string skillName;
	public SkillType type;
	public int level;
	public string description;

	public Skill(string skillName, SkillType type)
	{
		this.skillName = skillName;
		this.type = type;
	}
	public void UseSkill() {}
	public void LearnSkill() {}
	public void ForgetSkill() {}
	public void SkillLevelUp() {}
}
