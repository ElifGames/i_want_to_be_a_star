using IWantToBeAStar;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHazard : MonoBehaviour
{
    //protected SoundPlayer Sound;
    //protected float Volume { get; set; }

    void Start()
    {
        HazardStart();
    }

    void Awake()
    {
        HazardAwake();
    }

    /// <summary>
    /// base.HazardStart()를 먼저 사용 후 사용하세요.
    /// </summary>
    protected virtual void HazardStart()
    {
    }

    protected virtual void HazardAwake()
    {
        //Sound = GetComponent<SoundPlayer>();
        //Volume = 0.5f;
    }
    /*
    protected void PlaySound()
    {
        if (Sound != null)
        {
            // 맵을 3등분
            float center = GameData.UpPosition.x / 3;
            float x = transform.position.x;

            // 오브젝트가 3등분된 맵에서 안쪽인지 바깥쪽인지 확인
            if (Mathf.Abs(x) < center)
            {
                Sound.PlaySound(0, Volume);
            }
            else
            {
                Sound.PlaySound(x > 0 ? SoundPlayer.RIGHT_SOUND : -SoundPlayer.RIGHT_SOUND, Volume);
            }
        }
    }
    */
}
