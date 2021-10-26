using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public SnakeManager SnakeManager;
    public UIManager UIManager;
    private void Awake() 
    {
        LinkManagerEvents();
    }
    private void InitGame() 
    {

    }
    private void LinkManagerEvents()
    {
        SnakeManager.OnNewPartCollect += UIManager.UpdateScore;
    }
}
