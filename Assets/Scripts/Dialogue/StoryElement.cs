using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryElement : MonoBehaviour
{
    public Dialogue dialogue;
    public TextAsset script;

    [SerializeField]
    private class Wrapper<Dialogue>
    {
        public Dialogue[] Quete1;
    }
    public void triggerDialogue()
    {
        //get json dialogue
        Wrapper<Dialogue> test = JsonUtility.FromJson<Wrapper<Dialogue>>(script.text);
        Dialogue[] test2 = test.Quete1;

        FindObjectOfType<DialogueManager>().StartDialogue(test2[0]);
    }
}
