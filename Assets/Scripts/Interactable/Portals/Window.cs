using UnityEngine;

public class Window : Portal
{
    [SerializeField] private int floorNumber;

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.CompareTag("Player") & !GameManager.instance.GetPlayer().IsTeleporting())
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                int ropeCount = GameManager.instance.GetStolenManager().rope;

                if (GameManager.instance.GetOnEmergency())
                {
                    GameManager.instance.GetSoundManager().PlaySound(Config.DENIED_SFX);
                    GameManager.instance.ShowText("Seems to be blocked", 24, Color.white, new Vector3(GameManager.instance.GetPlayer().transform.position.x, GameManager.instance.GetPlayer().transform.position.y + 0.16f, 0), Vector3.up * 40, 1f);
                }
                else
                {
                    if (ropeCount < floorNumber)
                    {
                        GameManager.instance.GetSoundManager().PlaySound(Config.DENIED_SFX);
                        GameManager.instance.ShowText("Not enough rope to descend", 24, Color.white, new Vector3(GameManager.instance.GetPlayer().transform.position.x, GameManager.instance.GetPlayer().transform.position.y + 0.16f, 0), Vector3.up * 40, 1f);
                    }
                    else
                    {
                        UsePortal();
                    }
                }
            }
        }
    }
}
