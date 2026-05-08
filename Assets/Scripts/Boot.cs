using UnityEngine;

public class Boot : MonoBehaviour
{
    public static Boot Instance;

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
            return;
        }

        // 프레임 60 고정
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
    }
}