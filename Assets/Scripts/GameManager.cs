using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
	private GameEvents gameEvents;
	public List<GameObject> Levels = new List<GameObject>();
	public static GameManager instance;
	private void Awake()
	{
		instance = this;
	}
	private void Start()
	{
		Instantiate(Levels[PlayerPrefs.GetInt(Utilities.LevelIndex) % Levels.Count], transform.position, Quaternion.identity);
		gameEvents = GameEvents.instance;
		gameEvents.NextLevel += SetLevel;


	}
	private void Update()
	{
		if (Input.GetKey("n"))
		{
			SetLevel();
		}
		if (Input.GetKey("r"))
		{
			SceneManager.LoadScene("SampleScene");
		}
		if (Input.GetKey("space"))
		{
			gameEvents.Start?.Invoke();
		}
	}
	private void SetLevel()
	{
		Utilities.SetLevelPref();
		SceneManager.LoadScene("SampleScene");
	}

}
