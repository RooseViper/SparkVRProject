using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    public int startMinutes = 1; // Start time in minutes
    private float remainingTime;
    public List<TextMeshProUGUI> timerTexts; // UI Text element to display the timer

    void Start()
    {
        remainingTime = startMinutes * 60; // Convert minutes to seconds
        StartCoroutine(TimerCoroutine());
    }

    public void DeductTime() => remainingTime -= 10f;

    private IEnumerator TimerCoroutine()
    {
        while (remainingTime > 0)
        {
            int minutes = Mathf.FloorToInt(remainingTime / 60);
            int seconds = Mathf.FloorToInt(remainingTime % 60);

            timerTexts.ForEach(timer=> timer.text = string.Format("{0:00}:{1:00}", minutes, seconds));

            yield return new WaitForSeconds(1f);
            remainingTime--;
        }

        // Timer has reached zero
        timerTexts.ForEach(timer=> timer.text = "00:00");
        SceneManager.LoadScene(0);
    }
}
