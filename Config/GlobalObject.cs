using System.Collections;

public struct Account{
	public string Username;
	public string Password;
}
public static class GlobalObject {
	public static Account account = new Account();
	public static Player player = new Player();
}
