using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour, IInteractable, IInRadar
{

	[SerializeField] private List<Rigidbody> rbs = new List<Rigidbody>();
	private GameEvents gameEvents;

	private LevelController levelController;

	private bool isObstacleHit = false;
	private bool isFeverModeActive = false;

	private void Start()
	{
		levelController = GetComponentInParent<LevelController>();
		gameEvents = GameEvents.instance;
		gameEvents.OnFeverStatusChanged += OnFeverStatusChanged;
	}
	public void InRadar(GameObject gameObject, Transform trOfGhost)
	{
		levelController.Obstacles.Remove(this);
		gameObject.transform.position = new Vector3(levelController.Obstacles[0].gameObject.transform.position.x,
			levelController.Obstacles[0].gameObject.transform.position.y - 2.6f,
			levelController.Obstacles[0].gameObject.transform.position.z);
		Destroy(levelController.Obstacles[0].gameObject,3f);
		if (levelController.Obstacles.Capacity <= 0)
		{
			Destroy(gameObject);
		}
		gameObject.SetActive(true);
		//
		if (isFeverModeActive)
			ExplodeYourself();
	}

	public void Interact(Transform tr)
	{
		Rigidbody rb = tr.gameObject.AddComponent<Rigidbody>();

		rbs.Add(rb);

		isObstacleHit = true;

		for (int i = 0; i <= rbs.Count; i++)
		{

			transform.GetChild(i);
			rb.AddForce(Vector3.forward * 400);
			rb.gameObject.transform.SetParent(null);
			rb.gameObject.layer = LayerMask.NameToLayer("IgnoreIt");
			Destroy(rb.gameObject, 2);
			rbs.Remove(rb);

		}
	}

	public void InteractExit()
	{
		if (!isFeverModeActive)
			gameEvents.SpeedControl?.Invoke(1.1f, 1.2f);

		// exit 
		if (!isObstacleHit && !isFeverModeActive)
			gameEvents.OnIncrementFever?.Invoke();
	}

	private void ExplodeYourself()
	{
		int childCount = transform.childCount;

		for (int i = 0; i < childCount; i++)
		{
			int explosionDir = Random.Range(350, 600);
			Transform child = transform.GetChild(0);
			Rigidbody rb = child.gameObject.AddComponent<Rigidbody>();
			rbs.Add(rb);
			rb.gameObject.layer = LayerMask.NameToLayer("IgnoreIt");
			rb.AddRelativeForce(-300, explosionDir, explosionDir);
			child.SetParent(null);
			Destroy(rb.gameObject, 2);

		}

		gameEvents.OnFeverStatusChanged -= OnFeverStatusChanged;


	}

	private void OnFeverStatusChanged(FeverStatus feverStatus)
	{
		isFeverModeActive = (feverStatus == FeverStatus.Activated);
	}
}
