using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;
    public Text continueText;
    public TextAsset script;

    /*
     * Nous avons utilisé des animations pour faire afficher la box des dialogues en la faisant glisser.
     * Comme chaque fenêtre ont des positions différentes, il fallait fixer la position du canva à la caméra.
     * Mais en fixant à la caméra la position, il n'est plus possible de modifier la position du canva dans la scène.
     */
    //public Animator animator;
    public Canvas dialogueCanva;

    private Queue<string> sentences;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        dialogueCanva.enabled = false;
    }

    public void StartDialogue(Dialogue dialogue)
    {
        Dialogue[] test = JsonUtility.FromJson<Dialogue[]>(script.text.Replace("\r\n", ""));
        Debug.Log(script.text.Replace("\r\n", "").Replace("\t", ""));
        foreach (var item in test)
        {
            Debug.Log(item);
        }

        //animator.SetBool("isOpen", true);
        dialogueCanva.enabled = true;

        nameText.text = dialogue.name;

        sentences.Clear();

		foreach (string sentence in dialogue.sentences)
		{
            sentences.Enqueue(sentence);
		}

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));

    }

    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    void EndDialogue()
    {
        dialogueCanva.enabled = false;
        //animator.SetBool("isOpen", false);

        Dialogue[] test = JsonUtility.FromJson<Dialogue[]>(script.text);
		foreach (var item in test)
		{
            Debug.Log(item);
		}
    }
}
