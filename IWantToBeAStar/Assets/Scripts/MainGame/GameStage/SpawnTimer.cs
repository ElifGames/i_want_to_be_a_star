using System.Collections;
using UnityEngine;

namespace IWantToBeAStar.MainGame.GameStage
{
    /// <summary>
    /// 위험요소의 생성주기를 줄이는 타이머에 대한 클래스입니다.
    /// </summary>
    public class SpawnTimer
    {
        /// <summary>
        /// 처음 생성 주기
        /// </summary>
        public float? DefaultSpawnWait { get; private set; }

        /// <summary>
        /// 최소 주기
        /// </summary>
        public float MinSpawnWait { get; private set; }

        /// <summary>
        /// 생성주기를 줄이는 횟수
        /// </summary>
        public int ReduceCount { get; private set; }

        /// <summary>
        /// 생성주기를 줄이는 값
        /// </summary>
        public float SpawnGain { get; private set; }

        /// <summary>
        /// 생성주기를 한번 줄이기까지 기다리는 시간
        /// </summary>
        public int WaitSecond { get; private set; }

        /// <summary>
        /// 현재 생성주기
        /// </summary>
        public float SpawnWait { get; private set; }

        /// <summary>
        /// 생성주기를 줄이는 타이머를 설정합니다.
        /// <para>
        /// <paramref name="defaultSpawnWait"/>은 <paramref name="minSpawnWait"/>보다 값이 커야 합니다.
        /// </para>
        /// </summary>
        /// <param name="defaultSpawnWait">처음 생성 주기</param>
        /// <param name="minSpawnWait">최소 생성 주기</param>
        /// <param name="waitSecond">생성 주기를 한번 줄인 후 기다릴 시간</param>
        /// <param name="reduceCount">줄이는 행동을 반복할 횟수</param>
        public SpawnTimer(float defaultSpawnWait, float minSpawnWait, int waitSecond, int reduceCount)
        {
            DefaultSpawnWait = null;
            if (defaultSpawnWait <= minSpawnWait)
            {
                Debug.LogError("최소 생성주기가 기본 생성 주기보다 큽니다.");
                return;
            }
            if (reduceCount == 0)
            {
                Debug.LogError("reduce count 값이 0입니다.");
                return;
            }

            DefaultSpawnWait = defaultSpawnWait;
            SpawnWait = defaultSpawnWait;
            MinSpawnWait = minSpawnWait;
            ReduceCount = reduceCount;
            SpawnGain = (defaultSpawnWait - minSpawnWait) / reduceCount;
            WaitSecond = waitSecond;

            Debug.Log($"기본 생성주기: {DefaultSpawnWait}");
            Debug.Log($"현재 생성주기: {SpawnWait}");
            Debug.Log($"최소 생성주기: {MinSpawnWait}");
            Debug.Log($"생성주기를 줄이는 횟수: {ReduceCount}");
            Debug.Log($"생성주기를 줄이는 값: {SpawnGain}");
            Debug.Log($"한번 기다리는 시간: {WaitSecond}");
        }

        /// <summary>
        /// 처음 생성 주기에서
        /// <see cref="MinSpawnWait"/>이 될때까지 생성주기를 줄이는 행동을
        /// <paramref name="waitSecond"/>초에 한번씩 <see cref="ReduceCount"/>번 반복합니다.
        /// </summary>
        /// <param name="waitSecond">한번 생성주기를 줄인 후 기다리는 시간</param>
        public IEnumerator StartReduceSpawnTimer()
        {
            if (DefaultSpawnWait == null)
            {
                throw new System.NullReferenceException("필수 변수가 null값을 가지고 있습니다.");
            }
            for (int i = 0; i < ReduceCount; i++)
            {
                yield return new WaitForSeconds(WaitSecond);
                SpawnWait -= SpawnGain;
                Debug.Log($"현재 생성주기: {SpawnWait}");
            }
            yield return new WaitForSeconds(WaitSecond);
        }
    }
}