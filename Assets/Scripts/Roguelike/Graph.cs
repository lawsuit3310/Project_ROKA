using System.Collections.Generic;
using UnityEngine;

namespace Roguelike
{
    public class Graph
    {
        public Graph (int w, int h)
        {
            width = w;
            height = h;
			
            root = new Node(0,h,w,0);
            vertexes.Add(root);
			
            serialized = new char[w*h];
            for (int i = 0; i < width * height; i ++)
                serialized[i]='\n';
        }
        
        public Node root;

        private int width;
        private int height;
        private List<Node> vertexes = new();
        private List<Edge> edges = new();
        private char[] serialized;
        
        public int GetWidth(){return width;}
        public int GetHeight(){return height;}
        public char[] GetSerialized(){return serialized;}
        public List<Node> GetVertexes(){return vertexes;}

        private int maxDepth = 5;

        public void Create()
        {
            Queue<Node[]> q = new ();
            Queue<Node[]> processed_q = new ();
            //Divine Nodes ===========================================================
	
            for (int i = 0; i < maxDepth; i++)
            {
                if (q.Count == 0)
                {
                    var v = root.Divide(i%2 == 0);
                    q.Enqueue(v);
                    v[0].id = vertexes.Count+1;
                    v[1].id = vertexes.Count+2;
                    vertexes.Add(v[0]);
                    vertexes.Add(v[1]);
                }
                else
                {
                    for (int j = 0; j < q.Count; )
                    {
                        var vec = q.Dequeue();

                        var v = vec[0].Divide(i%2 == 0);
                        processed_q.Enqueue(v);
				
                        v[0].id = vertexes.Count+1;
                        v[1].id = vertexes.Count+2;
                        vertexes.Add(v[0]);
                        vertexes.Add(v[1]);

                        v = vec[1].Divide(i%2 == 0);
                        processed_q.Enqueue(v);
				
                        v[0].id = vertexes.Count+1;
                        v[1].id = vertexes.Count+2;
                        vertexes.Add(v[0]);
                        vertexes.Add(v[1]);
                    }
                    //큐를 비우자!
                    q.Clear();

                    for (int j = 0; j < processed_q.Count; )
                    {
                        q.Enqueue(processed_q.Dequeue());
                    }
                    processed_q.Clear();

                }
		
            }
	
            //Create room ===========================================================
            CreateRooms();
            //TODO Connect Rooms ====================================================
            //일단 인접한 노드의 정보를 찾음
            Dijkstra();
	
            //Serialize ==============================================================
            //출력 가능한 형태로 변환
            Serialize();


        }

        void CreateRooms()
        {
            
            List<Node> lowestVertex = new List<Node>();
	
            //최하위 노드만 추려냄.
            for (int i = 0; i < vertexes.Count; i++)
            {
                var vertex = vertexes[i];
                if(vertex.depth == maxDepth)
                    lowestVertex.Add(vertex);
            }

            System.Random rand = new ();
	
            for (int i = 0; i < lowestVertex.Count; i++)
            {
                var vertex = lowestVertex[i];
		
                // 노드 최소 일때의 최대 너비
                int w = (width / 4 * (maxDepth / 2 + maxDepth % 2 == 0 ? 0 : 1));
		
                // 노드 최소 일때의 최대 높이
                int h = (height / 4 * (maxDepth / 2 ));
		
                int rtlx = rand.Next (vertex.pTopLeft.x, (int)( vertex.pBottomRight.x - vertex.pTopLeft.x) / 4 +  vertex.pTopLeft.x);
                int rbrx = rand.Next((vertex.pBottomRight.x - vertex.pTopLeft.x) /4 * 3 + vertex.pTopLeft.x, vertex.pBottomRight.x-1);

                int rbry = rand.Next(vertex.pBottomRight.y,
                    (int)(vertex.pTopLeft.y - vertex.pBottomRight.y) / 3 + vertex.pBottomRight.y);
                int rtly = rand.Next((int)((vertex.pTopLeft.y - vertex.pBottomRight.y) /4 * 3) + vertex.pBottomRight.y, vertex.pTopLeft.y-1);

                vertex.room.pTopLeft.x = rtlx;
                vertex.room.pTopLeft.y = rtly;
                vertex.room.pBottomRight.x = rbrx;
                vertex.room.pBottomRight.y = rbry;
		
                //cout << vertex->room.pTopLeft.x << " " << vertex->room.pTopLeft.y << " " << vertex->room.pBottomRight.x << " " << vertex->room.pBottomRight.y << " " << endl;
            }
        }

