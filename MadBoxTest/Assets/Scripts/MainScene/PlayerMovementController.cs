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
    [SerializeField] private MainSceneUIController mainSceneUIController;


    //Custom
    [SerializeField] private float closeDistance = 0.5f;

    //Debug
    [SerializeField] private bool drawRails; // For debug

    //Internal
    [Range(0, 1)] [SerializeField] private float percentage;
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
        Run(Input.GetMouseButton(0) || Input.touchCount > 0); // Handles player input
        if (!wasHit & !win)
        {
            if (navMeshAgent.remainingDistance <= closeDistance)
            {
                if (currentWaypoint < levelData.waypoints.Length - 2)
                {
                    ++currentWaypoint;
                    navMeshAgent.SetDestination(levelData.waypoints[currentWaypoint + 1]);
                }
                else
                {
                    win = true;
                    mainSceneUIController.OnWin();
                    //Start winning coroutine
                }
            }
            percentage = Mathf.InverseLerp(0, levelData.waypoints.Length, currentWaypoint); // should add the amount between waypoints
            mainSceneUIController.SetSliderValue(percentage);
        }
        
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
        if (wasHit || win) return;
        if (collision.collider.CompareTag(hazardTag))
        {
            wasHit = true;
            rigidBody.useGravity = true;
            rigidBody.constraints = RigidbodyConstraints.None; //Free the player so it can act like a ragdoll
            navMeshAgent.isStopped = true;
            navMeshAgent.enabled = false;
            mainSceneUIController.OnLose();
            //End game
        }
    }

    public void Run(bool shouldRun)
    {
        if (wasHit || win) return;
        navMeshAgent.isStopped = !shouldRun;

    }
}
