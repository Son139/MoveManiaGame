using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    //public static ItemManager instance;
    public static int itemCollected;

    //private void Awake()
    //{
    //    if (instance == null)
    //    {
    //        instance = this;
    //        DontDestroyOnLoad(gameObject);
    //    }
    //    else
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CollectStar();
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
