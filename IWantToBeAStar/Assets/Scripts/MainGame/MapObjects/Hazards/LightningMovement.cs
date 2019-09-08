using IWantToBeAStar;
using System.Collections;
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
        lightningWarning.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        lightningWarning.SetActive(false);
        lightningHazard.SetActive(true);

        SoundPlayer player = GetComponent<SoundPlayer>();
        if (player != null)
        {
            float center = GameData.UpPosition.x / 3;
            float x = transform.position.x;

            if (Mathf.Abs(x) < center)
            {
                player.PlaySound(0);
            }
            else
            {
                player.PlaySound(x > 0 ? SoundPlayer.RIGHT_SOUND : -SoundPlayer.RIGHT_SOUND);
            }
        }

        yield return new WaitForSeconds(0.1f);
        lightningHazard.SetActive(false);
        while (true)
        {
            if (!player?.IsPlaying() ?? false)
            {
                Destroy(gameObject);
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}
