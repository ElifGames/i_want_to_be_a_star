using System.Collections.Generic;
using UnityEngine;

namespace IWantToBeAStar
{
    public class AudioPlayer : MonoBehaviour
    {
        public List<AudioClip> Clips;
        public bool IsLoop;
        public bool PlayOnStart;

        public const float RIGHT_SOUND = 0.7f;

        public bool IsPlaying => source.isPlaying;

        private AudioSource source;

        private void Awake()
        {
            source = gameObject.AddComponent<AudioSource>();
            source.clip = Clips.Count > 0 ? Clips[0] : null;
            source.playOnAwake = false;
            source.loop = IsLoop;
        }

        private void Start()
        {
            if (PlayOnStart)
            {
                Debug.Log("소리 재생");
                PlaySound(0, 1);
            }
        }

        /// <summary>
        /// <paramref name="position"/>에 맞는 위치의 스테레오 사운드를 재생합니다.
        /// 사용하기 전 해당 인스턴스가 null값을 가지고 있는지 확인하는 코드 추가를 해주세요.
        /// </summary>
        /// <param name="position">-1(Left) ~ 0(Center) ~ 1(Right)</param>
        public void PlaySound(float position, float volume)
        {
            source.panStereo = position;
            source.volume = volume;
            source.Play();
        }

        public void StopSound()
        {
            source.Stop();
        }
    }
}