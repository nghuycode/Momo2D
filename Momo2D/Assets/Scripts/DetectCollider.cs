using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DetectCollider : MonoBehaviour
{
    public UnityAction OnCollideWall, OnCollideBot, OnCollidePlayer;
    public UnityAction<Color> OnCollideFood;
    private void OnCollisionEnter2D(Collision2D other) 
    {
        switch (other.gameObject.tag)
        {
            case "Wall":
                CollideWall();
                break;
            case "Bot":
                CollideBot();
                break;
            case "Player":
                CollidePlayer();
                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        switch (other.gameObject.tag)
        {
            case "Food":
                CollideFood(other.gameObject);
                break;
            case "Bot":
                CollideBot();
                break;
            case "Player":
                CollidePlayer();
                break;
        }
    }
    private void CollideFood(GameObject food) 
    {
        GameObject.Destroy(food);
        Debug.Log("EAT");
        OnCollideFood?.Invoke(food.GetComponent<SpriteRenderer>().color);
    }
    private void CollideWall() 
    {
        Debug.Log("DIE");
        OnCollideWall?.Invoke();
    }
    private void CollideBot()
    {
        Debug.Log("DIE");
        OnCollideBot?.Invoke();
    }
    private void CollidePlayer()
    {
        Debug.Log("KILL");
        OnCollidePlayer?.Invoke();
    }
}
