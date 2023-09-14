using UnityEngine;

public class TrunkView : MonoBehaviour
{
    private Animator anim;
    private SpriteRenderer sprite;

    private void Start()
    {
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    public void UpdateAnimationState(TrunkMovementState trunk_state)
    {
        anim.SetInteger("trunk_state", (int)trunk_state);
    }

    // Autres fonctions liées à l'affichage du personnage
}
