using System;
using UnityEngine;
using Roguelike;
using TMPro;

public class Game : MonoBehaviour
{
    public TMP_Text txt;

    public void Awake()
    {
        var g = new Graph(240, 70);
        g.Create();
        txt.text = "";
        int col = 0;
        foreach (char c in g.GetSerialized())
        {
            txt.text += c;
            if (++col == g.GetWidth())
            {
                col = 0;
                txt.text += '\n';
            }
        }
        Debug.Log(col);
    }
}