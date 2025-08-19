using System;
using Abstracts;
using Misc;

namespace Managers
{
    public static class EventManager
    {
        // Event to notify when the game state changes.
        public static Action<GameState> OnGameStateChanged;

        // Event to notify when a fighter is dead.
        public static Action<BaseFighter> OnPlayerFighterDead;
        public static Action<BaseFighter> OnEnemyFighterDead;
    }
}