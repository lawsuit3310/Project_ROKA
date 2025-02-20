using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roguelike
{
    public struct Matrix
    {
        public int x;
        public int y;
        public Matrix(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
    public class Node
    {
        public Node(int tlX, int tlY, int brX, int brY)
        {
            pTopLeft.x = tlX;
            pTopLeft.y = tlY;
            pBottomRight.x = brX;
            pBottomRight.y = brY;
        }

        public Matrix pTopLeft = new (0,0);
        public Matrix pBottomRight =new (0,0);
        
        public Node parent = null;
        public Node[] children = { null, null };
        public List<Node> neighbor = new List<Node>();

        public Room room = new Room(0,0,0,0);

        public int depth = 0;
        public int id = 0;
        
        public bool IsNextTo(Node target)
        {
            if (target.pTopLeft.x - pBottomRight.x <= 1)
            {
                if (target.pBottomRight.y < pBottomRight.y && pBottomRight.y < target.pTopLeft.y) return true;
                if (pBottomRight.y < target.pBottomRight.y && target.pBottomRight.y < pTopLeft.y) return true;
            }
            if (target.pBottomRight.x == pTopLeft.x)
            {			
                if (target.pBottomRight.y < pBottomRight.y && pBottomRight.y < target.pTopLeft.y) return true;
                if (pBottomRight.y < target.pBottomRight.y && target.pBottomRight.y < pTopLeft.y) return true;
            }
            if (target.pTopLeft.y == pBottomRight.y)
            {
                if (target.pTopLeft.x < pTopLeft.x &&  pTopLeft.x < target.pBottomRight.x) return true;
                if (pTopLeft.x < target.pTopLeft.x &&  target.pTopLeft.x < pBottomRight.x) return true;
            }
            if (target.pBottomRight.y == pTopLeft.y)
            {
                if (target.pTopLeft.x < pTopLeft.x &&  pTopLeft.x < target.pBottomRight.x) return true;
                if (pTopLeft.x < target.pTopLeft.x &&  target.pTopLeft.x < pBottomRight.x) return true;
            }
			
            return false;
        }
        
        public Vector2 GetMiddlePoint(){return new Vector2( MathF.Round((pBottomRight.x - pTopLeft.x)/2 + pTopLeft.x), MathF.Round((pTopLeft.y - pBottomRight.y)/2 + pBottomRight.y) );}
        
        public Node[] Divide(bool flag)
        {
            Node[] result = new Node[2];
            System.Random rand = new System.Random();
						
            if (flag)
            {
                //TODO 세로로 나눌 때
				
                var x = (pBottomRight.x - pTopLeft.x) * (rand.Next(10) % 2 == 0 ? 6 : 4) / 10 + pTopLeft.x;
				
                var nodeA = new Node(pTopLeft.x, pTopLeft.y, x -1, pBottomRight.y);
                var nodeB = new Node(x, pTopLeft.y, pBottomRight.x, pBottomRight.y);
				
                nodeA.parent = this;
                nodeB.parent = this;
				
                children[0] = nodeA;
                children[1] = nodeB;
				
                nodeA.depth = depth + 1;
                nodeB.depth = depth + 1;
				
                result[0] = nodeA;
                result[1] = nodeB;
            }
			
            else
            {
                //TODO 가로로 나눌 때
				
                int y = (pTopLeft.y - pBottomRight.y) * (rand.Next(10) % 2 == 0 ? 6 : 4) / 10 + pBottomRight.y;

                var nodeA = new Node(pTopLeft.x, y, pBottomRight.x, pBottomRight.y);
                var nodeB = new Node(pTopLeft.x, pTopLeft.y, pBottomRight.x, y);
				
                nodeA.parent = this;
                nodeB.parent = this;
				
                children[0] = nodeA;
                children[1] = nodeB;
				
                nodeA.depth = depth + 1;
                nodeB.depth = depth + 1;
				
                result[0] = nodeA;
                result[1] = nodeB;
            }
						
            return result;
        }
    }

    public class Room
    {
        public Matrix pTopLeft = new (0,0);
        public Matrix pBottomRight = new (0,0);
	
        public Matrix GetMiddlePoint(){return new Matrix( (int)MathF.Round((pBottomRight.x - pTopLeft.x)/2 + pTopLeft.x), (int)MathF.Round((pTopLeft.y - pBottomRight.y)/2 + pBottomRight.y) );}
	
        public Room(int tlX, int tlY, int brX, int brY)
        {
            pTopLeft.x = tlX;
            pTopLeft.y = tlY;
            pBottomRight.x = brX;
            pBottomRight.y = brY;
        }
    }

    public class Edge
    {
        public Node from = null;
        public Node to = null;
        public float cost = 0;
	
        public Edge (Node _from, Node _to)
        {
            from = _from; to = _to;
        }
    }
}