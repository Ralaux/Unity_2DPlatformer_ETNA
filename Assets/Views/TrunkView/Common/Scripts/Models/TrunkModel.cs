using UnityEngine;

public class TrunkModel
{
    public float MoveSpeed { get; set; }
    public bool Aware { get; set; }
    public bool Patrol { get; set; }
    public float AwareDistance { get; set; }
    public bool IsFacingLeft { get; set; }
    public Transform PlayerTransform { get; set; }

    public TrunkModel(Transform playerTransform, float awareDistance, bool isFacingLeft, float moveSpeed = 3f)
    {
        MoveSpeed = moveSpeed;
        PlayerTransform = playerTransform;
        AwareDistance = awareDistance;
        IsFacingLeft = isFacingLeft;
    }
}
