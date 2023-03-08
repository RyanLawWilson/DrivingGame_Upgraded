using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Delivery : MonoBehaviour
{
    [SerializeField] Color32 hasPackageColor = new Color32(1,1,1,1);
    [SerializeField] Color32 noPackageColor = new Color32(1,1,1,1);

    SpriteRenderer spriteRenderer;

    Color packageColor;


    [SerializeField] InventoryManager inventoryManager;
    [SerializeField] int money = 500;
    [SerializeField] TextMeshProUGUI moneyText;

    private void Awake() {
        moneyText.text = money.ToString("C0");
    }

    // Gets 'this' Sprite Renderer component
    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    bool hasPackage;
    
    // The 'other' parameter gives information about the entity we bumped into.
    private void OnCollisionEnter2D(Collision2D other) {
        Debug.Log("You must construct additional pylons");
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Package" && inventoryManager.GetIsFull()) {return;}

        if (other.tag == "Package" && !inventoryManager.GetIsFull()) {
            // Put package in inventory

            inventoryManager.PickUpPackage(other.gameObject);

            other.gameObject.SetActive(false);
        }

        if (other.tag == "Customer") {
            DeliverPackage(other.GetComponent<Customer>());
        }
        
    }

    private void DeliverPackage(Customer customer)
    {
        if (inventoryManager.GetInventorySlots().Count == 0) {Debug.Log("You have no inventory slots");return;}

        List<int> acceptablePackageSizes = customer.GetAcceptablePackageSizesList();
        List<InventorySlot> inventorySlots = inventoryManager.GetInventorySlots();

        foreach (InventorySlot slot in inventorySlots) {
            Package package = slot.GetCurrentPackage();
            if (package == null) {continue;}

            if (acceptablePackageSizes.Contains(package.GetSize())) {
                slot.RemovePackage();
                inventoryManager.SetIsFull(false);

                money += package.GetValue();
                moneyText.text = money.ToString("C0");
            }
        }

        inventoryManager.ReorganizeInventory();
    }

    public int GetMoney() {
        return money;
    }

    public void SetMoney(int newAmount) {
        money = newAmount;
        moneyText.text = money.ToString("C0");
    }
}
