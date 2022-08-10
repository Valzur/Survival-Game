using System.Collections.Generic;

namespace Valzuroid.SurvivalGame.Buildings
{
    [System.Serializable]
    public struct Connections
    {
        HashSet<Building> _leftConnections;
        HashSet<Building> _rightConnections;
        HashSet<Building> _topConnections;
        HashSet<Building> _bottomConnections;

        public HashSet<Building> LeftConnections
        { 
            get
            {
                if(_leftConnections == null) _leftConnections = new HashSet<Building>();
                return _leftConnections; 
            }
            set{ _leftConnections = value; }
        }
        public HashSet<Building> RightConnections
        { 
            get
            {
                if(_rightConnections == null) _rightConnections = new HashSet<Building>();
                return _rightConnections; 
            }
            set{ _rightConnections = value; }
        }
        public HashSet<Building> TopConnections
        { 
            get
            {
                if(_topConnections == null) _topConnections = new HashSet<Building>();
                return _topConnections; 
            }
            set{ _topConnections = value; }
        }
        public HashSet<Building> BottomConnections
        { 
            get
            { 
                if(_bottomConnections == null) _bottomConnections = new HashSet<Building>();
                return _bottomConnections; 
            }
            set{ _bottomConnections = value; }
        }
        
        public bool isConnected
        { 
            get
            { 
                return (LeftConnections.Count == 0) && (RightConnections.Count == 0) && (TopConnections.Count == 0) && (BottomConnections.Count == 0);
            } 
        }
        
        public void EmptyAll()
        {
            LeftConnections.Clear();
            RightConnections.Clear();
            TopConnections.Clear();
            BottomConnections.Clear();
        }
    }
}