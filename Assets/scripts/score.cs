using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class score : MonoBehaviour
{
    public Text text;
    public int Score = 0;
    // Start is called before the first frame update
    void Start()
    {
        Score = 0;
        text.text = Score.ToString();
    }

    // Update is called once per frame
    public void AddScore()
    {
        Score++;
        text.text = Score.ToString();
    }
}
