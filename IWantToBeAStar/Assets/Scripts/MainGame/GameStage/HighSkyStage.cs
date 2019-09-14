using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IWantToBeAStar.MainGame.GameStage
{
    public class HighSkyStage : Stage
    {
        SpawnTimer timer;

        public HighSkyStage() : base(StageType.HighSky)
        {
        }

        protected override IEnumerator StageMain()
        {
            timer = new SpawnTimer(1f, 0.2f, 10, 5);
            IEnumerator spawn = SpawningAirplane();
            StartCoroutine(spawn);
            yield return StartCoroutine(timer.StartReduceSpawnTimer());
            StopCoroutine(spawn);
        }

        private IEnumerator SpawningAirplane()
        {
            while (true)
            {
                hazardManager.SpawnAirplane();
                yield return new WaitForSeconds(timer.SpawnWait);
            }
        }
    }
}
