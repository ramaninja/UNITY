using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    [Header("Arrow Setup")]
    [SerializeField] float m_RotationSpeed;
    [SerializeField] float m_GlobalLocalRatio;
    [SerializeField] float m_ArrowDisapearTimer;

    [Header("Projectile Setup")]
    [SerializeField] GameObject m_BallPrefab;
    [SerializeField] float m_PowerMultiplier;
    [SerializeField] float m_BallSpeed;
    [SerializeField] float m_BallDuration;
    [SerializeField] float m_ShotCoolDownDuration;
    [SerializeField] float m_GrowSpeed;

    bool isCharging = false;
    float startTimePressed = 0F;
    float nextShotTime = 0F;
    float ballSpeed = 0F;

    float disabledUntil = 0F;


    // Start is called before the first frame update
    void Start()
    {
        ballSpeed = m_BallSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > disabledUntil && !isCharging)
            gameObject.transform.localScale = new Vector3(5F, 1F, 5F);

        fireControl();
        globalRotate(Input.GetKey(KeyCode.Q), Input.GetKey(KeyCode.D), Input.GetKey(KeyCode.Z), Input.GetKey(KeyCode.S));
        localRotate(Input.GetKey(KeyCode.A), Input.GetKey(KeyCode.E), Vector3.forward);
    }

    // Prend en parametre les deux touches correspondant à la rotation ainsi que l'axe associé
    // Effectue une rotation sur l'axe spécifié
    void localRotate(bool leftRotate, bool rightRotate, Vector3 vector)
    {
        var input = 0;

        if (leftRotate)
            input = 1;
        else if (rightRotate)
            input = -1;

        transform.Rotate(vector, m_RotationSpeed * Time.deltaTime * input);
    }

    void globalRotate(bool leftRotate, bool rightRotate, bool upRotate, bool downRotate)
    {
        float xAngle = 0, yAngle = 0, zAngle = 0;

        if (leftRotate)
            yAngle = -m_RotationSpeed / m_GlobalLocalRatio;
        else if (rightRotate)
            yAngle = m_RotationSpeed / m_GlobalLocalRatio;

        else if (upRotate)
            xAngle = -m_RotationSpeed / m_GlobalLocalRatio;
        else if (downRotate)
            xAngle = m_RotationSpeed / m_GlobalLocalRatio;

        gameObject.transform.Rotate(xAngle, yAngle, zAngle, Space.World);
    }

    void fireControl()
    {
        var fire = Input.GetButton("Fire1");

        if (fire && !isCharging && Time.time > nextShotTime)
        {
            isCharging = true;
            startTimePressed = Time.time;
        }
        else if (!fire && isCharging)
        {
            GameObject ballGO = Instantiate(m_BallPrefab);
            ballGO.transform.position = gameObject.transform.position;
            ballGO.GetComponent<Rigidbody>().velocity = gameObject.transform.forward * ballSpeed;
            Destroy(ballGO, m_BallDuration);
            nextShotTime = Time.time + m_ShotCoolDownDuration;
            isCharging = false;
            gameObject.transform.localScale = new Vector3(0F, 0F, 0F);
            disabledUntil = Time.time + m_ShotCoolDownDuration;
            ballSpeed = m_BallSpeed;
        }
        else if(fire && isCharging)
        {
            if (gameObject.transform.localScale.z > 12 || gameObject.transform.localScale.z < 5)
            {
                m_GrowSpeed *= -1;
                m_PowerMultiplier *= -1;
            }

            ballSpeed += m_PowerMultiplier;
            gameObject.transform.localScale += new Vector3(0, 0, m_GrowSpeed);
        }
    }
}
