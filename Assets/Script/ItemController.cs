using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public static int itemCollected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CollectStar();
            AudioManager.instance.PlaySFX(AudioManager.instance.itemCollected);
            Destroy(gameObject);
        }
    }

    public void CollectStar()
    {
        itemCollected++;
    }

    public static int GetStarsCollected()
    {
        return itemCollected;
    }

    public static void ResetStarsCollected()
    {
        itemCollected = 0;
    }
}
