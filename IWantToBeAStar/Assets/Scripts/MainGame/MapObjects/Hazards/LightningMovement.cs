using IWantToBeAStar;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningMovement : BaseHazard
{
    private GameObject lightningWarning;
    private GameObject lightningHazard;

    protected override void HazardAwake()
    {
        base.HazardAwake();
        lightningWarning = gameObject.transform.Find("LightningWarning").gameObject;
        lightningHazard = gameObject.transform.Find("LightningHazard").gameObject;
        lightningHazard.SetActive(false);
        lightningWarning.SetActive(false);

    }

    protected override void HazardStart()
    {
        base.HazardStart();
        StartCoroutine(SpawnLightning());
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
        /*
        while (true)
        {
            if (!Sound?.IsPlaying ?? false)
            {
                Destroy(gameObject);
            }
            yield return new WaitForSeconds(0.1f);
        }
        */
    }
}
