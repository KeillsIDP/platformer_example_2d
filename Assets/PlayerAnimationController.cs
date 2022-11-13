using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        
    }

    public static void Idle()
    {

    }

    public static void Walk()
    {

    }

    public static void Jump()
    {

    }
}
