using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IWantToBeAStar.MainGame.GameStage
{
    public class InfiniteSpaceStage : Stage
    {
        public InfiniteSpaceStage() : base(StageTypes.Space)
        {
        }

        protected override IEnumerator StageMain()
        {
            GameData.BackgroundScrollSpeed = 3;
            var patterns = GetNewPatterns();
            for (int i = 0; i < 3; i++)
            {
                yield return StartCoroutine(patterns[i]);
                yield return StartCoroutine(hazardManager.WaitForAllHazardRemoved());
            }

            //모든 패턴 끝난 후에 패턴 랜덤으로 정해서 무한반복
            while (true)
            {
                patterns = GetNewPatterns();
                int idx = Random.Range(0, 3);
                yield return StartCoroutine(patterns[idx]);
                yield return StartCoroutine(hazardManager.WaitForAllHazardRemoved());
            }
        }
        private IEnumerator[] GetNewPatterns()
        {
            return new IEnumerator[]
            {
                Pattern1(),
                Pattern2(),
                Pattern3()
            };
        }

        private IEnumerator Pattern1()
        {
            var timer = new SpawnTimer(0.6f, 0.2f, 3, 10);
            IEnumerator spawn = SpawningMeteo(timer);
            StartCoroutine(spawn);
            yield return StartCoroutine(timer.StartReduceSpawnTimer());
            StopCoroutine(spawn);
        }

        private IEnumerator Pattern2()
        {
            var timer = new SpawnTimer(0.8f, 0.3f, 3, 10);
            IEnumerator spawn = SpawningUFO(timer);
            StartCoroutine(spawn);
            yield return StartCoroutine(timer.StartReduceSpawnTimer());
            StopCoroutine(spawn);
        }

        private IEnumerator Pattern3()
        {
            var meteoTimer = new SpawnTimer(0.6f, 0.3f, 2, 15);
            IEnumerator meteo = SpawningMeteo(meteoTimer);
            var ufoTimer = new SpawnTimer(1f, 0.8f, 3, 10);
            IEnumerator ufo = SpawningUFO(ufoTimer);

            StartCoroutine(meteo);
            StartCoroutine(ufo);
            StartCoroutine(ufoTimer.StartReduceSpawnTimer());
            yield return StartCoroutine(meteoTimer.StartReduceSpawnTimer());
            StopCoroutine(meteo);
            StopCoroutine(ufo);
        }

        private IEnumerator SpawningMeteo(SpawnTimer timer)
        {
            while (true)
            {
                hazardManager.RandomSpawnMeteo();
                yield return new WaitForSeconds(timer.SpawnWait);
            }
        }

        private IEnumerator SpawningUFO(SpawnTimer timer)
        {
            while (true)
            {
                hazardManager.RandomSpawnUFO();
                yield return new WaitForSeconds(timer.SpawnWait);
            }
        }
    }
}