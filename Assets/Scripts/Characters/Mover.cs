using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] protected bool has2DAnimation;

    protected BoxCollider2D boxCollider;
    protected Animator animator;
    protected Vector2 movement;
    protected Vector2 direction;
    protected RaycastHit2D hit;

    public float xSpeed = 1f;
    public float ySpeed = 1f;
    protected float normalXSpeed = 1f;
    protected float normalYSpeed = 1f;

    private Vector3 originalSize;

    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        originalSize = transform.localScale;
    }

    private void UpdateSprite(Vector2 movement)
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
        movement = new Vector2(input.x, input.y).normalized;

        if (!has2DAnimation)
        {
            UpdateSprite(movement);
        }

        float movementX = movement.x * xSpeed;
        float movementY = movement.y * ySpeed;

        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, movementY),
                                Mathf.Abs(movementY * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));

        if (hit.collider == null)
        {
            transform.Translate(0, movementY * Time.deltaTime, 0);
        }

        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(movementX, 0),
                                Mathf.Abs(movementX * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));

        if (hit.collider == null)
        {
            transform.Translate(movementX * Time.deltaTime, 0, 0);
        }
    }

    public Vector2 GetDirection()
    {
        return direction;
    }

    public void ResetToNormalSpeed()
    {
        xSpeed = normalXSpeed;
        ySpeed = normalYSpeed;

        GetComponent<Animator>().speed = 1;
    }

    public void AlterSpeed(float percentage)
    {
        xSpeed *= percentage;
        ySpeed *= percentage;

        GetComponent<Animator>().speed = percentage;
    }
}