        List<Node> Dijkstra()
        {
            List<Node> lowestVertex = new List<Node>();
	
            for (int i = 0; i < vertexes.Count; i++)
            {
                var vertex = vertexes[i];
                if(vertex.depth == maxDepth)
                {
                    lowestVertex.Add(vertex);
                }
            }

            Node n;
            for(int i = 0; i < lowestVertex.Count; i++)
            {
                n = lowestVertex[i];

                for(int j = 0; j < lowestVertex.Count; j++)
                {
                    var target = lowestVertex[j];
                    if (n == target) continue;
			
                    if (n.IsNextTo(target))
                    {
                        n.neighbor.Add(target);
                    }
                }
            }
            //cout << lowestVertex[0]->edges[0]->cost << endl;
	
            //Dijkstra Algorithm
            n = lowestVertex[0];
            float maxDistance = Mathf.Sqrt(Mathf.Pow(width, 2) + Mathf.Pow(height, 2));
            float[] d = new float[lowestVertex.Count];
	
            for(int i = 1; i < lowestVertex.Count; i++)
                d[i] = maxDistance;
	
            List<Node> _checked = new ();
            Node mClose = null;
            for(int i = 0; i < lowestVertex.Count - 1; i++)
            {
                foreach (var it in n.neighbor)
                {
                    //이미 방문한 노드면 계산에서 제외
                    if(_checked.Contains(it)) continue;
			
                    var from = n.GetMiddlePoint(); var to = it.GetMiddlePoint();
                    float r = Mathf.Sqrt( Mathf.Pow ( (int)from.x - (int)to.x,2) + Mathf.Pow ((int)from.y - (int)to.y,2) );
                    d[i+1] = d[i+1] < r ? d[i+1] : r;
                    mClose = mClose == null ? it : mClose;
                    mClose = d[i+1] < r ? mClose : it;
                }
                _checked.Add(n);
                //이웃한 미방문 노드 중 가장 가까운 노드를 기록
		
                float min_r = 111111111;
                // 미방문된 모든 노드 중 가장 가까운 곳을 탐색
                foreach (var it in lowestVertex)
                {
                    if(_checked.Contains(it)) continue;
                    var from = n.GetMiddlePoint(); var to = it.GetMiddlePoint();
                    float r = Mathf.Sqrt( Mathf.Pow  ( (int)from.x - (int)to.x,2) + Mathf.Pow  ((int)from.y - (int)to.y,2) );
                    min_r = min_r < r ? min_r : r;
                    mClose = min_r < r ? mClose : it;
                }
                //cout << (char)(n->id+33) << " -> ";
                var e = new Edge(n, mClose); e.cost = min_r;
                edges.Add(e);
		
                n = mClose;
                mClose = null;
            }
            _checked.Add(n);
	
            return _checked;
        }

