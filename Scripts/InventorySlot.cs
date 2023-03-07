using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] Sprite emptyInventorySlotImage;
    [SerializeField] Sprite smallPackageImage;
    [SerializeField] Sprite mediumPackageImage;
    [SerializeField] Sprite largePackageImage;
    [SerializeField] Sprite xLargePackageImage;
    [SerializeField] Sprite massivePackageImage;

    [SerializeField] Package currentPackage = null;

    InventoryManager inventoryManager;

    private void Awake() {
        inventoryManager = FindObjectOfType<InventoryManager>();
    }

    public void InsertPackage(Package package) {
        currentPackage = package;
        SetSpriteImage(package);
    }

    public void RemovePackage() {
        if (currentPackage == null) {Debug.Log("There is no package in this slot...");return;}
        
        Destroy(currentPackage);
        currentPackage = null;
        GetComponent<Image>().sprite = emptyInventorySlotImage;
    }

    public Package GetCurrentPackage() {
        return currentPackage;
    }

    public void SetCurrentPackage(Package package) {
        currentPackage = package;
        SetSpriteImage(package);
    }

    private void SetSpriteImage(Package package) {
        if (package == null) {GetComponent<Image>().sprite = emptyInventorySlotImage;return;}

        switch(package.GetSize()) {
            case 1:
                GetComponent<Image>().sprite = smallPackageImage;
                break;
            case 2:
                GetComponent<Image>().sprite = mediumPackageImage;
                break;
            case 3:
                GetComponent<Image>().sprite = largePackageImage;
                break;
            case 5:
                GetComponent<Image>().sprite = xLargePackageImage;
                break;
            case 7:
                GetComponent<Image>().sprite = massivePackageImage;
                break;
            default:
                GetComponent<Image>().sprite = emptyInventorySlotImage;
                break;
        }
    }
}
