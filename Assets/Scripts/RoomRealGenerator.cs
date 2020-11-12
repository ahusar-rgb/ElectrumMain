using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomRealGenerator : MonoBehaviour
{
    public GameObject[] RoomPrefabs;
    public GameObject HallPrefVertical, HallPrefHorizontal;
    List<GameObject> SpawnedObj = new List<GameObject>();
    public int amount;
    const float OFFSET = 23.01f;
    void Start()
    {
        SpawnedObj.Add(gameObject);
        for(int i = 0; i < amount-1; i++)
        {
            Spawn();
        }
    }

    void Spawn()
    {
        Vector2 dir = ChooseDir();
        Vector2 pos = new Vector2(SpawnedObj[SpawnedObj.Count-1].transform.position.x, SpawnedObj[SpawnedObj.Count-1].transform.position.y) + dir * OFFSET;
        for(int i = SpawnedObj.Count - 1; i >= 0; i--)
        {
            if(new Vector2(SpawnedObj[i].transform.position.x, SpawnedObj[i].transform.position.y) == pos)
            {
                Spawn();
                return;
            }
        }
        GameObject g = Instantiate(RoomPrefabs[Random.Range(0,RoomPrefabs.Length)], pos, Quaternion.identity);
        SpawnedObj.Add(g);  
        SpawnHall(dir);
    }

    Vector2 ChooseDir()
    {
        int x = 0;
        int y = 0;
        int random = Random.Range(0,100);
        if(random <= 25)
        {
            x = -1;
            y = 0; 
        }
        else if(random <= 50)
        {
            x = 0;
            y = 1;
        }
        else if(random <= 75)
        {
            x = 1;
            y = 0;
        }
        else if(random <= 100)
        {
            x = 0;
            y = -1;
        }
        Vector2 vector = new Vector2(x,y);
        return vector;
    }


    void SpawnHall(Vector2 dir)
    {
        SpawnHallOpening(-dir,1);
        if(SpawnedObj.Count - 2 >= 0)
        {
            SpawnHallOpening(dir,2);
            SpawnHallPref(SpawnedObj[SpawnedObj.Count - 1].transform, SpawnedObj[SpawnedObj.Count - 2].transform);
        }
    }


    void SpawnHallOpening(Vector2 dir, int count)
    {
        if(dir == Vector2.up)
        {
            for(int i = 0; i < 6; i++)
            {
                if(SpawnedObj[SpawnedObj.Count - count].tag == "origPref")
                {
                    Destroy(SpawnedObj[SpawnedObj.Count - count].transform.GetChild(3).GetChild(7+i).gameObject);
                }
                else
                {
                    Destroy(SpawnedObj[SpawnedObj.Count - count].transform.Find("RoomPref1").GetChild(3).GetChild(7+i).gameObject);
                }
            }
        }
        else if(dir == Vector2.down)
        {
            for(int i = 0; i < 6; i++)
            {
                if(SpawnedObj[SpawnedObj.Count - count].tag == "origPref")
                {
                    Destroy(SpawnedObj[SpawnedObj.Count - count].transform.GetChild(2).GetChild(8+i).gameObject);
                }
                else
                {
                    Destroy(SpawnedObj[SpawnedObj.Count - count].transform.Find("RoomPref1").GetChild(2).GetChild(8+i).gameObject);
                }
            }
        }
        else if(dir == Vector2.right)
        {
            for(int i = 0; i < 6; i++)
            {
                if(SpawnedObj[SpawnedObj.Count - count].tag == "origPref")
                {
                    Destroy(SpawnedObj[SpawnedObj.Count - count].transform.GetChild(0).GetChild(7+i).gameObject);
                }   
                else
                {
                    Destroy(SpawnedObj[SpawnedObj.Count - count].transform.transform.Find("RoomPref1").GetChild(0).GetChild(7+i).gameObject);
                }
            }
        }
        else if(dir == Vector2.left)
        {
            for(int i = 0; i < 6; i++)
            {
                if(SpawnedObj[SpawnedObj.Count - count].tag == "origPref")
                {
                    Destroy(SpawnedObj[SpawnedObj.Count - count].transform.GetChild(1).GetChild(8+i).gameObject);
                }
                else
                {
                    Destroy(SpawnedObj[SpawnedObj.Count - count].transform.Find("RoomPref1").GetChild(1).GetChild(8+i).gameObject);
                }
            }
        }
    }


    void SpawnHallPref(Transform room1, Transform room2)
    {
        Vector2 vector = room1.position - room2.position;
        if(vector.x == 0)
        {
            Instantiate(HallPrefVertical, new Vector2(room2.position.x, room2.position.y + 0.135f) + vector/2, Quaternion.identity);
        }
        else if(vector.y == 0)
        {
             Instantiate(HallPrefHorizontal, new Vector2(room2.position.x, room2.position.y +0.1f) + vector/2, HallPrefHorizontal.transform.rotation);
        }
        else
        {
            Debug.LogError("Impossible to spawn a hall");
        }
    }
}
