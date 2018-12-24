using IWantToBeAStar;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace IWantToBeAStar.Settings
{
    public class SettingsManager : MonoBehaviour
    {
        public List<Toggle> CharactorToggles;
        public List<Toggle> ControllerToggles;

        public Charactors FindPickedCharactor()
        {
            var result = CharactorToggles.Find(x => x.isOn);
            return (Charactors)Enum.Parse(typeof(Charactors), result.name);
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

            Debug.Log("선택 결과: " + charactor.ToString() + ", " + controller.ToString());
            new Tools.ChangeScene().ChangeGameScene("MainGame");
        }
    }
}

