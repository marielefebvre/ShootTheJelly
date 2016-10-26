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

        agent.updateRotation = false;
        agent.updatePosition = true;

        // Disabling auto-braking allows for continuous movement
        // between points (ie, the agent doesn't slow down as it
        // approaches a destination point).
        agent.autoBraking = false;

        GotoNextPoint();
    }

    void GotoNextPoint()
    {
        // Returns if no points have been set up
        if (points.Length < 2)
            return;

        // Set the agent to go to the currently selected destination.
        agent.destination = points[destPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % points.Length;
        if (destPoint == 0)
            destPoint++;
    }

    void Update () {
        //if (!gameManager.pause)
        //    transform.LookAt(enemy);

        // Choose the next destination point when the agent gets
        // close to the current one.
        if (agent.remainingDistance < 0.5f)
            GotoNextPoint();

        //if (points.Length > 0)
        //    agent.SetDestination(points[0].position);

        //if (agent.remainingDistance > agent.stoppingDistance)
        //    character.Move(agent.desiredVelocity, false, false);
        //else
        //    character.Move(Vector3.zero, false, false);
    }

    public override bool shootRequested()
    {
        return true;
    }
}
