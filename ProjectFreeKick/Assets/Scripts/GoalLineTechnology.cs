using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalLineTechnology : MonoBehaviour
{
    private AudioSource crowd;
    private AudioSource zone;
    private AudioSource zone2;
    // Start is called before the first frame update
    void Start()
    {
        var audios = GetComponents<AudioSource>();
        Debug.Log("HHHHHHHHHH " + audios.Length);
        crowd = audios[0];
        zone = audios[1];
        zone2 = audios[2];
    }

    // Update is called once per frame
    void Update()
    {

    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.GetComponent<Ball>())
    //    {
    //        goal.Play();
    //    }
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.GetComponent<Ball>())
    //    {
    //        Debug.Log("TEEEEST");
    //        goal.Play();
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Ball>())
        {
            crowd.Play();

            System.Random rand = new System.Random();

            if (rand.Next(2)%2 == 0)
            {
                zone.Play();
            } else
            {
                zone2.Play();
            }
            
            //Debug.Log("TEEEEST : " + gameObject.name);

            //if (gameObject.name.Equals("Centre"))
            //{
            //    goal.Play();
            //} else if (gameObject.name.Equals("Transversale"))
            //{
            //    goal.Play();
            //}
        }
    }
}
