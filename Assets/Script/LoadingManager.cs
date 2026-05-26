using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using System.Collections;

public class LoadingManager : MonoBehaviour
{
    [Header("Video")]
    public VideoPlayer videoPlayer;

    [Header("Fade")]
    public SceneFader sceneFader;

    [Header("Loading")]
    public float minimumLoadTime = 3f;

    IEnumerator Start()
    {
        // =========================
        // FADE IN
        // =========================

        if (sceneFader != null)
        {
            yield return StartCoroutine(
                sceneFader.FadeIn()
            );
        }

        // =========================
        // PLAY VIDEO
        // =========================

        if (videoPlayer != null)
        {
            videoPlayer.Play();
        }

        // timer loading minimum
        float timer = 0f;

        // =========================
        // LOAD CORE SCENE
        // =========================

        if (
            !SceneManager
            .GetSceneByName("Core Scene")
            .isLoaded
        )
        {
            yield return SceneManager
                .LoadSceneAsync(
                    "Core Scene",
                    LoadSceneMode.Additive
                );
        }

        // =========================
        // LOAD GAMEPLAY SCENE
        // =========================

        AsyncOperation gameplayLoad =
            SceneManager.LoadSceneAsync(
                "Kamar Bara",
                LoadSceneMode.Additive
            );

        // tunggu gameplay selesai load
        while (!gameplayLoad.isDone)
        {
            timer += Time.deltaTime;

            yield return null;
        }

        // =========================
        // MINIMUM LOADING TIME
        // =========================

        while (timer < minimumLoadTime)
        {
            timer += Time.deltaTime;

            yield return null;
        }

        // =========================
        // SET ACTIVE SCENE
        // =========================

        Scene gameplay =
            SceneManager.GetSceneByName(
                "Kamar Bara"
            );

        SceneManager.SetActiveScene(
            gameplay
        );

        // kasih waktu scene initialize
        yield return null;
        yield return null;

        // =========================
        // STOP VIDEO
        // =========================

        if (videoPlayer != null)
        {
            videoPlayer.Stop();
        }

        // =========================
        // FADE OUT
        // =========================

        if (sceneFader != null)
        {
            yield return StartCoroutine(
                sceneFader.FadeOut()
            );
        }

        // =========================
        // UNLOAD LOADING SCENE
        // =========================

        yield return SceneManager
            .UnloadSceneAsync(
                "LoadingScene"
            );
    }
}