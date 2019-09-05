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
        /// 상품을 줄 수 있는 최저 점수
        /// </summary>
        internal static int Goal { get; set; }

        /// <summary>
        /// 스폰 시간 간격
        /// </summary>
        internal static float SpawnWait { get; set; }

        /// <summary>
        /// 스폰 시간 감소 폭
        /// </summary>
        internal static float SpawnGain { get; set; }

        /// <summary>
        /// 최소 스폰시간 간격
        /// </summary>
        internal static float MinSpawnWait { get; set; }

        /// <summary>
        /// 점수 증가 시간 폭
        /// </summary>
        internal static float ScoreTimeGain { get; set; }

        /// <summary>
        /// 해당 값의 점수대마다 스폰 시간 감소
        /// </summary>
        internal static int ReduceSpawnGainScore { get; set; }


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