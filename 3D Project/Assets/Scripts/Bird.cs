using UnityEngine;

public class Bird : Agent
{
    [SerializeField]
    private Flock flock;

    [SerializeField, Range(0f, 10.0f)]  
    private float cohesionWeight = 1.0f;

    [SerializeField, Range(0f, 10.0f)]
    private float alignmentWeight = 1.0f;

    [SerializeField, Range(0f, 10.0f)]
    private float separateWeight = 1.0f;

    [SerializeField, Range(0f, 10.0f)]
    private float separateDistance = 1.0f;

    [SerializeField, Range(0f, 10.0f)]
    private float wanderDistance = 3.0f;

    [SerializeField, Range(0f, 10.0f)]
    private float wanderRadius = 1.0f;

    [SerializeField, Range(0f, 10.0f)]
    private float wanderJitter = 1.0f;

    [SerializeField, Range(0f, 10.0f)]
    private float wanderWeight = 1.0f;

    [SerializeField]
    BoxCollider worldBounds;

    [SerializeField, Range(0f, 1f)]
    float boundsWeight;

    protected override Vector3 CalcSteering()
    {
        Vector3 totalForce = Vector3.zero;

        totalForce += Cohesion() * cohesionWeight;
        totalForce += Alignment() * alignmentWeight;
        totalForce += Separate(separateDistance) * separateWeight;
        totalForce += Wander(wanderDistance, wanderRadius, wanderJitter) * wanderWeight;

        Vector3 futurePos = GetFuturePosition(1);
        if (futurePos.x > worldBounds.transform.position.x + worldBounds.size.x / 2 ||
           futurePos.x < worldBounds.transform.position.x - worldBounds.size.x / 2 ||
           futurePos.y > worldBounds.transform.position.y + worldBounds.size.y / 2 ||
           futurePos.y < worldBounds.transform.position.y - worldBounds.size.y / 2 ||
           futurePos.z > worldBounds.transform.position.z + worldBounds.size.z / 2 ||
           futurePos.z < worldBounds.transform.position.z - worldBounds.size.z / 2)
        {
            totalForce += Seek(worldBounds.transform.position) * boundsWeight;
        }

        return totalForce;
    }

    private Vector3 Cohesion()
    {
        return Seek(flock.AveragePosition);
    }

    private Vector3 Alignment()
    {
        return (flock.AverageVelocity.normalized * maxSpeed) - Velocity;
    }

    private Vector3 Separate(float range)
    {
        Vector3[] nearby = flock.FindNearby(transform, range);

        if(nearby.Length == 0)
        {
            return Vector3.zero;
        }

        Vector3 separateForce = Vector3.zero;
        foreach(Vector3 loc in nearby)
        {
            Vector3 fleeForce = Flee(loc);
            float distance = Vector3.Distance(loc, transform.position);
            fleeForce *= 1.0f - (distance / range);
            separateForce += fleeForce;
        }

        return separateForce.normalized * maxSpeed;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position
            + Velocity.normalized * wanderDistance, wanderRadius);

        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(transform.position, 
            transform.position + Velocity.normalized * wanderDistance + wanderTarget);
    }
}

/*
[SerializeField]
BoxCollider worldBounds;

[SerializeField, Range(0f, 1f)]
float boundsWeight;

[SerializeField]
GameObject seekTarget;

[SerializeField, Range(0f, 1f)]
float seekWeight;

[SerializeField]
GameObject fleeTarget;

[SerializeField, Range(0f, 1f)]
float fleeWeight;

[SerializeField]
Agent pursuitTarget;

[SerializeField, Range(0f, 1f)]
float pursueWeight;

[SerializeField]
Agent evadeTarget;

[SerializeField, Range(0f, 1f)]
float evadeWeight;

Vector3 seekForce, fleeForce, pursuitForce, evadeForce, totalForce;


protected override Vector3 CalcSteering()
{
    totalForce = Vector3.zero;
    seekForce = Seek(seekTarget) * seekWeight;
    fleeForce = Flee(fleeTarget) * fleeWeight;
    float distance = Vector3.Distance(transform.position, pursuitTarget.transform.position) / 5.0f;
    pursuitForce = Pursue(pursuitTarget, distance) * pursueWeight;

    evadeForce = Evade(evadeTarget, distance) * evadeWeight;

    Vector3 futurePos = GetFuturePosition(1);
    if(futurePos.x > worldBounds.transform.position.x + worldBounds.size.x / 2 ||
       futurePos.x < worldBounds.transform.position.x - worldBounds.size.x / 2 ||
       futurePos.y > worldBounds.transform.position.y + worldBounds.size.y / 2 ||
       futurePos.y < worldBounds.transform.position.y - worldBounds.size.y / 2 ||
       futurePos.z > worldBounds.transform.position.z + worldBounds.size.z / 2 ||
       futurePos.z < worldBounds.transform.position.z - worldBounds.size.z / 2)
    {
        totalForce += Seek(worldBounds.transform.position) * boundsWeight;
    }

    totalForce += seekForce;
    totalForce += fleeForce;
    totalForce += pursuitForce;
    totalForce += evadeForce;
    return totalForce;
}

private void OnDrawGizmos()
{
    //Gizmos.color = Color.red;
    //Gizmos.DrawRay(transform.position, fleeForce);
    //
    //Gizmos.color = Color.green;
    //Gizmos.DrawRay(transform.position, seekForce);
    //
    //Gizmos.color = Color.blue;
    //Gizmos.DrawRay(transform.position, totalForce);

    Gizmos.color = Color.red;
    Gizmos.DrawRay(transform.position, pursuitForce);

    Gizmos.color = Color.green;
    Gizmos.DrawWireSphere(GetFuturePosition(2), 0.25f);
}*/