using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodGenerator : MonoBehaviour
{
    public static FoodGenerator Insstance;
    public CameraFollow Cam;
    public GameObject FoodPrefab;
    public int FoodSpawnDelayTime;

    private void Awake() 
    {
        Insstance = this;
        InvokeRepeating("SpawnFood", 5, FoodSpawnDelayTime);
    }
    public void SpawnFood() 
    {
        GameObject newFood = Instantiate(FoodPrefab, this.transform);
        Vector3 targetPos;
        Collider2D[] res = new Collider2D[100];
        do 
        {
            targetPos = new Vector3(Random.Range(Cam.borderLeft, Cam.borderRight), Random.Range(Cam.borderBot, Cam.borderTop));
        }
        while (Physics2D.OverlapCircleNonAlloc(targetPos, 1, res) > 0);
        newFood.transform.position = targetPos;
        newFood.GetComponent<SpriteRenderer>().color = ColorPool.Instance.ColorList[Random.Range(0, ColorPool.Instance.ColorList.Count)];
    }
    public void SpawnFood(Vector3 pos) 
    {
        GameObject newFood = Instantiate(FoodPrefab, this.transform);
        newFood.transform.position = pos;
        newFood.GetComponent<SpriteRenderer>().color = ColorPool.Instance.ColorList[Random.Range(0, ColorPool.Instance.ColorList.Count)];
    }
    public void SpawnFood(int foodCount, List<Vector3> diePos)
    {
        for (int i = 0; i < foodCount; ++i)
        {
            SpawnFood(diePos[i]);
        }
    }
}
