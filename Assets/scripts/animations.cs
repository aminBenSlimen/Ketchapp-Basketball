using System;
using UnityEngine;

public class animations : MonoBehaviour
{
    //public animation[] anim;
    public void begin( Vector3 pos,Quaternion rot)
    {
       // Array.Find(anim, animation => animation.name == name);
        Instantiate(this.gameObject, pos, rot);      
    }
    public void stop()
    {
        if (this.gameObject != null)
            Destroy(this.gameObject);

       if(this.gameObject.name == "boom" || this.gameObject.name == "wallAnim")
            Destroy(transform.parent.gameObject);
    }
}
