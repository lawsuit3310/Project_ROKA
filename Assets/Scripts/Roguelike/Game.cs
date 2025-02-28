using System;
using UnityEngine;
using Roguelike;
using TMPro;

public class Game : MonoBehaviour
{
    public GameObject tile;
    public PlayerController player;

    private void Awake()
    {
        CreateWorld(200, 200);
    }

    public void CreateWorld(int w, int h)
    {
        var g = new Graph(w, h);
        g.Create();
        int col = 0;
        int row = 0;
        foreach (char c in g.GetSerialized())
        {
            var t = Instantiate(tile, transform, true);
            t.transform.position = new Vector3(col, c == '#' ? 1 : 0, row);
            if (++col == g.GetWidth())
            {
                col = 0;
                row++;
            }
        }

        System.Random rand = new();

        int id = rand.Next() % g.GetEdges().Count;
        var playerController = Instantiate(player);
        var p = playerController.gameObject.AddComponent<Player>();
        p.Instanitate(g, id);

    }

   
    
}