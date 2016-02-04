using UnityEngine;
using System.Collections;

public class WorldGeneration : MonoBehaviour {

    public Transform[] plants;
    public Transform[] Enemies;
    int randomNumF;
    int randomNumE;
    public int numOfF;
    public int numOfE;
    float distFromSpawn;

	void Start ()
    {
        distFromSpawn = 10;
        SpawnFoliage();
        //SpawnEnemies();
       
	}
    void SpawnFoliage()
    {
        randomNumF = Random.Range(12, 28);
        Debug.Log(randomNumF);
        for (int i = 0; i < numOfF; i++)
        {
            for (int j = 0; j < randomNumF; j++)
            {
                Instantiate(plants[i], new Vector2(Random.Range(EnemyScript.XLimit.x, EnemyScript.XLimit.y), Random.Range(EnemyScript.YLimit.x, EnemyScript.YLimit.y)), Quaternion.identity);
            }
        }
    }
    void SpawnEnemies()
    {
        randomNumE = Random.Range(4, 12);
        Debug.Log(randomNumE);
        for (int i = 0; i <numOfE; i++)
        {
            for (int j = 0; j < randomNumE; j++)
            {
                distFromSpawn*=i;
                Vector2 spawnPoint = new Vector2(Random.Range(EnemyScript.XLimit.x - distFromSpawn, EnemyScript.XLimit.y + distFromSpawn), Random.Range(EnemyScript.YLimit.x - distFromSpawn, EnemyScript.YLimit.y + distFromSpawn));
                Debug.Log(spawnPoint);
                Instantiate(Enemies[i], spawnPoint, Quaternion.identity);
            }
        }
    }
}
