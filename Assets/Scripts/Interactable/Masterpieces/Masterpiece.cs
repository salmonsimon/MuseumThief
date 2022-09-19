using System.Collections;
using UnityEngine;
using ZSerializer;

public class Masterpiece : Collectable
{
    [SerializeField] private Stealable stealable;

    private float newSpeedRate;
    private Vector3 originalPosition;
    private Transform originalParent;

    private SpriteRenderer spriteRenderer;

    protected virtual void Awake()
    {
        originalPosition = transform.position;
        originalParent = transform.parent;

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void Start()
    {
        base.Start();

        newSpeedRate = 1f - (Config.MAX_SPEED_DECREASE_RATE * ((float)stealable.weight / 10));
    }

    private void OnBecameInvisible()
    {
        spriteRenderer.ResetBounds();
    }

    protected override void OnCollect()
    {
        int remainingBackpackCapacity = GameManager.instance.GetStolenManager().GetCarryingCapacity() - GameManager.instance.GetStolenManager().GetUsedCarryingCapacity();

        
        if (GameManager.instance.GetHeldMasterpiece())
        {
            Throw();
        }
        else if (stealable.weight <= remainingBackpackCapacity)
        {
            PutInBag();
            GameManager.instance.ShowText("Masterpiece picked up", 24, Color.white, new Vector3(GameManager.instance.GetPlayer().transform.position.x, GameManager.instance.GetPlayer().transform.position.y + 0.16f, 0), Vector3.up * 40, 1f);
        }
        else if (stealable.weight < 11)
        {
            if ((stealable.weight == 10 && GameManager.instance.GetStolenManager().protein) || stealable.weight < 10)
            {
                Grab();
            }
            else if (stealable.weight == 10 && !GameManager.instance.GetStolenManager().protein)
            {
                GameManager.instance.GetSoundManager().PlaySound(Config.DENIED_SFX);
                GameManager.instance.ShowText("Too heavy to lift", 24, Color.white, new Vector3(GameManager.instance.GetPlayer().transform.position.x, GameManager.instance.GetPlayer().transform.position.y + 0.16f, 0), Vector3.up * 40, 1f);
            }
        }
        else if (stealable.weight > 10)
        {
            GameManager.instance.GetSoundManager().PlaySound(Config.DENIED_SFX);
            GameManager.instance.ShowText("Not even hulk can lift this", 24, Color.white, new Vector3(GameManager.instance.GetPlayer().transform.position.x, GameManager.instance.GetPlayer().transform.position.y + 0.16f, 0), Vector3.up * 40, 1f);
        }

    }

    public Stealable GetStealable()
    {
        return stealable;
    }

    protected virtual void Grab()
    {
        GameManager.instance.GetSoundManager().PlaySound(Config.GRAB_SFX);

        collected = true;

        GameManager.instance.SetHeldMasterpiece(this);

        GameManager.instance.GetHeldMasterpiece().gameObject.transform.parent = GameManager.instance.GetPlayer().transform;
        GameManager.instance.GetHeldMasterpiece().gameObject.transform.position = GameManager.instance.GetMasterpieceHoldingPosition().transform.position;

        transform.GetChild(0).gameObject.layer = Config.DEFAULT_LAYER;

        GameManager.instance.GetPlayer().AlterSpeed(newSpeedRate);

        GameManager.instance.GetPathfinderGraphUpdater().UpdatePathfinderGraphs();
    }

    public void Throw()
    {
        Vector2 playerDirection = GameManager.instance.GetPlayer().GetDirection();
        Vector3 playerPosition = GameManager.instance.GetPlayer().transform.position;
        BoxCollider2D playerCollider = GameManager.instance.GetPlayer().GetComponent<BoxCollider2D>();

        Vector3 scale = transform.localScale;

        float masterpieceScaledSizeX = boxCollider.size.x * scale.x;
        float masterpieceScaledSizeY = boxCollider.size.y * scale.y;

        float masterpieceBlockingScaledSizeX = transform.GetChild(0).gameObject.GetComponent<BoxCollider2D>().size.x * scale.x;
        float masterpieceBlockingScaledSizeY = transform.GetChild(0).gameObject.GetComponent<BoxCollider2D>().size.y * scale.y;

        Vector2 masterpieceBlockingScaledSize = new Vector2(masterpieceBlockingScaledSizeX, masterpieceBlockingScaledSizeY);

        Vector3 afterThrowPosition = GameManager.instance.GetPlayer().transform.position
                + (new Vector3(playerDirection.x * masterpieceScaledSizeX, playerDirection.y * masterpieceScaledSizeY, 0));

        RaycastHit2D hit = Physics2D.BoxCast(afterThrowPosition, masterpieceBlockingScaledSize, 0, new Vector2(playerDirection.x, playerDirection.y),
                                Mathf.Abs(playerDirection.magnitude * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));

        

        if (hit.collider == null)
        {
            GameManager.instance.GetSoundManager().PlaySound(Config.THROW_SFX);

            collected = false;

            GameManager.instance.GetHeldMasterpiece().transform.parent = originalParent;
            GameManager.instance.GetHeldMasterpiece().transform.position = afterThrowPosition;

            transform.GetChild(0).gameObject.layer = Config.BLOCKING_LAYER;

            GameManager.instance.SetHeldMasterpiece(null);
            GameManager.instance.GetPlayer().ResetToNormalSpeed();

            GameManager.instance.GetPathfinderGraphUpdater().UpdatePathfinderGraphs();
        }
        else
        {
            GameManager.instance.GetSoundManager().PlaySound(Config.DENIED_SFX);
            GameManager.instance.ShowText("Can't throw there!", 24, Color.white, new Vector3(GameManager.instance.GetPlayer().transform.position.x, GameManager.instance.GetPlayer().transform.position.y + 0.16f, 0), Vector3.up * 40, 1f);
        }
    }

    public virtual void PutInBag()
    {
        GameManager.instance.GetSoundManager().PlaySound(Config.STORE_SFX);

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
