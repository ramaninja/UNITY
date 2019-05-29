using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    bool m_IsCharging = false;
    float m_timePressed;
    float m_StartTimePressed;

    float m_NextShotTime;

    Rigidbody m_Rigidbody;

    bool m_IsGrounded = false;

    AudioSource hitTheBall;

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    // Use this for initialization
    void Start () {

        m_NextShotTime = Time.time; //Temps ecoulé depuis le moment où on a ouvert l'executable
        hitTheBall = GetComponent<AudioSource>();


    }

    //Quaternion
    //C'est un nombre 
    //Donc on peut faire des opérations
    //Deux multiplication successives -> deux rotations successives
    //Les opérations se font dans le sens inverse
	
    //Update() == comportement cinématique
    //FixedUpdate() == comportement physique ; need a rigidbody
    //====> Dans le FixedUpdate(), on n'émet que des souhaits, donc c'est possible qu'un objet ne bouge pas à la frame n
    

	// Update is called once per frame
	//void Update () {

 //       var hInput = Input.GetAxis("Horizontal");
 //       var vInput = Input.GetAxis("Vertical");

 //       // new Vector3(0,0,1) == Vector3.forward
 //       transform.Translate(vInput * Vector3.forward * m_TranslationSpeed * Time.deltaTime, Space.Self);
 //       // transform.Translate(hInput * Vector3.right * m_TranslationSpeed * Time.deltaTime, Space.Self);

 //       transform.Rotate(Vector3.up, m_RotationSpeed * Time.deltaTime * hInput);
 //   }

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
            hitTheBall.Play();
            GameObject ballGO = Instantiate(m_BallPrefab);
            ballGO.transform.position = m_BallSpawnPoint.position;
            m_timePressed = Time.time - m_StartTimePressed;
            ballGO.GetComponent<Rigidbody>().velocity = m_BallSpawnPoint.forward * m_BallInitSpeed * m_timePressed;
            Destroy(ballGO, m_BallDuration);
            m_NextShotTime = Time.time + m_ShotCoolDownDuration;
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
}
