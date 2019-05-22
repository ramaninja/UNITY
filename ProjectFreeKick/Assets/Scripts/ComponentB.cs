using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyTools;
using Random = UnityEngine.Random;

public class ComponentB : MonoBehaviour {

    delegate float EasingFunctionDelegate(float start, float end, float value);

    [SerializeField] bool m_DisplayFrameCount;
    [SerializeField] bool m_DisplayTime;

    AudioSource m_AudioSource;

    IEnumerator m_MultipleTransfCoroutine;

#region MonoBehaviour life cycle methods
    private void Awake()
    {
        m_AudioSource = GetComponent<AudioSource>();
        DebugTools.Log("Awake", gameObject, m_DisplayFrameCount, m_DisplayTime);
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(10,10,100,30), "Stop\nCoroutine"))
        {
            StopCoroutine(m_MultipleTransfCoroutine);
        }
    }

    // Use this for initialization
    void Start()
    {
        DebugTools.Log("Start", gameObject, m_DisplayFrameCount, m_DisplayTime);

        //StartCoroutine(TranslationCoroutine(2, transform, transform.position, transform.position + Random.onUnitSphere * 5));
        //StartCoroutine(RescaleCoroutine(4, transform, transform.localScale, transform.localScale * 10));
        m_MultipleTransfCoroutine = MultipleRandomTranslationsCoroutine(10, transform, () =>
        {
            m_AudioSource.Play();
        },
            () => RescaleCoroutine(4, transform, transform.localScale, transform.localScale * 10));
        StartCoroutine(m_MultipleTransfCoroutine);
    }

    private void OnEnable()
    {
        DebugTools.Log("OnEnable", gameObject, m_DisplayFrameCount, m_DisplayTime);
    }

    private void OnDisable()
    {
        DebugTools.Log("OnDisable", gameObject, m_DisplayFrameCount, m_DisplayTime);
    }

    // Update is called once per frame
    //void Update()
    //{
    //    DebugTools.Log("Update", gameObject, m_DisplayFrameCount, m_DisplayTime);
    //}

    //private void FixedUpdate()
    //{
    //    DebugTools.Log("FixedUpdate", gameObject, m_DisplayFrameCount, m_DisplayTime);
    //}

    //private void LateUpdate()
    //{
    //    DebugTools.Log("LateUpdate", gameObject, m_DisplayFrameCount, m_DisplayTime);
    //}

    //private void OnDestroy()
    //{
    //    DebugTools.Log("OnDestroy", gameObject, m_DisplayFrameCount, m_DisplayTime);
    //}
    #endregion

    IEnumerator TranslationCoroutine(float delay, Transform transf, Vector3 startPos, Vector3 endPos, EasingFunctionDelegate easingFunction)
    {
        float elapsedTime = 0;
        DebugTools.Log("TranslationCoroutine START", null, m_DisplayFrameCount, m_DisplayTime);
        //yield return new WaitForSeconds(delay);
        while (elapsedTime < delay)
        {
            DebugTools.Log("TranslationCoroutine UPDATE", null, m_DisplayFrameCount, m_DisplayTime);

            float k = elapsedTime / delay;
            transf.position = Vector3.Lerp(startPos, endPos, easingFunction(0,1,k));

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transf.position = endPos;
        DebugTools.Log("TranslationCoroutine END", null, m_DisplayFrameCount, m_DisplayTime);
    }

    IEnumerator RescaleCoroutine(int delay, Transform transf, Vector3 startScale, Vector3 endScale)
    {
        float elapsedTime = 0;

        while (elapsedTime < delay)
        {

            float k = elapsedTime / delay;
            transf.localScale = Vector3.Lerp(startScale, endScale, k);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transf.localScale = endScale;
    }

    //Yield break pour sortir de la coroutine

    IEnumerator MultipleRandomTranslationsCoroutine(int nTranslations, Transform transf, Action startAction = null, Action endAction = null)
    {
        if (startAction != null) startAction();
        int index = 0;

        while(index++ < nTranslations)
        {
            yield return StartCoroutine(TranslationCoroutine(Random.Range(0.5f, 2f), transf, transf.position, transf.position + Random.onUnitSphere * 5, EasingFunction.EaseInCubicD));
        }

        if (endAction != null) endAction();
    }

    void PlaySound()
    {
        m_AudioSource.Play();
    }
}
