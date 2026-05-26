using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerSpawn : MonoBehaviour
{
    void OnEnable()
    {
        SceneManager.sceneLoaded +=
            OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -=
            OnSceneLoaded;
    }

    void OnSceneLoaded(
        Scene scene,
        LoadSceneMode mode
    )
    {
        // skip non gameplay scenes
        if (
            scene.name == "Core Scene"
            || scene.name == "LoadingScene"
            || scene.name == "OpeningScene"
            || scene.name == "MainMenu"
        )
        {
            return;
        }

        StartCoroutine(
            SpawnRoutine()
        );
    }

    IEnumerator SpawnRoutine()
    {
        // tunggu scene siap
        yield return null;
        yield return null;

        string spawnName =
            SpawnManager.spawnPointName;

        if (string.IsNullOrEmpty(spawnName))
        {
            spawnName = "Spawn_Utama";
        }

        GameObject spawn =
            GameObject.Find(spawnName);

        if (spawn != null)
        {
            Debug.Log(
                "Spawn ditemukan: "
                + spawnName
            );

            transform.position =
                spawn.transform.position;
        }
        else
        {
            Debug.LogWarning(
                "Spawn point tidak ditemukan: "
                + spawnName
            );
        }
    }
}