using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour, IInteractable
{


    public string GetInteractPrompt()
    {
        string str = "[L Click] Grab";
        return str;
    }

    public void OnInteract()
    {
        
    }

}
