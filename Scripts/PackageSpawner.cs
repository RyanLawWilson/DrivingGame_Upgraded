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
    [SerializeField] SpawnLocation[] packageSpawns;

    private void Awake() {
        packageSpawns = spawnLocationsContainer.GetComponentsInChildren<SpawnLocation>().Where(o => o.tag == "Package Spawn Location").ToArray();
    }

    private void Start() {
        SpawnPackages(numberOfPackagesOnGameStart);
    }

    public void SpawnPackages(int quantity) {
        for (int i = 0; i < quantity; i++) {
            SpawnLocation randomSpawnPoint = packageSpawns[Random.Range(0, packageSpawns.Length)];
    
            randomSpawnPoint.SpawnPackage();

            // Instantiate(
            //     listOfSpawnablePackages[Random.Range(0, listOfSpawnablePackages.Count)],
            //     randomSpawnPoint,
            //     Quaternion.identity,
            //     packagesContainer.transform);
        }
    }

    public List<Package> GetListOfSpawnablePackages() {
        return listOfSpawnablePackages;
    }

    public GameObject GetPackagesContainer() {
        return packagesContainer;
    }

}
