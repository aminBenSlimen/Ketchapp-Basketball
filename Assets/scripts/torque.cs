using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class torque : MonoBehaviour
{
    public GameObject ball;
    public folwMouse connecte;

    // Update is called once per frame
    void Update()
    { 
            transform.position = ball.transform.position;
        if(Mathf.Abs(ball.GetComponent<Rigidbody2D>().velocity.y )>1 && connecte.connected)
            ball.transform.localEulerAngles = new Vector3(ball.transform.localEulerAngles.y, transform.localEulerAngles.y, ball.transform.localEulerAngles.y);
    }

    public void addTorque()
    {
        GetComponent<Rigidbody>().AddTorque(Vector3.up * 2500*Time.deltaTime);
    }
    public void stopTorque()
    {
        transform.localEulerAngles = Vector3.zero;
        ball.transform.localEulerAngles = Vector3.zero;
    }
}
