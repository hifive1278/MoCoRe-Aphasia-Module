using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bandwidth_2 : MonoBehaviour
{
    //https://gamedev.stackexchange.com/questions/105378/how-to-change-the-transparency-of-an-object-in-unity-c-scripting
    //https://www.youtube.com/watch?v=hXUMdWXr2so
    //https://www.youtube.com/watch?v=ALE9Z_wxavg

    public KeyCode IncreaseAlpha;
    public KeyCode DecreaseAlpha;
    public float alphaLevel = .0f;
    public GameObject firstBandwidth;
    public GameObject secondBandwidth;
    public GameObject thirdBandwidth;
    private Material firstMat;
    private Material secondMat;
    private Material thirdMat;
    public GameObject ball;
    public static string sector = "";

    // start is called before the first frame update
    void Start()
    {
        firstBandwidth = gameObject;
        secondBandwidth = gameObject;
        thirdBandwidth = gameObject;
        firstMat = firstBandwidth.GetComponent<Renderer>().material;
        secondMat = secondBandwidth.GetComponent<Renderer>().material;
        thirdMat = thirdBandwidth.GetComponent<Renderer>().material;
        ChangeAlpha(alphaLevel, "Bandwidth1");
        ChangeAlpha(alphaLevel, "Bandwidth2");
        ChangeAlpha(alphaLevel, "Bandwidth3");
    }

    // update is called once per frame
    void Update()
    {
        float distanceFromBand1 = (float)(Math.Abs(ball.transform.position.z - 0.664));
        float distanceFromBand2 = (float)(Math.Abs(ball.transform.position.x - (-1.003)));
        float distanceFromBand3 = (float)(Math.Abs(ball.transform.position.z - 2.534));

        //Bandwidth one sections
        bool lowerRectangle1 = (-2.4 < ball.transform.position.x && ball.transform.position.x < 0.4) && (ball.transform.position.z <= 0.76);
        bool midLeft1 = (-2.4 < ball.transform.position.x && ball.transform.position.x < -1.4) && (0.76 < ball.transform.position.z && ball.transform.position.z < 1.06);
        bool midRight1 = (-0.6 < ball.transform.position.x && ball.transform.position.x < 0.4) && (0.76 < ball.transform.position.z && ball.transform.position.z < 1.06);
        bool thinLeft1 = (-1.4 < ball.transform.position.x && ball.transform.position.x < -1.1) && (0.76 < ball.transform.position.z && ball.transform.position.z < 0.92);
        bool thinRight1 = (-0.9 < ball.transform.position.x && ball.transform.position.x < -0.6) && (0.76 < ball.transform.position.z && ball.transform.position.z < 0.92);

        //Bandwidth two sections
        bool mainStrip2 = (-1.1 < ball.transform.position.x && ball.transform.position.x < -0.9) && (0.76 < ball.transform.position.z && ball.transform.position.z < 2.85);
        bool leftStrip2 = (-1.4 < ball.transform.position.x && ball.transform.position.x < -1.1) && (0.92 < ball.transform.position.z && ball.transform.position.z < 2.7);
        bool rightStrip2 = (-0.9 < ball.transform.position.x && ball.transform.position.x < -0.6) && (0.92 < ball.transform.position.z && ball.transform.position.z < 2.2);

        //Bandwidth three sections
        bool leftSide3 = (-0.9 < ball.transform.position.x && ball.transform.position.x < -0.6) && (2.2 < ball.transform.position.z && ball.transform.position.z < 3.0);
        bool rightSide3 = (-0.6 <= ball.transform.position.x) && (2.1 < ball.transform.position.z && ball.transform.position.z < 3.0);

        if ((lowerRectangle1) || (midLeft1) || (midRight1) || (thinLeft1) || (thinRight1))
        {
            sector = "Bandwidth1";
        }
        else if ((mainStrip2) || (leftStrip2) || (rightStrip2))
        {
            sector = "Bandwidth2";
        }
        else if (leftSide3 || rightSide3)
        {
            sector = "Bandwidth3";
        }

        if (sector == "Bandwidth1")
        {
            if (distanceFromBand1 > 0.12)
            {

                if (alphaLevel >= 2)
                {
                    alphaLevel = 2;
                    //ChangeAlpha(alphaLevel, sector); //does this need to be here?
                }

                else
                {
                    alphaLevel = distanceFromBand1 * 2.3f;
                    ChangeAlpha(alphaLevel, sector);
                }

            }

            else
            {
                alphaLevel = 0.0f;
            }
        }

        else if (sector == "Bandwidth2")
        {
            if (distanceFromBand2 > 0.12)
            {

                if (alphaLevel >= 2)
                {
                    alphaLevel = 2;
                    //ChangeAlpha(alphaLevel, sector); //does this need to be here?
                }

                else
                {
                    alphaLevel = distanceFromBand2 * 2.3f;
                    ChangeAlpha(alphaLevel, sector);
                }

            }

            else
            {
                alphaLevel = 0.0f;
            }
        }

        else if (sector == "Bandwidth3")
        {
            if (distanceFromBand3 > 0.12)
            {

                if (alphaLevel >= 2)
                {
                    alphaLevel = 2;
                    //ChangeAlpha(alphaLevel, sector); //does this need to be here?
                }

                else
                {
                    alphaLevel = distanceFromBand3 * 2.3f;
                    ChangeAlpha(alphaLevel, sector);
                }

            }

            else
            {
                alphaLevel = 0.0f;
            }
        }


    }

    void ChangeAlpha(float alphaVal, string sector)
    {
        if (sector == "Bandwidth1")
        {
            Color oldColor = firstMat.color;
            Color newColor = new Color(oldColor.r, oldColor.g, oldColor.b, alphaVal);
            firstMat.SetColor("_Color", newColor);
        }
        else if (sector == "Bandwidth2")
        {
            Color oldColor = secondMat.color;
            Color newColor = new Color(oldColor.r, oldColor.g, oldColor.b, alphaVal);
            secondMat.SetColor("_Color", newColor);
        }
        else if (sector == "Bandwidth3")
        {
            Color oldColor = thirdMat.color;
            Color newColor = new Color(oldColor.r, oldColor.g, oldColor.b, alphaVal);
            thirdMat.SetColor("_Color", newColor);
        }
    }
}

