﻿using System.Collections;
using UnityEngine;

public class LightningMovement : BaseHazard
{
    private GameObject lightningWarning;
    private GameObject lightningHazard;

    protected override void HazardAwake()
    {
        lightningWarning = gameObject.transform.Find("LightningWarning").gameObject;
        lightningHazard = gameObject.transform.Find("LightningHazard").gameObject;
        lightningHazard.SetActive(false);
        lightningWarning.SetActive(false);
    }

    protected override void HazardFixedUpdate()
    {
    }

    protected override void HazardStart()
    {
        StartCoroutine(SpawnLightning());
    }

    protected override void HazardUpdate()
    {
    }

    private IEnumerator SpawnLightning()
    {
        lightningWarning.SetActive(true);
        yield return new WaitForSeconds(0.7f);
        lightningWarning.SetActive(false);
        lightningHazard.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        lightningHazard.SetActive(false);
        Destroy(gameObject);
    }
}