using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour {

	void OnCollisionEnter(Collision collision)
	{
		var hit = collision.gameObject;
		var player = hit.GetComponent<PlayerScript>();
        var enemy = hit.GetComponent<EnemyScript>();
		if (player || enemy)
		{
            var health = hit.GetComponent<Health>();
			health.TakeDamage(10);
            Destroy(gameObject);
        }
	}
}
