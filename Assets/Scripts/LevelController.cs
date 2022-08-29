using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using System.Linq;

public class LevelController : MonoBehaviour
{
	[SerializeField] private GameObject shadow;
	private ObstacleManager[] obstacleManager;
	private PlayerController playerController;
	[SerializeField] private GameObject Player;
	public List<ObstacleManager> Obstacles = new List<ObstacleManager>();
	private void Start()
	{
		obstacleManager = GetComponentsInChildren<ObstacleManager>();
		playerController = Player.GetComponent<PlayerController>();
		DistanceCalculator();
	}

	private void DistanceCalculator()
	{

		Obstacles.AddRange(obstacleManager);
		shadow.transform.position = obstacleManager[0].transform.position;
		Obstacles = Obstacles.OrderBy(obstacleManager => Vector3.Distance(obstacleManager.gameObject.transform.position, playerController.gameObject.transform.position)).ToList();
		
	}



	public void Rotate(RotaterTrigger rotater)
	{
		this.gameObject.transform.SetParent(rotater.gameObject.transform);

		if (rotater.RotateDecider == RotateDecider.Right)
		{
			rotater.transform.DORotate(new Vector3(0, 45, 0), 0.1f, RotateMode.Fast).OnComplete(() => this.transform.parent = null);
		}
		else
		{
			rotater.transform.DORotate(new Vector3(0, 135, 0), 0.1f, RotateMode.Fast).OnComplete(() => this.transform.parent = null);
		}

	}

}
