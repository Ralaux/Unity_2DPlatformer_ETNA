using UnityEngine;

public class RinoModel
{
    public float MoveSpeed { get; private set; }

    public Transform[] PatrolPoints { get; private set; }
    public int patrolDestination { get; set; }
    
    public RinoModel(float moveSpeed, Transform[] patrolPoints)
    {
        MoveSpeed = moveSpeed;
        PatrolPoints = patrolPoints;
    }
}