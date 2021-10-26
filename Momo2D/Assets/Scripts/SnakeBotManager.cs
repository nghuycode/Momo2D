using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBotManager : MonoBehaviour
{
    public GameObject BotPrefab;
    public CameraFollow Cam;
    private void Awake() 
    {
        InvokeRepeating("SpawnBot", 1, 5);
    }
    public void SpawnBot()
    {
        if (this.transform.childCount > 5) return;
        GameObject newBot = Instantiate(BotPrefab, this.transform);
        newBot.transform.position = new Vector3(Random.Range(Cam.borderLeft, Cam.borderRight), Random.Range(Cam.borderBot, Cam.borderTop));
        newBot.GetComponent<SnakeManager>().InitBot();
    }
}
