using IWantToBeAStar.Tools;
using System.Collections;
using UnityEngine;

namespace IWantToBeAStar.MainGame.GameStage
{
    public class HighSkyStage : Stage
    {
        public HighSkyStage() : base(StageTypes.HighSky)
        {
        }

        protected override IEnumerator StageMain()
        {
            GameData.BackgroundScrollSpeed = 15;
            SpawnTimer airplaneTimer = new SpawnTimer(1f, 0.35f, 10, 5);
            IEnumerator spawnAirplane = SpawningAirplane(airplaneTimer);
            StartCoroutine(spawnAirplane);

            IEnumerator spawnLightning = SpawningLightning();
            StartCoroutine(spawnLightning);

            yield return StartCoroutine(airplaneTimer.StartReduceSpawnTimer());

            StopCoroutine(spawnAirplane);
            StopCoroutine(spawnLightning);
        }

        private IEnumerator Warning()
        {
            UIManager.GameUI.ReadyText.color = Color.red;
            for (int i = 0; i < 3; i++)
            {
                UIManager.GameUI.ReadyText.text = "WARNING!";
                yield return new WaitForSeconds(1f);
                UIManager.GameUI.ReadyText.text = string.Empty;
                yield return new WaitForSeconds(1f);
            }
            UIManager.GameUI.SetDefaultToReadyText();
        }

        private IEnumerator SpawningAirplane(SpawnTimer timer)
        {
            while (true)
            {
                hazardManager.RandomSpawnAirplane();
                yield return new WaitForSeconds(timer.SpawnWait);
            }
        }

        private IEnumerator SpawningLightning()
        {
            while (true)
            {
                hazardManager.RandomSpawnLightning(1);
                yield return new WaitForSeconds(Random.Range(0.3f, 1f));
            }
        }
    }
}