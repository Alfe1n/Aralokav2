using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameStarter : MonoBehaviour
{
    IEnumerator Start()
    {
        yield return SceneManager.LoadSceneAsync(
            "MainMenu",
            LoadSceneMode.Additive
        );

        SceneManager.SetActiveScene(
            SceneManager.GetSceneByName(
                "MainMenu"
            )
        );
    }
}