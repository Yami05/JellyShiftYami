using UnityEngine;

public class PanelManager : MonoBehaviour
{
	[SerializeField] private GameObject LevelFinishedUI;
	private GameEvents gameEvents;
	private void Start()
	{
		gameEvents = GameEvents.instance;

		gameEvents.NextLevelPanel += NextLevelPanel;
	}

	private void NextLevelPanel()
	{
		LevelFinishedUI.SetActive(true);
	}


}
