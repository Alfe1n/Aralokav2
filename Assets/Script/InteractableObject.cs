using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using System.Collections;

public class InteractableObject : MonoBehaviour
{
    [Header("Dialogue")]
    public DialogueLine[] dialogueLines;

    [Header("Cutscene")]
    public bool useCutscene;

    public GameObject cutscenePanel;

    public VideoPlayer videoPlayer;

    public RawImage rawImageVideo;

    [Header("CG Image")]
    public GameObject cgImage;

    [Header("Quest")]
    public bool useQuest;

    public int requiredQuest = -1;

    public int nextQuest = -1;

    [Header("Player")]
    private PlayerMovement playerMovement;

    private bool playerInside = false;

    private bool isInteracting = false;

    void Start()
    {
        // cari player otomatis dari Core Scene
        playerMovement =
            FindFirstObjectByType<PlayerMovement>();

        // hide semua pas awal
        if (cutscenePanel != null)
        {
            cutscenePanel.SetActive(false);
        }

        if (rawImageVideo != null)
        {
            rawImageVideo.gameObject.SetActive(false);
        }

        if (cgImage != null)
        {
            cgImage.SetActive(false);
        }
    }

    void Update()
    {
        if (!playerInside) return;

        // cegah spam dialogue
        if (DialogueManager.instance.IsDialogueActive())
            return;

        // cegah spam interaction
        if (isInteracting)
            return;

        // =========================
        // QUEST CHECK
        // =========================

        if (useQuest)
        {
            if (
                QuestManager.Instance.currentQuest
                != requiredQuest
            )
            {
                return;
            }
        }

        // =========================
        // INTERACT
        // =========================

        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(
                BeginInteraction()
            );
        }
    }

    IEnumerator BeginInteraction()
    {
        isInteracting = true;

        DialogueManager.instance.HidePrompt();

        // =========================
        // LOCK PLAYER
        // =========================

        if (playerMovement != null)
        {
            playerMovement.canMove = false;
        }

        // =========================
        // CUTSCENE VIDEO
        // =========================

        if (useCutscene)
        {
            // tampilkan panel cutscene
            if (cutscenePanel != null)
            {
                cutscenePanel.SetActive(true);
            }

            // hide CG dulu
            if (cgImage != null)
            {
                cgImage.SetActive(false);
            }

            // tampilkan raw video
            if (rawImageVideo != null)
            {
                rawImageVideo.gameObject.SetActive(true);
            }

            // aktifkan videoplayer
            if (videoPlayer != null)
            {
                videoPlayer.enabled = true;

                // reset video
                videoPlayer.Stop();
                videoPlayer.frame = 0;

                // play
                videoPlayer.Play();

                // tunggu video mulai
                yield return new WaitUntil(() =>
                    videoPlayer.isPlaying
                );

                // tunggu video selesai
                yield return new WaitUntil(() =>
                    !videoPlayer.isPlaying
                );

                // stop video
                videoPlayer.Stop();
            }

            // hide raw video
            if (rawImageVideo != null)
            {
                rawImageVideo.gameObject.SetActive(false);
            }

            // tampilkan freeze frame / CG
            if (cgImage != null)
            {
                cgImage.SetActive(true);
            }
        }

        // =========================
        // DIALOGUE
        // =========================

        DialogueManager.instance.StartDialogue(
            dialogueLines
        );

        // tunggu dialogue selesai
        while (
            DialogueManager.instance
            .IsDialogueActive()
        )
        {
            yield return null;
        }

        // =========================
        // NEXT QUEST
        // =========================

        if (useQuest)
        {
            if (nextQuest >= 0)
            {
                QuestManager.Instance
                    .SetQuest(nextQuest);
            }
        }

        // =========================
        // TUTUP SEMUA
        // =========================

        // hide CG
        if (cgImage != null)
        {
            cgImage.SetActive(false);
        }

        // tutup panel cutscene
        if (cutscenePanel != null)
        {
            cutscenePanel.SetActive(false);
        }

        // =========================
        // UNLOCK PLAYER
        // =========================

        if (playerMovement != null)
        {
            playerMovement.canMove = true;
        }

        isInteracting = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;

            // =========================
            // QUEST CHECK
            // =========================

            if (useQuest)
            {
                if (
                    QuestManager.Instance.currentQuest
                    != requiredQuest
                )
                {
                    return;
                }
            }

            if (
                !DialogueManager.instance
                .IsDialogueActive()
            )
            {
                DialogueManager.instance
                    .ShowPrompt();
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;

            DialogueManager.instance
                .HidePrompt();
        }
    }
}