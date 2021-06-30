using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    private Inventory inventory;

    [Range(1, 3)]
    public float interactRange;
    public LayerMask whatIsItem;
    public LayerMask whatIsInteractable;

    public bool interactButtonPressed = false;

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    public void InteractWithObject()
    {
        if(this.gameObject.GetComponent<Player>().tagged)
        {
            //tagged stuff here
        }
        else
        {
            Collider2D[] itemsInArea = Physics2D.OverlapCircleAll(transform.position, interactRange, whatIsItem);
            Collider2D[] interactablesInArea = Physics2D.OverlapCircleAll(transform.position, interactRange, whatIsInteractable);

            if(itemsInArea.Length > 1)
            {
                for(int i = 0; i < itemsInArea.Length; i++)
                {
                    if(i == 0)
                    {
                        if(interactButtonPressed)
                        {
                            GameObject item = itemsInArea[0].gameObject;
                            Debug.Log(item.name);
                            inventory.isFull[i] = true;
                            item.transform.position = inventory.slots[i].transform.position;
                            interactButtonPressed = false;
                            break;
                        }
                    }
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        
    }

    public void buttonPressed()
    {
        interactButtonPressed = true;
        Debug.Log(interactButtonPressed);
    }
}
