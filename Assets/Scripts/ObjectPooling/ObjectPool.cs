using System.Collections.Generic;
using Abstracts;
using Misc;
using UnityEngine;

namespace ObjectPooling
{
    public class ObjectPool
    {
        [Header("Object Pool Settings")]
        private Dictionary<FighterType, Queue<BaseFighter>> _baseFighterQueue = new();
        private Transform _parent;
        private BaseFighter[] _baseFightersPrefabs;

        public ObjectPool(BaseFighter[] baseFighters, int initialSize, Transform parent = null)
        {
            _baseFightersPrefabs = baseFighters;
            _parent = parent;
            foreach (BaseFighter baseFighter in baseFighters)
            {
                var fighterType = baseFighter.GetFighterType();
                _baseFighterQueue[fighterType] = new Queue<BaseFighter>();
                
                for (int i = 0; i < initialSize; i++)
                {
                    BaseFighter fighter = Object.Instantiate(baseFighter, parent);
                    fighter.gameObject.SetActive(false);
                    _baseFighterQueue[fighterType].Enqueue(fighter);
                }
            }
        }
        
        public BaseFighter GetFighter(FighterType fighterType)
        {
            if (_baseFighterQueue.TryGetValue(fighterType, out Queue<BaseFighter> queue))
            {
                if (queue.Count > 0)
                {
                    BaseFighter fighter = queue.Dequeue();
                    fighter.gameObject.SetActive(true);
                    return fighter;
                }

                foreach (var baseFighter in _baseFightersPrefabs)
                {
                    if (baseFighter.GetFighterType() == fighterType)
                    {
                        BaseFighter fighter = Object.Instantiate(baseFighter, _parent);
                        fighter.gameObject.SetActive(true);
                        return fighter;
                    }
                }
            }
            Debug.LogError("No pool found for type: " + fighterType);
            return null;
        }
        
        public void ReturnFighterToPool(BaseFighter fighter)
        {
            fighter.gameObject.SetActive(false);
            if (_baseFighterQueue.TryGetValue(fighter.GetFighterType(), out Queue<BaseFighter> queue))
            {
                queue.Enqueue(fighter);
            }
        }
    }
}