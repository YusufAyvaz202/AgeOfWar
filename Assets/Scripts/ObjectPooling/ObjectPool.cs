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

        public ObjectPool(BaseFighter[] baseFighters, int initialSize, Transform parent = null)
        {
            foreach (BaseFighter baseFighter in baseFighters)
            {
                var fighterType = baseFighter.GetFighterType();
                _baseFighterQueue[fighterType] = new Queue<BaseFighter>();
                
                for (int i = 0; i < initialSize; i++)
                {
                    BaseFighter objectInstance = Object.Instantiate(baseFighter, parent);
                    objectInstance.gameObject.SetActive(false);
                    _baseFighterQueue[fighterType].Enqueue(objectInstance);
                }
            }
        }
        
        public BaseFighter GetFighter(FighterType fighterType)
        {
            if (_baseFighterQueue.TryGetValue(fighterType, out Queue<BaseFighter> queue))
            {
                if (queue.Count > 0)
                {
                    BaseFighter objectInstance = queue.Dequeue();
                    objectInstance.gameObject.SetActive(true);
                    return objectInstance;
                }

                Debug.LogWarning("No available objects in pool for type: " + fighterType);
                return null;
            }
            Debug.LogError("No pool found for type: " + fighterType);
            return null;
        }
        
        public void ReturnFighterToPool(BaseFighter objectInstance)
        {
            objectInstance.gameObject.SetActive(false);
            if (_baseFighterQueue.TryGetValue(objectInstance.GetFighterType(), out Queue<BaseFighter> queue))
            {
                queue.Enqueue(objectInstance);
            }
        }
    }
}