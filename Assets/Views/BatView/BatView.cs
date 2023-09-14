using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatView : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Cette fonction peut être utilisée pour mettre à jour l'animation de la chauve-souris en fonction de l'état.
    public void UpdateAnimationState(BatMovementState bat_state)
    {
        anim.SetInteger("bat_state", (int)bat_state);
    }

    // D'autres fonctions liées à l'affichage de la chauve-souris peuvent être placées ici.
}
