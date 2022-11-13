using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public static bool IsGrounded;

    private static Animator _animator;
    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _animator.SetFloat("AirSpeedY", _rigidbody.velocity.y);
        _animator.SetBool("Grounded", IsGrounded);
    }

    public static void Idle()
    {
        _animator.SetInteger("AnimState", 0);
    }

    public static void Walk()
    {
        _animator.SetInteger("AnimState", 1);
    }

    public static void Jump()
    {
        _animator.SetTrigger("Jump");
    }
}
