﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningMovement : MonoBehaviour
{
    private GameObject lightningWarning;
    private GameObject lightningHazard;

    private void Awake()
    {
        lightningWarning = gameObject.transform.Find("LightningWarning").gameObject;
        lightningHazard = gameObject.transform.Find("LightningHazard").gameObject;
        lightningHazard.SetActive(false);
        lightningWarning.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnLightning());
    }

    private IEnumerator SpawnLightning()
    {
        for (int i = 0; i < 3; i++)
        {
            lightningWarning.SetActive(true);
            yield return new WaitForSeconds(0.3f);
            lightningWarning.SetActive(false);
            yield return new WaitForSeconds(0.3f);
        }
        lightningHazard.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}
