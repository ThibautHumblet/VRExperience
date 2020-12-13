using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPad : MonoBehaviour
{

	public bool damageOnCollision = false;

	void OnCollisionEnter(Collision collision)                      // this is used for contact on touching the teloprt pad
	{
		if (damageOnCollision)
		{

			if (collision.gameObject.GetComponent<TeleportContact>() != null)
			{
				collision.gameObject.GetComponent<TeleportContact>().Teleport();

			}
		}
	}
}
