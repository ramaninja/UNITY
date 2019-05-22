using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyTools;

public class ComponentA : MonoBehaviour {

    [SerializeField] bool m_DisplayFrameCount;
    [SerializeField] bool m_DisplayTime;

    private void Awake()
    {
        DebugTools.Log("Awake", gameObject, m_DisplayFrameCount, m_DisplayTime);
    }

    // Use this for initialization
    void Start () {
        DebugTools.Log("Start", gameObject, m_DisplayFrameCount, m_DisplayTime);
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
    void Update () {
        DebugTools.Log("Update", gameObject, m_DisplayFrameCount, m_DisplayTime);
    }

    private void FixedUpdate()
    {
        DebugTools.Log("FixedUpdate", gameObject, m_DisplayFrameCount, m_DisplayTime);
    }

    private void LateUpdate()
    {
        DebugTools.Log("LateUpdate", gameObject, m_DisplayFrameCount, m_DisplayTime);
    }

    private void OnDestroy()
    {
        DebugTools.Log("OnDestroy", gameObject, m_DisplayFrameCount, m_DisplayTime);
    }
}
