using UnityEngine;

public class RinoController : MonoBehaviour
{
    private RinoModel model;
    private RinoView view;

    [SerializeField] private Transform[] patrolPoints;

    private void Start()
    {
        model = new RinoModel(4f, patrolPoints);
        view = GetComponent<RinoView>();
    }

    public void Update()
    {
        if (model.patrolDestination == 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, model.PatrolPoints[0].position, model.MoveSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, model.PatrolPoints[0].position) < .2f)
            {
                Flip();
                model.patrolDestination = 1;
            }
        }

        if (model.patrolDestination == 1)
        {
            transform.position = Vector2.MoveTowards(transform.position, model.PatrolPoints[1].position, model.MoveSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, model.PatrolPoints[1].position) < .2f)
            {
                Flip();
                model.patrolDestination = 0;
            }
        }
    }

    private void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}