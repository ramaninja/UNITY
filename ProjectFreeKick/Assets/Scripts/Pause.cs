using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject m_pausePanel;
    [SerializeField] private GameObject m_player;
    [SerializeField] private GameObject m_arrow;

    AudioSource pause1;
    AudioSource pause2;
    AudioSource continue1;
    AudioSource continue2;

    void Start()
    {
        m_pausePanel.SetActive(false);

        var audios = GetComponents<AudioSource>();
        pause1 = audios[0];
        pause2 = audios[1];
        continue1 = audios[2];
        continue2 = audios[3];
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!m_pausePanel.activeInHierarchy)
            {
                PauseGame();
            }
            else if (m_pausePanel.activeInHierarchy)
            {
                ContinueGame();
            }
        }
    }

    private void PauseGame()
    {
        System.Random rand = new System.Random();

        if (rand.Next(0, 2) == 0)
        {
            pause1.Play();
        } else
        {
            pause2.Play();
        }

        Time.timeScale = 0;
        m_pausePanel.SetActive(true);
        m_player.GetComponent<Player>().enabled = false;
        m_arrow.GetComponent<ArrowController    >().enabled = false;
    }

    public void Continue()
    {
        ContinueGame();
    }

    private void ContinueGame()
    {
        System.Random rand = new System.Random();

        if (rand.Next(0, 2) == 0)
        {
            continue1.Play();
        }
        else
        {
            continue2.Play();
        }

        Time.timeScale = 1;
        m_pausePanel.SetActive(false);
        m_player.GetComponent<Player>().enabled = true;
        m_arrow.GetComponent<ArrowController>().enabled = true;
    }

    public void BackMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}

