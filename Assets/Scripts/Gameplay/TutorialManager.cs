using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class TutorialManager : MonoBehaviour
{
    
    public GameObject[] popUps;
    private int popUpIndex;
    public float WaitTime = 5f;

    void Update()
    {
        
        if (WaitTime <= 0)
        {

            for (int i = 0; i < popUps.Length; i++)
            {
                if (i == popUpIndex)
                {
                    popUps[i].SetActive(true);
                }
                else
                {
                    popUps[i].SetActive(false);
                    
                }
            }
        }
        else
        {
            WaitTime -= Time.deltaTime;
        }

        if (popUpIndex == 0)
        {
            
            if (CrossPlatformInputManager.GetButtonDown("up") || CrossPlatformInputManager.GetButtonDown("down")
               || CrossPlatformInputManager.GetButtonDown("left") || CrossPlatformInputManager.GetButtonDown("right")
               || CrossPlatformInputManager.GetButtonDown("interact") || CrossPlatformInputManager.GetButtonDown("sound")
                    || CrossPlatformInputManager.GetButtonDown("menu-open"))
            {
                popUpIndex++;
            }
        }


        else if (popUpIndex == 1)
        {
            if (CrossPlatformInputManager.GetButtonDown("up") || CrossPlatformInputManager.GetButtonDown("down")
               || CrossPlatformInputManager.GetButtonDown("left") || CrossPlatformInputManager.GetButtonDown("right"))
            {
                popUpIndex++;
            }
        }

        else if (popUpIndex == 2)
        {
            if (CrossPlatformInputManager.GetButtonDown("interact"))
            {
                popUpIndex++;
            }
        }

        else if (popUpIndex == 3)
        {

            if (CrossPlatformInputManager.GetButtonDown("sound"))
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 4)
        {

            if (CrossPlatformInputManager.GetButtonDown("menu-open"))
            {
                popUpIndex++;
               

            }
        }

    }
}
