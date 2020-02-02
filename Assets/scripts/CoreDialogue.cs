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
        allNpcs.Add("envy_1", 0);
        allNpcs.Add("wrath_1", 0);




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
        _b.Add("pride_2", Pride_2());
        _b.Add("pride_3", Pride_3());

        _b.Add("envy_1", Envy_1());
        _b.Add("envy_2", Envy_2());

        _b.Add("wrath_1", Wrath_1());
        _b.Add("wrath_2", Wrath_2());
        _b.Add("wrath_3", Wrath_3());


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

        m.blurbs.Add(TextBoxBlurb.AddSimple("Well, hello there!"));
        m.blurbs.Add(TextBoxBlurb.AddSimple("I'm impressed you can move."));
        m.blurbs.Add(TextBoxBlurb.AddSimple("In here, nobody has the will to move anymore."));
        m.blurbs.Add(TextBoxBlurb.AddSimple("I once wished to see heaven, a long time ago."));
        m.blurbs.Add(TextBoxBlurb.AddBranchBlurb("I now wish somebody could <color=blue>show me</color>.", "I will", "head_2", "head_2"));
        m.blurbs.Add(TextBoxBlurb.AddSpecialBlurb("I guess, <color=yellow>I will</color> just stay here until the end of time.", "I will"));
        return m;
    }

    public static Blurbber Head_2()
    {
        Blurbber m = new Blurbber();
        m.blurbs = new List<TextBoxBlurb>();
        m.blurbs.Add(TextBoxBlurb.AddSimple("Will you take me there?"));
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

    // TODO: To lower case before comparing strings

    /// 
    /// === PRIDE ===
    ///

    public static Blurbber Pride_1()
    {
        Blurbber m = new Blurbber();
        m.blurbs = new List<TextBoxBlurb>();

        m.blurbs.Add(TextBoxBlurb.AddSpecialBlurb("Oh, a <color=yellow>newcomer</color>, just what we needed.", "newcomer"));
        m.blurbs.Add(TextBoxBlurb.AddSimple("Do you think you can just waltz in as if you owned the place?"));
        m.blurbs.Add(TextBoxBlurb.AddSimple("Who do you think keeps this place from falling apart?"));
        m.blurbs.Add(TextBoxBlurb.AddSimple("It is I and no one else."));
        m.blurbs.Add(TextBoxBlurb.AddBranchBlurb("Call me, <color=blue>the great</color>, if you will. ", "none", "pride_2", "pride_2"));
        m.blurbs.Add(TextBoxBlurb.AddSimple("The one that stood where no one else could."));
        m.blurbs.Add(TextBoxBlurb.AddSimple("The one that spoke when no one else would."));
        m.blurbs.Add(TextBoxBlurb.AddBranchBlurb("The one that stands selflessly and, <color=blue>unrecognized</color>. ", "Thank you", "pride_3", "pride_2"));
        m.blurbs.Add(TextBoxBlurb.AddSimple("The one."));
        return m;
    }

    public static Blurbber Pride_2()
    {
        Blurbber m = new Blurbber();
        m.blurbs = new List<TextBoxBlurb>();
        m.blurbs.Add(TextBoxBlurb.AddSimple("I AM the greatest."));
        return m;
    }

    public static Blurbber Pride_3()
    {
        Blurbber m = new Blurbber();
        m.blurbs = new List<TextBoxBlurb>();
        m.blurbs.Add(TextBoxBlurb.AddSimple("Yes, yes!"));
        m.blurbs.Add(TextBoxBlurb.AddSimple("I see you are one who understands."));
        m.blurbs.Add(TextBoxBlurb.AddSimple("One who will one day be worthy of even my praise."));
        m.blurbs.Add(TextBoxBlurb.AddSimple("Take this, little one, may they help you in your quest."));
        return m;
    }

    /// 
    /// === ENVY ===
    ///

    public static Blurbber Envy_1()
    {
        Blurbber m = new Blurbber();
        m.blurbs = new List<TextBoxBlurb>();
        m.blurbs.Add(TextBoxBlurb.AddSimple("Hey! Hey! Did you know?"));
        m.blurbs.Add(TextBoxBlurb.AddSimple("That old man is actually quite the crybaby?"));
        m.blurbs.Add(TextBoxBlurb.AddSimple("He flaunts those strong arms of his all day..."));
        m.blurbs.Add(TextBoxBlurb.AddSimple("But when night comes..."));
        m.blurbs.Add(TextBoxBlurb.AddSimple("Ohh, when night comes..."));
        m.blurbs.Add(TextBoxBlurb.AddSimple("Hey! Hey! Did you know?"));
        m.blurbs.Add(TextBoxBlurb.AddBranchBlurb("When night comes he becomes quite the <color=blue>crybaby</color>.", "none", "envy_2", "envy_2"));
        m.blurbs.Add(TextBoxBlurb.AddSimple("He flaunts those strong legs of his all day..."));
        m.blurbs.Add(TextBoxBlurb.AddSimple("But when night comes..."));
        m.blurbs.Add(TextBoxBlurb.AddSimple("Ohh, when night comes..."));
        m.blurbs.Add(TextBoxBlurb.AddSimple("Hey! Hey! Did you know?"));
        m.blurbs.Add(TextBoxBlurb.AddSimple("That cute girl is actually quite the talker?"));
        m.blurbs.Add(TextBoxBlurb.AddBranchBlurb("She flaunts her <color=blue>good looks</color>.", "none", "envy_2", "envy_2"));        
        m.blurbs.Add(TextBoxBlurb.AddSimple("But when day comes..."));
        m.blurbs.Add(TextBoxBlurb.AddSimple("Ohh, when day comes..."));
        m.blurbs.Add(TextBoxBlurb.AddSimple("She talks! and talks! and talks!"));
        m.blurbs.Add(TextBoxBlurb.AddSpecialBlurb("But it's not like <color=yellow>I want to listen</color>, you know?", "I want to listen"));
        return m;
    }
   
    public static Blurbber Envy_2()
    {
        Blurbber m = new Blurbber();
        m.blurbs = new List<TextBoxBlurb>();
        m.blurbs.Add(TextBoxBlurb.AddSimple("Hey! Hey! Did you know?"));
        return m;
    }

    /// 
    /// === WRATH ===
    ///

    public static Blurbber Wrath_1()
    {
        Blurbber m = new Blurbber();
        m.blurbs = new List<TextBoxBlurb>();
        // TODO: Add Branch Blurb if right send to wrath_3 if wrong send to wrath 2
        m.blurbs.Add(TextBoxBlurb.AddBranchBlurb("<color=blue>Get lost</color>.", "I want to listen", "wrath_3", "wrath_2"));        
        return m;
    }

    public static Blurbber Wrath_2()
    {
        Blurbber m = new Blurbber();
        m.blurbs = new List<TextBoxBlurb>();
        m.blurbs.Add(TextBoxBlurb.AddSimple("Get lost."));
        return m;
    }

    public static Blurbber Wrath_3()
    {
        Blurbber m = new Blurbber();
        m.blurbs = new List<TextBoxBlurb>();
        m.blurbs.Add(TextBoxBlurb.AddSimple("I hate them! I hate them so much!"));
        m.blurbs.Add(TextBoxBlurb.AddSimple("The other day I was minding my own business when I heard that scrap pile talking trash about me."));
        m.blurbs.Add(TextBoxBlurb.AddSimple("I wish he minded his own business. As I do."));
        m.blurbs.Add(TextBoxBlurb.AddSimple("I hate the way he floats around. Just because you can float, it doesn't mean you can cut the line!"));
        m.blurbs.Add(TextBoxBlurb.AddSimple("I hate how he talks in rhymes. Just get to the point!"));
        m.blurbs.Add(TextBoxBlurb.AddSimple("And don't get me started on the old man."));
        m.blurbs.Add(TextBoxBlurb.AddSimple("I hate how he carries that rock around. It makes the floor dirty!"));
        m.blurbs.Add(TextBoxBlurb.AddSimple("I hate the way he looks at me."));
        m.blurbs.Add(TextBoxBlurb.AddSimple("I wish he minded his own business. As I do."));
        m.blurbs.Add(TextBoxBlurb.AddSimple("I hate that I'm trapped here with those weirdoes!"));
        m.blurbs.Add(TextBoxBlurb.AddSimple("I hate them! I hate them! I hate them!"));
        m.blurbs.Add(TextBoxBlurb.AddSimple("Wow, I really needed to vent."));
        m.blurbs.Add(TextBoxBlurb.AddSpecialBlurb("<color=yellow>Thank you</color>.", "Thank you"));
        return m;
    }

    ///
    /// Sloth
    ///
 
    public static Blurbber Sloth_1()
    {
        Blurbber m = new Blurbber();
        m.blurbs = new List<TextBoxBlurb>();
        m.blurbs.Add(TextBoxBlurb.AddSpecialBlurb("Busy. Can't <color=yellow>talk</color> ", "talk"));
        m.blurbs.Add(TextBoxBlurb.AddSimple("So busy. Can't stop."));
        m.blurbs.Add(TextBoxBlurb.AddSimple("Too busy. Too much."));
        m.blurbs.Add(TextBoxBlurb.AddBranchBlurb("Can't stop. <color=blue>Won't stop</color>.", "none", "sloth_2", "sloth_2"));        
        m.blurbs.Add(TextBoxBlurb.AddBranchBlurb("Won't stop. <color=blue>Can't stop</color>.", "you can", "sloth_3", "sloth_2"));        
        return m;
    }
 
    public static Blurbber Sloth_2()
    {
        Blurbber m = new Blurbber();
        m.blurbs = new List<TextBoxBlurb>();
        m.blurbs.Add(TextBoxBlurb.AddSimple("Busy. So busy."));
        return m;
    }

    public static Blurbber Sloth_3()
    {
        Blurbber m = new Blurbber();
        m.blurbs = new List<TextBoxBlurb>();
        m.blurbs.Add(TextBoxBlurb.AddSimple("I can?"));
        m.blurbs.Add(TextBoxBlurb.AddSimple("Can I?"));
        m.blurbs.Add(TextBoxBlurb.AddSimple("Can I stop?"));
        m.blurbs.Add(TextBoxBlurb.AddSimple("Stop?"));
        m.blurbs.Add(TextBoxBlurb.AddSimple("Dont stop."));
        // TODO: Give legs to D4T3
        return m;
    }

    ///
    /// Greed
    ///
 
    public static Blurbber Greed_1()
    {
        Blurbber m = new Blurbber();
        m.blurbs = new List<TextBoxBlurb>();
        m.blurbs.Add(TextBoxBlurb.AddSimple("Ya gonna steal mah stuff as well youngin?"));
        m.blurbs.Add(TextBoxBlurb.AddSimple("Le me tell you about that big fellah over there.")); 
        m.blurbs.Add(TextBoxBlurb.AddSpecialBlurb("Day in and day out, it is food, food, <color=yellow>food</color>...", "food"));
        m.blurbs.Add(TextBoxBlurb.AddBranchBlurb("I ain't letting him, or you, or <color=blue>anybody</color> touch my stuff.", "none", "greed_2", "greed_2"));
        m.blurbs.Add(TextBoxBlurb.AddSimple("I worked for it, and I worked hard."));
        m.blurbs.Add(TextBoxBlurb.AddBranchBlurb("Ain't gonna <color=blue>share</color>. So get goin now.", "fair", "greed_3", "greed_2"));
        return m;
    }

    public static Blurbber Greed_2()
    {
        Blurbber m = new Blurbber();
        m.blurbs = new List<TextBoxBlurb>();
        m.blurbs.Add(TextBoxBlurb.AddSimple("Get lost. I ain't sharing."));
        return m;
    }


    public static Blurbber Greed_3()
    {
        Blurbber m = new Blurbber();
        m.blurbs = new List<TextBoxBlurb>();
        m.blurbs.Add(TextBoxBlurb.AddSimple("Fair you say?"));
        m.blurbs.Add(TextBoxBlurb.AddSimple("Le me tell ya what is fair."));
        m.blurbs.Add(TextBoxBlurb.AddSimple("Ya work hard, ya get stuff."));
        m.blurbs.Add(TextBoxBlurb.AddSpecialBlurb("I did it before, so <color=yellow>you can</color> do it too.", "you can"));
        return m;
    }

    ///
    /// Gluttony
    ///
 
    public static Blurbber Gluttony_1()
    {
        Blurbber m = new Blurbber();
        m.blurbs = new List<TextBoxBlurb>();
        m.blurbs.Add(TextBoxBlurb.AddSimple("Blarg blar blaggg blaaa"));
        m.blurbs.Add(TextBoxBlurb.AddBranchBlurb("Blaaa <color=blue>rlaggg</color> ghhh", "gluttony_2", "gluttony_2"));
        m.blurbs.Add(TextBoxBlurb.AddSimple("Blarg Bla"));
        m.blurbs.Add(TextBoxBlurb.AddSimple("Foood"));
        m.blurbs.Add(TextBoxBlurb.AddSimple("Food Food Food"));
        m.blurbs.Add(TextBoxBlurb.AddSimple("Eat eat eat"));
        m.blurbs.Add(TextBoxBlurb.AddBranchBlurb("<color=blue>Noooooooooow</color>", "take it easy", "gluttony_3", "gluttony_2"));
        return m;
    }

    public static Blurbber Gluttony_2()
    {
        Blurbber m = new Blurbber();
        m.blurbs = new List<TextBoxBlurb>();
        m.blurbs.Add(TextBoxBlurb.AddSimple("Blar Blaaag Blard"));
        m.blurbs.Add(TextBoxBlurb.AddSimple("Laar Blaag Bla"));
        m.blurbs.Add(TextBoxBlurb.AddSimple("Blar Bla Bla"));
        return m;
    }
 
    public static Blurbber Gluttony_3()
    {
        Blurbber m = new Blurbber();
        m.blurbs = new List<TextBoxBlurb>();
        m.blurbs.Add(TextBoxBlurb.AddSpecialBlurb("Not fair. Not fair. Not <color=yellow>fair</color>.", "fair"));
        m.blurbs.Add(TextBoxBlurb.AddSimple("No faaaaaair!"));
        return m;
    }

    ///
    /// Lust
    ///
 
    public static Blurbber Lust_1()
    {
        Blurbber m = new Blurbber();
        m.blurbs = new List<TextBoxBlurb>();
        m.blurbs.Add(TextBoxBlurb.AddBranchBlurb("<color=blue>Welcome to my lair</color>", "talk", "lust_3", "lust_2"));
        return m;
    }

    public static Blurbber Lust_2()
    {
        Blurbber m = new Blurbber();
        m.blurbs = new List<TextBoxBlurb>();
        m.blurbs.Add(TextBoxBlurb.AddSimple("Relax for a while."));
        return m;
    }

    public static Blurbber Lust_3()
    {
        Blurbber m = new Blurbber();
        m.blurbs = new List<TextBoxBlurb>();
        m.blurbs.Add(TextBoxBlurb.AddSimple("Like the coming spring."));
        m.blurbs.Add(TextBoxBlurb.AddSimple("Like the blessed rain."));
        m.blurbs.Add(TextBoxBlurb.AddSpecialBlurb("<color=yellow>Hope</color> again.", "hope"));
        m.blurbs.Add(TextBoxBlurb.AddSimple("Like bird's songs."));
        m.blurbs.Add(TextBoxBlurb.AddSimple("Like a new sunny day."));
        m.blurbs.Add(TextBoxBlurb.AddSpecialBlurb("<color=yellow>Shine</color> again.", "shine"));
        m.blurbs.Add(TextBoxBlurb.AddSimple("Like the free bird."));
        m.blurbs.Add(TextBoxBlurb.AddSimple("Like rainbows decorating dreams."));
        m.blurbs.Add(TextBoxBlurb.AddSpecialBlurb("<color=yellow>Smile</color> again.", "shine"));
        m.blurbs.Add(TextBoxBlurb.AddSimple("..."));
        m.blurbs.Add(TextBoxBlurb.AddSpecialBlurb("Sit back my friend, lets <color=yellow>take it easy</color> for a while.", "take it easy"));
        return m;
    }

    //    m.blurbs.Add(TextBoxBlurb.AddSimple(""));
    //    m.blurbs.Add(TextBoxBlurb.AddSpecialBlurb(" <color=yellow>newcomer</color> ", "newcomer"));
    //    m.blurbs.Add(TextBoxBlurb.AddBranchBlurb("She flaunts her <color=blue>good looks</color>.", "none", "envy_2"));        



}
