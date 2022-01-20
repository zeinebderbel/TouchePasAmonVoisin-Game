using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;
    public Text continueText;
    private Camera cam;

    /*
     * Nous avons utilis� des animations pour faire afficher la box des dialogues en la faisant glisser.
     * Comme chaque fen�tre ont des positions diff�rentes, il fallait fixer la position du canva � la cam�ra.
     * Mais en fixant � la cam�ra la position, il n'est plus possible de modifier la position du canva dans la sc�ne.
     */
    //public Animator animator;
    public Canvas dialogueCanva;

    private Queue<string> sentences;
    private Queue<string> responses;


    private void Awake()
    {
        cam = Camera.main;
    }

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        responses = new Queue<string>();
        dialogueCanva.enabled = false;
    }

    
    public void StartDialogue(Dialogue dialogue)
    {
        //show dialogue box
        //animator.SetBool("isOpen", true);
        dialogueCanva.enabled = true;
        cam.GetComponent<Transform>().rotation = Quaternion.Euler(2.4f, -90, 0);


        //show the name of the character whom talks
        nameText.text = dialogue.name;

        //show sentences
        sentences.Clear();
        responses.Clear();
		foreach (string sentence in dialogue.sentences)
		{
            sentences.Enqueue(sentence);
		}
        foreach (string res in dialogue.continues)
        {
            responses.Enqueue(res);
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
        string response = responses.Dequeue();
        continueText.text = response;
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
        cam.GetComponent<Transform>().rotation = Quaternion.Euler(0, -90, 0);
        //animator.SetBool("isOpen", false);
    }
}
