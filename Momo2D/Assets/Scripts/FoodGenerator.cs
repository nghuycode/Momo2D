using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodGenerator : MonoBehaviour
{
    public CameraFollow Cam;
    public GameObject FoodPrefab;

    private void Awake() 
    {
        InvokeRepeating("SpawnFood", 0, 3);
    }
    public void SpawnFood() 
    {
        GameObject newFood = Instantiate(FoodPrefab, this.transform);
        newFood.transform.position = new Vector3(Random.Range(Cam.borderLeft, Cam.borderRight), Random.Range(Cam.borderBot, Cam.borderTop));
        newFood.GetComponent<SpriteRenderer>().color = ColorPool.Instance.ColorList[Random.Range(0, ColorPool.Instance.ColorList.Count)];
    }
}
