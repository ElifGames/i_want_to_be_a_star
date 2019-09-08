using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHazard : MonoBehaviour
{
    protected SoundPlayer Sound;


    // Start is called before the first frame update
    void Start()
    {
        HazardStart();
    }

    // Update is called once per frame
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
        Sound = GetComponent<SoundPlayer>();
    }

    protected void PlaySound()
    {
        if (Sound != null)
        {
            Sound.PlaySound(0);
        }
    }

    protected void PlaySound(bool isRight)
    {
        if (Sound != null)
        {
            Sound.PlaySound(isRight ? SoundPlayer.RIGHT_SOUND : -SoundPlayer.RIGHT_SOUND);
        }
    }

}
