using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IWantToBeAStar
{
    public class GameController : MonoBehaviour
    {
        public GameObject Hazard;
        public Vector2 SpawnValues;

        public int HazardIncrease;
        public int HazardCount = 10;
        public float SpawnWait;
        public float StartWait;
        public float WaveWait;

        // Use this for initialization
        void Start()
        {
            StartCoroutine(SpawnWaves());
        }

        // Update is called once per frame
        void Update()
        {
        }

        IEnumerator SpawnWaves()
        {
            // 게임 시작
            GameData.Wave = 1;
            yield return new WaitForSeconds(StartWait);
            while (true)
            {
                // 한 웨이브 진행
                Debug.Log(GameData.Wave + "번째 웨이브 시작");
                for (int i = 0; i < HazardCount; i++)
                {
                    Vector2 spawnPosition = new Vector2
                        (Random.Range(-SpawnValues.x, SpawnValues.x), SpawnValues.y);
                    Quaternion spawnRotation = Quaternion.identity;
                    Instantiate(Hazard, spawnPosition, spawnRotation);
                    yield return new WaitForSeconds(SpawnWait);
                }
                // 한 웨이브 끝

                // 다음 웨이브 준비
                GameData.Wave++;
                HazardCount += HazardIncrease;
                if (SpawnWait > 0.05f)
                {
                    SpawnWait -= 0.01f;
                }
                yield return new WaitForSeconds(WaveWait);
            }
        }
    }
}
