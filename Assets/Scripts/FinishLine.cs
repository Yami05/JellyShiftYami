using UnityEngine;
using DG.Tweening;

public class FinishLine : MonoBehaviour, IInRadar
{
	private float jumpDur;
	private GameEvents gameEvents;
	private Vector3 jumpPos;
	private bool jumpOneTime = true;
	private void Start()
	{

		jumpDur = 1;

		gameEvents = GameEvents.instance;

	}

	public void InRadar(GameObject gameObject, Transform tr)
	{
		jumpPos = tr.position;
		jumpPos.z = tr.transform.position.z + 2;
		gameEvents.NextLevelPanelTrigger();
		if (jumpOneTime == true)
		{
			tr.DOJump(jumpPos, 3, 1, jumpDur, jumpOneTime = false);
			tr.DORotate(new Vector3(359, 180, 0), 3, RotateMode.Fast);

		}

	}



}
