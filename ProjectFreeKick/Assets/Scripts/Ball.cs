using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {


    public float speed;
    public float effect;
    public Rigidbody rb;

    public bool isReplaying = false;

    private List<Vector3> m_positions;
    public Camera mainCam;
    public Camera replayCam;

    bool collided = false;
    float angle = 0.0F;

	// Use this for initialization
	void Start () {
        Debug.Log("speed : " + speed + ", effect : " + effect);

        System.Random rand = new System.Random();

        rb = gameObject.GetComponent<Rigidbody>();
        mainCam = GameObject.Find("MainCamera").GetComponent<Camera>();
        replayCam = GameObject.Find("ReplayCamera" + rand.Next(0,4)).GetComponent<Camera>();

        m_positions = new List<Vector3>();
		
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

    private void FixedUpdate()
    {
        //Debug.Log("SIZE : " + m_positions.Count);
        if (isReplaying)
        {
            replayCam.enabled = true;
            mainCam.enabled = false;
            Replay();
        } else
        {
            replayCam.enabled = false;
            mainCam.enabled = true;
            Record();
        }
    }

    public void Replay()
    {
        if (m_positions.Count > 0)
        {
            Time.timeScale = 0.4f;
            transform.position = m_positions[0];
            m_positions.RemoveAt(0);
        } else
        {
            Time.timeScale = 1;
            StopReplay();
        }
    }

    public void Record()
    {
        m_positions.Add(transform.position);
    }

    public void StartReplay()
    {
        isReplaying = true;
        rb.detectCollisions = false;
    }

    public void StopReplay()
    {
        isReplaying = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        collided = true;

        if (collision.gameObject.GetComponent<Destroyable>())
        {
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.GetComponent<GoalLineTechnology>())
        {
            StartReplay();
        }

    }
}
