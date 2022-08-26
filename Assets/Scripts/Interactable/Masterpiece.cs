using System.Collections;
using UnityEngine;
using ZSerializer;

public class Masterpiece : Collectable
{
    [SerializeField] private Stealable stealable;
    [NonZSerialized] private Sprite empty;

    private float speedDecreaseRate;
    public Vector3 originalPosition;

    private void Awake()
    {
        originalPosition = transform.position;
    }

    protected override void Start()
    {
        base.Start();

        speedDecreaseRate = 1f - (Config.MAX_SPEED_DECREASE_RATE * ((float)stealable.weight / 10));
    }

    protected override void OnCollect()
    {
        int remainingBackpackCapacity = GameManager.instance.GetStolenManager().backpackCapacity - GameManager.instance.GetStolenManager().usedBackpackCapacity;

        if (GameManager.instance.GetHeldMasterpiece())
        {
            Throw();
        }
        else if (stealable.weight <= remainingBackpackCapacity)
        {
            PutInBag();
        }
        else if (stealable.weight < 11)
        {
            if ((stealable.weight == 10 && GameManager.instance.GetStolenManager().spinach) || stealable.weight < 10)
            {
                Grab();
            }
        }
    }

    public Stealable GetStealable()
    {
        return stealable;
    }

    private void Grab()
    {
        collected = true;

        GameManager.instance.SetHeldMasterpiece(this);

        GameManager.instance.GetHeldMasterpiece().gameObject.transform.position = GameManager.instance.GetMasterpieceHoldingPosition().transform.position;
        GameManager.instance.GetHeldMasterpiece().gameObject.transform.parent = GameManager.instance.GetPlayer().transform;

        transform.GetChild(0).gameObject.layer = Config.DEFAULT_LAYER;

        GameManager.instance.GetPlayer().AlterSpeed(speedDecreaseRate);
    }

    private void Throw()
    {
        collected = false;

        Vector2 playerDirection = GameManager.instance.GetPlayer().GetDirection();

        GameManager.instance.GetHeldMasterpiece().transform.position = GameManager.instance.GetPlayer().transform.position
            + (new Vector3(playerDirection.x * boxCollider.size.x, playerDirection.y * boxCollider.size.y, 0));
        GameManager.instance.GetHeldMasterpiece().transform.parent = null;

        transform.GetChild(0).gameObject.layer = Config.BLOCKING_LAYER; 

        GameManager.instance.SetHeldMasterpiece(null);
        GameManager.instance.GetPlayer().ResetToNormalSpeed();
    }

    public void PutInBag()
    {
        collected = true;

        GameManager.instance.GetStolenManager().AddToCarrying(stealable);

        // FOR NOW WE DESTROY IT, LATER WE SHOULD CARRY THIS G.O SO IF WE GET CAUGHT WE DROP THEM
        // OR JUST SAVE THE NAME AND THEN INSTANTIATE THEM WHEN CAUGHT
        Destroy(gameObject);
    }

    public void BackToOriginalPosition()
    {
        if (!collected)
            transform.position = originalPosition;
    }
}
