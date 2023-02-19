using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed, _jumpForce;
    [SerializeField] private Rigidbody2D _rb;
    private bool _isFacingRight = true;

    [SerializeField] private Transform _groundCheck, _wallCheck;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _coyoteTime, _maxJumpTime;
    private float _offGroundTime, _jumpTime;

    [SerializeField] private float _gravityScale;

    GameManager GameManager;

    private void Awake()
    {
        GameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        if(Input.GetButton("Jump"))
        {
            if(_isGrounded() || _offGroundTime > 0 || _jumpTime > 0)
            {
                _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
                _offGroundTime = 0;
                _jumpTime -= Time.deltaTime;
            }
        }

        if(Input.GetButtonUp("Jump"))
        {
            _jumpTime = 0;
        }

        _offGroundTime -= Time.deltaTime;

        if(_isGrounded())
        {
            _offGroundTime = _coyoteTime;
            _jumpTime = _maxJumpTime;
        }

        if (isWallClinging())
        {
            _rb.gravityScale = _gravityScale / 4;
            if (_rb.velocity.y > 0) _rb.velocity = new Vector2(_rb.velocity.x, 0);
        }
        else _rb.gravityScale = _gravityScale;

        Flip();
    }

    private void FixedUpdate()
    {
        _rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * _moveSpeed, _rb.velocity.y);
    }

    private bool _isGrounded()
    {
        return Physics2D.OverlapCircle(_groundCheck.position, 0.2f, _groundLayer);
    }

    public bool isWallClinging()
    {
        if (Physics2D.OverlapCircle(_wallCheck.position, 0.2f, _groundLayer) && Input.GetAxisRaw("Horizontal") == transform.localScale.x) return true;
        else if (_isGrounded()) return false;
        else return false;
    }

    private void Flip()
    {
        if(_isFacingRight && Input.GetAxisRaw("Horizontal") < 0f || !_isFacingRight && Input.GetAxis("Horizontal") > 0f)
        {
            _isFacingRight = !_isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Win") && GameManager.levelBeaten)
        {
            GameManager.ChangeScene(GameManager.levelNumber + 1);
        }
    }
}
