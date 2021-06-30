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

    private void Update()
    {
        InteractWithObject();
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

            //Check for items nearby 
            if(itemsInArea.Length > 0)
            {
                for(int i = 0; i < itemsInArea.Length; i++)
                {
                    if(i == 0)
                    {
                        if(interactButtonPressed)
                        {
                            GameObject item = itemsInArea[0].gameObject;
                            inventory.isFull[i] = true;
                            item.transform.SetParent(inventory.slots[i].transform);
                            item.transform.position = inventory.slots[i].transform.position;
                            interactButtonPressed = false;
                            break;
                        }
                    }
                }
            }

            //check for interactables nearby
            if(interactablesInArea.Length > 0)
            {
                for(int i = 0; i < interactablesInArea.Length; i++)
                {
                    if(i == 0)
                    {
                        if (interactButtonPressed)
                        {
                            GameObject interactableItem = interactablesInArea[0].gameObject;
                            GameObject requiredItem = interactableItem.GetComponent<InteractableItem>().requiredItem;
                            Debug.Log(requiredItem.name);
                            if(inventory.slots[i].transform.GetChild(0).gameObject == requiredItem)
                            {
                                interactableItem.GetComponent<InteractableItem>().Interact();
                                Destroy(inventory.slots[i].transform.GetChild(0).gameObject);
                            }
                            else
                            {
                                Debug.Log("Don't have the required item");
                            }
                            interactButtonPressed = false;
                        }
                    }
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, interactRange);
        Gizmos.color = Color.red;
    }

    public void buttonPressed()
    {
        interactButtonPressed = true;
    }
}
