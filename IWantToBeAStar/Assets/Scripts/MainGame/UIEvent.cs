﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IWantToBeAStar.MainGame;

namespace IWantToBeAStar.MainGame
{
    public class UIEvent : MonoBehaviour
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
    }
}