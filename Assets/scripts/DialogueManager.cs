﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    public static DialogueManager instance;

    public string currentWord;

    public bool hasCurrentWord;
    public TextMeshProUGUI currentWordLabel;
    public Image textBox;

    public Image blubbleBox;
    public Sprite canUse;
    public Sprite idleuse;
    public Sprite canTake;


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
        blubbleBox.sprite = idleuse;

    }

    private void Start()
    {
        RemoveCurrentWord();

        if (!hasCurrentWord)
        {
            blubbleBox.color = Color.clear;
            blubbleBox.sprite = idleuse;
        }
        else
        {
            blubbleBox.color = Color.white;

            blubbleBox.sprite = canTake;
        }

    }

    private void OnDestroy()
    {
        if(DialogueManager.instance == this)
        {
            DialogueManager.instance = null;
        }
    }

    public void AddCurrentWord()
    {
        currentWord = currentBlurber.blurbs[currentBlurb - 1].specialWord;
        hasCurrentWord = true;
        currentWordLabel.text = currentWord;
    }

    public void RemoveCurrentWord()
    {
        currentWord = "";
        hasCurrentWord = false;
        currentWordLabel.text = "";
    }

    public bool AtEndOfBlurb()
    {
        return currentBlurb >= currentBlurber.blurbs.Count;
    }

    public void LoadDialogue(string id)
    {
        Debug.Log(id);
        currentBlurb = 0;
        currentBlurber = CoreDialogue.instance.allBlurbs[id];
        dialogueUI.gameObject.SetActive(true);
        NextMessage();
    }

    public void NextMessage()
    {
        blubbleBox.color = Color.white;


        if (currentBlurber.blurbs[currentBlurb].hasSpecialWord)
        {
            blubbleBox.sprite = canTake;
        }
        else if (currentBlurber.blurbs[currentBlurb].hasBranch)
        {
            if (hasCurrentWord)
            {
                blubbleBox.color = Color.white;

                blubbleBox.sprite = canUse;
            }
            else
            {
                blubbleBox.color = Color.clear;

            }
        }
        else
        {
            if (!hasCurrentWord)
            {
                blubbleBox.color = Color.clear;

                blubbleBox.sprite = canUse;
            }
            else
            {
                blubbleBox.color = Color.white;
                
                blubbleBox.sprite = canTake;

            }
        }

        if (currentBlurber.blurbs[currentBlurb].isStateChanger)
        {
            CoreDialogue.instance.allNpcs[currentBlurber.blurbs[currentBlurb].message] = currentBlurber.blurbs[currentBlurb].setNewState;
            message.text = "<i>The world is shifting</i>";
            currentBlurb += 1;
        }
        else
        {
            message.text = currentBlurber.blurbs[currentBlurb].message;
            currentBlurb += 1;
        }
    }

    public bool UseWord()
    {
        int lastBlurb = currentBlurb - 1;
        if(lastBlurb < 0)
        {
            return false;
        }
        if(currentBlurber.blurbs[lastBlurb].hasBranch)
        {
            if(currentWord == currentBlurber.blurbs[lastBlurb].specialWord)
            {
                string secretBlurbId = currentBlurber.blurbs[lastBlurb].branchPath;
                LoadDialogue(secretBlurbId);
                RemoveCurrentWord();
                return true;
            }
            else
            {
                string secretBlurbId = currentBlurber.blurbs[lastBlurb].losingBranch;
                LoadDialogue(secretBlurbId);
                RemoveCurrentWord();
                return false;
            }
        }
        else
        {
            RemoveCurrentWord();
            LoadDialogue("what");
        }
        return false;
    }

    public void CloseDialogue()
    {

        if(!hasCurrentWord)
        {
            blubbleBox.color = Color.clear;
            blubbleBox.sprite = idleuse;
        }
        else
        {
            blubbleBox.color = Color.white;

            blubbleBox.sprite = canTake;
        }

        dialogueUI.gameObject.SetActive(false);
    }
}

[System.Serializable]
public class Blurbber
{
    public List<TextBoxBlurb> blurbs;
}

[System.Serializable]
public class TextBoxBlurb
{
    public string message;

    public bool hasSpecialWord;
    public string specialWord;

    public bool hasBranch;
    public string branchPath;

    public string losingBranch;

    public bool isStateChanger;
    public int setNewState;

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

    public static TextBoxBlurb AddBranchBlurb(string _message, string _specialWord, string _branchPath, string losingBranch = "what")
    {
        TextBoxBlurb mm = new TextBoxBlurb();

        mm.message = _message;
        mm.hasSpecialWord = false;
        mm.specialWord = _specialWord;
        mm.hasBranch = true;
        mm.branchPath = _branchPath;
        mm.losingBranch = losingBranch;
        return mm;
    }

    public static TextBoxBlurb ChangeNpcId(string id, int _newState)
    {
        TextBoxBlurb mm = new TextBoxBlurb();

        mm.message = id;
        mm.hasSpecialWord = false;
        mm.specialWord = "";
        mm.hasBranch = false;
        mm.branchPath = "";
        mm.setNewState = _newState;
        mm.isStateChanger = true;
        return mm;
    }
}