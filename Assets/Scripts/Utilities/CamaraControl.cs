using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class CamaraControl : MonoBehaviour
{
    // Start is called before the first frame update
    CinemachineConfiner2D cinemachineConfiner2D;
    public CinemachineImpulseSource impulseSource;
    public VoidEventSO cameraShakeEvent;
    void Awake()
    {
        cinemachineConfiner2D = GetComponent<CinemachineConfiner2D>();
    }
    private void OnEnable()
    {
        cameraShakeEvent.OnEventRaised += OnCameraShakeEvent;
    }
    private void OnDisable()
    {
        cameraShakeEvent.OnEventRaised -= OnCameraShakeEvent;
    }
    private void OnCameraShakeEvent()
    {
        impulseSource.GenerateImpulse();
    }


    private void Start()
    {
        GetNewCameraBounds();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void GetNewCameraBounds()
    {
        var obj = GameObject.FindGameObjectWithTag("Bound");
        if (obj == null) return;
        cinemachineConfiner2D.m_BoundingShape2D = obj.GetComponent<Collider2D>();
        cinemachineConfiner2D.InvalidateCache();
    }
}
