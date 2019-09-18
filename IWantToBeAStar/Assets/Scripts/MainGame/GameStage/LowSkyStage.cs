using IWantToBeAStar.MainGame;
using IWantToBeAStar.MainGame.GameStage;
using System.Collections;
using UnityEngine;

namespace IWantToBeAStar.MainGame.GameStage
{
    public class LowSkyStage : Stage
    {
        private SpawnTimer timer;

        public LowSkyStage() : base(StageTypes.LowSky)
        {
        }

        protected override IEnumerator StageMain()
        {
            timer = new SpawnTimer(1f, 0.2f, 10, 5);
            IEnumerator spawn = SpawningBird();
            StartCoroutine(spawn);
            yield return StartCoroutine(timer.StartReduceSpawnTimer());
            StopCoroutine(spawn);
        }

        private IEnumerator SpawningBird()
        {
            while (true)
            {
                hazardManager.RandomSpawnBird();
                yield return new WaitForSeconds(timer.SpawnWait);
            }
        }
    }
}

