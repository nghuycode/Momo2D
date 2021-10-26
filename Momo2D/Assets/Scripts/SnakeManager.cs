using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SnakeManager : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _turnSpeed;
    [SerializeField] private GameObject _bodyPartPrefab;
    [SerializeField] private List<Node> _bodyPartList = new List<Node>();

    public UnityAction<int> OnNewPartCollect;
    public bool IsBot;
    public Transform Player;

    private void Awake() 
    {
        if (!IsBot)
        {
            DetectCollider detectCollider = this.GetComponentInChildren<DetectCollider>();
            detectCollider.OnCollideFood += SnakeNewPart;
        }
        else
        {
            Player = GameObject.Find("Player").transform.GetChild(0);
        }
    }
    private void FixedUpdate() 
    {
        if (!IsBot)
            SnakeMove();
        else
            SnakeBotMove();
    }
    private void Update() 
    {
        if (IsBot) return;
        if (Input.GetMouseButtonDown(0))
        {
            _speed *= 2; 
        }
        if (Input.GetMouseButtonUp(0))
        {
            _speed /= 2;
        }
    }
    
    #region SNAKE PLAYER
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
    #endregion
    
    #region SNAKE BOT
    public void InitBot()
    {
        StartCoroutine(CRInitBot());
    }
    private IEnumerator CRInitBot()
    {
        int snakeBotLength = Random.Range(4, 8);
        for (int i = 0; i < snakeBotLength; ++i)
        {
            yield return new WaitForSeconds(4);
            SnakeNewPartBot();
        }
    }
    private void SnakeBotMove() 
    {
        _bodyPartList[0].transform.position = Vector3.MoveTowards(_bodyPartList[0].transform.position, Player.transform.position, _speed * Time.fixedDeltaTime);
        _bodyPartList[0].transform.up = Player.position - _bodyPartList[0].transform.position;
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
    private void SnakeNewPartBot()
    {
        Node lastNode = _bodyPartList[_bodyPartList.Count - 1].GetComponent<Node>();
        GameObject newNode = Instantiate(_bodyPartPrefab, lastNode.NodePointList[0].Position, lastNode.NodePointList[0].Rotation, transform);
        newNode.GetComponent<Node>().ClearNodePointList();
        newNode.GetComponent<SpriteRenderer>().color = ColorPool.Instance.ColorList[Random.Range(0, ColorPool.Instance.ColorList.Count)];
        _bodyPartList.Add(newNode.GetComponent<Node>());
        newNode.tag = "Bot";
        //layer 9 is bot
        newNode.layer = 9;
    }
    #endregion
    
}
