using UnityEngine;

public sealed class TeleportTrigger : MonoBehaviour
{
	[SerializeField] Transform destination;
	[SerializeField] bool keepPlayerYaw = true; // сохран€ть поворот игрока

	void OnTriggerEnter(Collider other)
	{
		if (!other.CompareTag("Player"))
			return;
		if (destination == null)
			return;

		// »щем CharacterController на игроке (обычно он на корневом Player)
		var cc = other.GetComponent<CharacterController>();
		if (cc == null)
		{
			// если вдруг триггер зацепил дочерний объект Ч поднимемс€ вверх
			cc = other.GetComponentInParent<CharacterController>();
			if (cc == null)
				return;
		}

		var player = cc.transform;

		// ¬ј∆Ќќ: CharacterController нельз€ просто так телепортить во врем€ коллизий Ч выключаем на миг
		cc.enabled = false;

		player.position = destination.position;

		if (!keepPlayerYaw)
		{
			// если хочешь, чтобы игрок смотрел как destination
			player.rotation = Quaternion.Euler(0f, destination.eulerAngles.y, 0f);
		}

		cc.enabled = true;
	}
}
