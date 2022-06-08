using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    protected Animator m_Animator;
    public bool IsHit => m_Animator.GetCurrentAnimatorStateInfo(0).IsTag("Hit"); // If hit animation playing is be true else is  be false

    private void Start() => m_Animator ??= GetComponent<Animator>();

    public void PlayRunAnimation(bool run) => m_Animator.SetBool("Run", run && !IsHit);
    public void PlayHitAnimation(bool hit) => m_Animator.SetBool("Hit", hit);


}
