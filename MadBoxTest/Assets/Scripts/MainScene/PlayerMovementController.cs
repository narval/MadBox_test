using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovementController : MonoBehaviour
{
    // References
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private LevelData levelData;
    [SerializeField] private NavMeshAgent navMeshAgent;
    [Range(0, 1)] [SerializeField] private float percentage;

    //Custom
    [SerializeField] private float closeDistance = 0.5f;

    //Debug
    [SerializeField] private bool drawRails; // For debug

    //Internal
    [SerializeField] private bool wasHit;
    private bool win;
    [SerializeField] private string hazardTag = "Hazard";
    [SerializeField] private int currentWaypoint = 0;


    void Start()
    {
        currentWaypoint = 0;
        transform.position = levelData.waypoints[currentWaypoint];
        navMeshAgent.SetDestination(levelData.waypoints[currentWaypoint + 1]);
        navMeshAgent.isStopped = true;
    }

    void Update()
    {
        if (wasHit || win)
        {
            //SetPlayerPosition(percentage);
        }
        else
        {
            if (navMeshAgent.remainingDistance <= closeDistance)
            {

                if (currentWaypoint < levelData.waypoints.Length - 1)
                {
                    ++currentWaypoint;
                    navMeshAgent.SetDestination(levelData.waypoints[currentWaypoint + 1]);
                }
                else
                {
                    win = true;
                }
            }
        }
        //calculate percentage
    }

    private void OnDrawGizmos()
    {
        if (drawRails)
        {
            for (int i = 0; i < levelData.waypoints.Length; i++)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawSphere(levelData.waypoints[i], 0.5f);
            }

            #if UNITY_EDITOR
            for (int i = 0; i < levelData.waypoints.Length - 1; i++)
            {
                UnityEditor.Handles.color = Color.magenta;
                UnityEditor.Handles.DrawDottedLine(levelData.waypoints[i], levelData.waypoints[i + 1], 3f);
            }
            #endif
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag(hazardTag))
        {
            wasHit = true;
            rigidBody.useGravity = true;
            rigidBody.constraints = RigidbodyConstraints.None; //Free the player so it can act like a ragdoll
            navMeshAgent.isStopped = true;
            navMeshAgent.enabled = false;
        }
    }

    public void Run(bool shouldRun)
    {
        if (wasHit || win) return;
        navMeshAgent.isStopped = !shouldRun;

    }
}
