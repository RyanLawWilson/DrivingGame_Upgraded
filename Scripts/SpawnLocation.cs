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

    public void SpawnPackage(int spawnPoint = -1) {
        if (!isClearOfObstacles) {return;}

        isClearOfObstacles = false;

        List<Package> listOfSpawnablePackages = packageSpawner.GetListOfSpawnablePackages();

        spawnedPackage = Instantiate(
                listOfSpawnablePackages[Random.Range(0, listOfSpawnablePackages.Count)],
                transform.position,
                Quaternion.identity,
                packageSpawner.GetPackagesContainer().transform);

        checkSpawn = StartCoroutine(CheckIfSpawnedPackageIsStillThere());
    }

    private IEnumerator CheckIfSpawnedPackageIsStillThere() {
        while(true) {
            if (spawnedPackage.gameObject.activeInHierarchy && spawnedPackage != null) {
                if (Vector3.Distance(spawnedPackage.GetComponent<Transform>().position, transform.position) < 0.5f) {
                    // The Package is still on the spawn point
                    isClearOfObstacles = false;
                    yield return new WaitForSeconds(1);
                } else {
                    // Package has left the spawn area.
                    isClearOfObstacles = true;
                    StopCoroutine(checkSpawn);
                }
            }
        }
    }
}
