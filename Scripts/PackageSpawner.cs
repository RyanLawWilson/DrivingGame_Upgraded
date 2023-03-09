using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PackageSpawner : MonoBehaviour
{
    [SerializeField] GameObject spawnLocationsContainer;
    [SerializeField] GameObject packagesContainer;
    [SerializeField] List<Package> listOfSpawnablePackages;
    [SerializeField] int numberOfPackagesOnGameStart = 10;
    [SerializeField] Transform[] packageSpawns;

    private void Awake() {
        packageSpawns = spawnLocationsContainer.GetComponentsInChildren<Transform>().Where(o => o.tag == "Package Spawn Location").ToArray();
    }

    private void Start() {
        SpawnPackages(numberOfPackagesOnGameStart);
    }

    public void SpawnPackage(int spawnPoint = -1) {
        if (spawnPoint == -1) {spawnPoint = Random.Range(0, packageSpawns.Length);}
    }

    public void SpawnPackages(int quantity) {
        for (int i = 0; i < quantity; i++) {
            Vector3 spawnLocation = packageSpawns[Random.Range(0, packageSpawns.Length)].position;
            Instantiate(
                listOfSpawnablePackages[Random.Range(0, listOfSpawnablePackages.Count)],
                spawnLocation,
                Quaternion.identity,
                packagesContainer.transform);
        }
    }

    public List<Package> GetListOfSpawnablePackages() {
        return listOfSpawnablePackages;
    }

    public GameObject GetPackagesContainer() {
        return packagesContainer;
    }

}
