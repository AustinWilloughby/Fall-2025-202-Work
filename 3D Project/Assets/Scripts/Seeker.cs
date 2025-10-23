using UnityEngine;

public class Seeker : Agent
{
    [SerializeField]
    GameObject seekTarget;

    [SerializeField, Range(0f, 1f)]
    float seekWeight;

    protected override Vector3 CalcSteering()
    {
        Vector3 totalForce = Vector3.zero;
        totalForce += Seek(seekTarget) * seekWeight;

        return totalForce;
    }
}
