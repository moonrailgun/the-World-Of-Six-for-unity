using UnityEngine;
using System.Collections;

public class Login : MonoBehaviour {
	public void Game_Login () {
		string Username = GameObject.Find("Anchor Center/LoginWindow/Background/Input Field (Username)/Input Field").GetComponent<UIInput>().value;
		string Password = GameObject.Find("Anchor Center/LoginWindow/Background/Input Field (Password)/Input Field").GetComponent<UIInput>().value;

		if(Username != "" && Password != "")
		{
			//here add net communication

			GlobalObject.account.Username = Username;
			GlobalObject.account.Password = Password;

			Debug.Log("登陆成功\n账户:"+GlobalObject.account.Username +"密码:"+GlobalObject.account.Password);
		}
	}
}
