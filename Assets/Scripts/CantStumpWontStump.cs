using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CantStumpWontStump : MonoBehaviour {
    public GameObject toLookat;
    private int personality;
    private float speed;
    public Rigidbody rb;
	// Use this for initialization
	void Start () {
        //personality = (int) Random.Range(0, 2);
        rb = toLookat.GetComponent<Rigidbody>();
        speed = 2;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag == "Player")
            {
                personality = 1;
            }
            else
            {
                personality = 0;
            }
        }
        if (personality == 1)
        {
            transform.LookAt(hit.collider.transform);
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, hit.collider.transform.position, step);
            Debug.LogWarning("ToLookAt: " + toLookat.transform.position);
            Debug.LogWarning("HitCollider: " + hit.collider.transform.position);
        }
        
	}
}
