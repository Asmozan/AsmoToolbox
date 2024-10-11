# Example Usage
---
## Delete Save:

```csharp
using UnityEngine.UI;

class DeleteSaveButton : MonoBehaviour
{
	private ISaveLoadUtility _saveLoadUtility;
	private Button _button;
	
	private void Awake()
	{
		_saveLoadUtility = new SaveLoadUtility();
		_button = GetComponent<Button>();
	}
	
	private void Start()
	{
		_button.onClick.AddListener(DeleteSave);
	}
	
	private void DeleteSave()
	{
		_saveLoadUtility.DeleteData(DataKeys.PlayerStats);
	}
}
```
---

## Data Save:
```csharp
using UnityEngine.UI;

class SaveButton : MonoBehaviour
{	
	private ISaveLoadUtility _saveLoadUtility;
	private Button _button;
	
	private void Awake()
	{
		_saveLoadUtility = new SaveLoadJson(); // or new SaveLoadPlayerPrefs()
		_button = GetComponent<Button>();
	}
	
	private void Start()
	{
		_button.onClick.AddListener(Save);
	}
	
	private void Save()
	{
		_saveLoadUtility.SaveData(DataKeys.ExampleData);
	}
}
```
---

## Data Load:
```csharp
using UnityEngine.UI;

class LoadButton : MonoBehaviour
{	
	private ISaveLoadUtility _saveLoadUtility;
	private Button _button;
	private Data _data;	

	private void Awake()
	{
		_saveLoadUtility = new SaveLoadJson(); // or new SaveLoadPlayerPrefs()
		_button = GetComponent<Button>();
	}
	
	private void Start()
	{
		_button.onClick.AddListener(Save);
	}
	
	private void Load()
	{
		_data = _saveLoadUtility.LoadData(DataKeys.ExampleData);
	}
}
```
---
