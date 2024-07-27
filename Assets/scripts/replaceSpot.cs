using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class replaceSpot : MonoBehaviour
{
    public folwMouse spot;
   // public GameObject newSpot;
  //  public Camera cam;
    private void OnCollisionEnter2D(Collision2D collision)
    {   if(spot.curentBasket.gameObject != collision.gameObject && collision.gameObject.tag != "else")
        {
            
            //float xPos = Random.Range(-3f, 3f);
            spot.basketPrefab.GetComponent<EdgeCollider2D>().isTrigger = true;
            spot.basketPrefab.GetComponent<BoxCollider2D>().isTrigger = true;
          //  spot.curentBasket = collision.gameObject;
          //  Instantiate(newSpot,new Vector3(xPos,(spot.basketPrefab.transform.position.y+4),0f),Quaternion.identity);
           // Vector3 nextPosition = new Vector3(cam.transform.position.x, transform.position.y, cam.transform.position.z);
           // cam.transform.position = Vector3.Lerp(transform.position, nextPosition, 100f);
          
        }
        
    }
}
