using UnityEngine;

public class BeeController : MonoBehaviour
{
    private BeeModel model;
    private BeeView view;

    [SerializeField] private Transform playerTransform;
    [SerializeField] public float yDistanceToPlayer;
    [SerializeField] public float xSpeed;
    [SerializeField] public float ySpeed;
    [SerializeField] public float chaseDistance;

    public GameObject bullet;
    public Transform bee_projectileSpawnPoint;
    private GameObject bee_projectileInst;
    private float timer;

    private void Start()
    {
        model = new BeeModel(playerTransform, yDistanceToPlayer, chaseDistance, ySpeed, xSpeed);
        view = GetComponent<BeeView>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (Vector2.Distance(transform.position, playerTransform.position) < chaseDistance)
        {
            model.isChasing = true;
        }
        if (Vector2.Distance(transform.position, playerTransform.position) > chaseDistance)
        {
            model.isChasing = false;
        }
        if (model.isChasing)
        {
            Chase();
            if (timer > 1)
            {
                timer = 0;
                Attack();
            }
        }
        view.UpdateAnimationState(GetBeeState());
    }

    private void Chase()
    {
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(playerTransform.position.x, playerTransform.position.y), xSpeed * Time.deltaTime);

        if (transform.position.y - playerTransform.position.y < yDistanceToPlayer)
        {
            transform.position += Vector3.up * ySpeed * Time.deltaTime;
        }
        else if (transform.position.y - playerTransform.position.y > yDistanceToPlayer)
        {
            transform.position += Vector3.down * ySpeed * Time.deltaTime;
        }
        else
        {
            transform.position = transform.position;
        }

        if (transform.position.x > playerTransform.position.x)
        {
            transform.localScale = new Vector3(3, 3, 3);
        }
        if (transform.position.x < playerTransform.position.x)
        {
            transform.localScale = new Vector3(-3, 3, 3);
        }
    }

    private void Attack()
    {
        bee_projectileInst = Instantiate(bullet, bee_projectileSpawnPoint.position, transform.rotation);
    }

    private BeeMovementState GetBeeState()
    {
       if (!model.isChasing)
        {
            return BeeMovementState.Idle;
        }
        else
        {
            return BeeMovementState.Attack;
        }
    }
}
