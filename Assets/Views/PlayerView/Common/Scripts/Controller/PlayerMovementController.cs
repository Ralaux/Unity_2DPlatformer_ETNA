using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;


public class PlayerMovementController : MonoBehaviour
{
    public PlayerModel model;
    private PlayerView view;

    // -------------  Base Movement  -------------
    [SerializeField] public float MovementSpeedInit; 
    [SerializeField] public float JumpForceInit; 
    [SerializeField] private LayerMask JumpableGround;
    [SerializeField] private Transform GroundCheck;

    // -----------------  Dash  -----------------
    [SerializeField] private float DashDuration;
    [SerializeField] private float DashSpeed;

    // ---------------  WallJump  ---------------
    [SerializeField] private LayerMask WallLayer;
    [SerializeField] private Transform WallCheckRight;
    [SerializeField] private Transform WallCheckLeft;
    [SerializeField] private float WallJumpingDuration;
    [SerializeField] private Vector2 WallJumpingPower = new Vector2(8f, 16f);

    // -----------  Fragile Platform  -----------
    [SerializeField] private float FragilePlatformDuration;

    // --------------  Components  --------------
    private Rigidbody2D _rigidbody;
    private Animator anim;
    private SpriteRenderer sprite;
    private BoxCollider2D coll;
    private PlayerLifeController pl;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
        pl = GetComponent<PlayerLifeController>();
        model = new PlayerModel(
            MovementSpeedInit, JumpForceInit, JumpableGround, GroundCheck, DashDuration, DashSpeed, WallLayer, WallCheckRight,
            WallCheckLeft, WallJumpingDuration, WallJumpingPower, FragilePlatformDuration
            );
        view = GetComponent<PlayerView>();
    }

    private void Update()
    {
        MovementUpdate();
        view.UpdateAnimationState(GetPlayerState());
    }

    private void MovementUpdate(){
        if (!model.IsWallJumping && !model.IsDashing && !MenuScript.gameIsPaused) {
            if (model.IsHanging && Input.GetButtonDown("Jump") && !IsGrounded()) {
                WallJump();
            }

            else if (Input.GetButtonDown("Jump") && IsGrounded()) {
                _rigidbody.AddForce(new Vector2(0, model.JumpForce), ForceMode2D.Impulse);
            }

            else if (Input.GetAxisRaw("Vertical") == -1 && !IsGrounded() && !model.IsFastFalling) {
                _rigidbody.AddForce(new Vector2(0, -model.JumpForce), ForceMode2D.Impulse);
                model.IsFastFalling = true;
            }

            if (((IsWalledLeft() && Input.GetAxisRaw("Horizontal") == -1) || (IsWalledRight() && Input.GetAxisRaw("Horizontal") == 1)) && !IsGrounded() && !model.IsWallJumping) {
                _rigidbody.velocity = new Vector2(0, 0);
                model.IsHanging = true;
                model.IsFastFalling = false;
                StartCoroutine(FreezeUntilInput());
            }

            if (Input.GetKeyDown(KeyCode.C)) {
                Dash();
            }

            else if (!model.IsHanging){
                model.Movement = Input.GetAxisRaw("Horizontal");
                if (model.Movement != 0)
                    _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
                transform.position += new Vector3(model.Movement, 0, 0) * Time.deltaTime * model.MovementSpeed;
            }
        }      
    }

    private IEnumerator FreezeUntilInput() {
        _rigidbody.gravityScale = 0.0f;
        if (IsWalledLeft()){
            sprite.flipX = false;
        }
        else if (IsWalledRight()){
            sprite.flipX = true;
        }
        yield return new WaitForSeconds(0.5f);
        yield return WaitForKeyPress(); 
        _rigidbody.gravityScale = 1.0f;
        model.IsHanging = false;
    }

    private IEnumerator WaitForKeyPress()
    {
        bool done = false;
        while(!done) 
        {
            if(Input.anyKey || model.IsWallJumping || (!IsWalledLeft() && !IsWalledRight()))
            {
                done = true;
                model.IsHanging = false;
            }
            yield return null;
        }
    }

    private void WallJump() {

        model.IsWallJumping = true;
        _rigidbody.velocity = new Vector2(0f, _rigidbody.velocity.y);
        float direction = -1f;
        if (sprite.flipX) 
            direction = 1f;
        _rigidbody.AddForce(new Vector2(direction * model.WallJumpingPower.x, model.WallJumpingPower.y), ForceMode2D.Impulse);
        sprite.flipX = !sprite.flipX;
        StartCoroutine(StopWallJumping(direction));
    }

    private IEnumerator StopWallJumping(float direction) {
        yield return new WaitForSeconds(model.WallJumpingDuration);
        model.IsWallJumping = false;
        _rigidbody.gravityScale = 1.0f;
    }

    private void Dash() {
        if(Time.time > model.NextDash)
        {
            model.NextDash = Time.time + model.DashRate;
            model.IsDashing = true;
            float dir = 1f;
            if (sprite.flipX) 
                dir = -1f;
            _rigidbody.velocity = Vector2.zero;
            _rigidbody.velocity = new Vector2(model.DashSpeed * dir, 0f);
            StartCoroutine(StopDashing());
        }
    }

    private IEnumerator StopDashing() {
        Color ColorToAdjust =  sprite.material.color;
        ColorToAdjust.a = 0.5f;
        sprite.material.color = ColorToAdjust;
        StartCoroutine(pl.BecomeTemporarilyInvincible(model.DashDuration));
        yield return new WaitForSeconds(model.DashDuration);
        ColorToAdjust.a = 1f;
        sprite.material.color = ColorToAdjust;
        model.IsDashing = false;
        _rigidbody.velocity = Vector2.zero;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Sticky Ground")) {
            model.MovementSpeed = model.MovementSpeedInit / 2;
            model.JumpForce = model.JumpForceInit / 2;
            _rigidbody.velocity = Vector2.zero;
        }
        if (collision.gameObject.CompareTag("Wall")) {
            model.MovementSpeed = model.MovementSpeedInit;
            model.JumpForce = model.JumpForceInit;
            _rigidbody.velocity = Vector2.zero;
        }
        if (collision.gameObject.CompareTag("Fragile Platform")) {
            StartCoroutine(DestroyPlatform(collision.gameObject));
        }
    }

    private IEnumerator DestroyPlatform(GameObject platform) {
        yield return new WaitForSeconds(model.FragilePlatformDuration/2);
        Color ColorToAdjust =  platform.GetComponent<Renderer>().material.color;
        ColorToAdjust.a = 0.5f;
        platform.GetComponent<Renderer>().material.color = ColorToAdjust;
        yield return new WaitForSeconds(model.FragilePlatformDuration/2);
        Destroy(platform);
    }

    private bool IsGrounded() {
        if (Physics2D.OverlapCircle(model.GroundCheck.position, 0.2f, model.JumpableGround)) {
            model.IsFastFalling = false;
            return true;
        }
        return false;
    }

    private bool IsWalledRight() {
        return Physics2D.OverlapCircle(model.WallCheckRight.position, 0.2f, model.WallLayer);
    }

    private bool IsWalledLeft() {
        return Physics2D.OverlapCircle(model.WallCheckLeft.position, 0.2f, model.WallLayer);
    }

    private PlayerMovementState GetPlayerState(){

        
        
        if (IsWalledLeft() || IsWalledRight()) {
            return PlayerMovementState.hanging;
        }
        else if (_rigidbody.velocity.y > 0.1f) {
                if (model.Movement > 0f) {
                sprite.flipX = false;
            }
            else if (model.Movement < 0f) {
                sprite.flipX = true;
            }
            return PlayerMovementState.jumping;
        }
        else if (_rigidbody.velocity.y < -0.1f) {
            if (model.Movement > 0f) {
                sprite.flipX = false;
            }
            else if (model.Movement < 0f) {
                sprite.flipX = true;
            }
            return PlayerMovementState.falling;
        }
        else if (model.Movement > 0f) {
            sprite.flipX = false;
            return PlayerMovementState.running;
        }
        else if (model.Movement < 0f) {
            sprite.flipX = true;
            return PlayerMovementState.running;
        }
        else {
            return PlayerMovementState.idle;
        }
    }

}
