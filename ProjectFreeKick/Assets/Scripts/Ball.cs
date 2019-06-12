using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {


    public float speed;
    public float effect;

    bool collided = false;
    float angle = 0.0F;

	// Use this for initialization
	void Start () {
        Debug.Log("speed : " + speed + ", effect : " + effect);
		
	}
	
	// Update is called once per frame
	void Update()
    {
        if (!collided)
        {
            var x = transform.eulerAngles.z * Mathf.Deg2Rad;
            angle += 0 - (effect/30);
            Debug.Log(angle);
            transform.position += new Vector3(x+angle, 0, 0) * speed * Time.deltaTime;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(name + " HIIIIT " + collision.gameObject.name);

        collided = false;

        if (collision.gameObject.GetComponent<Destroyable>())
        {
            Destroy(collision.gameObject);
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    Debug.Log("AIE");
    //    other.GetComponent<MeshRenderer>().material.color = Random.ColorHSV();
    //    Destroy(other.gameObject);
    //}
}
