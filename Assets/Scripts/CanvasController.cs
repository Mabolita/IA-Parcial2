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
    public int gearsCount;
    public float seconds;
    public float minute;
    public float hours;
    public bool hack;
    public bool dash;
    public bool activeImage;

    private void Start()
    {
        timeText.text = hours + ":" + minute + ":" + seconds;
        gearsText.text = gearsCount.ToString();
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
