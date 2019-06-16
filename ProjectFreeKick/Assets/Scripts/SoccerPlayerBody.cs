using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoccerPlayerBody : MonoBehaviour
{
    [Header("Textures Player")]
    [SerializeField] Material m_player1Texture;
    [SerializeField] Material m_player2Texture;

    Player p;

    // Start is called before the first frame update
    void Start()
    {
        p = (Player)GameObject.Find("Player").GetComponent(typeof(Player));
    }

    // Update is called once per frame
    void Update()
    {
        if (!p.isSecondShooter())
        {
            gameObject.GetComponent<Renderer>().material = m_player1Texture;
        }
        else
        {
            gameObject.GetComponent<Renderer>().material = m_player2Texture;
        }
    }
}
