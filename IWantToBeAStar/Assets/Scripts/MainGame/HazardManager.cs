using IWantToBeAStar.MainGame.MapObjects.Hazards;
using System.Collections;
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

        /// <summary>
        /// 처음 스폰 간격
        /// </summary>
        public float SpawnWait;

        /// <summary>
        /// 스폰 간격 감소 폭
        /// </summary>
        public float SpawnGain;
        /// <summary>
        /// 최소 스폰 간격
        /// </summary>
        public float MinSpawnWait;

        #endregion

        private ScoreManager scoreManager;
        private GameManager gameManager;

        private void Awake()
        {
            scoreManager = GameObject.Find("Score Manager").GetComponent<ScoreManager>();
            gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
            scoreManager.StageChangedEvent += HandleStageChangedEvent;
            gameManager.GameStartEvent += HandleGameStartEvent;

            GameData.SpawnWait = SpawnWait;
            GameData.SpawnGain = SpawnGain;
            GameData.MinSpawnWait = MinSpawnWait;
            GameData.UpPosition = UpPosition;
            GameData.LeftPosition = LeftPosition;
            GameData.RightPosition = RightPosition;
        }

        private void HandleGameStartEvent()
        {
            StartCoroutine("StartSpawningLeftRightMove", Bird);
        }

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
                    //StartCoroutine("StartSpawningLeftRightMove", Airplane);
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

        private IEnumerator StartSpawningLightning()
        {
            while (true)
            {
                SpawnHazard(Lightning, Direction.Center);
                yield return new WaitForSeconds(GameData.SpawnWait);
            }
        }

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

        private void SpawnHazard(GameObject hazard, Direction direction)
        {
            Vector2 spawnPosition = new Vector2();

            switch (direction)
            {
                case Direction.Up:
                    spawnPosition = new Vector2
                        (Random.Range(-UpPosition.x, UpPosition.x), UpPosition.y);
                    break;

                case Direction.Left:
                    spawnPosition = new Vector2
                        (LeftPosition.x, Random.Range(-LeftPosition.y, LeftPosition.y));
                    break;

                case Direction.Right:
                    spawnPosition = new Vector2
                        (RightPosition.x, Random.Range(-RightPosition.y, RightPosition.y));
                    break;

                case Direction.Center:
                    spawnPosition = new Vector2
                        (Random.Range(-UpPosition.x, UpPosition.x), 0);
                    break;
            }

            Quaternion spawnRotation = Quaternion.identity;
            Instantiate(hazard, spawnPosition, spawnRotation);
        }

        //private IEnumerator RemoveNotDelectedLightnings()
        //{
        //    for (int i = 0; i < 10; i++)
        //    {
        //        var list = GameObject.FindGameObjectsWithTag("Notification");
        //        var list2 = GameObject.FindGameObjectsWithTag("Hazard");
        //        if (list.Length != 0)
        //        {
        //            foreach (var item in list)
        //            {
        //                Destroy(item);
        //            }
        //        }
        //        if (list2.Length != 0)
        //        {
        //            foreach (var item in list2)
        //            {
        //                if (item.name == "Lightning")
        //                {
        //                    Destroy(item);
        //                }
        //            }
        //        }
        //        yield return new WaitForSeconds(0.1f);
        //    }
        //}
    }
}