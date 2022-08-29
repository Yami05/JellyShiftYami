using UnityEngine;
public class CameraFollow : MonoBehaviour
{

	[SerializeField] private Transform target;
	[SerializeField] private Vector3 offset;


	
	
	void FixedUpdate()
	{

		Vector3 desiredPosition = target.position + offset;
		transform.position = Vector3.Lerp(transform.position, desiredPosition, 0.2f);

	}
}