        void Serialize()
        {
            List<Node> lowestVertex = new ();
	
			//최하위 노드만 추려냄.
			for (int i = 0; i < vertexes.Count; i++)
			{
				var vertex = vertexes[i];
				if(vertex.depth == maxDepth)
					lowestVertex.Add(vertex);
			}
			//cout << edges.size() << endl;
			foreach(var e in edges)
			{
				var from = e.from.room.GetMiddlePoint();
				var to = e.to.room.GetMiddlePoint();
				
				//cout << (char)(e->from->id +33) << " -> " << (char)(e->to->id + 33<< endl;
				
				if (from.x == to.x || from.y == to.y)
				{
					// 기울기가 0 일 때, y = 상수 꼴의 그래프
					for (int y = 0; y < height; y++)
					{
						for (int x = 0; x < width; x++)
						{
							bool vb = from . y < to . y ? from . y <= y && y <= to . y : to . y <= y && y <= from . y; 
							bool hb = from . x < to . x ? from . x <= x && x <= to . x : to . x <= x && x <= from . x;
							
							//세로 연결 (x = 0)
							if (from . x == x & vb)
								serialized[y * width + x] = '+';
							//가로 연결 (y = 0)
							else if (from . y == y & hb)
								serialized[y * width + x] = '+';
						}
					}
				}
				else
				{
					float inc = ( from.y - (float)to.y ) / ( from . x - (float) to . x );
						//절편
					float r = (float)from . y - inc * from . x ;


					for (int y = 0; y < height; y++)
					{
						for (int x = 0; x < width; x++)
						{
							float result = inc * x + r;
							// x가 선분 범위 안에 있는 유효한 값인지 검사
							bool hb = from . x < to . x ? ( from.x <= x && x <= to . x) : (to.x <= x && x <= from . x);
							bool vb = from . y < to . y ? ( from.y <= y && y <= to . y) : (to.y <= y && y <= from . y);
							if(hb && vb)
							{
								float f = Mathf.Log(Mathf.Abs(inc)) * 6.0f;
								//log 를 씌웠는데 y가 음수면 기울기가 1보다 작은것.
								//기울기가 1에 가까운 친구들          // -0.6 ~ 정도의 기울기를 가진 애들을 걸러내기 위함.
								if(Mathf.Abs(Mathf.Log(Mathf.Abs(inc))) < 0.5f || Mathf.Abs(Mathf.Log(Mathf.Abs(-1/inc))) < 0.5f)
								{
									if (Mathf.Abs(result - y) < 3.0f)
										serialized[y * width + x] = '+';
								}
								// 기울기가 1보다 큰 친구들
								else if(Mathf.Log(Mathf.Abs(inc)) > 0.5f)
								{
									if (Mathf.Abs(result - y) < f)
										serialized[y * width + x] = '+';
								}
								else
								{
									float incPF = (from . y - y) / (float)(from . x - x);
									incPF = from . x == x ? -1 : incPF;
									float incTF = (from . y - to . y) / (float)(from . x - to . x);
									incTF = from . x != to . x ? -1 : incTF;
									if (Mathf.Abs(incPF + 1.0f) < 1 && Mathf.Abs(incTF + 1.0f) < 1)
										continue;
									
									//내적
									float prod = Mathf.Abs((float)to . x - from . x) * Mathf.Abs((float)x - from.x) + Mathf.Abs((float)to . y - from . y) * Mathf.Abs( (float)y - from . y);
									//외적
									float cProd = Mathf.Abs((float)to . x - from . x) * Mathf.Abs( (float)y - from . y) - Mathf.Abs((float)x - from.x) * Mathf.Abs((float)to . y - from . y);
									float tract = Mathf.Abs(cProd / 2);
									r = Mathf.Sqrt (Mathf.Pow((float)to . x - from . x, 2) + Mathf.Pow((float)to . y - from . y, 2));
									//tract = r * h / 2 => h = tract * 2 / r
									float h = tract * 2 / r;
									
									
									if (h <= 0.7f)
									{
										serialized[y * width + x] = '+';
									}
								}
							}
						}
					}
				}
			
			}		
			
			for (int row = 0; row < height; row++)
			{
				for (int col = 0; col < width; col++)
				{			
					foreach (Node n in lowestVertex)
					{
						if(n .pTopLeft.x <= col && col <= n.pBottomRight.x && n .pBottomRight.y <= row && row < n.pTopLeft.y)
						{
							if(n .room.pTopLeft.x <= col && col < n.room.pBottomRight.x && n .room.pBottomRight.y <= row && row < n.room.pTopLeft.y )
							{
								serialized[row * width + col] = '.'; continue;
							}
							else
							{
								 serialized[row * width + col] = serialized[row * width + col] == '+' || serialized[row * width + col] == '='|| serialized[row * width + col] == '-'|| serialized[row * width + col] == '*' ? serialized[row * width + col] : '#';
							}
						}
					}
				}
			}
        }
    }
}