using UnityEngine;

public class Seeker : Agent
{
    [SerializeField]
    GameObject seekTarget;

    [SerializeField, Range(0, 1)]
    float seekScaler;

    protected override Vector3 CalcSteering()
    {
        Vector3 totalForce = Vector3.zero;
        totalForce += Seek(seekTarget) * seekScaler;

        return totalForce;
    }
}
