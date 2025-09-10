using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private float spawnOffset;

    private void Start()
    {
        StartCoroutine(SpawnAsteroids());
    }

    IEnumerator SpawnAsteroids()
    {
        while (true)
        {
            Vector3 screenPoint = CalculateCoordinatesBehindTheScreen();
            int enemyIndex = Random.Range(0, enemyPrefabs.Length);
            Instantiate(enemyPrefabs[enemyIndex], screenPoint, Quaternion.identity);

            yield return new WaitForSeconds(5f);
        }
    }

    private Vector3 CalculateCoordinatesBehindTheScreen()
    {
        int side = Random.Range(0, 4);

        Vector3 viewportPoint = Vector3.zero;

        switch (side)
        {
            case 0:
                viewportPoint = new Vector3(Random.Range(0f, 1f), 1f + spawnOffset, 10f);
                break;
            case 1:
                viewportPoint = new Vector3(1f + spawnOffset, Random.Range(0f, 1f), 10f);
                break;
            case 2:
                viewportPoint = new Vector3(Random.Range(0f, 1f), -spawnOffset, 10f);
                break;
            case 3:
                viewportPoint = new Vector3(-spawnOffset, Random.Range(0f, 1f), 10f);
                break;
        }

        return Camera.main.ViewportToWorldPoint(viewportPoint);
    }
}
