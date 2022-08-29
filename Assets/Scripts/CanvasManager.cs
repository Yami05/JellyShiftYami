using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI coinText;
	[SerializeField] private TextMeshProUGUI levelText;
	private GameEvents gameEvents;
	[SerializeField] Button start;
	// Start is called before the first frame update
	void Start()
	{
		LevelCounter();
		gameEvents = GameEvents.instance;
		gameEvents.CoinCounter += CoinCounter;
		gameEvents.NextLevel += LevelCounter;
		gameEvents.Start += DeleteButton;
	}
	private void CoinCounter()
	{
		coinText.text = PlayerPrefs.GetInt(Utilities.CoinCount).ToString();
	}
	private void LevelCounter()
	{
		levelText.text = PlayerPrefs.GetInt(Utilities.LevelIndex).ToString();
	}

	public void NextLevel()
	{
		gameEvents.NextLevelTrigger();
	}

	public void StartTheGame()
	{
		gameEvents.Start?.Invoke();

	}

	private void DeleteButton()
	{
		start.gameObject.SetActive(false);
	}
}
