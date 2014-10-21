using System.Collections;

public enum PlayerControlMethod {
	MouseControl,TouchScreen
}

public static class ConfigurationSetting {
	public static PlayerControlMethod CONTROL_METHOD = PlayerControlMethod.TouchScreen;
}
