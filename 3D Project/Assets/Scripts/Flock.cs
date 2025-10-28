using UnityEngine;
using System.Collections.Generic;

public class Flock : MonoBehaviour
{
    private Vector3 averagePosition, averageVelocity;
    private List<Agent> childAgents;
    private List<Vector3> childPositions;

    public Vector3 AveragePosition { get { return averagePosition; } }
    public Vector3 AverageVelocity { get { return averageVelocity; } }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        childAgents = new List<Agent>(GetComponentsInChildren<Agent>());
        childPositions = new List<Vector3>();
    }

    // Update is called once per frame
    void Update()
    {
        averagePosition = Vector3.zero;
        childPositions.Clear();
        foreach (Transform child in transform)
        {
            averagePosition += child.position;
            childPositions.Add(child.position);
        }
        averagePosition /= childPositions.Count;

        averageVelocity = Vector3.zero;
        foreach(Agent a in childAgents)
        {
            averageVelocity += a.Velocity;
        }
        averageVelocity /= childAgents.Count;
    }

    public Vector3[] FindNearby(Transform self, float range)
    {
        List<Vector3> neighbors = new List<Vector3>();

        foreach(Transform child in transform)
        {
            if(child != self 
                && Vector3.Distance(self.position, child.position) <= range)
            {
                neighbors.Add(child.position);
            }
        }

        return neighbors.ToArray();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(averagePosition, 5);

        Gizmos.color = Color.magenta;
        Gizmos.DrawRay(averagePosition, averageVelocity * 10.0f);
    }
}
