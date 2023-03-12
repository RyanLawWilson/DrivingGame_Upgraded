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
    [SerializeField] List<SpawnLocation> packageSpawns;

    private void Awake() {
        packageSpawns = spawnLocationsContainer.GetComponentsInChildren<SpawnLocation>().Where(o => o.tag == "Package Spawn Location").ToList();

        //Randomize all of the spawn locations on Awake.
        packageSpawns.Shuffle();
    }

    private void Start() {
        SpawnPackages(numberOfPackagesOnGameStart);
    }

    public void SpawnPackages(int quantity) {
        // If you are trying to spawn more packages than there are locations, set quantity to # of locations.
        if (quantity > packageSpawns.Count) {quantity = packageSpawns.Count;}

        for (int i = 0; i < quantity; i++) {    
            packageSpawns[i].SpawnPackage();
        }
    }

    public List<Package> GetListOfSpawnablePackages() {
        return listOfSpawnablePackages;
    }

    public GameObject GetPackagesContainer() {
        return packagesContainer;
    }
}
