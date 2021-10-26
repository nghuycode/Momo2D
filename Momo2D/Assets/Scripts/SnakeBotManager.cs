using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBotManager : MonoBehaviour
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
        GameObject newBot = Instantiate(BotPrefab, this.transform);
        newBot.transform.position = new Vector3(Random.Range(Cam.borderLeft, Cam.borderRight), Random.Range(Cam.borderBot, Cam.borderTop));
    }
}
