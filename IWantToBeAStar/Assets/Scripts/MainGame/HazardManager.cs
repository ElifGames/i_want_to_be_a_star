using IWantToBeAStar.MainGame.MapObjects.Hazards;
using System.Collections;
using System.Linq;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace IWantToBeAStar.MainGame
{
    public class HazardManager : MonoBehaviour
    {
        #region 유니티 세팅 값
        public Vector2 UpPosition;
        public Vector2 LeftPosition;
        public Vector2 RightPosition;

        public GameObject Bird;
        public GameObject Airplane;
        public GameObject Lightning;
        public GameObject UFO;
        public GameObject Meteo;
        #endregion

        public int SpawnedHazardsCount { get { return transform.childCount; } }

        private void Awake()
        {
            GameData.UpPosition = UpPosition;
            GameData.LeftPosition = LeftPosition;
            GameData.RightPosition = RightPosition;
        }

        public void SpawnBird()
        {
            SpawnLeftOrRight(Bird);
        }

        public void SpawnAirplane()
        {
            SpawnLeftOrRight(Airplane);
        }

        public void SpawnLightning(int count)
        {
            SpawnHazard(Lightning, Direction.Center, count);
        }

        public void SpawnMeteo()
        {
            SpawnHazard(Meteo, Direction.Up);
        }

        public void SpawnUFO()
        {
            SpawnLeftOrRight(UFO);
        }

        /// <summary>
        /// 게임 상의 모든 위험요소들이 사라질때까지 기다립니다.
        /// </summary>
        public IEnumerator WaitForAllHazardRemoved()
        {
            yield return new WaitUntil(() => SpawnedHazardsCount == 0);
        }

        /// <summary>
        /// 왼쪽 과 오른족 중 랜덤으로 하나를 골라 스폰합니다.
        /// </summary>
        /// <param name="hazard"></param>
        private void SpawnLeftOrRight(GameObject hazard)
        {
            int i = Random.Range(0, 100);
            if (i <= 50)
            {
                SpawnHazard(hazard, Direction.Left);
            }
            else
            {
                SpawnHazard(hazard, Direction.Right);
            }
        }

        /// <summary>
        /// 위, 아래, 왼쪽, 오른쪽 중 한 구간을 골라 랜덤한 위치에 생성합니다.
        /// </summary>
        /// <param name="hazard">생성할 장애물</param>
        /// <param name="direction">방향</param>
        /// <param name="spawnCount">
        /// <paramref name="spawnCount"/>가 2개 이상일 경우 서로 겹치지 않게 생성합니다.
        /// </param>
        private void SpawnHazard(GameObject hazard, Direction direction, int spawnCount = 1)
        {
            // Array를 쓸 경우 값을 미리 초기화해야되기 때문에 초기화된 값때문에 [0, 0, 0]이 먼저 입력되어버림
            List<Vector2> spawnedPositions = new List<Vector2>();
            for (int i = 0; i < spawnCount; i++)
            {
                float posX = 0f;
                float posY = 0f;
                switch (direction)
                {
                    case Direction.Up:
                        posX = GetNotOverlappedRandomPosition(spawnedPositions, PositionType.X, UpPosition.x);
                        posY = UpPosition.y;
                        break;

                    case Direction.Left:
                        posX = LeftPosition.x;
                        posY = GetNotOverlappedRandomPosition(spawnedPositions, PositionType.Y, LeftPosition.y);
                        break;

                    case Direction.Right:
                        posX = RightPosition.x;
                        posY = GetNotOverlappedRandomPosition(spawnedPositions, PositionType.Y, RightPosition.y);
                        break;

                    case Direction.Center:
                        posX = GetNotOverlappedRandomPosition(spawnedPositions, PositionType.X, UpPosition.x);
                        posY = 0f;
                        break;
                }
                var spawnPosition = new Vector2(posX, posY);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation, transform);
                spawnedPositions.Add(spawnPosition);
            }
        }

        /// <summary>
        /// <paramref name="positions"/>에서 +1 -1 위치 까지 겹치지 않는 랜덤 숫자를 뽑아서 반환합니다.
        /// </summary>
        /// <param name="positions">대조해볼 값들</param>
        /// <param name="pos">x 위치인지 y위치인지를 알려줌</param>
        /// <param name="MaxRandomNumber">최대 랜덤 숫자. 최소 랜덤 숫자는 최대 랜덤 숫자의 음수 값</param>
        /// <returns></returns>
        private float GetNotOverlappedRandomPosition(List<Vector2> positions, PositionType pos, float MaxRandomNumber)
        {
            float border = 1f;
            float goodNumber = 0f;
            bool complete = false;
            while (!complete)
            {
                float randomNumber = Random.Range(-MaxRandomNumber, MaxRandomNumber);
                IEnumerable<Vector2> result = null;
                switch (pos)
                {
                    case PositionType.X:
                        result = from position in positions
                                 where (randomNumber > position.x - border) && (randomNumber < position.x + border)
                                 select position;
                        break;
                    case PositionType.Y:
                        result = from position in positions
                                 where (randomNumber > position.y - border) && (randomNumber < position.y + border)
                                 select position;
                        break;
                    default:
                        break;
                }
                if (result == null)
                {
                    throw new NullReferenceException();
                }
                if (result.Count() == 0)
                {
                    goodNumber = randomNumber;
                    complete = true;
                }
            }

            return goodNumber;
        }
        /*
        /// <summary>
        /// 게임 시작 시 최초 장애물을 생성합니다.
        /// </summary>
        private void HandleGameStartEvent()
        {
            StartCoroutine("StartSpawningLeftRightMove", Bird);
        }

        /// <summary>
        /// <paramref name="changedStage"/>에 맞게 장애물을 생성합니다.
        /// </summary>
        /// <param name="changedStage">바뀌는 스테이지</param>
        private void HandleStageChangedEvent(Stage changedStage)
        {
            StopAllCoroutines();

            switch (changedStage)
            {
                case Stage.Ground:
                    break;

                case Stage.LowSky:
                    StartCoroutine("StartSpawningLeftRightMove", Bird);
                    break;

                case Stage.HighSky:
                    StartCoroutine("StartSpawningLeftRightMove", Airplane);
                    StartCoroutine("StartSpawningLightning");
                    break;

                case Stage.Space:
                    GameData.SpawnSpaceHazard = SpaceHazards.Meteo;
                    StartCoroutine("StartSpawningSpaceHazards");
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// 왼쪽이나 오른쪽으로 랜덤 스폰을 무한 반복합니다.
        /// </summary>
        /// <param name="hazard">
        /// 스폰할 장애물
        /// </param>
        /// <returns></returns>
        private IEnumerator StartSpawningLeftRightMove(GameObject hazard)
        {
            while (true)
            {
                SpawnLeftOrRight(hazard);

                yield return new WaitForSeconds(GameData.SpawnWait);
            }
        }



        /// <summary>
        /// 우주 장애물을 현재 상태에 맞게 생성하는 것을 무한반복합니다.
        /// </summary>
        /// <returns></returns>
        private IEnumerator StartSpawningSpaceHazards()
        {
            while (true)
            {
                switch (GameData.SpawnSpaceHazard)
                {
                    case SpaceHazards.Meteo:
                        SpawnHazard(Meteo, Direction.Up);
                        break;

                    case SpaceHazards.UFO:
                        SpawnLeftOrRight(UFO);
                        break;

                    default:
                        Debug.LogError("해당하는 SpaceHazard가 없습니다.");
                        break;
                }
                yield return new WaitForSeconds(GameData.SpawnWait);
            }
        }
        */
    }
}