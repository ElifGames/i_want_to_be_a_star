using UnityEngine;

namespace IWantToBeAStar.MainGame
{
    /// <summary>
    /// UI에 등록되는 이벤트 모음
    /// </summary>
    internal class UIEvent : MonoBehaviour
    {
        public void OnWriteInfoButtonClick()
        {
            UIManager.GameUI.OpenWriteInfoPanel();
        }

        public void OnRestartButtonClick()
        {
            UIManager.GameUI.Restart();
        }

        public void OnMainMenuButtonClick()
        {
            UIManager.GameUI.GoMainMenu();
        }

        public void SendInfoButtonClick()
        {
            UIManager.GameUI.SendInfo();
        }

        public void CancelSendInfoButtonClick()
        {
            UIManager.GameUI.CancelSendInfo();
        }

        public void OnEndWritingClass()
        {
            UIManager.GameUI.EndWritingClass();
        }

        public void OnEndWritingName()
        {
            UIManager.GameUI.EndWritingName();
        }

        public void OnResume()
        {
            UIManager.GameUI.Resume();
        }
    }
}