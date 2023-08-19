using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    private NavMeshAgent agent;
    public Vector3 DestPoint;

    SceneController scene;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        //scene = FindObjectOfType<SceneController>();
        DestPoint = transform.position;

        Application.targetFrameRate = 120;
    }

    private void Update()
    {
        if (DestPoint != null)
        {
            agent.SetDestination(DestPoint);
        }
    }
}