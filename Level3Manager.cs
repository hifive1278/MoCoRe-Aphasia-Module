using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class Level3Manager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI countDown;
    private int score;
    public GameObject ball;
    private bool ballInHole;
    public static int Level3Score;
    private float startTime;
    public static float Level3Time;
    public TextMeshProUGUI prompt;
    public AudioSource errorNoise;
    public AudioSource successNoise;
    private float lastXValue;
    private float lastZValue;
    public static float Level3Distance;
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
            prompt.color = new Color32(0, 255, 0, 255);
            Level3Time = Time.time - startTime;
        }
        else if (collision.gameObject.name == "flag_wrong1")
        {
            print("flag_wrong1");
            prompt.color = new Color32(255, 0, 0, 255);
            UpdateScore(10);
            errorNoise.Play(5);
            //ChangeColor(0);
        }
    }

    //IEnumerator ChangeColor(int number)      Supposed to change prompt back to white after 1.5 seconds...not registering tho.
    //{
    //    print("Before yield");
    //    yield return new WaitForSeconds(1.5f);
    //    prompt.color = new Color32(255, 255, 255, 255);
    //    print("Color switched to white...");
    //}

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
        Level3Score = score;
        //yield return new WaitForSeconds(1.5f);
        //SceneManager.LoadScene("Level 4");

        Send();

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
        Level3Distance += distanceToAdd;
        lastXValue = xValue;
        lastZValue = zValue;
    }

    public IEnumerator Post(int Score1, int Score2, int Score3, float Time1, float Time2, float Time3, float Dist1, float Dist2, float Dist3)
    {
        WWWForm form = new WWWForm();
        form.AddField("entry.1254448702", Score1);
        form.AddField("entry.16436953", Score2);
        form.AddField("entry.1573396103", Score3);
        form.AddField("entry.2015174098", Time1.ToString("#.000"));
        form.AddField("entry.723328435", Time2.ToString("#.000"));
        form.AddField("entry.794581889", Time3.ToString("#.000"));
        form.AddField("entry.1360676134", Dist1.ToString("#.0000"));
        form.AddField("entry.815178765", Dist2.ToString("#.0000"));
        form.AddField("entry.789352640", Dist3.ToString("#.0000"));

        byte[] rawData = form.data;
        //UnityWebRequest www = UnityWebRequest.Post(BASE_URL, form);
        //yield return www.SendWebRequest();
        WWW www = new WWW(BASE_URL, rawData);
        yield return www;
    }

    public void Send()
    {
        int Score1 = Level1Manager.Level1Score;
        int Score2 = Level2Manager.Level2Score;
        int Score3 = Level3Manager.Level3Score;
        float Time1 = Level1Manager.Level1Time;
        float Time2 = Level2Manager.Level2Time;
        float Time3 = Level3Manager.Level3Time;
        float Dist1 = Level1Manager.Level1Distance;
        float Dist2 = Level2Manager.Level2Distance;
        float Dist3 = Level3Manager.Level3Distance;

        StartCoroutine(Post(Score1, Score2, Score3, Time1, Time2, Time3, Dist1, Dist2, Dist3));
    }

}
