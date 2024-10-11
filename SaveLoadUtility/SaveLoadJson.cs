using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using Utilities.Interfaces;

namespace Utilities
{
	public class SaveLoadJson : ISaveLoadUtility
	{
		private string GetFilePath(DataKeys key)
		{
			return Path.Combine(Application.persistentDataPath, $"{key.ToString()}.json");
		}

		public void SaveData<T>(DataKeys key, T data)
		{
			if (data == null)
			{
				Debug.LogError($"Failed to save data: The data for key '{key}' is null.");
				return;
			}

			string jsonData = JsonConvert.SerializeObject(data);

			if (string.IsNullOrEmpty(jsonData))
			{
				Debug.LogError($"Failed to serialize data for key '{key}'");
				return;
			}

			string filePath = GetFilePath(key);

			try
			{
				File.WriteAllText(filePath, jsonData);
				Debug.Log($"Data saved successfully to file: {filePath}");
			}
			catch (IOException e)
			{
				Debug.LogError($"Failed to save data to file '{filePath}': {e.Message}");
			}
		}

		public T LoadData<T>(DataKeys key)
		{
			string filePath = GetFilePath(key);

			if (!File.Exists(filePath))
			{
				Debug.LogWarning($"No file found for key: {key} at path: {filePath}");
				return default;
			}

			try
			{
				string jsonData = File.ReadAllText(filePath);

				if (string.IsNullOrEmpty(jsonData))
				{
					Debug.LogError($"Data for key '{key}' is empty or could not be read.");
					return default;
				}

				T data = JsonConvert.DeserializeObject<T>(jsonData);

				if (data == null)
				{
					Debug.LogError($"Failed to deserialize data for key '{key}' from file.");
					return default;
				}

				return data;
			}
			catch (IOException e)
			{
				Debug.LogError($"Failed to load data from file '{filePath}': {e.Message}");
				return default;
			}
		}

		public bool HasData(DataKeys key)
		{
			string filePath = GetFilePath(key);
			return File.Exists(filePath);
		}

		public void DeleteData(DataKeys key)
		{
			string filePath = GetFilePath(key);

			if (!File.Exists(filePath))
			{
				Debug.LogWarning($"No file found to delete for key: {key} at path: {filePath}");
				return;
			}

			try
			{
				File.Delete(filePath);
				Debug.Log($"Data deleted for key: {key} at file: {filePath}");
			}
			catch (IOException e)
			{
				Debug.LogError($"Failed to delete data for key '{key}' at file '{filePath}': {e.Message}");
			}
		}
	}
} // namespace Utilities