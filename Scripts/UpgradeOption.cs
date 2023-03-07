using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeOption : MonoBehaviour
{
    [SerializeField] string upgradeName;

    [SerializeField] int price;

    [SerializeField] string subjectOfUpgrade = "TopSpeed";

    [SerializeField] float upgradeAmount = 2;

    ShopManager shopManager;

    private void Awake() {
        shopManager = FindObjectOfType<ShopManager>();
    }

    public void OptionClicked() {
        shopManager.PurchaseUpgrade(subjectOfUpgrade, upgradeAmount, price);

        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
