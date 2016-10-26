using UnityEngine;
using System.Collections;


/**
 * Implement patrol from https://docs.unity3d.com/Manual/nav-AgentPatrol.html
 */
[RequireComponent(typeof (NavMeshAgent))]
public class AIControl : BaseControl {

    public Transform enemy;
    public NavMeshAgent agent { get; private set; }
    public GameObject path;

    private Transform[] points;
    private int destPoint = 1;

    void Start () {
        points = path.GetComponentsInChildren<Transform>();
        agent = GetComponent<NavMeshAgent>();

        agent.updateRotation = true;
        agent.updatePosition = true;

        agent.autoBraking = false;

        GotoNextPoint();
    }

    void GotoNextPoint()
    {
        if (points.Length < 2)
            return;

        agent.destination = points[destPoint].position;

        destPoint = (destPoint + 1) % points.Length;
        if (destPoint == 0)//First point is the path points parent object
            destPoint++;
    }

    void Update () {
        if (agent.remainingDistance < 0.5f)
            GotoNextPoint();
    }

    public override bool shootRequested()
    {
        return true;
    }
}
