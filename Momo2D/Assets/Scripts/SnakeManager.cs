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

    public UnityAction<int> OnUpdateBodyPart;
    public UnityAction<int, List<Vector3>> OnDie;
    public bool IsBot, IsDying;
    public Transform Player;

    private void Awake() 
    {
        DetectCollider detectCollider = this.GetComponentInChildren<DetectCollider>();
        if (!IsBot)
        {
            detectCollider.OnCollideFood += SnakeNewPart;
            detectCollider.OnCollideBot += Die;
            detectCollider.OnCollideWall += Die;
        }
        else
        {
            Player = GameObject.Find("Player").transform.GetChild(0);
            detectCollider.OnCollidePlayer += Die;
            detectCollider.OnCollideFood += SnakeNewPartBot;
            OnDie += FoodGenerator.Insstance.SpawnFood;
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
        Node lastNode = _bodyPartList[_bodyPartList.Count - 1].GetComponent<Node>();
        GameObject newNode = Instantiate(_bodyPartPrefab, lastNode.NodePointList[0].Position, lastNode.NodePointList[0].Rotation, transform);
        newNode.GetComponent<Node>().ClearNodePointList();
        newNode.GetComponent<SpriteRenderer>().color = color;
        newNode.tag = "Player";
        _bodyPartList.Add(newNode.GetComponent<Node>());
        OnUpdateBodyPart?.Invoke(_bodyPartList.Count - 1);
    }
    private void Die()
    {
        if (!IsDying)
            StartCoroutine(CRDieAnimation());
    }
    private IEnumerator CRDieAnimation()
    {
        IsDying = true;
        int partCount = _bodyPartList.Count;
        List<Vector3> partPosList = new List<Vector3>();
        for (int i = 0; i < partCount; ++i)
            partPosList.Add(_bodyPartList[i].transform.position);
        if (_bodyPartList.Count > 1)
        {
            for (int i = _bodyPartList.Count - 1; i > 0; --i)
            {
                GameObject node = _bodyPartList[_bodyPartList.Count - 1].gameObject;
                _bodyPartList.RemoveAt(_bodyPartList.Count - 1);
                GameObject.Destroy(node);
                yield return new WaitForSeconds(0.1f);
            }
        }
        if (!IsBot)
            OnUpdateBodyPart?.Invoke(_bodyPartList.Count - 1);
        else
        {
            OnDie?.Invoke(partCount, partPosList);
            GameObject.Destroy(this.gameObject);
        }
        IsDying = false;
    }
    #endregion
    
    #region SNAKE BOT
    private void SnakeBotMove() 
    {
        if (_bodyPartList.Count < 0) return;
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
    private void SnakeNewPartBot(Color color)
    {
        Node lastNode = _bodyPartList[_bodyPartList.Count - 1].GetComponent<Node>();
        GameObject newNode = Instantiate(_bodyPartPrefab, lastNode.NodePointList[0].Position, lastNode.NodePointList[0].Rotation, transform);
        newNode.GetComponent<Node>().ClearNodePointList();
        newNode.GetComponent<SpriteRenderer>().color = color;
        _bodyPartList.Add(newNode.GetComponent<Node>());
        newNode.tag = "Bot";
        //layer 9 is bot
        newNode.layer = 9;
    }
    #endregion
    
}
