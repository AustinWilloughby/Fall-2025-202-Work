using UnityEngine;
using System.Collections.Generic;

public class Flock : MonoBehaviour
{
    private Vector3 averagePosition, averageVelocity;
    private List<Agent> flockMembers;

    public Vector3 AveragePosition { get { return averagePosition; } }
    public Vector3 AverageVelocity { get { return averageVelocity; } }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        flockMembers = new List<Agent>(GetComponentsInChildren<Agent>());
    }

    // Update is called once per frame
    void Update()
    {
        averagePosition = Vector3.zero;
        foreach(Transform child in transform)
        {
            averagePosition += child.position;
        }
        averagePosition /= transform.childCount;


        averageVelocity = Vector3.zero;
        foreach(Agent a in flockMembers)
        {
            averageVelocity += a.Velocity;
        }
        averageVelocity /= flockMembers.Count;
    }

    public Vector3[] FindNearby(Transform self, float range)
    {
        List<Vector3> neighbors = new List<Vector3>();

        foreach(Transform child in transform)
        {
            if(child != self && Vector3.Distance(self.position, child.position) <= range)
            {
                neighbors.Add(child.position);
            }
        }

        return neighbors.ToArray();
    }
}