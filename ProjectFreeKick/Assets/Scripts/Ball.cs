using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(name + " HIIIIT " + collision.gameObject.name);

        if (collision.gameObject.GetComponent<Destroyable>())
        {
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<MeshRenderer>().material.color = Random.ColorHSV();
        Destroy(other.gameObject);
    }
}
