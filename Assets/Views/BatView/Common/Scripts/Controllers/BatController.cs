using UnityEngine;

public class BatController : MonoBehaviour
{
    private BatModel model;
    private BatView view;

    [SerializeField] private Transform startingPoint;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float chaseDistance;
    [SerializeField] private float awareDistance;

    private void Start()
    {
        model = new BatModel(3f, startingPoint, playerTransform, chaseDistance, awareDistance);
        view = GetComponent<BatView>();
    }

    private void Update()
    {
        // Gérer les entrées utilisateur, s'il y en a.
        // Mettre à jour le modèle en fonction des entrées.
        // Exemple :
        if (Vector2.Distance(transform.position, model.PlayerTransform.position) < model.AwareDistance)
        {
            model.Aware = true;
        }
        if (Vector2.Distance(transform.position, model.PlayerTransform.position) < model.ChaseDistance)
        {
            model.IsChasing = true;
            model.IsAtStart = false;
        }
        if (Vector2.Distance(transform.position, model.PlayerTransform.position) > model.ChaseDistance)
        {
            model.IsChasing = false;
        }
        if (Vector2.Distance(transform.position, model.PlayerTransform.position) > model.AwareDistance)
        {
            model.Aware = false;
        }
        if (Vector2.Distance(transform.position, model.StartingPoint.position) < 0.2f)
        {
            model.IsAtStart = true;
        }
        if(model.IsChasing) {
            Chase();
        } else {
            ReturnStartPoint();
        }

        // Mettre à jour la vue en fonction du modèle.
        view.UpdateAnimationState(GetBatState());
    }

    private void Chase()
    {
        transform.position = Vector3.MoveTowards(transform.position, model.PlayerTransform.position, model.MoveSpeed * Time.deltaTime);
        if(transform.position.x > model.PlayerTransform.position.x)
        {
            transform.localScale = new Vector3(3, 3, 3);
        }
        if (transform.position.x < model.PlayerTransform.position.x)
        {
            transform.localScale = new Vector3(-3, 3, 3);
        }
    }

    private void ReturnStartPoint()
    {
        transform.position = Vector2.MoveTowards(transform.position, model.StartingPoint.position, model.MoveSpeed * Time.deltaTime);
        if (transform.position.x > model.StartingPoint.position.x)
        {
            transform.localScale = new Vector3(3, 3, 3);
        }
        if (transform.position.x < model.StartingPoint.position.x)
        {
            transform.localScale = new Vector3(-3, 3, 3);
        }
    }

    private BatMovementState GetBatState()
    {
        if (!model.IsAtStart && model.Aware && model.IsChasing)
        {
            return BatMovementState.Flying;
        }
        else if (!model.IsAtStart && !model.Aware && !model.IsChasing)
        {
            return BatMovementState.Flying;
        }
        else if (model.Aware && !model.IsChasing && model.IsAtStart)
        {
            return BatMovementState.Aware;
        }
        else if (!model.Aware && model.IsAtStart && !model.IsChasing)
        {
            return BatMovementState.GoBack;
        }
        else
        {
            return BatMovementState.Idle;
        }
    }

}
