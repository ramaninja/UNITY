using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour {

    [Header("HUD")]
    [SerializeField] Text m_DisplayScore1;
    [SerializeField] Text m_DisplayScore2;
    [SerializeField] Text m_displayLevel;
    [SerializeField] Text m_victory;
    [SerializeField] GameObject m_victoryPanel;

    [Header("Levels")]
    [SerializeField] GameObject m_level1;
    [SerializeField] GameObject m_level2;
    [SerializeField] GameObject m_level3;
    [SerializeField] GameObject m_level4;

    int scorePlayer1;
    int scorePlayer2;
    int nb_level = 5;

    float m_NextShotTime;

    Rigidbody m_Rigidbody;

    bool m_IsGrounded = false;

    public int currentLevel = 0;

    bool secondShooter = false;

    AudioSource comment1;
    AudioSource comment2;
    AudioSource end1;
    AudioSource end2;

    private void Awake()
    {
        
        GameObject.Find("ReplayCamera0").GetComponent<Camera>().enabled = false;
        GameObject.Find("ReplayCamera1").GetComponent<Camera>().enabled = false;
        GameObject.Find("ReplayCamera2").GetComponent<Camera>().enabled = false;
        GameObject.Find("ReplayCamera3").GetComponent<Camera>().enabled = false;
        GameObject.Find("MainCamera").GetComponent<Camera>().enabled = true;

        m_level1.SetActive(false);
        m_level2.SetActive(false);
        m_level3.SetActive(false);
        m_level4.SetActive(false);

        Time.timeScale = 1;

        m_Rigidbody = GetComponent<Rigidbody>();

        m_victoryPanel.SetActive(false);

        scorePlayer1 = 0;
        scorePlayer2 = 0;
        SetScoreDisplay();
        SetLevelDisplay();

        var audios = GetComponents<AudioSource>();
        comment1 = audios[0];
        comment2 = audios[1];
        end1 = audios[2];
        end2 = audios[3];

        System.Random rand = new System.Random();

        if (rand.Next(0, 2) == 0)
        {
            comment1.Play();
        }
        else
        {
            comment2.Play();
        }
    }

    private void Update()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        m_IsGrounded = collision.gameObject.GetComponent<Ground>() != null ? true : false;
    }

    private void OnCollisionExit(Collision collision)
    {
        m_IsGrounded = collision.gameObject.GetComponent<Ground>() ? false : m_IsGrounded;
    }

    IEnumerator LevelUp()
    {
        if (!secondShooter)
        {
            currentLevel++;
            nb_level--;
        }
        

        yield return new WaitForSeconds(1);

        if (currentLevel == 1)
        {
            m_level1.SetActive(true);
            transform.position = m_level1.transform.position - new Vector3(0, 0, 15.0f);
        }
        else if (currentLevel == 2)
        {
            m_level1.SetActive(false);
            m_level2.SetActive(true);
            transform.position = m_level2.transform.position - new Vector3(0, 0, 15.0f);
        }
        else if (currentLevel == 3)
        {
            m_level2.SetActive(false);
            m_level3.SetActive(true);
            transform.position = m_level3.transform.position - new Vector3(0, 0, 15.0f);
        }
        else if (currentLevel == 4)
        {
            m_level3.SetActive(false);
            m_level4.SetActive(true);
            transform.position = m_level4.transform.position - new Vector3(0, 0, 15.0f);
        }

        SetLevelDisplay();

        if (!secondShooter && currentLevel == 5)
        {
            SetVictoryDisplay();
        }
    }

    public void ScoreIncrement(int points)
    {
        if (!secondShooter)
        {
            secondShooter = true;
            scorePlayer1 += points;
            SetScoreDisplay();
        } else
        {
            secondShooter = false;
            scorePlayer2 += points;
            SetScoreDisplay();
        }

        StartCoroutine(LevelUp());
    }

    public void SetScoreDisplay()
    {
        m_DisplayScore1.text = "Score Joueur 1 : " + scorePlayer1;
        m_DisplayScore2.text = "Score Joueur 2 : " + scorePlayer2;
    }

    public void SetLevelDisplay()
    {
        m_displayLevel.text = "Niveaux restant : " + nb_level;
    }

    public void SetVictoryDisplay()
    {
        m_victoryPanel.SetActive(true);

        if (scorePlayer1 > scorePlayer2)
        {
            m_victory.text = "Le joueur 1 l'emporte!";
        } else if (scorePlayer2 > scorePlayer1)
        {
            m_victory.text = "Le joueur 2 l'emporte!";
        } else
        {
            m_victory.text = "C'est un match nul!";
        }

        System.Random rand = new System.Random();

        if (rand.Next(0, 2) == 0)
        {
            end1.Play();
        }
        else
        {
            end2.Play();
        }
    }

    public bool isSecondShooter()
    {
        return secondShooter;
    }
}
