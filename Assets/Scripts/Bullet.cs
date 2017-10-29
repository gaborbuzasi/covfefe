using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour {

    private Color[] colors = { Color.black, Color.blue, Color.cyan, Color.gray, Color.green, Color.magenta, Color.red, Color.yellow };
    private int randomNumber = -1;
    void Start()
    {
        var renderer = GetComponent<Renderer>();
        randomNumber = Random.Range(0, colors.Length);
        renderer.material.color = colors[randomNumber];
    }

    void OnCollisionEnter(Collision collision)
	{
		var hit = collision.gameObject;
		var player = hit.GetComponent<PlayerScript>();
        var enemy = hit.GetComponent<EnemyScript>();
		if (player || enemy)
		{
            collision.collider.gameObject.GetComponent<EnemyScript>().bodyColor = this.colors[this.randomNumber];
            var health = hit.GetComponent<Health>();
			health.TakeDamage(10);
            Destroy(gameObject);
        }
	}
}
