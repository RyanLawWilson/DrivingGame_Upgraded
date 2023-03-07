using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopManager : MonoBehaviour
{
    [SerializeField] GameObject shopPanel;
    [SerializeField] GameObject closeButton;
    Delivery delivery;
    Driver driver;


    private void Awake() {
        delivery = FindObjectOfType<Delivery>();
        driver = FindObjectOfType<Driver>();
    }


    public void CloseShop() {
        shopPanel.SetActive(false);
    }

    public void PurchaseUpgrade(string subjectOfUpgrade, float upgradeAmount, int price) {
        // If too expensive, return, else buy.
        if (price > delivery.GetMoney()) {
            return;
        } else {
            delivery.SetMoney(delivery.GetMoney() - price);
            switch (subjectOfUpgrade) {
                case "TopSpeed":
                    driver.SetTopSpeed(driver.GetTopSpeed() + upgradeAmount);
                    break;
                default:
                    break;
            }
            
        }
    }
}
