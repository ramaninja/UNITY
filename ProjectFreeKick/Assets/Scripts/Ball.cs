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
            angle += 0 - (effect/90);
            gameObject.transform.localPosition += new Vector3(angle, 0, 0) * speed * Time.deltaTime;

            gameObject.transform.Rotate(0, effect, 0, Space.Self);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(name + " HIIIIT " + collision.gameObject.name);

        collided = true;

        if (collision.gameObject.GetComponent<Destroyable>())
        {
            Destroy(collision.gameObject);
        }
    }
}
