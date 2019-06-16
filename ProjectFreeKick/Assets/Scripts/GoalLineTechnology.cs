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

    IEnumerator PlaysSounds()
    {
        System.Random rand = new System.Random();
        int val = rand.Next(0, 2);

        if (val == 0)
        {
            comment1.Play();
        }
        else
        {
            comment2.Play();
        }

        is_scored_yet = false;

        yield return new WaitUntil(() => val == 0 ? comment1.isPlaying == false : comment2.isPlaying == false);
        is_scored_yet = true;

    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.GetComponent<Ball>())
        {
             crowdSound.Play();

            Player p = (Player)m_player.GetComponent(typeof(Player));
            p.ScoreIncrement(m_points);

            if (is_scored_yet)
            {
                StartCoroutine(PlaysSounds());
            }
        }
    }
}
