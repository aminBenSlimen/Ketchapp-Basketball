using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trajocery : MonoBehaviour
{
    GameObject trajocerParent;
    public GameObject[] dots;
    public int numberOfDots;
    bool shoot;
    public Vector2 shotForce;
    public folwMouse ball;
    // Update is called once per frame

    private void Start()
    {
        shoot = false;
        trajocerParent = GameObject.Find("trajectory");

        for (int i = 0; i < 40; i++)
        {
            dots[i] = GameObject.Find("dot (" + i + ")");
        }
        for (int i = numberOfDots; i < 40; i++)
        {
            GameObject.Find("dot (" + i + ")").SetActive(false);
        }
        trajocerParent.gameObject.SetActive(false);

    }
    void Update()
    {
        Vector3 fingerPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        fingerPos.z = 0;
        if (ball.isClicked)
            shotForce = (transform.position - fingerPos) * 3;

        if (40 < numberOfDots)
            numberOfDots = 40;

        if (Input.GetMouseButtonDown(0) && ball.connected)
        {
            trajocerParent.gameObject.SetActive(true);
            shoot = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            trajocerParent.gameObject.SetActive(false);
            shoot = false;

        }

        if (shoot)
        {
            for (int k = 0; k < numberOfDots; k++)
            {

                float x1 = transform.position.x + shotForce.x * Time.fixedDeltaTime * (5 * k + 5);    //X position for each point is found
                float y1 = transform.position.y + shotForce.y * Time.fixedDeltaTime * (5 * k + 5) - (-Physics2D.gravity.y / 2f * Time.fixedDeltaTime * Time.fixedDeltaTime * (5 * k + 3) * (5 * k + 3));
                rebown(k, x1, y1);
                opacityUpdate(k);

            }
        }


    }
    void opacityUpdate(int k)
    {
        if (Mathf.Abs(shotForce.y) < 7 && Mathf.Abs(shotForce.x) < 7)
        {
            if (Mathf.Abs(shotForce.y) < 4.5f && Mathf.Abs(shotForce.x) < 4.5f)
            {
                ball.ShotActive = false;
            }
            dots[k].GetComponent<SpriteRenderer>().material.color = new Color(dots[k].GetComponent<SpriteRenderer>().material.color.r, dots[k].GetComponent<SpriteRenderer>().material.color.g, dots[k].GetComponent<SpriteRenderer>().material.color.b, (Mathf.Abs(shotForce.magnitude) * (float)(7 / 3) - (float)(28 / 3)) / 7);

        }
        else
        {
            ball.ShotActive = true;
            float lerp = Mathf.Lerp(dots[k].GetComponent<SpriteRenderer>().material.color.a, 1, .4f);
            dots[k].GetComponent<SpriteRenderer>().material.color = new Color(dots[k].GetComponent<SpriteRenderer>().material.color.r, dots[k].GetComponent<SpriteRenderer>().material.color.g, dots[k].GetComponent<SpriteRenderer>().material.color.b, lerp);
        }
    }

    void rebown(int k,float x1,float y1)
    {
        if (x1 < -4.1f)
        {
            //flip it to right

            float x = x1 + (Mathf.Abs(x1 + 4.1f) * 2);
            dots[k].transform.position = new Vector3(x, y1, dots[k].transform.position.z);
        }
        else if (x1 > 4.1f)
        {
            //flip it to left
            float x = x1 - (Mathf.Abs(x1 - 4.1f) * 2);
            dots[k].transform.position = new Vector3(x, y1, dots[k].transform.position.z);
        }
        else
        {
            dots[k].transform.position = new Vector3(x1, y1, dots[k].transform.position.z);
        }
    }
}
