using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    private void Awake()
    {
        PlayerPrefs.SetInt("Score", 0);
    }

    public void OnStartButtonClick()
    {
        SceneManager.LoadScene("Stage1");
    }

    public void OnGameEnd()
    {
        Application.Quit();
    }
}
