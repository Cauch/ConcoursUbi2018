using System.Linq;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {
    public int DialogueId;
    public Dialogue Dialogue;
    private DialogueManager _dialogueManager;

    private void Start()
    {
        _dialogueManager = GameEssentials.DialogueManager;
        Dialogue = _dialogueManager.Dialogues.Where(dialogue => dialogue.Id == DialogueId).First();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (GameEssentials.IsGirl(other))
        {
            GameEssentials.DialogueSync.Cmd_ChangeDialogueToServer(Dialogue);
            Destroy(this);
        }
    }
}
