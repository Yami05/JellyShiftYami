using DG.Tweening;
using UnityEngine;
public class CoinManager : MonoBehaviour, IInRadar
{
	private GameEvents gameEvents;
	private void Start()
	{
		gameEvents = GameEvents.instance;
		gameEvents.CoinCounterTriger();

		transform.DORotate(new Vector3(0, 360, 0), 2f, RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear).SetLoops(-1);
		
	}


	public void InRadar(GameObject gameObject, Transform transform)
	{
		Utilities.IncrementCoin();
		DOTween.Kill(this.transform);
		Destroy(this.gameObject);
		gameEvents.CoinCounterTriger();
	}



}

