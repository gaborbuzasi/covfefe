using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerScript : NetworkBehaviour {

	public GameObject bulletPrefab;
	public Transform bulletSpawn;
    public Texture[] faces;
    private Rigidbody body;

    [SyncVar(hook = "OnChangeImage")]
    public int chosenImage;

    private Health healthBar;
    private float deathTime = -1;

    public void Start()
    {
        body = GetComponent<Rigidbody>();
        this.chosenImage = new System.Random().Next(0, 4);
        healthBar = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
	{
		if (!isLocalPlayer) {
			return;
		}

		var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
		var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

		transform.Rotate(0, x, 0);
		transform.Translate(0, 0, z);
        CmdUpdateTexture();

        if (Input.GetKeyDown(KeyCode.E))
		{
			CmdFire();
		}

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

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            body.AddForce(Vector3.up * 2, ForceMode.Impulse);
        }
    }

    void OnChangeImage(int chosenImage)
    {
        GetComponent<Renderer>().material.mainTexture = faces[chosenImage];
    }

    [Command]
	void CmdFire()
	{
		// Create the Bullet from the Bullet Prefab
		var bullet = (GameObject)Instantiate (
			bulletPrefab,
			bulletSpawn.position,
			bulletSpawn.rotation);

		// Add velocity to the bullet
		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6;

		// Spawn the bullet on the Clients
		NetworkServer.Spawn(bullet);

		// Destroy the bullet after 2 seconds
		Destroy(bullet, 2.0f);
	}

    [Command]
    void CmdUpdateTexture()
    {
        GetComponent<Renderer>().material.mainTexture = faces[chosenImage];
    }

}
