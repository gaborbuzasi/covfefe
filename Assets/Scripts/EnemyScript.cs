using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

    private Health healthBar;
    private float deathTime = -1;
    public Color bodyColor = Color.black;
	// Use this for initialization
	void Start () {
        healthBar = GetComponent<Health>();
	}
	
	// Update is called once per frame
	void Update () {

        GetComponent<Renderer>().material.color = bodyColor;

		if (healthBar.currentHealth <= 0)
        {
            if (deathTime == -1)
                deathTime = Time.time;

            if (Time.time - deathTime >= 3)
            {
                Destroy(gameObject);
            }
        }
	}
}
