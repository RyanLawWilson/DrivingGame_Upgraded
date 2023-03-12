using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLocation : MonoBehaviour
{
    [SerializeField] string tagOfWhatWeAreSpawning;
    [SerializeField] PackageSpawner packageSpawner;
    [SerializeField] bool isClearOfObstacles = true;
    Package spawnedPackage;
    Coroutine checkSpawn;

    /// <summary>
    /// Creates a random package at this SpawnLocation's position.
    /// </summary>
    /// <returns>Bool of whether or not the package was successfully created.
    /// Returns true if the package was created.  Returns false if the creation failed.</returns>
    public bool SpawnPackage() {
        if (!isClearOfObstacles) {Debug.Log("This Spawn Point is not clear of obstacles.");return false;}

        isClearOfObstacles = false;

        List<Package> listOfSpawnablePackages = packageSpawner.GetListOfSpawnablePackages();

        spawnedPackage = Instantiate(
                listOfSpawnablePackages[Random.Range(0, listOfSpawnablePackages.Count)],
                transform.position,
                Quaternion.identity,
                packageSpawner.GetPackagesContainer().transform);

        checkSpawn = StartCoroutine(CheckIfSpawnedPackageIsStillThere());
        return true;
    }

    private IEnumerator CheckIfSpawnedPackageIsStillThere() {
        while(true) {
            if (spawnedPackage.gameObject.activeInHierarchy && spawnedPackage != null) {
                isClearOfObstacles = false;
            } else {
                // Package has left the spawn area.
                isClearOfObstacles = true;
                StopCoroutine(checkSpawn);
                checkSpawn = null;
            }
            yield return new WaitForSeconds(1);
        }
    }
}
