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
        InitGame();
    }
    private void InitGame() 
    {
        Cursor.lockState = CursorLockMode.Locked;
        LinkManagerEvents();
    }
    private void LinkManagerEvents()
    {
        SnakeManager.OnUpdateBodyPart += UIManager.UpdateScore;
    }
}
