using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RinoView: MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("isRunning", true);
    }
}
