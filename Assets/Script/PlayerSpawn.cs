using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    void Start()
    {
        string spawnName = SpawnManager.spawnPointName;

        // 🔥 kalau belum ada spawn sebelumnya
        if (string.IsNullOrEmpty(spawnName))
        {
            spawnName = "Spawn_Utama";
        }

        GameObject spawn = GameObject.Find(spawnName);

        if (spawn != null)
        {
            transform.position = spawn.transform.position;
        }
        else
        {
            Debug.LogWarning("Spawn point tidak ditemukan: " + spawnName);
        }
    }
}