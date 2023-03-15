using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerDetectionRadius : MonoBehaviour
{
    Dictionary<int, int> packageSizeToSpriteIdDictionry;

    [SerializeField] TextMeshPro floatingText;
    private Customer customer;

    private void Awake() {
        customer = gameObject.GetComponentInParent<Customer>();
        if (customer == null) {return;}
        
        packageSizeToSpriteIdDictionry = new Dictionary<int, int>() {
            {1,0}, {2,3}, {3,2}, {5,1}, {7,4}
        };

        List<int> acceptablePackages = customer.GetAcceptablePackageSizesList();

        floatingText.text = string.Empty;
        foreach (int size in acceptablePackages) {
            floatingText.text += $"<sprite={packageSizeToSpriteIdDictionry[size]}> ";
        }

        floatingText.text = floatingText.text.TrimEnd();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            floatingText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Player") {
            floatingText.gameObject.SetActive(false);
        }
    }
}
