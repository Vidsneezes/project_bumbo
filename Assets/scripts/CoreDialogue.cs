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
        _b.Add("pride_1",Pride_1());
        _b.Add("pride_2", Pride_2());
        _b.Add("pride_done", Pride_done());

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
    public static Blurbber Pride_1()
    {
        Blurbber m = new Blurbber();
        m.blurbs = new List<TextBoxBlurb>();

        m.blurbs.Add(TextBoxBlurb.AddSpecialBlurb("<color=yellow>Hello</color> There!", "Hello"));
        m.blurbs.Add(TextBoxBlurb.AddBranchBlurb("<color=blue>So lonely</color> sigh", "Hello","pride_2"));

        return m;
    }

    public static Blurbber Pride_2()
    {
        Blurbber m = new Blurbber();
        m.blurbs = new List<TextBoxBlurb>();
        m.blurbs.Add(TextBoxBlurb.AddSimple("Yes yes hello friend! Have my head!"));
        m.blurbs.Add(TextBoxBlurb.ChangeNpcId("pride_1", 1));


        return m;
    }

    public static Blurbber Pride_done()
    {
        Blurbber m = new Blurbber();
        m.blurbs = new List<TextBoxBlurb>();

        m.blurbs.Add(TextBoxBlurb.AddSimple("Be gone then friend good luck to you!"));

        return m;
    }

}
