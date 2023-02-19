using UnityEngine;
using UnityEngine.Animations;

public class PlayerAnimation : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Animator _animator;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (_rb.velocity.x != 0f) _animator.SetBool("isMoving", true); else _animator.SetBool("isMoving", false);
        if (_rb.velocity.y > 0f) _animator.SetBool("isJumping", true); else _animator.SetBool("isJumping", false);
        if (_rb.velocity.y < 0f) _animator.SetBool("isFalling", true); else _animator.SetBool("isFalling", false);
        if (GetComponent<PlayerController>().isWallClinging()) _animator.SetBool("isWallClinging", true); else _animator.SetBool("isWallClinging", false);
    }
}
