using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    // allows us to set the game's time scale to a new value with a transitional rate effect between the old and new time scale
   public void ManipulateTime(float newTime, float duration) {
        // we do not want the time scale to be complete zero since we may have loops or other objects that still need to execute correctly in the game
        // this selection statement is a failsafe
        if (Time.timeScale == 0)
            Time.timeScale = 0.1f;

        StartCoroutine(FadeTo(newTime, duration));
    }

    IEnumerator FadeTo(float value, float time) {

        for (float t = 0f; t < 1; t += Time.deltaTime / time) {// this ensure we are actually incrementing the time based on the difference between frame rather than some arbitrary speed to fast to notice transition

            Time.timeScale = Mathf.Lerp(Time.timeScale, value, t); // slides the timescale from its current value to the desired value by "t" amount, stepping smoothly closer to the goal with each iteration of the loop

            // if time is close enough to zero, we'll just set it to zero so we don't have a very long scale down time as we try to reach zero
            if (Mathf.Abs(value - Time.timeScale) < .01f) {
                Time.timeScale = value;
                break; //yield return false does not seem to stop the coroutine
            }

            yield return null;
        }
    }
}
