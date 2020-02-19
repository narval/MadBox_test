using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private LevelData levelData;
    [Range(0, 1)] [SerializeField] private float percentage;

    public bool drawRails;

    [SerializeField] private bool wasHit;
    [SerializeField] private string hazardTag = "Hazard";

    void Start()
    {
        SetPlayerPosition(0);
    }

    void Update()
    {
        if (!wasHit)
        {
            SetPlayerPosition(percentage); // for debug only
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

    public void SetPlayerPosition(float percentage)
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
            rigidBody.constraints = RigidbodyConstraints.None; //Free the player so it can act like a ragdoll
        }
    }
}
