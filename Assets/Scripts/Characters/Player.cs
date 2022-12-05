using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Mover
{
    private bool isTeleporting = false;

    private void Update()
    {
        if (!GameManager.instance.IsGamePaused())
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            if (movement != Vector2.zero)
            {
                animator.SetFloat("Horizontal", movement.x);
                animator.SetFloat("Vertical", movement.y);

                direction = movement.normalized;
            }

            animator.SetFloat("Speed", movement.sqrMagnitude);
        }
    }

    private void FixedUpdate()
    {
        UpdateMotor(movement, has2DAnimation);
    }

    public bool IsTeleporting()
    {
        return isTeleporting;
    }

    public void SetIsTeleporting(bool value)
    {
        isTeleporting = value; 
    }

    public void ShakeIfHeavy()
    {
        Masterpiece heldMasterpiece = GameManager.instance.GetHeldMasterpiece();

        if (heldMasterpiece)
        {
            int weight = heldMasterpiece.GetStealable().weight;

            if (weight >= 6)
            {
                int shakeAmplitude = weight - 5;

                GameManager.instance.GetCinemachineShake().ShakeCamera(shakeAmplitude, .2f);
            }
        }
    }
}
