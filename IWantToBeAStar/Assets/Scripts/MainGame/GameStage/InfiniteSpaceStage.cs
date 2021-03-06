﻿using IWantToBeAStar.Tools;
using System.Collections;
using UnityEngine;

namespace IWantToBeAStar.MainGame.GameStage
{
    public class InfiniteSpaceStage : Stage
    {
        private float pattern1SpawnWait = 0.6f;
        private float pattern2SpawnWait = 0.8f;
        private float pattern3SpawnWait = 1f;
        private float pattern4SpawnWait = 0.6f;

        private const float patternSpawnGain = 0.07f;

        public InfiniteSpaceStage() : base(StageTypes.Space)
        {
        }

        protected override IEnumerator StageMain()
        {
            GameData.BackgroundScrollSpeed = 3.0f;
            var patterns = GetNewPatterns();
            for (int i = 0; i < patterns.Length; i++)
            {
                RandomSeed.SetRandomSeed();
                yield return StartCoroutine(patterns[i]);
                yield return StartCoroutine(HazardManager.WaitForAllHazardRemoved());
            }
            //모든 패턴 끝난 후에 패턴 랜덤으로 정해서 무한반복

            int beforeIdx = -1;
            int idx;
            while (true)
            {
                RandomSeed.SetRandomSeed();
                patterns = GetNewPatterns();
                do
                {
                    idx = Random.Range(0, patterns.Length);
                }
                while (idx == beforeIdx);

                yield return StartCoroutine(patterns[idx]);
                yield return StartCoroutine(HazardManager.WaitForAllHazardRemoved());
                beforeIdx = idx;
            }
        }
        private IEnumerator[] GetNewPatterns()
        {
            return new IEnumerator[]
            {
                Pattern1(),
                Pattern2(),
                Pattern3(),
                Pattern4()
            };
        }

        private IEnumerator Pattern1()
        {
            Debug.Log("1번패턴");
            const float minSpawnWait = 0.2f;
            pattern1SpawnWait = ReducePatternSpawnWait(pattern1SpawnWait, minSpawnWait);

            var timer = new SpawnTimer(pattern1SpawnWait, minSpawnWait, 3, 10);
            IEnumerator spawn = SpawningMeteo(timer);
            StartCoroutine(spawn);
            yield return StartCoroutine(timer.StartReduceSpawnTimer());
            StopCoroutine(spawn);
        }

        private IEnumerator Pattern2()
        {
            Debug.Log("2번패턴");
            const float minSpawnWait = 0.3f;
            pattern2SpawnWait = ReducePatternSpawnWait(pattern2SpawnWait, minSpawnWait);

            var timer = new SpawnTimer(pattern2SpawnWait, minSpawnWait, 3, 10);
            IEnumerator spawn = SpawningUFO(timer);
            StartCoroutine(spawn);
            yield return StartCoroutine(timer.StartReduceSpawnTimer());
            StopCoroutine(spawn);
        }

        private IEnumerator Pattern3()
        {
            Debug.Log("3번패턴");
            const float minSpawnWait = 0.3f;
            pattern3SpawnWait = ReducePatternSpawnWait(pattern3SpawnWait, minSpawnWait);

            var meteoTimer = new SpawnTimer(pattern3SpawnWait, minSpawnWait, 2, 15);
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

        private IEnumerator Pattern4()
        {
            Debug.Log("4번패턴");
            const float minSpawnWait = 0.2f;
            pattern4SpawnWait = ReducePatternSpawnWait(pattern4SpawnWait, minSpawnWait);

            var starTimer = new SpawnTimer(pattern4SpawnWait, minSpawnWait, 3, 10);
            IEnumerator spawn = SpawningStar(starTimer);
            StartCoroutine(spawn);
            yield return StartCoroutine(starTimer.StartReduceSpawnTimer());
            StopCoroutine(spawn);
        }

        private IEnumerator SpawningMeteo(SpawnTimer timer)
        {
            while (true)
            {
                HazardManager.RandomSpawnMeteo();
                yield return new WaitForSeconds(timer.SpawnWait);
            }
        }

        private IEnumerator SpawningUFO(SpawnTimer timer)
        {
            while (true)
            {
                HazardManager.RandomSpawnUFO();
                yield return new WaitForSeconds(timer.SpawnWait);
            }
        }

        private IEnumerator SpawningStar(SpawnTimer timer)
        {
            while (true)
            {
                HazardManager.RandomSpawnStar();
                yield return new WaitForSeconds(timer.SpawnWait);
            }
        }

        private float ReducePatternSpawnWait(float patternSpawnWait, float minSpawnWait)
        {
            patternSpawnWait -= patternSpawnGain;
            var check = minSpawnWait + patternSpawnGain;
            if (patternSpawnWait <= check)
            {
                patternSpawnWait = check;
            }

            return patternSpawnWait;
        }
    }
}