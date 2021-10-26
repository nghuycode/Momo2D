using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SnakeManager : MonoBehaviour
{
    [SerializeField] private float _distanceBetweenNodes = 0.2f;
    [SerializeField] private float _speed = 280;
    [SerializeField] private float _turnSpeed = 180;
    [SerializeField] private GameObject _bodyPartPrefab;
    [SerializeField] private List<Node> _bodyPartList = new List<Node>();

    public UnityAction<int> OnNewPartCollect;

    private void Awake() 
    {
        DetectCollider detectCollider = this.GetComponentInChildren<DetectCollider>();
        detectCollider.OnCollideFood += SnakeNewPart;
    }
    private void Start() 
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void FixedUpdate() 
    {
        SnakeMove();
    }
    private void Update() 
    {
        if (Input.GetMouseButtonDown(0))
        {
            _speed *= 2; 
        }
        if (Input.GetMouseButtonUp(0))
        {
            _speed /= 2;
        }
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
    private void SnakeNewPart(Color color) 
    {
        Debug.Log("new");
        Node lastNode = _bodyPartList[_bodyPartList.Count - 1].GetComponent<Node>();
        GameObject newNode = Instantiate(_bodyPartPrefab, lastNode.NodePointList[0].Position, lastNode.NodePointList[0].Rotation, transform);
        newNode.GetComponent<Node>().ClearNodePointList();
        newNode.GetComponent<SpriteRenderer>().color = color;
        _bodyPartList.Add(newNode.GetComponent<Node>());

        OnNewPartCollect?.Invoke(_bodyPartList.Count - 1);
    }
}
