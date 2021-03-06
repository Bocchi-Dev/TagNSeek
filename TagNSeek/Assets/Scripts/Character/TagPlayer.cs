using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TagPlayer : MonoBehaviour
{
    [Range(1, 3)]
    public float tagRange = 3f;
    public LayerMask whatIsUntagged;

    public Image tagButton;
    public float tagCooldown = 5f;
    bool isCooldown;

    Vector2 currentSmallestDistance;
    Vector2 currentDistance;

    int taggedPlayer;
    public Material tagTargetMaterial;
    public Material defaultSpriteMaterial;

    [HideInInspector] public GameObject[] playersToClear;
    bool tagButtonPressed;

    private void Awake()
    {
        playersToClear = GameObject.FindGameObjectsWithTag("Player");
    }

    void Start()
    {
        tagButton.fillAmount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        Tag();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, tagRange);
    }

    public void Tag()
    {
        //Detects any untagged player in range
        Collider2D[] playersInArea = Physics2D.OverlapCircleAll(transform.position, tagRange, whatIsUntagged);

        if (playersInArea.Length > 1) //Check if 2 or more players are within tag range
        {
            //Checks which player is the closest to the tagger so they would be the tag target
            for (int i = 0; i < playersInArea.Length; i++)
            {
                currentDistance = playersInArea[i].GetComponent<Transform>().position;

                if (i == 0)
                {
                    currentSmallestDistance = currentDistance;
                    taggedPlayer = i;
                    continue;
                }

                else
                {
                    if (Vector2.Distance(currentDistance, transform.position) < Vector2.Distance(currentSmallestDistance, transform.position))
                    {
                        currentSmallestDistance = currentDistance;
                        taggedPlayer = i;
                    }
                }
                TagTarget(playersInArea[taggedPlayer]);
            }
        }

        else if (playersInArea.Length == 1) //Check if only one player is within tag range
        {
            taggedPlayer = 0;
            TagTarget(playersInArea[0]);
        }

        else //Removes red outline if no player is within tag range
        {
            foreach (GameObject player in playersToClear)
            {
                player.GetComponent<SpriteRenderer>().material = defaultSpriteMaterial;
            }

        }

        //When the tag button gets pressed, tag the player
        if (tagButtonPressed == true && isCooldown == false)
        {
            tagButtonPressed = false;

            if (playersInArea.Length == 0)
            {
                return;
            }

            transform.position = playersInArea[taggedPlayer].transform.position;
            playersInArea[taggedPlayer].GetComponent<SpriteRenderer>().color = Color.red;
            playersInArea[taggedPlayer].gameObject.layer = 6; //6 is Tagged
            playersInArea[taggedPlayer].GetComponent<SpriteRenderer>().material = defaultSpriteMaterial;

            isCooldown = true;
            tagButton.fillAmount = 0;

        }

        //Cooldown for the tag button
        if (isCooldown)
        {
            tagButton.fillAmount += 1 / tagCooldown * Time.deltaTime;

            if (tagButton.fillAmount >= 1)
            {
                tagButton.fillAmount = 1;
                isCooldown = false;
                currentSmallestDistance = Vector2.zero;
            }
        }
    }

    //Shows a red outline when an untagged is in range of getting tagged
    public void TagTarget(Collider2D player)
    {
        if (Vector2.Distance(transform.position, player.transform.position) < tagRange)
        {
            player.GetComponent<SpriteRenderer>().material = tagTargetMaterial;
        }
        else
        {
            player.GetComponent<SpriteRenderer>().material = defaultSpriteMaterial;
        }
    }

    public void TagButton()
    {
        tagButtonPressed = true;
    }
}
