using System.IO;
using Economy;
using Managers;
using UnityEngine;

namespace SaveSystem
{
    public class JsonSaveLoad : MonoBehaviour
    {
        [Header("Game Data")]
        private AllGameData _allGameData = new();
        
        #region Unity Methods

        private void Start()
        {
            InitializeLoadData();
            GameManager.Instance.SetCurrentLevelIndex(_allGameData.CurrentLevelIndex);
            EconomyManager.Instance.SetMeatProduction(_allGameData.PlayerData.CurrentMeatProductionRate);
            GoldManager.Instance.SetGoldAmount(_allGameData.PlayerData.GoldCount);
        }

        private void OnDisable()
        {
            InitializeSaveData();
        }

        #endregion
        
        private string GetPath() => Path.Combine(Application.persistentDataPath, "allGameData.json");
        
        private void InitializeSaveData()
        {
            _allGameData ??= new AllGameData();
            _allGameData.PlayerData ??= new PlayerData();
            
            _allGameData.CurrentLevelIndex = GameManager.Instance.GetCurrentLevelIndex();
            _allGameData.PlayerData.CurrentMeatProductionRate = EconomyManager.Instance.GetCurrentMeatProduction();
            _allGameData.PlayerData.GoldCount = GoldManager.Instance.GetGoldAmount();
            SaveAllGameData(_allGameData);
        }

        private void SaveAllGameData(AllGameData data)
        {
            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(GetPath(), json);
            Debug.Log("Saved to: " + GetPath());
        }
        
        private void InitializeLoadData()
        {
            _allGameData = LoadAllGameData();
            if (_allGameData?.PlayerData != null)
                Debug.Log(_allGameData.PlayerData.GoldCount);
            else
                Debug.LogWarning("AllGameData or PlayerData came null!");
        }
        
        private AllGameData LoadAllGameData()
        {
            string path = GetPath();
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                AllGameData data = JsonUtility.FromJson<AllGameData>(json);
                return data;
            }
            Debug.LogWarning("Save file not found at: " + path);
            return new AllGameData();
        }
    }
}