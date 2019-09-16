using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IWantToBeAStar.MainGame.GameStage
{
    public class InfiniteSpaceStage : Stage
    {
        public InfiniteSpaceStage() : base(StageType.Space)
        {
        }

        protected override IEnumerator StageMain()
        {
            var patterns = new List<IEnumerator>
            {
                Pattern1(),
                Pattern2(),
                Pattern3()
            };

            for (int i = 0; i < 3; i++)
            {
                yield return StartCoroutine(patterns[i]);
                yield return StartCoroutine(hazardManager.WaitForAllHazardRemoved());
                yield return StartCoroutine(Countdown()); // 스테이지 처음에 이미 카운트다운 했음
            }

            //모든 패턴 끝난 후에 패턴 랜덤으로 정해서 무한반복
            while (true)
            {
                System.Random random = new System.Random();
                int num = random.Next(0, 3);
                yield return StartCoroutine(patterns[num]);

                yield return StartCoroutine(hazardManager.WaitForAllHazardRemoved());
                yield return StartCoroutine(Countdown());
            }
        }

        private IEnumerator Pattern1()
        {
            var timer = new SpawnTimer(0.6f, 0.2f, 5, 12);
            IEnumerator spawn = SpawningMeteo(timer);
            StartCoroutine(spawn);
            yield return StartCoroutine(timer.StartReduceSpawnTimer());
            StopCoroutine(spawn);
        }

        private IEnumerator Pattern2()
        {
            var timer = new SpawnTimer(0.8f, 0.3f, 5, 12);
            IEnumerator spawn = SpawningMeteo(timer);
            StartCoroutine(spawn);
            yield return StartCoroutine(timer.StartReduceSpawnTimer());
            StopCoroutine(spawn);
        }

        private IEnumerator Pattern3()
        {
            var meteoTimer = new SpawnTimer(0.6f, 0.2f, 5, 12);
            IEnumerator meteo = SpawningMeteo(meteoTimer);
            var ufoTimer = new SpawnTimer(1f, 0.8f, 10, 6);
            IEnumerator ufo = SpawningUFO(ufoTimer);

            StartCoroutine(meteo);
            StopCoroutine(ufo);
            yield return StartCoroutine(meteoTimer.StartReduceSpawnTimer());
            StopCoroutine(meteo);
            StopCoroutine(ufo);
        }

        private IEnumerator SpawningMeteo(SpawnTimer timer)
        {
            while (true)
            {
                hazardManager.SpawnMeteo();
                yield return new WaitForSeconds(timer.SpawnWait);
            }
        }

        private IEnumerator SpawningUFO(SpawnTimer timer)
        {
            while (true)
            {
                hazardManager.SpawnUFO();
                yield return new WaitForSeconds(timer.SpawnWait);
            }
        }
    }
}