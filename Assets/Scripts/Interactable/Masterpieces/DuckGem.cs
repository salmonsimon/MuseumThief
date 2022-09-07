using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckGem : Masterpiece
{
    [SerializeField] private Animator emergencyAlert;
    [SerializeField] private GameObject emergencyGuardContainers;

    private bool hasBeenPickedUp;

    protected override void Awake()
    {
        base.Awake();
        hasBeenPickedUp = false;
    }

    protected override void Grab()
    {
        base.Grab();

        if (!hasBeenPickedUp)
            HasBeenPickedUp();
    }

    public override void PutInBag()
    {
        base.PutInBag();

        if (!hasBeenPickedUp)
            HasBeenPickedUp();
    }

    private void HasBeenPickedUp()
    {
        hasBeenPickedUp = true;

        GameManager.instance.SetOnEmergency(true);

        emergencyAlert.SetTrigger("Emergency");
        emergencyGuardContainers.SetActive(true);
    }
}

