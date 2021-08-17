using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private float typingSpeed = 0.05f;
    [SerializeField] private bool PlayerSpeakingFirst;

    [Header("Dialogue Text")]
    [SerializeField] private TextMeshProUGUI playerDialogueText;

    [Header("Next Buttons")]
    [SerializeField] private GameObject playerContinueBtn;

    [Header("Animation Controllers")]
    [SerializeField] private Animator playerSpeechBubbleAnimator;

    [Header("UI Audio Source")]
    [SerializeField] private AudioSource uIAudioSource;

    [Header("Dialogue Sentences")]
    [TextArea]
    [SerializeField] private string[] playerDialogueSentences;
    private int dialogueIndex;

    private bool dialogueStarted;
    private float speechBubbleAnimDelay = 0.6f;

    private void Start() 
    {
        StartCoroutine(StartDialogue());
        Debug.Log(dialogueStarted);
        // Debug.Log("index: "+dialogueIndex);
    }

    private void Update() 
    {
        if (playerContinueBtn.activeSelf)
        {
            if(Input.GetKeyDown(KeyCode.Return))
            {
                TriggerContinuePlayerDialogue();
            }
        }    
    }
    public IEnumerator StartDialogue()
    {
        if (PlayerSpeakingFirst)
        {
            playerSpeechBubbleAnimator.SetTrigger("Open");

            yield return new WaitForSeconds(speechBubbleAnimDelay);
            StartCoroutine(TypePlayerDialogue());
        }
        playerContinueBtn.SetActive(true);    
    }

    private IEnumerator TypePlayerDialogue()
    {
        Debug.Log("dailogue index: "+dialogueIndex);
        foreach (char letter in playerDialogueSentences[dialogueIndex].ToCharArray())
        {
            
            playerDialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }    
    }
    public IEnumerator ContinuePlayerDialogue()
    { 
        uIAudioSource.Play();
        if (dialogueIndex < playerDialogueSentences.Length -1 && !dialogueStarted)
        {
            dialogueIndex++;
            playerDialogueText.text = string.Empty;// using string.Empty instead of "" cuz its garbage lol
        
            StartCoroutine(TypePlayerDialogue());
        }
        else
        {
            dialogueStarted = true;
            //playerContinueBtn.SetActive(false);
            playerSpeechBubbleAnimator.SetTrigger("Close");
            yield return null;
        }
        
    }

    public void TriggerContinuePlayerDialogue()
    {

        StartCoroutine(ContinuePlayerDialogue());
    }
}
