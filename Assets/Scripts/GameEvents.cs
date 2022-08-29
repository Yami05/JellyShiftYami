using System;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
	public static GameEvents instance;

	private void Awake()
	{
		instance = this;
	}

	public Action CoinCounter;
	public Action NextLevelPanel;
	public Action NextLevel;
	public Action<float, float> SpeedControl;
	public Action<FeverStatus> OnFeverStatusChanged;
	public Action OnIncrementFever;
	public Action Start;

	public void CoinCounterTriger()
	{
		CoinCounter?.Invoke();
	}

	public void NextLevelPanelTrigger()
	{
		NextLevelPanel?.Invoke();
	}
	public void NextLevelTrigger()
	{
		NextLevel?.Invoke();
	}

}
