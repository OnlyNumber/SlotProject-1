using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeBeforeRewardController : MonoBehaviour
{
    public TMPro.TMP_Text timeText;

    private void Update()
    {
        if (DataControl.Instance.GetDate().AddMinutes(5) < DateTime.Now)
        {
            timeText.text = "Ready to claim";
        }
        else
        {
            //float timeInDay = 24 * 3600;

            

            float result = (float)(FromTimeToInt(DataControl.Instance.GetDate().AddMinutes(5)) - FromTimeToInt(DateTime.Now));

            Debug.Log("Time: " + result);

            TimeSpan time = TimeSpan.FromSeconds(result);
            string text = time.ToString("hh':'mm':'ss");

            timeText.text = text.ToString();
        }
    
    
    }

    public double FromTimeToInt(DateTime date)
    {
        double year = date.Year;
        double month = date.Month;
        double day = date.Day;
        double hours = date.Hour;
        double minutes = date.Minute;
        double seconds = date.Second;

        double timeToInt = year*12*30* 24 * 3600 + month*30*24*3600+day *24*3600 + hours * 3600 + minutes * 60 + seconds;

        Debug.Log(timeToInt);

        return timeToInt;

    }

}
