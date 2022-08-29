using UnityEngine;



public interface IInteractable
{
	void Interact(Transform transform);
	void InteractExit();
}
public interface IInRadar
{
	void InRadar(GameObject gameObject, Transform transform);
}


public class InterfaceClasses : MonoBehaviour
{

}
