using UnityEngine;
using UnityEngine.SceneManagement;

namespace IWantToBeAStar.Tools
{
    public class ChangeScene : MonoBehaviour
    {
        public void ChangeGameScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}