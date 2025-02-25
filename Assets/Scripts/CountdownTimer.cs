using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    public float startMinutes = 1; // Start time in minutes
    private float remainingTime;
    public List<TextMeshProUGUI> timerTexts; // UI Text element to display the timer
    private Coroutine timerCoroutine;
    public void StartCountDown()
    {
        remainingTime = startMinutes * 60; // Convert minutes to seconds
        timerCoroutine = StartCoroutine(TimerCoroutine());
    }

    public void StopTimer()
    {
        StopCoroutine(timerCoroutine);
    }

    private IEnumerator TimerCoroutine()
    {
        while (remainingTime > 0)
        {
            var minutes = Mathf.FloorToInt(remainingTime / 60);
            var seconds = Mathf.FloorToInt(remainingTime % 60);

            timerTexts.ForEach(timer=> timer.text = $"{minutes:00}:{seconds:00}");

            yield return new WaitForSeconds(1f);
            remainingTime--;
            if (remainingTime < 11f)
            {
                var audioSource = GetComponent<AudioSource>();
                audioSource.volume = 0.75f;
                Escape_Room.Audio.AudioManager.Instance.Play("Beep Agro");
            }
            else
            {
                Escape_Room.Audio.AudioManager.Instance.Play("Beep");
            }
        }

        // Timer has reached zero
        timerTexts.ForEach(timer=> timer.text = "00:00");
        Debug.Log("Done");
    }
}
