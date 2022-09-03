using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : Portal
{
    [SerializeField] private int floorNumber;

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                int ropeCount = GameManager.instance.GetStolenManager().rope;

                if (ropeCount < floorNumber)
                {
                    GameManager.instance.GetSoundManager().PlaySound(Config.HOVER_SFX);
                    GameManager.instance.ShowText("Not enough rope to descend", 24, Color.white, new Vector3(GameManager.instance.GetPlayer().transform.position.x, GameManager.instance.GetPlayer().transform.position.y + 0.16f, 0), Vector3.up * 40, 1f);
                }
                else
                {
                    BackToStudio();
                }
            }
        }
    }
}
