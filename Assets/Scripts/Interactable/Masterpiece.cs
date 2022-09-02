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
    private AudioSource audioSource;
    private AudioClip grabSFX, throwSFX, putInBagSFX, tooHeavySFX;

    private void Awake()
    {
        originalPosition = transform.position;
        originalParent = transform.parent;

        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

        grabSFX = Resources.Load<AudioClip>("Audio/Sound FX/Grab");
        throwSFX = Resources.Load<AudioClip>("Audio/Sound FX/Drop");
        putInBagSFX = Resources.Load<AudioClip>("Audio/Sound FX/Store");
        tooHeavySFX = Resources.Load<AudioClip>("Audio/Sound FX/Denied");
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
                PlaySound(tooHeavySFX);
                GameManager.instance.ShowText("Too heavy to lift", 24, Color.white, new Vector3(GameManager.instance.GetPlayer().transform.position.x, GameManager.instance.GetPlayer().transform.position.y + 0.16f, 0), Vector3.up * 40, 1f);
            }
        }
        else if (stealable.weight > 10)
        {
            PlaySound(tooHeavySFX);
            GameManager.instance.ShowText("Not even hulk can lift this", 24, Color.white, new Vector3(GameManager.instance.GetPlayer().transform.position.x, GameManager.instance.GetPlayer().transform.position.y + 0.16f, 0), Vector3.up * 40, 1f);
        }
    }

    public Stealable GetStealable()
    {
        return stealable;
    }

    private void Grab()
    {
        PlaySound(grabSFX);

        collected = true;

        GameManager.instance.SetHeldMasterpiece(this);

        GameManager.instance.GetHeldMasterpiece().gameObject.transform.parent = GameManager.instance.GetPlayer().transform;
        GameManager.instance.GetHeldMasterpiece().gameObject.transform.position = GameManager.instance.GetMasterpieceHoldingPosition().transform.position;

        transform.GetChild(0).gameObject.layer = Config.DEFAULT_LAYER;

        GameManager.instance.GetPlayer().AlterSpeed(newSpeedRate);
    }

    public void Throw()
    {
        PlaySound(throwSFX);

        collected = false;
        Vector3 scale = transform.localScale;

        Vector2 playerDirection = GameManager.instance.GetPlayer().GetDirection();

        GameManager.instance.GetHeldMasterpiece().transform.parent = originalParent;
        GameManager.instance.GetHeldMasterpiece().transform.position = GameManager.instance.GetPlayer().transform.position
            + (new Vector3(playerDirection.x * boxCollider.size.x * scale.x, playerDirection.y * boxCollider.size.y * scale.y, 0));

        transform.GetChild(0).gameObject.layer = Config.BLOCKING_LAYER; 

        GameManager.instance.SetHeldMasterpiece(null);
        GameManager.instance.GetPlayer().ResetToNormalSpeed();
    }

    public void PutInBag()
    {
        PlaySound(putInBagSFX);

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

    private void PlaySound(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }
}
