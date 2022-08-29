using System;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEditorInternal;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

	[SerializeField] private float sensitivity;
	[SerializeField] float MainAcc;
	[SerializeField] float MainSpeed;

	[SerializeField] private GameObject shadow;

	[SerializeField] private Camera cam;

	private Vector3 diffMousePos;
	private Vector3 scale;

	private Vector3 firstMousePos;
	private Vector3 lastMousePos;

	private Rigidbody rb;
	private GameEvents gameEvents;


	void Start()
	{
		this.enabled = false;
		MainAcc = 8;
		MainSpeed = 15;
		rb = GetComponent<Rigidbody>();
		scale = gameObject.transform.localScale;
		gameEvents = GameEvents.instance;
		gameEvents.NextLevelPanel += EndLevel;
		gameEvents.SpeedControl += SetSpeed;
		gameEvents.OnFeverStatusChanged += OnFeverStatusChanged;
		gameEvents.Start += StartTheMovement;
	}
	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			OnMouseButtonDown();
		}
		if (Input.GetMouseButton(0))
		{
			OnMouseButton();
		}
		if (Input.GetMouseButtonUp(0))
		{
			OnMouseButtonUp();
		}
	}
	private void FixedUpdate()
	{
		MoveCharacter();
	}

	private void OnMouseButtonDown()
	{
		firstMousePos = cam.ScreenToWorldPoint(Input.mousePosition);

		lastMousePos = firstMousePos;
	}
	private void OnMouseButton()
	{
		lastMousePos = cam.ScreenToWorldPoint(Input.mousePosition);

		diffMousePos = lastMousePos - firstMousePos;

		diffMousePos *= sensitivity;

		diffMousePos = diffMousePos.normalized;

		Mssd();
	}
	private void OnMouseButtonUp()
	{
		diffMousePos = Vector3.zero;
	}
	private void Mssd()
	{

		transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(transform.localScale.x - diffMousePos.y*0.6f, transform.localScale.y + diffMousePos.y*0.6f, transform.localScale.z), 0.2f);     // TODO : lerp
		transform.localScale = new Vector3(
			Mathf.Clamp(transform.localScale.x, 0.8f, 5f),
			Mathf.Clamp(transform.localScale.y, 1f, 3.9f),
			transform.localScale.z
			);
	}

	private void MoveCharacter()
	{
		rb.AddForce(Vector3.forward * MainSpeed);
		rb.velocity = Vector3.ClampMagnitude(rb.velocity, MainAcc);

	}
	private void OnCollisionEnter(Collision collision)
	{
		collision.transform.GetComponentInParent<IInteractable>()?.Interact(collision.transform);
	}

	private void OnTriggerEnter(Collider other)
	{
		other.GetComponent<IInRadar>()?.InRadar(shadow, this.transform);

	}

	private void OnTriggerExit(Collider other)
	{
		other.GetComponent<IInteractable>()?.InteractExit();
	}
	private void EndLevel()
	{
		rb.isKinematic = true;
		this.enabled = false;

	}
	private void SetSpeed(float speedMult, float accMult)
	{

		MainAcc = Mathf.Clamp(MainAcc, 0, 20);
		MainSpeed = Mathf.Clamp(MainSpeed, 0, 25);
		MainAcc = accMult * MainAcc;
		MainSpeed = speedMult * MainSpeed;
	}

	public void OnFeverStatusChanged(FeverStatus feverStatus)
	{
		switch (feverStatus)
		{
			case FeverStatus.Activated:
				MainAcc = 28;
				MainSpeed = 35;
				break;
			case FeverStatus.Finished:
				MainAcc = 15;
				MainSpeed = 20;
				break;
			default:
				break;
		}

	}
	private void StartTheMovement()
	{
		this.enabled = true;
	}
}
