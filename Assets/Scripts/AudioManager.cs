using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("BGM")]
    public AudioSource bgmSource;

    [Header("SFX")]
    public AudioSource sfxSource;
    public AudioClip eatSushiSFX;
    public AudioClip gameOverSFX;
    public AudioClip gameClearSFX;
    public AudioClip jumpSFX;

    void Awake()
    {
        // 싱글톤 설정
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // BGM 재생
    public void PlayBGM(AudioClip clip)
    {
        if (bgmSource.clip == clip) return;
        bgmSource.clip = clip;
        bgmSource.loop = true;
        bgmSource.Play();
    }

    // BGM 정지
    public void StopBGM()
    {
        bgmSource.Stop();
    }

    // SFX 재생
    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    // 초밥 먹을 때
    public void PlayEatSushi()
    {
        PlaySFX(eatSushiSFX);
    }

    // 점프
    public void PlayJump()
    {
        PlaySFX(jumpSFX);
    }

    // 게임 오버
    public void PlayGameOver()
    {
        StopBGM();
        PlaySFX(gameOverSFX);
    }

    // 게임 클리어
    public void PlayGameClear()
    {
        StopBGM();
        PlaySFX(gameClearSFX);
    }

    // BGM 볼륨 설정
    public void SetBGMVolume(float volume)
    {
        bgmSource.volume = volume;
    }

    // SFX 볼륨 설정
    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }
}