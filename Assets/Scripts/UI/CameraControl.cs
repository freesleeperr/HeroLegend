using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private CinemachineConfiner2D confiner2D;
    public CinemachineImpulseSource impulseSource;

    public VoidEventSO cameraShakeEvent;

    private void OnEnable()
    {
        cameraShakeEvent.OnEventRaised += OnCameraShakeEvent;
    }
    private void OnDisable()
    {
        cameraShakeEvent.OnEventRaised -= OnCameraShakeEvent;
    }
    void OnCameraShakeEvent()
    {
        impulseSource.GenerateImpulse();
    }
    private void Awake()
    {
        confiner2D = GetComponent<CinemachineConfiner2D>();
    }
    private void Start()
    {
        GetNewCameraBounds();
    }
    //TODO:场景切换后更改
    private void GetNewCameraBounds()
    {
        var obj = GameObject.FindGameObjectWithTag("Bound");
        if (obj == null)
        {
            return;
        }
        confiner2D.m_BoundingShape2D = obj.GetComponent<Collider2D>();
        confiner2D.InvalidateCache();
    }
}
