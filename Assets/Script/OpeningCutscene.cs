using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class OpeningCutscene : MonoBehaviour
{
    public float cutsceneDuration = 7f;

    IEnumerator Start()
    {
        // spawn awal player
        SpawnManager.spawnPointName =
            "Spawn_Utama";

        // tunggu opening selesai
        yield return new WaitForSeconds(
            cutsceneDuration
        );

        // load loading screen
        yield return SceneManager
            .LoadSceneAsync(
                "LoadingScene",
                LoadSceneMode.Additive
            );

        // set loading active
        Scene loadingScene =
            SceneManager.GetSceneByName(
                "LoadingScene"
            );

        SceneManager.SetActiveScene(
            loadingScene
        );

        yield return null;

        // unload opening
        yield return SceneManager
            .UnloadSceneAsync(
                "OpeningScene"
            );
    }
}