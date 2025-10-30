using UnityEngine;

public abstract class Agent : MonoBehaviour
{
    Rigidbody rb;

    Vector3 velocity, acceleration, steeringForce;

    Quaternion nextRotation;

    [SerializeField]
    protected float maxSpeed;

    protected Vector3 wanderTarget;

    public Vector3 Velocity { get { return velocity; } }

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if(rb == null)
        {
            Destroy(this);
        }
        nextRotation = transform.rotation;

        wanderTarget = Random.onUnitSphere;
    }

    void FixedUpdate()
    {
        acceleration = Vector3.zero;
        steeringForce = CalcSteering();
        acceleration += steeringForce;

        velocity += acceleration * Time.fixedDeltaTime;
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);

        if(velocity.magnitude > 0)
        {
            nextRotation = Quaternion.LookRotation(velocity, Vector3.up);
        }

        Vector3 nextPosition = transform.position + velocity * Time.fixedDeltaTime;
        rb.Move(nextPosition, nextRotation);
    }

    protected abstract Vector3 CalcSteering();

    public Vector3 GetFuturePosition(float timeInSeconds)
    {
        return transform.position + velocity * timeInSeconds;
    }

    protected Vector3 Seek(Vector3 targetPosition)
    {
        Vector3 desiredVelocity = targetPosition - transform.position;
        desiredVelocity = desiredVelocity.normalized * maxSpeed;

        Vector3 seekForce = desiredVelocity - velocity;
        return seekForce;
    }

    protected Vector3 Seek(GameObject targetObject)
    {
        return Seek(targetObject.transform.position);
    }

    protected Vector3 Flee(Vector3 fleeTarget)
    {
        Vector3 desiredVelocity = transform.position - fleeTarget;
        desiredVelocity = desiredVelocity.normalized * maxSpeed;

        return desiredVelocity - velocity;
    }

    protected Vector3 Flee(GameObject fleeTarget)
    {
        return Flee(fleeTarget.transform.position);
    }

    protected Vector3 Pursue(Agent target, float timeInSeconds)
    {
        return Seek(target.GetFuturePosition(timeInSeconds));
    }

    protected Vector3 Evade(Agent target, float timeInSeconds)
    {
        return Flee(target.GetFuturePosition(timeInSeconds));
    }

    protected Vector3 Wander(float wanderDistance, float wanderRadius, float wanderJitter)
    {
        wanderJitter *= Time.deltaTime;
        wanderTarget += new Vector3(
            Random.Range(-1.0f, 1.0f) * wanderJitter,
            Random.Range(-1.0f, 1.0f) * wanderJitter,
            Random.Range(-1.0f, 1.0f) * wanderJitter
        );

        wanderTarget = wanderTarget.normalized * wanderRadius;

        Vector3 targetInWorldSpace = transform.position +
            (Velocity.normalized * wanderDistance) + wanderTarget;

        return Seek(targetInWorldSpace);
    }
}
