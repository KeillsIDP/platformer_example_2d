using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed;
    [SerializeField]
    private float _jumpForce;
    [SerializeField]
    private Transform _groundPoint;
    [SerializeField]
    private LayerMask _ground;

    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;

    private void Start() 
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (CheckForGround() && Input.GetButtonDown("Jump"))
            Jump();
    }

    private void FixedUpdate() 
    {
        Move();
    }

    private void Move()
    {
        var inpX = Input.GetAxis("Horizontal");

        if(inpX==0)
        {
            PlayerAnimationController.Idle();
            return;
        }

        if (inpX < 0)
            _spriteRenderer.flipX = true;
        else if (inpX > 0)
            _spriteRenderer.flipX = false;

        var dir = transform.right * _moveSpeed * inpX;
        dir.y = _rigidbody.velocity.y;

        _rigidbody.velocity = dir;
        PlayerAnimationController.Walk();
    }

    private void Jump()
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);
        PlayerAnimationController.Jump();
    }


    private bool CheckForGround()
    {
        bool res = Physics2D.BoxCast(_groundPoint.position, new Vector2(0.45f, 0.1f), 0, Vector2.down, .1f, _ground);

        PlayerAnimationController.IsGrounded = res;
        return res;
    }
    
}
