using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalLineTechnology : MonoBehaviour
{
    private AudioSource goal;
    // Start is called before the first frame update
    void Start()
    {
        goal = GetComponent<AudioSource>();
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
            Debug.Log("TEEEEST");
            goal.Play();
        }
    }
}
