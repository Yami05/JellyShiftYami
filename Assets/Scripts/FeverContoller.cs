using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class FeverContoller : MonoBehaviour
{
	static public FeverContoller instance;

	[SerializeField] private Slider feverSlider;

	[SerializeField] private float feverTime;

	[SerializeField] private float feverActivateCount;

	public float feverCount;

	private bool isFeverActivated;

	private GameEvents gameEvents;

	private WaitForSeconds wait;

	private float sliderCountDown;

	[SerializeField] private Image barImage;


	void Start()
	{

		wait = new WaitForSeconds(feverTime);
		gameEvents = GameEvents.instance;
		gameEvents.OnFeverStatusChanged += OnFeverStatusChanged;
		gameEvents.OnIncrementFever += OnIncrementFever;
	}

	private void OnIncrementFever()

	{   //early Return
		if (isFeverActivated)
			return;
		feverCount++;
		feverSlider.maxValue = feverActivateCount;
		feverSlider.value = feverCount;
		barImage.fillAmount = feverCount / feverActivateCount;
		if (feverCount >= feverActivateCount)
		{
			// Activate fever
			gameEvents.OnFeverStatusChanged?.Invoke(FeverStatus.Activated);
		}
	}


	IEnumerator CountDown()
	{
		while (sliderCountDown >= 0)
		{
			ForCountDown();
			feverSlider.value = sliderCountDown;

			yield return new WaitForSeconds(1);

			sliderCountDown--;
		}
	}
	IEnumerator FeverTime()
	{

		yield return wait;
		gameEvents.OnFeverStatusChanged?.Invoke(FeverStatus.Finished);
	}

	private void OnFeverStatusChanged(FeverStatus feverStatus)
	{
		if (feverStatus == FeverStatus.Activated)
		{
			feverSlider.maxValue = feverTime;
			sliderCountDown = feverTime;
			isFeverActivated = true;
			StartCoroutine(FeverTime());
			gameEvents.OnIncrementFever -= FeverSlider;
			StartCoroutine(CountDown());

		}
		else
		{

			feverCount = 0;
			isFeverActivated = false;

		}
	}

	private void FeverSlider()
	{
		if (isFeverActivated == false)
		{
			feverSlider.value = feverCount;

		}



	}
	private void ForCountDown()
	{
		barImage.fillAmount = sliderCountDown / feverTime;

	}

}

