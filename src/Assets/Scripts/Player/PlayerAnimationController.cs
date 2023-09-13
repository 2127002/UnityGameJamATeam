using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController
{
    private Animator anim;

    public PlayerAnimationController(Animator anim)
    {
        this.anim = anim;
    }

    public void SetAnimationBool(string animationName, bool isActive)
    {
        anim.SetBool(animationName, isActive);
    }
}
