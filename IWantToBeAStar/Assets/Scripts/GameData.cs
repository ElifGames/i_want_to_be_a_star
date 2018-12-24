using System;
using UnityEngine;

namespace IWantToBeAStar
{
    [Serializable]
    internal static class GameData
    {
        /// <summary>
        /// 현재 배경 상태
        /// </summary>
        internal static BackgroundStatus BgStatus { get; set; }

        /// <summary>
        /// 현재 점수
        /// </summary>
        internal static int Score { get; set; }

        /// <summary>
        /// 게임이 끝났는지 확인
        /// </summary>
        internal static bool IsGameEnd { get; set; }

        internal static bool IsStarted { get; set; }

        /// <summary>
        /// 스폰 시간 간격
        /// </summary>
        internal static float SpawnWait { get; set; }

        internal static Vector2 UpPosition { get; set; }
        internal static Vector2 LeftPosition { get; set; }
        internal static Vector2 RightPosition { get; set; }

        internal static bool StartSpawnMeteo { get; set; }
        internal static bool StartSpawnUFO { get; set; }
        internal static bool CheckSpaceSpawn { get; set; }
    }
}