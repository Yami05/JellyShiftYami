using UnityEngine;

public enum FeverStatus
{
	Activated,
	Finished,
}

public enum RotateDecider
{
	Right,
	Left,
}

public static class Utilities
{
	public const string LevelIndex = "LevelIndex";
	public const string CoinCount = "CoinCount";
	public static void IncrementCoin(int count = 1)
	{
		PlayerPrefs.SetInt(CoinCount, PlayerPrefs.GetInt(CoinCount, 0) + count);

	}

	public static void SetLevelPref(int levelCount = 1)
	{
		PlayerPrefs.SetInt(LevelIndex, PlayerPrefs.GetInt(LevelIndex, 1) + levelCount);

	}

}