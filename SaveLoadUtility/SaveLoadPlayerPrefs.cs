using UnityEngine;
using Newtonsoft.Json;
using Utilities.Interfaces;

namespace Utilities
{
	
public class SaveLoadPlayerPrefs : ISaveLoadUtility
{
	public void SaveData<T>(DataKeys key, T data)
	{
		try
		{
			if (data == null)
			{
				Debug.LogError($"Failed to save data: The data for key '{key}' is null.");
				return;
			}

			string jsonData = JsonConvert.SerializeObject(data);

			if (string.IsNullOrEmpty(jsonData))
			{
				Debug.LogError($"Failed to serialize data for key '{key}'.");
				return;
			}

			PlayerPrefs.SetString(key.ToString(), jsonData);
			PlayerPrefs.Save();
			Debug.Log($"Data saved successfully under key: {key}");
		}
		catch (System.Exception e)
		{
			Debug.LogError($"Failed to save data for key '{key}'. Error: {e.Message}");
		}
	}

	public T LoadData<T>(DataKeys key)
	{
		try
		{
			if (!PlayerPrefs.HasKey(key.ToString()))
			{
				Debug.LogWarning($"No data found for key: {key}");
				return default;
			}

			string jsonData = PlayerPrefs.GetString(key.ToString());

			if (string.IsNullOrEmpty(jsonData))
			{
				Debug.LogError($"Data for key '{key}' is empty or could not be retrieved.");
				return default;
			}

			T data = JsonConvert.DeserializeObject<T>(jsonData);

			if (data == null)
			{
				Debug.LogError($"Failed to deserialize data for key '{key}'.");
				return default;
			}

			return data;
		}
		catch (System.Exception e)
		{
			Debug.LogError($"Failed to load data for key '{key}'. Error: {e.Message}");
			return default;
		}
	}

	public bool HasData(DataKeys key)
	{
		return PlayerPrefs.HasKey(key.ToString());
	}

	public void DeleteData(DataKeys key)
	{
		if (!PlayerPrefs.HasKey(key.ToString()))
		{
			Debug.LogWarning($"No data found to delete for key: {key}");
			return;
		}

		PlayerPrefs.DeleteKey(key.ToString());
		PlayerPrefs.Save();
		Debug.Log($"Data deleted for key: {key}");
	}
}
	
}// namespace Utilities