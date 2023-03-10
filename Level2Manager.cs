using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class Level2Manager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI countDown;
    private int score;
    public GameObject ball;
    private bool ballInHole;
    public static int Level2Score;
    private float startTime;
    public static float Level2Time;
    public TextMeshProUGUI answer;
    public TextMeshProUGUI wrongOption1;
    public AudioSource errorNoise;
    public AudioSource successNoise;
    private float lastXValue;
    private float lastZValue;
    public static float Level2Distance;
    //private GameObject eventSys

    [SerializeField]
    public string BASE_URL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSflbng-NCsWiNO0aetSsux4i89soJGsGXCx-YTtfT-JA0pa1w/formResponse";


    // Start is called before the first frame update
    void Start()
    {
        print("Starting");
        lastXValue = ball.transform.position.x;
        lastZValue = ball.transform.position.z;
        score = 0;
        ballInHole = false;
        //eventSys = GameObject.Find("Event System");
        UpdateScore(0);
        StartCoroutine(Countdown(3));
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDistance(ball.transform.position.x, ball.transform.position.z);
    }

    IEnumerator Countdown(int seconds)
    {
        print("Counting down");

        int count = seconds;

        while (count > 0)
        {
            countDown.text = count.ToString();
            yield return new WaitForSeconds(1.5f);
            count--;
            //print((float)ball.transform.position.z);
        }

        if (count == 0)
        {
            countDown.text = "Go!";
            startTime = Time.time;
            yield return new WaitForSeconds(1.5f);
            countDown.text = "";

        }
        StartCoroutine(StartGame(0));
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "flag_right")
        {
            print("flag_right");
            successNoise.Play(0);
            ballInHole = true;
            answer.color = new Color32(0, 255, 0, 255);
            Level2Time = Time.time - startTime;
        }
        else if (collision.gameObject.name == "flag_wrong1")
        {
            print("flag_wrong1");
            wrongOption1.color = new Color32(255, 0, 0, 255);
            UpdateScore(10);
            errorNoise.Play(5);

        }
    }

    IEnumerator StartGame(int scoreToAdd)
    {
        print("2: Top of StartGame");

        while (ballInHole == false)
        {

            if (Bandwidth_2.sector == "Bandwidth1")
            {
                scoreToAdd = (int)(10f * Math.Abs(ball.transform.position.z - 0.664f));
            }
            else if (Bandwidth_2.sector == "Bandwidth2")
            {
                scoreToAdd = (int)(10f * Math.Abs(ball.transform.position.x - (-1.003)));
            }
            else if (Bandwidth_2.sector == "Bandwidth3")
            {
                scoreToAdd = (int)(10f * Math.Abs(ball.transform.position.z - 2.534));
            }

            UpdateScore(scoreToAdd);
            yield return new WaitForSeconds(0.2f);
            //print("Running right script");
        }
        countDown.text = "Hole in one!";
        Level2Score = score;
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("Level 3");

        //print(Level1Manager.Level1Score);

        StopCoroutine(StartGame(0));
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void UpdateDistance(float xValue, float zValue)
    {
        float xComponent = Mathf.Pow(lastXValue - xValue, 2);
        float zComponent = Mathf.Pow(lastZValue - zValue, 2);
        float distanceToAdd = Mathf.Sqrt(xComponent + zComponent);
        Level2Distance += distanceToAdd;
        lastXValue = xValue;
        lastZValue = zValue;
    }
}
