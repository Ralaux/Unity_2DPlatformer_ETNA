using UnityEngine;

public class BeeModel
{
    public float YDistanceToPlayer { get; private set; }

    public float XSpeed { get; private set; }

    public float YSpeed { get; private set; }

    public Transform PlayerTransform { get; private set; }

    public bool isChasing { get; set; }

    public float ChaseDistance { get; private set; }

    public BeeModel(Transform playerTransform, float chaseDistance, float yDistanceToPlayer, float xSpeed, float ySpeed)
    {
        YDistanceToPlayer = yDistanceToPlayer;
        ChaseDistance = chaseDistance;
        PlayerTransform = playerTransform;
        XSpeed = xSpeed;
        YSpeed = ySpeed;
    }
}