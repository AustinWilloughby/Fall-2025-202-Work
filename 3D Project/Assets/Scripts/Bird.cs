using UnityEngine;

public class Bird : Agent
{
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
    }
}