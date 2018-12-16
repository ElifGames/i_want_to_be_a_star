using System;

namespace IWantToBeAStar
{
    public class WaveStartedEventArgs : EventArgs
    {
        /// <summary>
        /// 이벤트가 호출됐을 때의 웨이브 레벨
        /// </summary>
        public int WaveCount { get; set; }

        public WaveStartedEventArgs(int waveCount)
        {
            WaveCount = waveCount;
        }
    }
}