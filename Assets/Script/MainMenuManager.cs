using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuManager : MonoBehaviour
{
    public void NewGame()
    {
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {

        // load opening cutscene
        yield return SceneManager.LoadSceneAsync(
            "OpeningScene",
            LoadSceneMode.Additive
        );


        // set active scene
        SceneManager.SetActiveScene(
            SceneManager.GetSceneByName(
                "OpeningScene"
            )
        );

        // unload menu
        yield return SceneManager.UnloadSceneAsync(
            "MainMenu"
        );
    }
    public void ContinueGame()
    {
        Debug.Log("Continue");
    }

    public void Settings()
    {
        Debug.Log("Settings");
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}