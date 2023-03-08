using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageSpawner : MonoBehaviour
{
    [SerializeField] GameObject spawnLocationsContainer;
    [SerializeField] GameObject packagesContainer;
    [SerializeField] List<Package> listOfSpawnablePackages;
    [SerializeField] int numberOfPackagesOnGameStart = 10;
    Transform[] packageSpawns;

    private void Awake() {
        packageSpawns = spawnLocationsContainer.GetComponentsInChildren<Transform>();
    }

    private void Start() {
        SpawnPackages(numberOfPackagesOnGameStart);
    }

    public void SpawnPackage(int spawnPoint = -1) {
        if (spawnPoint == -1) {spawnPoint = Random.Range(0, packageSpawns.Length);}


    }

    public void SpawnPackages(int quantity) {
        for (int i = 0; i < quantity; i++) {
            Instantiate(
                listOfSpawnablePackages[Random.Range(0, listOfSpawnablePackages.Count)],
                packageSpawns[Random.Range(0, Random.Range(0, packageSpawns.Length))].position,
                Quaternion.identity,
                packagesContainer.transform);
        }
    }

}
