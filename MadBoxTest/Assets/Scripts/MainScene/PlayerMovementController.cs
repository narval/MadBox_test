using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    // References
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private LevelData levelData;
    [Range(0, 1)] [SerializeField] private float percentage;

    //Custom
    [SerializeField] private float maxSpeed = 5;
    [SerializeField] private float speed = 0;
    [SerializeField] private float accelerationRate = 0.5f;
    [SerializeField] private float decelerationRate = 0.1f;

    //Debug
    [SerializeField] private bool drawRails; // For debug

    //Internal
    private bool wasHit;
    [SerializeField] private string hazardTag = "Hazard";


    void Start()
    {
        SetPlayerPosition(0);
    }

    void Update()
    {
        speed -= decelerationRate;
        speed = Mathf.Max(0, speed);

        if (!wasHit)
        {
            SetPlayerPosition(percentage);
        }
    }

    private void OnDrawGizmos()
    {
        if (drawRails)
        {
            for (int i = 0; i < levelData.waypoints.Count; i++)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawSphere(levelData.waypoints[i].position, 0.5f);
            }

            #if UNITY_EDITOR
            for (int i = 0; i < levelData.waypoints.Count - 1; i++)
            {
                UnityEditor.Handles.color = Color.magenta;
                UnityEditor.Handles.DrawDottedLine(levelData.waypoints[i].position, levelData.waypoints[i + 1].position, 3f);
            }
            #endif
        }
    }

    private void SetPlayerPosition(float percentage)
    {
        int currentNode = GetCurrentWaypoint(percentage);
        int nextNode = currentNode + 1;

        transform.position = Vector3.Lerp(
            levelData.waypoints[currentNode].position,
            levelData.waypoints[nextNode].position,
            Mathf.InverseLerp(levelData.waypoints[currentNode].percentage, levelData.waypoints[nextNode].percentage, percentage)
            );
    }

    private int GetCurrentWaypoint(float percentage)
    {
        int actual = -1;
        for (int i = 0; i < levelData.waypoints.Count - 1; i++)
        {
            if (percentage >= levelData.waypoints[i].percentage && percentage < levelData.waypoints[i + 1].percentage)
            {
                actual = i;
                break;
            }
        }
        if (actual == -1)
        {
            actual = levelData.waypoints.Count - 2;
        }
        return actual;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag(hazardTag))
        {
            wasHit = true;
            rigidBody.useGravity = true;
            rigidBody.constraints = RigidbodyConstraints.None; //Free the player so it can act like a ragdoll
        }
    }

    public void Run()
    {
        speed -= accelerationRate;
        speed = Mathf.Min(maxSpeed, speed);



    }
}
