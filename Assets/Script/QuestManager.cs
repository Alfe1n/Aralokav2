using TMPro;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

    [Header("UI")]
    public TMP_Text objectiveText;

    public int currentQuest = 0;

    void Awake()
    {
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

    void Start()
    {
        UpdateObjective();
    }

    public void SetQuest(int id)
    {
        currentQuest = id;

        UpdateObjective();
    }

    void UpdateObjective()
    {
        switch (currentQuest)
        {
            case 0:
                objectiveText.text =
                    "Pergi cek HP";
                break;

            case 1:
                objectiveText.text =
                    "Pergi istirahat";
                break;

            case 2:
                objectiveText.text =
                    "???";
                break;
        }
    }
}