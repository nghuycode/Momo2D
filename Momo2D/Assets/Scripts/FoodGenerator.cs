using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodGenerator : MonoBehaviour
{
    public CameraFollow Cam;
    public List<GameObject> FoodPrefabList;

    private void Awake() 
    {
        InvokeRepeating("SpawnFood", 0, 3);
    }
    public void SpawnFood() 
    {
        GameObject newFood = Instantiate(FoodPrefabList[Random.Range(0, FoodPrefabList.Count)], this.transform);
        newFood.transform.position = new Vector3(Random.Range(Cam.borderLeft, Cam.borderRight), Random.Range(Cam.borderBot, Cam.borderTop));
    }
}
