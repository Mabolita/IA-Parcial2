using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI gearsText;
    public Image dashImage;
    public Image dashFillImage;
    public Image hackImage;
    public Image hackFillImage;
    public PlayerController pc;
    public static int gearsCount;
    public static float seconds;
    public static float minute;
    public static float hundredths;
    public static bool lose = false;
    public float maxMinuteTime;
    public bool hack;
    public bool dash;
    public bool activeImage;

    private void Start()
    {
        minute = maxMinuteTime;
        timeText.text = minute + ":" + seconds + ":" + hundredths;
        gearsText.text = gearsCount.ToString();
        pc = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        if (hack || dash)
        {
            if (!activeImage)
            {
                if (hack)
                {
                    hackImage.gameObject.SetActive(true);
                    hackFillImage.gameObject.SetActive(true);
                }

                if (dash)
                {

                    dashImage.gameObject.SetActive(true);
                    dashFillImage.gameObject.SetActive(true);
                }
                activeImage = true;
            }
            else
            {
                if (hack)
                {
                    hackImage.fillAmount = ((100 * pc.powerTimer) / pc.powerTimerMax) / 100;
                }
                if (dash)
                {
                    dashImage.fillAmount = ((100 * pc.powerTimer) / pc.powerTimerMax) / 100;
                }
            }
        }
        if (!lose)
        {
            hundredths -= Time.deltaTime;
        }
        if (minute < 0 && seconds < 0 && hundredths < 0)
        {
            hundredths = 0;
            minute = 0;
            seconds = 0;
            lose = true;
        }

        if (hundredths <= 0 && seconds > 0)
        {
            hundredths = 0.99999f;
            seconds--;
        }
        if (seconds <= 0 && minute > 0)
        {
            seconds = 60;
            minute--;
        }
        string currentHundredths = hundredths.ToString();
        if (hundredths <= 0)
        {
            currentHundredths = "0";
        }
        else
        {
            currentHundredths = hundredths.ToString().Substring(2, 2);
        }
        timeText.text = minute + ":" + seconds + ":" + currentHundredths;
        gearsText.text = gearsCount.ToString();
    }
}
