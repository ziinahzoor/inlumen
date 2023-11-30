using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthbar : MonoBehaviour
{
	[SerializeField] private Health enemyHealth;
	[SerializeField] private Slider slider;
	[SerializeField] private RectTransform borderRect;

	private bool UpdatedBar = false;

	private void Start()
	{
		slider.maxValue = enemyHealth.startingHealth;
		slider.value = enemyHealth.currentHealth;
	}

	private void Update()
	{
		if (!UpdatedBar && enemyHealth.currentHealth < enemyHealth.startingHealth)
		{
			borderRect.offsetMax = new Vector2(-10, borderRect.offsetMax.y);
			UpdatedBar = true;
		}

		StartCoroutine(UpdateHealth());
	}

	private IEnumerator UpdateHealth()
	{
		float targetValue = enemyHealth.currentHealth;

		float currentValue = slider.value;
		while (currentValue >= targetValue)
		{
			currentValue -= 3 * Time.deltaTime;
			slider.value = currentValue;
			yield return null;
		}

		slider.value = targetValue;
		if (slider.value == 0)
		{
			this.gameObject.SetActive(false);
		}
	}
}