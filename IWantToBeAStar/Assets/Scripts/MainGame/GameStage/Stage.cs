using IWantToBeAStar.Tools;
using System.Collections;
using UnityEngine;

namespace IWantToBeAStar.MainGame.GameStage
{
    /// <summary>
    /// 스테이지의 기본 클래스입니다.
    /// </summary>
    public abstract class Stage : MonoBehaviour
    {
        /// <summary>
        /// 해당 스테이지의 타입입니다.
        /// </summary>
        public readonly StageTypes StageType;

        /// <summary>
        /// 게임 내의 <see cref="MainGame.HazardManager"/>를 제공합니다.
        /// </summary>
        protected HazardManager HazardManager { get; private set; }

        /// <summary>
        /// 게임 내의 <see cref="MainGame.GameManager"/>를 제공합니다.
        /// </summary>
        protected GameManager GameManager { get; private set; }

        #region Coroutines
        private IEnumerator stageMainCoroutine;
        #endregion

        /// <summary>
        /// <paramref name="type"/>으로 스테이지의 타입을 설정합니다.
        /// </summary>
        /// <param name="type">스테이지의 타입</param>
        public Stage(StageTypes type)
        {
            StageType = type;
        }

        /// <summary>
        /// 3초 카운트 다운을 한 후 스테이지를 시작합니다.
        /// <para>해당 함수는 스테이지의 전체 진행과정이며 스테이지가 끝난 후에 종료됩니다.</para>
        /// </summary>
        public IEnumerator Run()
        {
            HazardManager = GameObject.Find("Hazard Manager").GetComponent<HazardManager>();
            GameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

            yield return StartCoroutine(Countdown());
            GameManager.GameEndEvent += HandleGameEndEvent;

            RandomSeed.SetRandomSeed();
            stageMainCoroutine = StageMain();
            yield return StartCoroutine(stageMainCoroutine);

            GameManager.GameEndEvent -= HandleGameEndEvent;
            Debug.Log("스테이지 종료");
        }

        /// <summary>
        /// 스테이지의 메인 지점입니다.
        /// </summary>
        /// <returns></returns>
        protected abstract IEnumerator StageMain();

        /// <summary>
        /// 게임이 종료되었을 때 호출됩니다.
        /// </summary>
        protected virtual void HandleGameEndEvent()
        {
            StopAllCoroutines();
        }

        /// <summary>
        /// 3초 카운트다운을 하는 동안 기다립니다.
        /// </summary>
        private IEnumerator Countdown()
        {
            UIManager.GameUI.ReadyText.color = Color.yellow;
            for (int i = 3; i > 0; i--)
            {
                UIManager.GameUI.ReadyText.text = i.ToString();
                yield return new WaitForSeconds(1f);
            }
            UIManager.GameUI.SetDefaultToReadyText();
        }
    }
}