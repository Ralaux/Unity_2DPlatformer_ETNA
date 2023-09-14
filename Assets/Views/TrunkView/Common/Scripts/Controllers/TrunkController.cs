using UnityEngine;

public class TrunkController : MonoBehaviour
{
    private TrunkModel model;
    private TrunkView view;
    private float timer;

    private bool patrol = true;

    [SerializeField] private Bullet_trunk Bt;

    [SerializeField] private Transform[] patrolPoints;

    [SerializeField] private float moveSpeed;

    [SerializeField] private Transform playerTransform;

    [SerializeField] private float awareDistance;

    [SerializeField] private bool isFacingLeft;

    [SerializeField] private int patrolDestination;

    private GameObject trunk_projectileInst;



    [SerializeField] private GameObject Bullet_trunk;
    [SerializeField] private Transform trunk_projectileSpawnPoint;

    private void Start()
    {
        model = new TrunkModel( playerTransform, awareDistance, isFacingLeft, moveSpeed);
        view = GetComponent<TrunkView>();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (model.PlayerTransform == null)
        {
            return;
        }

        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        if(patrol)
        {
            if(patrolDestination == 0)
            {
                transform.position = Vector2.MoveTowards(transform.position, patrolPoints[0].position, model.MoveSpeed * Time.deltaTime);
                if(Vector2.Distance(transform.position, patrolPoints[0].position) < .2f)
                {
                    transform.localScale = new Vector3(-3, 3, 3);
                    patrolDestination = 1;
                    model.IsFacingLeft = false;
                }
            }
            if(patrolDestination == 1)
            {
                transform.position = Vector2.MoveTowards(transform.position, patrolPoints[1].position, model.MoveSpeed * Time.deltaTime);
                if (Vector2.Distance(transform.position, patrolPoints[1].position) < .2f)
                {
                    transform.localScale = new Vector3(3, 3, 3);
                    patrolDestination = 0;
                    model.IsFacingLeft = true;
                }
            }
        }

        if(Vector2.Distance(transform.position, model.PlayerTransform.position) < model.AwareDistance)
        {
            model.Aware = true;
            patrol = false;
        }

        if(Vector2.Distance(transform.position, model.PlayerTransform.position) > model.AwareDistance)
        {
            model.Aware = false;
            patrol = true;
            if(patrolDestination == 0 && model.IsFacingLeft == false)
            {
                transform.localScale = new Vector3(3, 3, 3);
                model.IsFacingLeft = true;
            }
            if (patrolDestination == 1 && model.IsFacingLeft == true)
            {
                transform.localScale = new Vector3(-3, 3, 3);
                model.IsFacingLeft = false;
            }
        }
        if(model.Aware)
        {
            Flip();
            if(timer > 1)
            {
                timer = 0;
                Attack();
            }
        }

        view.UpdateAnimationState(getTrunkState());
    }

    private void Flip()
    {
        if (transform.position.x > model.PlayerTransform.position.x)
        {
            transform.localScale = new Vector3(3, 3, 3);
            model.IsFacingLeft = true;
            Bt.facingLeft = true;
        }
        if(transform.position.x < model.PlayerTransform.position.x)
        {
            transform.localScale = new Vector3(-3, 3, 3);
            model.IsFacingLeft = false;
            Bt.facingLeft = false;
        }
    }

    private void Attack()
    {
        trunk_projectileInst = Instantiate(Bullet_trunk, trunk_projectileSpawnPoint.position, trunk_projectileSpawnPoint.rotation);
    }

    private TrunkMovementState getTrunkState()
    {
        if(!model.Aware)
        {
            return TrunkMovementState.Run;
        }
        else if(model.Aware)
        {
            return TrunkMovementState.Attack;
        }
        else
        {
            return TrunkMovementState.Idle;
        }
    }
}
