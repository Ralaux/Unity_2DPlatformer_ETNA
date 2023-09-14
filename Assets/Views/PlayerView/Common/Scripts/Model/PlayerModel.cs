using UnityEngine;

public class PlayerModel
{
    // ----------- MOVEMENT -----------
    // Editor updatable
    public float MovementSpeedInit { get; set; }
    public float JumpForceInit { get; set; }
    public LayerMask JumpableGround { get; set; }
    public Transform GroundCheck { get; set; }
    public float DashDuration { get; set; }
    public float DashSpeed { get; set; }
    public LayerMask WallLayer { get; set; }
    public Transform WallCheckRight { get; set; }
    public Transform WallCheckLeft { get; set; }
    public float WallJumpingDuration { get; set; }
    public Vector2 WallJumpingPower { get; set; }
    public float FragilePlatformDuration { get; set; }
    // Base movement
    public float Movement { get; set; }
    public float MovementSpeed { get; set; }
    public float JumpForce { get; set; }
    public bool IsFastFalling { get; set; }
    // Dash
    public float DashRate { get; set; }
    public float NextDash { get; set; } 
    public bool IsDashing{ get; set; }
    // WallJump
    public bool IsWallJumping { get; set; }
    public bool IsHanging { get; set; }


    public PlayerModel(
        float movementSpeedInit, float jumpForceInit, LayerMask jumpableGround, Transform groundCheck, float dashDuration,
        float dashSpeed, LayerMask wallLayer, Transform wallCheckRight, Transform wallCheckLeft, float wallJumpingDuration, 
        Vector2 wallJumpingPower, float fragilePlatformDuration
        )
    {
        MovementSpeedInit = movementSpeedInit;
        JumpForceInit = jumpForceInit;
        JumpableGround = jumpableGround;
        GroundCheck = groundCheck;
        DashDuration = dashDuration;
        DashSpeed = dashSpeed;
        WallLayer = wallLayer;
        WallCheckRight = wallCheckRight;
        WallCheckLeft = wallCheckLeft;
        WallJumpingDuration = wallJumpingDuration;
        WallJumpingPower = wallJumpingPower;
        FragilePlatformDuration = fragilePlatformDuration;
        IsDashing = false;
        DashRate = 0.8f;
        IsFastFalling = false;
        IsDashing = false;
        IsHanging = false;
        WallJumpingPower = new Vector2(9f, 4f);
        IsHanging = false;
    }
}