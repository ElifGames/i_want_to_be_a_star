using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace IWantToBeAStar.Settings
{
    public class SettingsManager : MonoBehaviour
    {
        #region Unity Settings
        public List<Toggle> CharactorToggles;
        public List<Toggle> ControllerToggles;
        #endregion

        public Characters FindPickedCharactor()
        {
            var result = CharactorToggles.Find(x => x.isOn);
            return (Characters)Enum.Parse(typeof(Characters), result.name);
        }

        public Controllers FindPickedController()
        {
            var result = ControllerToggles.Find(x => x.isOn);
            return (Controllers)Enum.Parse(typeof(Controllers), result.name);
        }

        public void WhenStartButtonClick()
        {
            var charactor = FindPickedCharactor();
            var controller = FindPickedController();

            GameData.Charactor = charactor;
            GameData.Controller = controller;

            Debug.Log("선택: " + charactor.ToString() + ", " + controller.ToString());
            SceneManager.LoadScene("MainGame");
        }
    }
}