using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Enums 
{ 
    namespace System
    {
        public enum SystemStates 
        {
            Cinematic,
            Playing,
            Paused
        }

    }
    namespace Game
    {
        public enum GameStates
        {
            Exploration,
            Combat,
            Dialog,
            Dead
        }
        public enum Tags
        {
            Player,
            Enemy
        }
    }

}

