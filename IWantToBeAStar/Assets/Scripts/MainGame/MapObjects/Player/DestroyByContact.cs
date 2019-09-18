using UnityEngine;

namespace IWantToBeAStar.MainGame.MapObjects.Player
{
    public class DestroyByContact : MonoBehaviour
    {
        #region Unity Settigns
        public bool Invincibility = false;
        #endregion

        private GameManager gameManager;

        private void Start()
        {
            gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!Invincibility && other.tag == "Hazard")
            {
                gameManager.PlayerHasDead();
                Destroy(gameObject);
            }
        }
    }
}