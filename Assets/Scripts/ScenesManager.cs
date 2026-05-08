using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public static ScenesManager Instance;



    // 씬 이름으로 이동
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
                Time.timeScale = 1f; // 게임이 일시정지 상태라면 시간 스케일을 원래대로 돌려놓음
    }

    // 씬 인덱스로 이동
    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    // 현재 씬 재시작
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // 다음 씬으로 이동
    public void LoadNextScene()
    {
        int nextIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextIndex);
        Time.timeScale = 1f; // 게임이 일시정지 상태라면 시간 스케일을 원래대로 돌려놓음
    }

    // 이전 씬으로 이동
    public void LoadPreviousScene()
    {
        int prevIndex = SceneManager.GetActiveScene().buildIndex - 1;
        SceneManager.LoadScene(prevIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}