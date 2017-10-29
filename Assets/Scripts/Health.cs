using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Health : NetworkBehaviour {

	public const int maxHealth = 100;
    public bool destroyOnDeath;

    [SyncVar(hook = "OnChangeHealth")]
    public int currentHealth = maxHealth;

	public RectTransform healthBar;

	public void TakeDamage(int amount)
	{
        if (!isServer)
            return;

		currentHealth -= amount;
		if (currentHealth <= 0)
		{
            float deathTime = Time.time;
            if (destroyOnDeath)
            {
                gameObject.GetComponent<Rigidbody>().freezeRotation = false;
            }
            else
            {
                currentHealth = maxHealth;
                // called on the server, will be invoked on the clients
                RpcRespawn();
            }
        }
	}

    void OnChangeHealth(int currentHealth)
    {
        healthBar.sizeDelta = new Vector2(currentHealth, healthBar.sizeDelta.y);
    }

    [ClientRpc]
    void RpcRespawn()
    {
        if (isLocalPlayer)
        {
            // move back to zero location
            transform.position = Vector3.zero;
        }
    }
}
