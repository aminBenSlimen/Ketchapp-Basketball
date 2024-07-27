
using UnityEngine;
public class connect : MonoBehaviour
{

    public folwMouse connecte;
    public animations wall;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "spot")
        {
            
            
            if (collision.collider.GetType().ToString() == "UnityEngine.EdgeCollider2D")
            {
                // perfect
                if(connecte.ball.gravityScale == 3)
                     FindObjectOfType<audioManger>().Play("drop");
                connecte.connected = true;
                connecte.perfectColl = true;
            }
            else
            {
                FindObjectOfType<audioManger>().Play("bounce");
            }
        }
        
        else if(collision.gameObject.name == "leftWall")
        {
            wall.begin(new Vector3(-1.6f, transform.position.y, 0), Quaternion.Euler(0,0,0));
            FindObjectOfType<audioManger>().Play("wallSplash");
        }
        else if (collision.gameObject.name == "rightWall")
        {
            wall.begin(new Vector3(1.6f, transform.position.y, 0), Quaternion.Euler(0, 0, -180));
            FindObjectOfType<audioManger>().Play("wallSplash");
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        
            connecte.perfectColl = false;
        if (collision.gameObject.tag == "spot")
        {
            if(collision.collider.GetType().ToString() == "UnityEngine.EdgeCollider2D")
                connecte.connected = false;
        }
    }
}
