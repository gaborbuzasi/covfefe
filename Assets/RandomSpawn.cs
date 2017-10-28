using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RandomSpawn : NetworkBehaviour {

    public GameObject prefab;
    public Texture[] politicians;

	public void Spawn()
    {
        GameObject player = (GameObject)Instantiate(prefab, transform.position, transform.rotation);
        player.GetComponent<Renderer>().material.mainTexture = politicians[Random.Range(0, 4)];
        NetworkServer.Spawn(player);
    }
}
