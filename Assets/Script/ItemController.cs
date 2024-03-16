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
            CollectItem();
            AudioManager.instance.PlaySFX(AudioManager.instance.itemCollected);
            Destroy(gameObject);
        }
    }

    public void CollectItem()
    {
        itemCollected++;
    }

    public static int GetItemsCollected()
    {
        return itemCollected;
    }

    public static void ResetItemsCollected()
    {
        itemCollected = 0;
    }
}
