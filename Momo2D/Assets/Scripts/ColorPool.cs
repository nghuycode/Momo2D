using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPool : MonoBehaviour
{
    public static ColorPool Instance;
    private void Awake() 
    {
        Instance = this;
    }
    public List<Color> ColorList = new List<Color>();
}
