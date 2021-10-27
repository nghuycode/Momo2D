using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBotGenerator : MonoBehaviour
{
    public GameObject BotPrefab;
    public CameraFollow Cam;
    public int MaxBot, SpawnBotDelayTime;
    private void Awake() 
    {
        InvokeRepeating("SpawnBot", 10, SpawnBotDelayTime);
    }
    public void SpawnBot()
    {
        if (this.transform.childCount >= MaxBot) return;
        Vector3 targetPos;
        Collider2D[] res = new Collider2D[100];
        do 
        {
            targetPos = new Vector3(Random.Range(Cam.borderLeft, Cam.borderRight), Random.Range(Cam.borderBot, Cam.borderTop));
        }
        while (Physics2D.OverlapCircleNonAlloc(targetPos, 1, res) > 0);
        GameObject newBot = Instantiate(BotPrefab, this.transform);
        newBot.transform.position = new Vector3(Random.Range(Cam.borderLeft, Cam.borderRight), Random.Range(Cam.borderBot, Cam.borderTop));
    }
}
