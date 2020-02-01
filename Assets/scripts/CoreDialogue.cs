using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreDialogue : MonoBehaviour
{
    public static CoreDialogue instance;

    private void Awake()
    {
        if(CoreDialogue.instance != null)
        {
            GameObject.Destroy(gameObject);
        }
        else
        {
            CoreDialogue.instance = this;
        }

        allBlurbs = GetAllBlurbs();

        allNpcs = new Dictionary<string, int>();
        allNpcs.Add("head_1", 0);
        allNpcs.Add("pride_1", 0);


    }

    private void OnDestroy()
    {
        if(CoreDialogue.instance == this)
        {
            CoreDialogue.instance = null;
        }
    }

    public Dictionary<string, Blurbber> allBlurbs;
    public Dictionary<string, int> allNpcs;

    public static Dictionary<string,Blurbber> GetAllBlurbs()
    {
        Dictionary<string, Blurbber> _b = new Dictionary<string, Blurbber>();

        _b.Add("what", WhatWhat());
        _b.Add("head_1",Head_1());
        _b.Add("head_2", Head_2());
        _b.Add("head_done", Head_done());
        _b.Add("pride_1", Pride_1());

        return _b;
    }

    public Blurbber DefaultResponse()
    {
        return allBlurbs["what"];
    }

    public static Blurbber WhatWhat()
    {
        Blurbber m = new Blurbber();
        m.blurbs = new List<TextBoxBlurb>();

        m.blurbs.Add(TextBoxBlurb.AddSimple("What was that?"));

        return m;
    }

    //Place holder
    public static Blurbber Head_1()
    {
        Blurbber m = new Blurbber();
        m.blurbs = new List<TextBoxBlurb>();

        m.blurbs.Add(TextBoxBlurb.AddSpecialBlurb("<color=yellow>Hello</color> There!", "Hello"));
        m.blurbs.Add(TextBoxBlurb.AddBranchBlurb("<color=blue>So lonely</color> sigh", "Hello","head_2"));

        return m;
    }

    public static Blurbber Head_2()
    {
        Blurbber m = new Blurbber();
        m.blurbs = new List<TextBoxBlurb>();
        m.blurbs.Add(TextBoxBlurb.AddSimple("Yes yes hello friend! Have my head!"));
        m.blurbs.Add(TextBoxBlurb.ChangeNpcId("head_1", 1));

        return m;
    }

    public static Blurbber Head_done()
    {
        Blurbber m = new Blurbber();
        m.blurbs = new List<TextBoxBlurb>();

        m.blurbs.Add(TextBoxBlurb.AddSimple("Be gone then friend good luck to you!"));

        return m;
    }


    public static Blurbber Pride_1()
    {
        Blurbber m = new Blurbber();
        m.blurbs = new List<TextBoxBlurb>();

        m.blurbs.Add(TextBoxBlurb.AddSpecialBlurb("Oh, a <color=yellow>newcomer</color>, just what we needed.", "newcomer"));
        m.blurbs.Add(TextBoxBlurb.AddSimple("Do you think you can just waltz in as if you owned the place?"));
        m.blurbs.Add(TextBoxBlurb.AddSimple("Who do you think keeps this place from falling apart?"));
        m.blurbs.Add(TextBoxBlurb.AddSimple("It is I and no one else. "));

        //m.blurbs.Add(TextBoxBlurb.AddBranchBlurb("Ancilla, <color=blue>the great</color>, if you will. ", "none", "pride_2"));

        return m;
    }

}
