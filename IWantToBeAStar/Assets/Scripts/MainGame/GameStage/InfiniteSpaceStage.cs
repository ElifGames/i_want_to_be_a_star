using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWantToBeAStar.MainGame.GameStage
{
    public class InfiniteSpaceStage : Stage
    {
        public InfiniteSpaceStage() : base(StageType.Space)
        {
        }

        protected override IEnumerator StageMain()
        {
            yield return null;
        }

        private IEnumerator SpawningMeteo()
        {
            while (true)
            {

            }
        }
    }
}
