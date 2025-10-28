using UnityEngine;

public class Bird : Agent
{
    [SerializeField]
    private Flock flock;

    [SerializeField, Range(0f, 10.0f)]
    float cohesionWeight = 1;

    [SerializeField, Range(0f, 10.0f)]
    float alignmentWeight = 1;

    [SerializeField, Range(0f, 10.0f)]
    float separateWeight = 1;

    [SerializeField, Range(0f, 10.0f)]
    float separateDistance = 1;

    protected override Vector3 CalcSteering()
    {
        Vector3 totalForce = Vector3.zero;

        totalForce += Cohesion() * cohesionWeight;
        totalForce += Alignment() * alignmentWeight;
        totalForce += Separate(separateDistance) * separateWeight;

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

    private Vector3 Separate(float distance)
    {
        Vector3[] nearby = flock.FindNearby(transform, distance);

        if(nearby.Length == 0)
        {
            return Vector3.zero;
        }

        Vector3 separateForce = Vector3.zero;
        foreach(Vector3 location in nearby)
        {
            Vector3 fleeForce = Flee(location);
            float dist = Vector3.Distance(location, transform.position);
            fleeForce *= 1.0f - (dist / distance);
            separateForce += fleeForce;
        }

        return separateForce.normalized * maxSpeed;
    }
}

/*
[SerializeField]
BoxCollider worldBounds;

[SerializeField, Range(0f, 1f)]
float stayInBoundsWeight;

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
float pursuitWeight;

[SerializeField]
Agent evadeTarget;

[SerializeField, Range(0f, 1f)]
float evadeWeight;

private Vector3 seekForce, fleeForce, pursuitForce, evadeForce, totalForce;

protected override Vector3 CalcSteering()
{
    totalForce = Vector3.zero;

    seekForce = Seek(seekTarget) * seekWeight;
    fleeForce = Flee(fleeTarget) * fleeWeight;


    Vector3 futurePos = GetFuturePosition(1);
    if(futurePos.x > worldBounds.transform.position.x + (worldBounds.size.x / 2) ||
       futurePos.x < worldBounds.transform.position.x - (worldBounds.size.x / 2) ||
       futurePos.y > worldBounds.transform.position.y + (worldBounds.size.y / 2) ||
       futurePos.y < worldBounds.transform.position.y - (worldBounds.size.y / 2) ||
       futurePos.z > worldBounds.transform.position.z + (worldBounds.size.z / 2) ||
       futurePos.z < worldBounds.transform.position.z - (worldBounds.size.z / 2))
    {
        totalForce += Seek(worldBounds.transform.position) * stayInBoundsWeight;
    }

    pursuitForce = Pursue(pursuitTarget, 10) * pursuitWeight;
    evadeForce = Evade(evadeTarget, 5) * evadeWeight;

    totalForce += seekForce;
    totalForce += fleeForce;
    totalForce += pursuitForce;
    totalForce += evadeForce;
    return totalForce;
}

private void OnDrawGizmos()
{
    //Gizmos.color = Color.magenta;
    //Gizmos.DrawRay(transform.position, seekForce);
    //
    //Gizmos.color = Color.cyan;
    //Gizmos.DrawRay(transform.position, fleeForce);

    //Gizmos.color = Color.green;
    //Gizmos.DrawRay(transform.position, totalForce);

    Gizmos.color = Color.red;
    Gizmos.DrawRay(transform.position, pursuitForce);

    //Gizmos.DrawWireSphere(GetFuturePosition(1), 0.25f);
}
*/