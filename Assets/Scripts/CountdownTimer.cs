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

    public void StartCountDown()
    {
        remainingTime = startMinutes * 60; // Convert minutes to seconds
        StartCoroutine(TimerCoroutine());
    }

    private IEnumerator TimerCoroutine()
    {
        while (remainingTime > 0)
        {
            int minutes = Mathf.FloorToInt(remainingTime / 60);
            int seconds = Mathf.FloorToInt(remainingTime % 60);

            timerTexts.ForEach(timer=> timer.text = $"{minutes:00}:{seconds:00}");

            yield return new WaitForSeconds(1f);
            remainingTime--;
        }

        // Timer has reached zero
        timerTexts.ForEach(timer=> timer.text = "00:00");
        Debug.Log("Done");
    }
}
