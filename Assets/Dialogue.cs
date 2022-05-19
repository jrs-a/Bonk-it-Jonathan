using System.Collections;
using UnityEngine;

public class Dialogue : MonoBehaviour //also called DIALOGUETRIGGER
{    
    [SerializeField] private GameObject visualCue;
    [SerializeField] private TextAsset inkJSON;
    [SerializeField] private GameObject tooltip;
    [SerializeField] private Animator anim;
    private bool playerInRange;
    private bool dialogueIsPlaying;
    
    private void Awake() {
        playerInRange = false;
        dialogueIsPlaying = false;
        tooltip.SetActive(false);
    }

    private void Start() {
        EventsSystem.current.onPlayerMove += checkDistance;
        EventsSystem.current.onDialogueStop += dialogueDeactivated;
    }

    private void Update() {
        if(playerInRange) {
            anim.SetBool("chat_active", true);
            tooltip.SetActive(true);
            LeanTween.alphaCanvas(tooltip.GetComponent<CanvasGroup>(), 1, 0.4f);
            if(Input.GetKeyDown(KeyCode.Space)) {
                if(!dialogueIsPlaying) {
                    DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
                    dialogueIsPlaying = true;
                }
            }
        }
        else 
            anim.SetBool("chat_active", false);
    }

    //event-based updaters
    private void checkDistance() { //subscribed to PlayerMove() so it updates the bool playerinrange everytime the player moves :D
        Vector3 player_pos = GameObject.Find("Player").transform.position;
        float dist = Vector3.Distance(transform.position, player_pos);
        if(dist < 1.5)
            playerInRange = true;
        else  {
            playerInRange = false;
            LeanTween.alphaCanvas(tooltip.GetComponent<CanvasGroup>(), 0, 0.4f);
            StartCoroutine(waiter_tooltipClose());
        }
    }

    private void dialogueDeactivated() {
        StartCoroutine(waiter_dialogueClose());
    }

    //waiters
    private IEnumerator waiter_dialogueClose() {
        yield return new WaitForSeconds(1);
        dialogueIsPlaying = false;
    }

    private IEnumerator waiter_tooltipClose() {
        yield return new WaitForSeconds(2);
        tooltip.SetActive(false);
    }

    //cleaners
    private void canYouMove(Vector2 dir) {}

    private void OnDestroy() {
        EventsSystem.current.onPlayerMove -= checkDistance;
        EventsSystem.current.onDialogueStop -= dialogueDeactivated;
    }
}