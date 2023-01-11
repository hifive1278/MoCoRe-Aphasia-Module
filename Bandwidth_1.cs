using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bandwidth : MonoBehaviour
{
    //https://gamedev.stackexchange.com/questions/105378/how-to-change-the-transparency-of-an-object-in-unity-c-scripting
    //https://www.youtube.com/watch?v=hXUMdWXr2so
    //https://www.youtube.com/watch?v=ALE9Z_wxavg

    public KeyCode IncreaseAlpha;
    public KeyCode DecreaseAlpha;
    public float alphaLevel = .0f;
    public GameObject currentGameObject;
    private Material currentMat;
    public GameObject ball;

    // start is called before the first frame update
    void Start()
    {
        currentGameObject = gameObject;
        currentMat = currentGameObject.GetComponent<Renderer>().material;
        ChangeAlpha(alphaLevel);
    }

    // update is called once per frame
    void Update()
    {
        float distanceFromOptimal = (float)(Math.Abs(ball.transform.position.z - 0.664));

        if (Math.Abs(ball.transform.position.z - 0.664) > 0.12)
        //ball.transform.position.z > 0.8 || ball.transform.position.z < 0.5
        {
            if (alphaLevel >= 2)
            {
                alphaLevel = 2;
                ChangeAlpha(alphaLevel);
            }
                
            else
            {
                
                    alphaLevel = distanceFromOptimal*2.3f;
                    ChangeAlpha(alphaLevel);
                
                
            }
            
        }

        else
        {
            alphaLevel = 0.0f;
        }


    }

    void ChangeAlpha(float alphaVal)
    {
        Color oldColor = currentMat.color;
        Color newColor = new Color(oldColor.r, oldColor.g, oldColor.b, alphaVal);
        currentMat.SetColor("_Color", newColor);

    }
}


