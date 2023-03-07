using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeKeeper : MonoBehaviour
{
    [Header("Time Stuff")]
    [SerializeField] int startingDay = 1;
    [SerializeField] int currentDay;
    [SerializeField] bool isMorning = true;
    [SerializeField] float currentTimeInMinutes = 0;
    [SerializeField] int currentTimeInHours = 8;
    [SerializeField] int timeIncrementPerSecond = 15;
    [SerializeField] int incrementAfterNSeconds = 1;
    private Coroutine incrementTimeCoroutine;
    private float timeSinceGameStart = 0;

    [Header("Time GUI Elements")]
    [SerializeField] TextMeshProUGUI dayText;
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] TextMeshProUGUI timePausedSymbol;

    private void Awake() {
        timeText.text = $"{currentTimeInHours.ToString("00")}:{currentTimeInMinutes.ToString("00")} {(isMorning ? "AM" : "PM")}";
    }

    private void Start() {
        incrementTimeCoroutine = StartCoroutine(IncrementTime());
    }

    private void Update() {
        timeSinceGameStart += Time.deltaTime;
    }

    private IEnumerator IncrementTime() {
        while (true) {
            yield return new WaitForSeconds(incrementAfterNSeconds);

            currentTimeInMinutes += timeIncrementPerSecond;

            if (currentTimeInMinutes == 60) {
                currentTimeInHours++;
                if (currentTimeInHours == 12) {
                    isMorning = !isMorning;
                }
                currentTimeInMinutes = 0;
            }
            if (currentTimeInHours == 13) {
                currentTimeInHours = 1;
            }

            timeText.text = $"{currentTimeInHours.ToString("00")}:{currentTimeInMinutes.ToString("00")} {(isMorning ? "AM" : "PM")}";

            if (isMorning && currentTimeInHours == 2 && currentTimeInMinutes == 0) {
                StartNextDay();
            }
        }
    }

    private void StartNextDay() {
        // Fade screen to black.
        // Transition to "Dawn of the Next Day" text or something.
        // Transition to the next scene.
    }

    public void StopIncrementTime() {
        timePausedSymbol.gameObject.SetActive(true);
        StopCoroutine(incrementTimeCoroutine);
    }

    public void StartIncrementTime() {
        incrementTimeCoroutine = StartCoroutine(IncrementTime());
        timePausedSymbol.gameObject.SetActive(false);
    }
}
