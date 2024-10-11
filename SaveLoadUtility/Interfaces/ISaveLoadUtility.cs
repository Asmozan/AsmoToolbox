public interface ISaveLoadUtility
{
	void SaveData<T>(DataKeys key, T data);
	T LoadData<T>(DataKeys key);
	bool HasData(DataKeys key);
	void DeleteData(DataKeys key);
}