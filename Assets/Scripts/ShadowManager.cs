using UnityEngine;

public class ShadowManager : MonoBehaviour
{
	[SerializeField] private GameObject Player;

	void FixedUpdate()
	{
		gameObject.transform.localScale = Player.transform.localScale;
	}
}
