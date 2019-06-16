using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalLineTechnology : MonoBehaviour
{
    [SerializeField] GameObject m_player;
    [SerializeField] int m_points;
    private AudioSource crowdSound;
    private AudioSource comment1;
    private AudioSource comment2;
    private static bool is_scored_yet;
    // Start is called before the first frame update
    void Start()
    {
        var audios = GetComponents<AudioSource>();
        crowdSound = audios[0];
        comment1 = audios[1];
        comment2 = audios[2];

        is_scored_yet = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //gameObject.GetComponent<Collider>().enabled = is_scored_yet;
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

    IEnumerator PlaysSounds()
    {
        System.Random rand = new System.Random();
        int val = rand.Next(0, 2);
        AudioSource tmp;

        if (val == 0)
        {
            comment1.Play();
        }
        else
        {
            comment2.Play();
        }

        Player p = (Player)m_player.GetComponent(typeof(Player));
        p.ScoreIncrement(m_points);

        is_scored_yet = false;
        //gameObject.GetComponent<Collider>().enabled = false;

        yield return new WaitUntil(() => val == 0 ? comment1.isPlaying == false : comment2.isPlaying == false);
        //gameObject.GetComponent<Collider>().enabled = true;
        is_scored_yet = true;

    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.GetComponent<Ball>())
        {
             crowdSound.Play();

            if (is_scored_yet)
            {
                StartCoroutine(PlaysSounds());
            }
        }
    }
}
