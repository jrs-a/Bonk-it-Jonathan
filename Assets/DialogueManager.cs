using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using TMPro;
using Ink.Runtime;

public class DialogueManager : MonoBehaviour
{
    private static DialogueManager instance;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private GameObject dim_bg;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI speakerName;
    private Story currentStory;
    private List<string> tags;
    public bool dialogueIsPlaying {get; private set;} //for playercontroller
    
    void Awake() {
        if(instance != null) {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    public static DialogueManager GetInstance() {
        return instance;
    }

    private void Start() {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dim_bg.SetActive(false);
        tags = new List<string>();
    }

    void Update() {
        if(!dialogueIsPlaying)
            return;
        else if(Input.GetKeyDown(KeyCode.Space))
            ContinueStory();
    }

    public void EnterDialogueMode(TextAsset inkJSON) {
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        StartCoroutine(waiter_openDialogue());
        ContinueStory();
    }

    private void ContinueStory() {
        if(currentStory.canContinue) {
            string sentence = currentStory.Continue();
            ParseTags();
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));
            Debug.Log(sentence);
        }
        else
            ExitDialogueMode();
    }

    IEnumerator TypeSentence(string sentence) {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray()) {
            dialogueText.text += letter;
            yield return null;
        }
    }

    private void ParseTags() {
        tags = currentStory.currentTags;
        foreach (string t in tags) {
            string task = t.Split(' ')[0];
            string value = t.Split(' ')[1];

            switch (task) {
                case "speaker":
                    speakerName.text = value;
                    break;
                default:
                    break;
            }
        }
    }

    private void ExitDialogueMode() {
        dialogueIsPlaying = false;
        StartCoroutine(waiter_closeDialogue());
        dialogueText.text = "";
        EventsSystem.current.StopDialogue();
    }

    IEnumerator waiter_openDialogue() {
        dialoguePanel.SetActive(true);
        dim_bg.SetActive(true);
        LeanTween.alphaCanvas(dim_bg.GetComponent<CanvasGroup>(), 1, 0.2f);
        LeanTween.alphaCanvas(dialoguePanel.GetComponent<CanvasGroup>(), 1, 0.2f);
        yield return new WaitForSeconds(1.4f);
    }

    IEnumerator waiter_closeDialogue() {
        LeanTween.alphaCanvas(dialoguePanel.GetComponent<CanvasGroup>(), 0, 0.2f);
        LeanTween.alphaCanvas(dim_bg.GetComponent<CanvasGroup>(), 0, 0.2f);
        yield return new WaitForSeconds(1f);
        dialoguePanel.SetActive(false);
        dim_bg.SetActive(false);
    }
}
