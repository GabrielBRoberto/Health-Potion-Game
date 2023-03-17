using System.Collections.Generic;
using System.Collections;
using Cinemachine;
using UnityEngine;
using System;

public class MultipleTargetsCamera : MonoBehaviour
{
    public List<Transform> targets;

    public Vector3 offset;

    public float smoothTime = 0.5f;

    public float maxZoom = 10f;
    public float minZoom = 40f;
    public float zoomLimiter = 50f;

    private Vector3 velocity;

    [SerializeField]
    private CinemachineVirtualCamera cam;

    private void LateUpdate()
    {
        Move();
        Zoom();
    }

    private void Move()
    {
        Vector3 centerPoint = GetCenterPoint();

        Vector3 newPosition = centerPoint + offset;

        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }
    private void Zoom()
    {
        float newZoom = Mathf.Lerp(maxZoom, minZoom, GetGreatestDistance() / zoomLimiter);

        cam.m_Lens.OrthographicSize = Mathf.Lerp(cam.m_Lens.OrthographicSize, Vector2.Distance(targets[0].position, targets[1].position) / 2, Time.deltaTime);
    }

    private Vector3 GetCenterPoint()
    {
        if (targets.Count == 1)
        {
            return targets[0].position;
        }

        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }

        return bounds.center;
    }

    float GetGreatestDistance()
    {
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }

        return bounds.size.x;
    }
}