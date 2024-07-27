using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class folwMouse : MonoBehaviour
{
    public bool ShotActive;
    public GameObject gzal;
    public score score;
    private float RandomPosX;
    public animations basketAnim;
    public animations basketAnimRev;
    public animations boom;
    public animations ballSmockAnim;
    public bool perfectColl;
    public torque BallTorque ;
    public List<GameObject> baskets;
    public bool isClicked = false;
    public GameObject basketPrefab;
    public Sprite griTop;
    public Sprite GriDown;
    public Sprite redTop;
    public Sprite redDown;
    public GameObject curentBasket;
    public Rigidbody2D ball;
    public GameObject cam;
    public Vector2 dir;
    float insialGzalScal;
    [SerializeField]
    public bool connected = false;

    private void Start()
    {
       
        insialGzalScal =1;
        connected = false;
        baskets.Add(Instantiate(basketPrefab, ball.transform.position-new Vector3(0,2,0),Quaternion.identity));
        baskets.Add(Instantiate(basketPrefab, ball.transform.position + new Vector3(-3, 0, 0), Quaternion.identity));

        curentBasket = baskets[0];
    }
    void Update()
    {
        
        
            
        for (int i = 0; i < baskets.Count; i++)
        {
            updateCurentBasket(i);
        }
        if (Input.GetMouseButtonDown(0) && connected)
        {
            isClicked = true;
            ball.velocity = Vector2.zero;
            ball.gravityScale = 0;
        }
        if (Input.GetMouseButtonUp(0) && isClicked)
        {

            ball.gravityScale = 3f;
            isClicked = false;
            if(ShotActive)
              luanch();

            ball.AddTorque(Random.Range(-10,10)*1000*Time.deltaTime); 
        }
        if (connected && ball.velocity.y >1)
        {
            BallTorque.stopTorque();
        }
       if(curentBasket == baskets[baskets.Count-1] && perfectColl)
        {
            addBasket();
        }

       if(baskets[0]!=null)
        destroyBaskets();
    }

    private void FixedUpdate()
    {
        if (isClicked)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            dir = (transform.position - curentBasket.transform.position).normalized;
            curentBasket.transform.up = -dir;
            ball.gameObject.transform.up = -dir;
            gzal = GameObject.Find("gzal");
            if(gzal != null)
            {
                float smoth = 1 / Mathf.Abs(insialGzalScal + Mathf.Abs(dir.magnitude)  - gzal.transform.localScale.y);
                float target = insialGzalScal + Mathf.Abs(dir.magnitude);
                target = Mathf.Clamp(target,insialGzalScal,insialGzalScal*2);
                gzal.transform.localScale = Vector3.Lerp(gzal.transform.localScale, new Vector3(gzal.transform.localScale.x, insialGzalScal + Mathf.Abs(dir.magnitude) , gzal.transform.localScale.z),smoth);
                
            }
          
        }

        if((Mathf.Abs(ball.velocity.y) > 5 || connected)&& (ball.transform.position.y  > curentBasket.transform.position.y-5))
             UpdateCameraMovement(ball.transform.position.y);
        

        
        if ((!connected || !isClicked) && gzal != null)
        {
            gzal.transform.localScale = Vector3.Lerp(gzal.transform.localScale, Vector3.one, 0.5f);
            if (!connected && !isClicked)
            {
                /*if(Mathf.Abs(ball.velocity.x) > Mathf.Abs(ball.velocity.y))
                {
                    float randomYpos = Random.Range(ball.transform.position.y - .5f, ball.transform.position.y + .5f);
                    ballSmockAnim.begin(new Vector3(ball.transform.position.x, randomYpos, ball.transform.position.z), Quaternion.identity);
                }else
                {
                    float randomXpos = Random.Range(ball.transform.position.x - .5f, ball.transform.position.x + .5f);
                    ballSmockAnim.begin(new Vector3(randomXpos, ball.transform.position.y, ball.transform.position.z), Quaternion.identity);
                }*/
                ballSmockAnim.begin(new Vector3(ball.transform.position.x, ball.transform.position.y, ball.transform.position.z), Quaternion.identity);
            }
                
        }
       
        
    }
    void luanch()
    {
        boom.begin(curentBasket.transform.position, curentBasket.transform.rotation);
        ball.AddForce(new Vector2(-dir.x,-dir.y) * 250000f * Time.deltaTime);
        BallTorque.addTorque();
        FindObjectOfType<audioManger>().Play("luanch");
    }
    void addBasket()
    {
            score.AddScore();
            RandomPosX = Random.Range(-2.21f, 2.39f);
            basketAnim.begin(new Vector3(RandomPosX, curentBasket.transform.position.y + 3, 0),Quaternion.identity);
            baskets.Add(Instantiate(basketPrefab, new Vector3(RandomPosX,curentBasket.transform.position.y + 3, 0), Quaternion.identity));// new basket
            curentBasket = baskets[baskets.Count - 1];
    }

    void destroyBaskets()
    {
        
        if (baskets.Count > 2)
        {
            basketAnimRev.begin(new Vector3(baskets[0].transform.position.x, baskets[0].transform.position.y, 0),Quaternion.identity);
            Destroy(baskets[0]);
            baskets.RemoveAt(0);
        }
    }

   void updateCurentBasket(int i)
    {
        if (Mathf.Abs(baskets[i].transform.position.y - ball.transform.position.y) < .5f && Mathf.Abs(baskets[i].transform.position.x - ball.transform.position.x) < .5f)
        {
            curentBasket = baskets[i];
            if (Mathf.Abs(ball.velocity.x) < 0.2f && Mathf.Abs(ball.velocity.y) < 0.2f && perfectColl)
            {
                curentBasket.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = griTop;
                curentBasket.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = GriDown;

            }
        }
        else if ((Mathf.Abs(ball.velocity.x) < 0.2f && Mathf.Abs(ball.velocity.y) < 0.2f) && perfectColl)
        {
            baskets[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = redTop;
            baskets[i].transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = redDown;
        }
    }
    void UpdateCameraMovement(float target)
    {
        float smoth = 1/Mathf.Abs(cam.transform.position.y - target);
        cam.transform.position = new Vector3(cam.transform.position.x, Mathf.Lerp(cam.transform.position.y, target,smoth), cam.transform.position.z);
    }

}
