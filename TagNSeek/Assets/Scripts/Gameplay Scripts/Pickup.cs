using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private Inventory inventory;
    public GameObject item;

    public bool pickedUp = false;

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    private void Update()
    {
       
    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Player"))
    //    {
    //        for(int i = 0; i < inventory.slots.Length; i++)
    //        {
    //            if(inventory.isFull[i] == false)
    //            {
    //                //add item
    //                inventory.isFull[i] = true;
    //                Instantiate(item, inventory.slots[i].transform, false);
    //                Destroy(gameObject);
    //                break;
    //            }
    //        }
    //    }
    //}
}
