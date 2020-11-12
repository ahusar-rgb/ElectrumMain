
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] GameObject[] decorations = new GameObject[5];

    [SerializeField] GameObject[] enemys = new GameObject[2];

    [SerializeField] List<GameObject> spawnPointsOnScene = new List<GameObject>();

    [SerializeField] GameObject floor;
    Vector2 pos;
    float startX;
    float startY;

    float floorScale;

    [SerializeField] GameObject spawnPref; 

    void Awake()
    {   
        Generation(spawnPointsOnScene, spawnPref);
        Invoke("SpawnObj", 0.2f);
    }

    void SpawnObj () 
    {
        for(int i = 0; i < spawnPointsOnScene.Count; i++)
        {
            if(spawnPointsOnScene[i] == null)
            {
               spawnPointsOnScene.Remove(spawnPointsOnScene[i]);
            }
        }

		Spawn(12, decorations); // спавним декорации

        Spawn(12, enemys);  // спавним врагов
	}
    

    void Generation(List<GameObject> spawnPointsOnScene, GameObject spawnPref)
    {
        floorScale = floor.transform.localScale.x / 2;
        startX = transform.position.x - floorScale;
        startY = transform.position.y - floorScale;

        for(int x = (int)startX; x < (int)floorScale; x++)
        {
            for(int y = (int)startY; y < (int)floorScale; y++)
            {
                GameObject generateObject = Instantiate(spawnPref, new Vector2((float)x, (float)y), Quaternion.identity);
                generateObject.transform.SetParent(gameObject.transform);
                generateObject.AddComponent<RMAditionalScript>();
                spawnPointsOnScene.Add(generateObject);
            }
        }
    }

    void Spawn(int maxFirst, GameObject[] spawnObjects)
    {
        for(int i = 0; i < spawnPointsOnScene.Count; i++)
        {
            int rnd = Random.Range(1, maxFirst);

            if(rnd == 1 && spawnPointsOnScene[i] != null)
            {
                int rndObj = Random.Range(0, spawnObjects.Length);
                Instantiate(spawnObjects[rndObj], spawnPointsOnScene[i].transform.position, Quaternion.identity);
            }
        }
    }
}

