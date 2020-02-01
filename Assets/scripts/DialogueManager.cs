using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{

    public static DialogueManager instance;

    public string currentWord;

    public bool hasCurrentWord;
    public TextMeshProUGUI currentWordLabel;

    public GameObject dialogueUI;
    public TextMeshProUGUI message;

    public int currentBlurb;
    public Blurbber currentBlurber;

    private void Awake()
    {
        if(DialogueManager.instance != null)
        {
            GameObject.Destroy(gameObject);
        }
        else
        {
            DialogueManager.instance = this;
        }
    }

    private void Start()
    {
        RemoveCurrentWord();
    }

    private void OnDestroy()
    {
        if(DialogueManager.instance == this)
        {
            DialogueManager.instance = null;
        }
    }

    public void AddCurrentWord(string word)
    {
        currentWord = word;
        hasCurrentWord = true;
        currentWordLabel.text = currentWord;
    }

    public void RemoveCurrentWord()
    {
        currentWord = "";
        hasCurrentWord = false;
        currentWordLabel.text = "--none--";
    }

    public bool AtEndOfBlurb()
    {
        return currentBlurb >= currentBlurber.blurbs.Count;
    }

    public void LoadDialogue(Blurbber blurber)
    {
        currentBlurb = 0;
        currentBlurber = blurber;
        dialogueUI.gameObject.SetActive(true);
        NextMessage();
    }

    public void NextMessage()
    {
        message.text = currentBlurber.blurbs[currentBlurb].message;
        currentBlurb += 1;
    }

    public void UseWord()
    {
        Debug.Log(currentBlurb);
        int lastBlurb = currentBlurb - 1;
        if(lastBlurb < 0)
        {
            return;
        }
        if(currentBlurber.blurbs[lastBlurb].hasBranch)
        {
            if(currentWord == currentBlurber.blurbs[lastBlurb].specialWord)
            {
                LoadDialogue(Blurbber.SecretBlurb());
            }
        }
        else
        {
            LoadDialogue(Blurbber.BadEndBlurb());

        }
    }

    public void CloseDialogue()
    {
        dialogueUI.gameObject.SetActive(false);
    }
}

[System.Serializable]
public class Blurbber
{
    public List<TextBoxBlurb> blurbs;

    public static Blurbber SimpleBlurb()
    {
        Blurbber m = new Blurbber();
        m.blurbs = new List<TextBoxBlurb>();

        m.blurbs.Add(TextBoxBlurb.AddSpecialBlurb("<color=yellow>Hello</color> There!", "Hello"));
        m.blurbs.Add(TextBoxBlurb.AddBranchBlurb("<color=blue>So lonely</color> sigh", "Hello"));

        return m;
    }

    public static Blurbber SecretBlurb()
    {

        Blurbber m = new Blurbber();
        m.blurbs = new List<TextBoxBlurb>();

        m.blurbs.Add(TextBoxBlurb.AddSimple("Yes yes friend hello! take my head"));

        return m;
    }


    public static Blurbber BadEndBlurb()
    {
        Blurbber m = new Blurbber();
        m.blurbs = new List<TextBoxBlurb>();

        m.blurbs.Add(TextBoxBlurb.AddSimple("What was that?"));

        return m;
    }
}

[System.Serializable]
public class TextBoxBlurb
{
    public string message;

    public bool hasSpecialWord;
    public string specialWord;

    public bool hasBranch;
    public string branchPath;

    public static TextBoxBlurb AddSimple(string _message)
    {
        TextBoxBlurb mm = new TextBoxBlurb();

        mm.message = _message;
        mm.hasSpecialWord = false;
        mm.specialWord = "";
        mm.hasBranch = false;
        mm.branchPath = "";
        return mm;
    }

    public static TextBoxBlurb AddSpecialBlurb(string _message, string _specialWord)
    {
        TextBoxBlurb mm = new TextBoxBlurb();

        mm.message = _message;
        mm.hasSpecialWord = true;
        mm.specialWord = _specialWord;
        mm.hasBranch = false;
        mm.branchPath = "";
        return mm;
    }

    public static TextBoxBlurb AddBranchBlurb(string _message, string _specialWord)
    {
        TextBoxBlurb mm = new TextBoxBlurb();

        mm.message = _message;
        mm.hasSpecialWord = false;
        mm.specialWord = _specialWord;
        mm.hasBranch = true;
        mm.branchPath = "";
        return mm;
    }
}