using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class Level1Manager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI countDown;
    private int score;
    public GameObject ball;
    public static int Level1Score;
    public static float Level1Time;
    private float startTime;
    public TextMeshProUGUI answer;
    public AudioSource successNoise;
    private float lastXValue;
    private float lastZValue;
    public static float Level1Distance;
    //private GameObject eventSys;

    // Start is called before the first frame update
    void Start()
    {
        print("Starting");
        lastXValue = ball.transform.position.x;
        lastZValue = ball.transform.position.z;
        score = 0;
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

    IEnumerator StartGame(int scoreToAdd)
    {
        print("1: Top of StartGame");
        while ((0.6f > ball.transform.position.z || ball.transform.position.z > 0.73f) || (-0.1f > ball.transform.position.x || ball.transform.position.x > 0.1f))// Level 1 hole
        {

            scoreToAdd = (int)(10f * Math.Abs(ball.transform.position.z - 0.664f));
            UpdateScore(scoreToAdd);
            yield return new WaitForSeconds(0.2f);

        }
        countDown.text = "Hole in one!";
        successNoise.Play(0);
        answer.color = new Color32(0, 255, 0, 255);
        Level1Score = score;
        Level1Time = Time.time - startTime;
        //print("Time: " + Level1Time);
        yield return new WaitForSeconds(1.5f);

        //SceneManager.LoadScene("Level 2", LoadSceneMode.Single);
        SceneManager.LoadScene("Level 2");

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
        Level1Distance += distanceToAdd;
        lastXValue = xValue;
        lastZValue = zValue;
    }
}
