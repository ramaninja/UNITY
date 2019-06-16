using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour {

    [Header("Movement SetUp")]
    [SerializeField] float m_TranslationSpeed;
    [SerializeField] float m_RotationSpeed;

    [Header("Projectile SetUp")]
    [SerializeField] GameObject m_BallPrefab;
    [SerializeField] Transform m_BallSpawnPoint;
    [SerializeField] float m_BallInitSpeed;
    [SerializeField] float m_BallDuration;
    [SerializeField] float m_ShotCoolDownDuration;

    [SerializeField] Text m_DisplayScore;

    [Header("Levels")]
    [SerializeField] GameObject m_level1;
    [SerializeField] GameObject m_level2;

    bool m_IsCharging = false;
    float m_timePressed;
    float m_StartTimePressed;
    int score;

    float m_NextShotTime;

    Rigidbody m_Rigidbody;

    bool m_IsGrounded = false;

    public int currentLevel = 0;

    AudioSource comment1;
    AudioSource comment2;

    private void Awake()
    {
        
        GameObject.Find("ReplayCamera0").GetComponent<Camera>().enabled = false;
        GameObject.Find("ReplayCamera1").GetComponent<Camera>().enabled = false;
        GameObject.Find("ReplayCamera2").GetComponent<Camera>().enabled = false;
        GameObject.Find("ReplayCamera3").GetComponent<Camera>().enabled = false;
        GameObject.Find("MainCamera").GetComponent<Camera>().enabled = true;

        //GameObject.Find("Level1").SetActive(false);
        m_level1 = GameObject.Find("Level1");
        m_level1.SetActive(false);
        m_level2 = GameObject.Find("Level2");
        m_level2.SetActive(false);

        m_Rigidbody = GetComponent<Rigidbody>();
        score = 0;
        SetScoreDisplay();

        var audios = GetComponents<AudioSource>();
        comment1 = audios[0];
        comment2 = audios[1];

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

    // Use this for initialization
    void Start () {

        m_NextShotTime = Time.time; //Temps ecoulé depuis le moment où on a ouvert l'executable
    }

    //Quaternion
    //C'est un nombre 
    //Donc on peut faire des opérations
    //Deux multiplication successives -> deux rotations successives
    //Les opérations se font dans le sens inverse

    //Update() == comportement cinématique
    //FixedUpdate() == comportement physique ; need a rigidbody
    //====> Dans le FixedUpdate(), on n'émet que des souhaits, donc c'est possible qu'un objet ne bouge pas à la frame n


    //Update is called once per frame

    void Update()
    {
        //       var hInput = Input.GetAxis("Horizontal");
        //       var vInput = Input.GetAxis("Vertical");

        //       // new Vector3(0,0,1) == Vector3.forward
        //       transform.Translate(vInput * Vector3.forward * m_TranslationSpeed * Time.deltaTime, Space.Self);
        //       // transform.Translate(hInput * Vector3.right * m_TranslationSpeed * Time.deltaTime, Space.Self);

        //       transform.Rotate(Vector3.up, m_RotationSpeed * Time.deltaTime * hInput);
    }

    private void FixedUpdate()
    {
        var hInput = Input.GetAxis("Horizontal");
        var vInput = Input.GetAxis("Vertical");
        var jump = Input.GetButton("Jump");
        var fire = Input.GetButton("Fire1"); //Clic Gauche/Ctrl/X Xbox One

        //Translation
        var vect = vInput * transform.forward * m_TranslationSpeed * Time.fixedDeltaTime;

        m_Rigidbody.MovePosition(m_Rigidbody.position + vect);

        //Rotation
        var qRot = Quaternion.AngleAxis(m_RotationSpeed * Time.fixedDeltaTime * hInput, transform.up);
        m_Rigidbody.MoveRotation(qRot * m_Rigidbody.rotation);

        if (m_IsGrounded)
        {
            if (jump)
            {
                m_Rigidbody.AddForce(Vector3.up * 10, ForceMode.Impulse);
            }
            m_Rigidbody.velocity = Vector3.zero;
            m_Rigidbody.angularVelocity = Vector3.zero;
        }

        //if (fire && Time.time > m_NextShotTime)
        //{
        //    GameObject ballGO = Instantiate(m_BallPrefab);
        //    ballGO.transform.position = m_BallSpawnPoint.position;
        //    ballGO.GetComponent<Rigidbody>().velocity = m_BallSpawnPoint.forward * m_BallInitSpeed;
        //    Destroy(ballGO, m_BallDuration);
        //    m_NextShotTime = Time.time + m_ShotCoolDownDuration;
        //}

        if (fire && !m_IsCharging && Time.time > m_NextShotTime)
        {
            m_IsCharging = true;
            m_StartTimePressed = Time.time;
        } else if (!fire && m_IsCharging)
        {
            //GameObject ballGO = Instantiate(m_BallPrefab);
            //ballGO.transform.position = m_BallSpawnPoint.position;
            //m_timePressed = Time.time - m_StartTimePressed;
            //ballGO.GetComponent<Rigidbody>().velocity = m_BallSpawnPoint.forward * m_BallInitSpeed * m_timePressed;
            //Destroy(ballGO, m_BallDuration);
            //m_NextShotTime = Time.time + m_ShotCoolDownDuration;
            m_IsCharging = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(name + " HIT " + collision.gameObject.name);

        m_IsGrounded = collision.gameObject.GetComponent<Ground>() != null ? true : false;
    }

    private void OnCollisionExit(Collision collision)
    {
        m_IsGrounded = collision.gameObject.GetComponent<Ground>() ? false : m_IsGrounded;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("TRIGGER");
    }

    IEnumerator LevelUp()
    {
        currentLevel++;

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
    }

    public void ScoreIncrement(int points)
    {
        score+=points;
        SetScoreDisplay();

        StartCoroutine(LevelUp());
    }

    public void SetScoreDisplay()
    {
        m_DisplayScore.text = "Score : " + score;
    }
}
