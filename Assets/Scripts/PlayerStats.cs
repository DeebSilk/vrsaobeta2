using UnityEngine;

public sealed class PlayerStats : MonoBehaviour
{
	[Header("Identity")]
	public string playerName = "Bogdan";
	public int level = 1;

	[Header("Health")]
	public float maxHp = 100f;
	public float hp = 100f;

	void Awake()
	{
		hp = Mathf.Clamp(hp, 0f, maxHp);
	}

	public float Hp01 => maxHp <= 0f ? 0f : hp / maxHp;

	// Тест: урон/хил
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.H))
			TakeDamage(10);
		if (Input.GetKeyDown(KeyCode.J))
			Heal(10);
	}

	public void TakeDamage(float dmg) => hp = Mathf.Clamp(hp - dmg, 0f, maxHp);
	public void Heal(float amount) => hp = Mathf.Clamp(hp + amount, 0f, maxHp);
}
