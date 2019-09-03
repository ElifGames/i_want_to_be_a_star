using IWantToBeAStar.MainGame.MapObjects.Hazards;
using System;
using UnityEngine;
namespace IWantToBeAStar
{
    /// <summary>
    /// 게임 데이터를 관리합니다.
    /// </summary>
    [Serializable]
    internal static class GameData
    {
        /// <summary>
        /// 현재 스테이지
        /// </summary>
        internal static Stage CurrentStage { get; set; }

        /// <summary>
        /// 현재 점수
        /// </summary>
        internal static int Score { get; set; }

        /// <summary>
        /// 게임이 진행중인지 확인
        /// </summary>
        //internal static bool IsGameRunning { get; set; }

        /// <summary>
        /// 스폰 시간 간격
        /// </summary>
        internal static float SpawnWait { get; set; }

        internal static Vector2 UpPosition { get; set; }
        internal static Vector2 LeftPosition { get; set; }
        internal static Vector2 RightPosition { get; set; }

        /// <summary>
        /// HighSky 시작 점수
        /// </summary>
        internal static int HighSkyStartScore { get; set; }

        /// <summary>
        /// Space 시작 점수
        /// </summary>
        internal static int SpaceStartScore { get; set; }

        /// <summary>
        /// 어떤 종류의 장애물을 생성해야 하는지 알려줍니다.
        /// </summary>
        internal static SpaceHazards SpawnSpaceHazard { get; set; }

        internal static Charactors Charactor { get; set; }
        internal static Controllers Controller { get; set; }
    }
}