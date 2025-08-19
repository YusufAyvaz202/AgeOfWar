namespace Misc
{
    public enum FighterType
    {
        CaveMan,
        Ninja
    }

    public enum FighterState
    {
        Waiting,
        Move,
        Attacking,
        Dead,
    }
    
    public enum GameState
    {
        Waiting,
        Playing,
        Paused,
        Win,
        Lose
    }
}