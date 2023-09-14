using UnityEngine;

public class BatModel
{
    public float MoveSpeed { get; private set; }
    public Transform StartingPoint { get; private set; }
    public Transform PlayerTransform { get; private set; }
    public bool IsChasing { get; set; }
    public bool Aware { get; set; }
    public bool IsAtStart { get; set; }
    public float ChaseDistance { get; private set; }
    public float AwareDistance { get; private set; }

    public BatModel(float moveSpeed, Transform startingPoint, Transform playerTransform, float chaseDistance, float awareDistance)
    {
        MoveSpeed = moveSpeed;
        StartingPoint = startingPoint;
        PlayerTransform = playerTransform;
        ChaseDistance = chaseDistance;
        AwareDistance = awareDistance;
    }
}