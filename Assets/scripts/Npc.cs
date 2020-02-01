using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
    public string npc_name;

    public List<string> intialId;

    public string GetId()
    {
        int npcId = CoreDialogue.instance.allNpcs[npc_name];
        return intialId[npcId];
    }

}
