using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeManager : MonoBehaviour
{
    [SerializeField] private float _distanceBetweenNodes = 0.2f;
    [SerializeField] private float _speed = 280;
    [SerializeField] private float _turnSpeed = 180;
    [SerializeField] private List<GameObject> _bodyPartPool = new List<GameObject>();
    [SerializeField] private List<Node> _bodyPartList = new List<Node>();

    private void Start() 
    {
        // Cursor.lockState = CursorLockMode.Confined;
        InitHead();
    }
    private void InitHead()
    {
        GameObject head = Instantiate(_bodyPartPool[0], this.transform.position, this.transform.rotation, transform);
        _bodyPartList.Add(head.GetComponent<Node>());
        _bodyPartPool.RemoveAt(0);
    }
    private void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("Z");
            SnakeNewPart();
        }
    }
    private void FixedUpdate() 
    {
        
        SnakeMove();
    }
    private void SnakeMove() 
    {
        //Move head with input from mouse
        _bodyPartList[0].transform.Translate(Vector2.up * _speed * Time.fixedDeltaTime, Space.Self);
        _bodyPartList[0].transform.Rotate(Vector3.forward * -Input.GetAxis("Horizontal") * _turnSpeed * Time.fixedDeltaTime);
        if (_bodyPartList.Count > 1)
        {
            for (int i = 1; i < _bodyPartList.Count; ++i)
            {
                Node tmpNode = _bodyPartList[i - 1];
                _bodyPartList[i].transform.position = tmpNode.NodePointList[0].Position;
                _bodyPartList[i].transform.rotation = tmpNode.NodePointList[0].Rotation;
                tmpNode.NodePointList.RemoveAt(0);
            }
        }
    }
    private void SnakeNewPart() 
    {
        Debug.Log("new");
        Node lastNode = _bodyPartList[_bodyPartList.Count - 1].GetComponent<Node>();
        GameObject newNode = Instantiate(_bodyPartPool[Random.Range(0, _bodyPartPool.Count)], lastNode.NodePointList[0].Position, lastNode.NodePointList[0].Rotation, transform);
        newNode.GetComponent<Node>().ClearNodePointList();
        _bodyPartList.Add(newNode.GetComponent<Node>());
    }
}
