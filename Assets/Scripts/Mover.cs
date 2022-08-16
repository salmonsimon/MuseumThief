using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] protected bool has2DAnimation;

    protected BoxCollider2D boxCollider;
    protected Animator animator;
    protected Vector2 movement;
    protected RaycastHit2D hit;
    protected float xSpeed = 1f;
    protected float ySpeed = 0.75f;

    private Vector3 originalSize;

    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        originalSize = transform.localScale;
    }

    private void updateSprite(Vector2 movement)
    {
        animator.SetFloat("Speed", movement.sqrMagnitude);

        if (movement.x > 0)
        {
            transform.localScale = originalSize;
        }
        else if (movement.x < 0)
        {
            transform.localScale = new Vector3(originalSize.x * -1f, originalSize.y * 1f, originalSize.z * 1f);
        }
    }

    protected virtual void UpdateMotor(Vector2 input, bool has2DAnimation)
    {
        movement = new Vector2(input.x * xSpeed, input.y * ySpeed);

        if (!has2DAnimation)
        {
            updateSprite(movement);
        }

        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, movement.y),
                                Mathf.Abs(movement.y * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));

        if (hit.collider == null)
        {
            transform.Translate(0, movement.y * Time.deltaTime, 0);
        }

        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(movement.x, 0),
                                Mathf.Abs(movement.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));

        if (hit.collider == null)
        {
            transform.Translate(movement.x * Time.deltaTime, 0, 0);
        }
    }
}
