using System.Collections;
using UnityEngine;


namespace IWantToBeAStar.MainGame.MapObjects.Hazards
{
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
            for (int i = 0; i < 2; i++)
            {
                lightningWarning.SetActive(true);
                yield return new WaitForSeconds(0.4f);
                lightningWarning.SetActive(false);
                yield return new WaitForSeconds(0.1f);
            }
            lightningHazard.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            lightningHazard.SetActive(false);
            Destroy(gameObject);
        }
    }
}
