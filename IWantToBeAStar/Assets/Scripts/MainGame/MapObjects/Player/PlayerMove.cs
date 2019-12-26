using UnityEngine;

namespace IWantToBeAStar.MainGame.MapObjects.Player
{
    public class PlayerMove : MonoBehaviour
    {
        #region Unity Settings
        public MapSize MapSize;
        public float CursorSpeed;
        public float KeyboardSpeed;
        #endregion

        // Update is called once per frame
        private void Update()
        {
            switch (GameData.Controller)
            {
                case Controllers.Keyboard:
                    KeyboardControl();
                    break;

                case Controllers.Mouse:
                    MouseControl();
                    break;
            }

            // 플레이어가 맵 밖을 나가지 않도록 하는 코드
            transform.position = new Vector2(
                Mathf.Clamp(transform.position.x, MapSize.xMin, MapSize.xMax),
                Mathf.Clamp(transform.position.y, MapSize.yMin, MapSize.yMax));
        }

        private void KeyboardControl()
        {
            float inputX = Input.GetAxis("Horizontal");
            float inputY = Input.GetAxis("Vertical");
            
            float moveX = inputX * KeyboardSpeed * Time.deltaTime;
            float moveY = inputY * KeyboardSpeed * Time.deltaTime;
            transform.Translate(new Vector2(moveX, moveY));
        }

        private void MouseControl()
        {
            var move = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            transform.position =
                Vector2.Lerp(transform.position, move, CursorSpeed * Time.deltaTime);
        }
    }
}