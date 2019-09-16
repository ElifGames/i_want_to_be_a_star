using System.Collections;
using UnityEngine;

namespace IWantToBeAStar.MainGame.GameStage
{
    public class HighSkyStage : Stage
    {
        public HighSkyStage() : base(StageType.HighSky)
        {
        }

        protected override IEnumerator StageMain()
        {
            GameData.BackgroundScrollSpeed = 15;
            SpawnTimer airplaneTimer = new SpawnTimer(1f, 0.35f, 10, 5);
            IEnumerator spawnAirplane = SpawningAirplane(airplaneTimer);
            StartCoroutine(spawnAirplane);

            SpawnTimer lightningTimer = new SpawnTimer(1f, 0.85f, 5, 10);
            IEnumerator spawnLightning = SpawningLightning(lightningTimer);
            StartCoroutine(spawnLightning);

            StartCoroutine(lightningTimer.StartReduceSpawnTimer());
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
                hazardManager.SpawnAirplane();
                yield return new WaitForSeconds(timer.SpawnWait);
            }
        }

        private IEnumerator SpawningLightning(SpawnTimer timer)
        {
            while (true)
            {
                hazardManager.SpawnLightning(1);
                yield return new WaitForSeconds(timer.SpawnWait);
            }
        }
    }
}