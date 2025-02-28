using UnityEngine;

namespace Roguelike
{
    public class Player : MonoBehaviour
    {
        public Matrix Position = new (0,0);
        
        public void Instanitate(Graph g, int room_id)
        {
            //if(isDebugMode)cout << "player locate" << endl;
            //if(isDebugMode)cout << g -> edges.size() << endl;
            var n = g.GetEdges()[room_id].from;
            //if(isDebugMode)cout << "collecting nodes.." << endl;
            var r = n.room;
            Position.x = r.GetMiddlePoint().x;
            Position.y = r.GetMiddlePoint().y;
            //Entity::game = shared_ptr<Game>(this);
            
            this.transform.position = new Vector3(Position.x, 2,Position.y);
        }
    }
}