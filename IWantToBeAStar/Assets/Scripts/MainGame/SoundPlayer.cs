using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    public AudioClip Clip;
    public bool IsLoop;
    public bool PlayOnStart;

    public const float RIGHT_SOUND = 0.7f;

    public bool IsPlaying
    {
        get
        {
            return source.isPlaying;
        }
    }

    private AudioSource source;

    private void Awake()
    {
        source = gameObject.AddComponent<AudioSource>();
        source.clip = Clip;
        source.playOnAwake = false;
        source.loop = IsLoop;
    }

    private void Start()
    {
        if (PlayOnStart)
        {
            Debug.Log("소리 재생");
            PlaySound(0);
        }
    }

    /// <summary>
    /// <paramref name="position"/>에 맞는 위치의 스테레오 사운드를 재생합니다.
    /// 사용하기 전 null 확인 코드 추가를 해주세요.
    /// </summary>
    /// <param name="position">-1(Left) ~ 0(Center) ~ 1(Right)</param>
    public void PlaySound(float position)
    {
        source.panStereo = position;
        source.Play();
    }

    public void StopSound()
    {
        source.Stop();
    }
}
