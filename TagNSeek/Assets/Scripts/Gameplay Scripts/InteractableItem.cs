using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItem : MonoBehaviour
{
    public GameObject requiredItem;

    //What happens when you interact with item
    public void Interact()
    {
        //test case for now. Will change w=interact action depending on object using switch cases
        Debug.Log("Gate Open");
        Destroy(gameObject);
    }
}
