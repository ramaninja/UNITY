using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poteau : MonoBehaviour
{
    public AudioSource hitPost;
    // Start is called before the first frame update
    void Start()
    {
        hitPost = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Ball>())
        {
            hitPost.Play();
        }
    }
}
