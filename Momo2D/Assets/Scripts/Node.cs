using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Node : MonoBehaviour
{
    [Serializable]
    public class NodePoint 
    {
        public Vector3 Position;
        public Quaternion Rotation;
        public NodePoint(Vector3 position, Quaternion rotation)
        {
            Position = position;
            Rotation = rotation;
        }
    }

    public List<NodePoint> NodePointList = new List<NodePoint>();
    
    private void FixedUpdate() 
    {
        UpdateNodePointList();
    }
    public void UpdateNodePointList()
    {
        NodePointList.Add(new NodePoint(this.transform.position, this.transform.rotation));
        if (NodePointList.Count > 20)
        {
            NodePointList.RemoveAt(0);
        }
    }
    public void ClearNodePointList() 
    {
        NodePointList.Clear();
        NodePointList.Add(new NodePoint(this.transform.position, this.transform.rotation));
    }
}
