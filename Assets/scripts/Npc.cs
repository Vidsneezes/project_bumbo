using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
    public string npc_name;

    public Camera viewCamera;

    public List<string> intialId;

    private void Awake()
    {
        HideCamera();
    }

    public string GetId()
    {
        int npcId = CoreDialogue.instance.allNpcs[npc_name];
        return intialId[npcId];
    }

    public void ShowCamera()
    {
        viewCamera.gameObject.SetActive(true);
        viewCamera.depth = 5;
    }

    public void HideCamera()
    {
        viewCamera.gameObject.SetActive(false);
    }

}
