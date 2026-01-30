using UnityEngine;
using UnityEngine.UI;

public sealed class SaoHpBar : MonoBehaviour
{
	[SerializeField] PlayerStats target;

	[Header("UI")]
	[SerializeField] Image hpFill;
	[SerializeField] Image damageFill;

	[Header("Settings")]
	[SerializeField] float damageDelay = 0.4f;
	[SerializeField] float damageLerpSpeed = 3f;

	float damageTimer;

	void Start()
	{
		if (!target)
		{
			var p = GameObject.FindGameObjectWithTag("Player");
			if (p)
				target = p.GetComponent<PlayerStats>();
		}
	}

	void Update()
	{
		if (!target)
			return;

		float hp01 = target.Hp01;

		// основная полоса — сразу
		hpFill.fillAmount = hp01;

		// задержанная красная
		if (damageFill.fillAmount > hp01)
		{
			damageTimer += Time.deltaTime;
			if (damageTimer >= damageDelay)
			{
				damageFill.fillAmount = Mathf.Lerp(
					damageFill.fillAmount,
					hp01,
					Time.deltaTime * damageLerpSpeed
				);
			}
		}
		else
		{
			damageFill.fillAmount = hp01;
			damageTimer = 0f;
		}
	}
}
