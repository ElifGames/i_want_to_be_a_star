using System;

namespace IWantToBeAStar
{
    [Serializable]
    internal static class GameData
    {
        /// <summary>
        /// 현재 웨이브 수
        /// </summary>
        internal static int Wave { get; set; }

        /// <summary>
        /// 현재 배경 상태
        /// </summary>
        internal static BackgroundStatus BgStatus { get; set; }

        /// <summary>
        /// 현재 점수
        /// </summary>
        internal static int Score { get; set; }

        /// <summary>
        /// 게임이 멈췄는지 아닌지 확인
        /// </summary>
        internal static bool IsGameStop { get; set; }

        /// <summary>
        /// 게임이 끝났는지 확인
        /// </summary>
        internal static bool IsGameEnd { get; set; }
    }
}