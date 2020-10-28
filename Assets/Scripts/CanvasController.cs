using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI gearsText;
    public int gearsCount;
    public float seconds;
    public float minute;
    public float hours;

    private void Start()
    {
        timeText.text = hours + ":" + minute + ":" + seconds;
        gearsText.text = gearsCount.ToString();
    }

    private void Update()
    {
        seconds += Time.deltaTime;
        if (seconds >= 60)
        {
            seconds = 0;
            minute++;
        }
        if (minute >= 60)
        {
            minute = 0;
            hours++;
        }
        timeText.text = hours + ":" + minute + ":" + (int)seconds;
        gearsText.text = gearsCount.ToString();
    }
}
