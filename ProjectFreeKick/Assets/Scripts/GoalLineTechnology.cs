using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalLineTechnology : MonoBehaviour
{
    [SerializeField] GameObject m_player;
    [SerializeField] int m_points;
    private AudioSource audio1;
    private AudioSource audio2;
    //private AudioSource audio3;
    private static bool is_scored_yet;
    // Start is called before the first frame update
    void Start()
    {
        var audios = GetComponents<AudioSource>();
        audio1 = audios[0];
        try
        {
            audio2 = audios[1];
        } catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
        
        //audio3 = audios[2];

        is_scored_yet = true;
    }

    // Update is called once per frame
    void Update()
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

    IEnumerator blockTrigger()
    {
        audio2.Play();
        Player p = (Player)m_player.GetComponent(typeof(Player));
        p.ScoreIncrement(m_points);
        is_scored_yet = false;
        yield return new WaitUntil(() => audio2.isPlaying == false);
        is_scored_yet = true;

    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.GetComponent<Ball>())
        {
            audio1.Play();

            if (is_scored_yet)
            {
                StartCoroutine(blockTrigger());
            }
        }
    }
}
