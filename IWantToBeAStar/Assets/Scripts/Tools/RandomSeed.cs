using System;

namespace IWantToBeAStar.Tools
{
    internal class RandomSeed
    {
        public static void SetRandomSeed()
        {
            UnityEngine.Random.InitState((int)DateTime.Now.Ticks);
        }
    }
}
