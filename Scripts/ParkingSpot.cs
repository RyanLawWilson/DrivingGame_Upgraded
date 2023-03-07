using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParkingSpot : MonoBehaviour
{
    [SerializeField] GameObject ShopPanel;
    private TimeKeeper timeKeeper;

    private void Awake() {
        timeKeeper = FindObjectOfType<TimeKeeper>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            ShopPanel.SetActive(true);
            timeKeeper.StopIncrementTime();
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Player") {
            ShopPanel.SetActive(false);
            timeKeeper.StartIncrementTime();
        }
    }
}
