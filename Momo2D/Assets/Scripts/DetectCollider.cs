using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DetectCollider : MonoBehaviour
{
    public UnityAction OnCollideFood, OnCollideWall;
    private void OnCollisionEnter2D(Collision2D other) 
    {
        switch (other.gameObject.tag)
        {
            case "Wall":
                CollideWall();
                break;
            case "Food":
                CollideFood(other.gameObject);
                break;
        }
    }
    private void CollideFood(GameObject food) 
    {
        GameObject.Destroy(food);
        Debug.Log("EAT");
        OnCollideFood?.Invoke();
    }
    private void CollideWall() 
    {
        Debug.Log("DIE");
        OnCollideWall?.Invoke();
    }
}
