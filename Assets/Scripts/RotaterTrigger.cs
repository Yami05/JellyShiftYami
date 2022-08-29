using UnityEngine;

public class RotaterTrigger : MonoBehaviour, IInRadar
{
	private LevelController levelController;
	[SerializeField] private RotateDecider rotateDecider;

	public RotateDecider RotateDecider { get => rotateDecider; set => rotateDecider = value; }

	private void Start()
	{
		levelController = GetComponentInParent<LevelController>();

	}
  

    public void InRadar(GameObject gameObject, Transform transform)
	{
		this.transform.parent = null;
		levelController.Rotate(this);
	}
}
