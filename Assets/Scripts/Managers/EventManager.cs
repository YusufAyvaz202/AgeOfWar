using System;
using Misc;

namespace Managers
{
    public static class EventManager
    {
        public static Action<GameState> OnGameStateChanged;       
    }
}