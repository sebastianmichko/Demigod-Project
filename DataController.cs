using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.IO;// The System.IO namespace contains functions related to loading and saving files

public class DataController : MonoBehaviour
{
    public GameData gameData;

    private string gameDataProjectFilePath = "/StreamingAssets/data.json";

    public string filePath;

    void Start()
    {
        DontDestroyOnLoad(gameObject);

        gameData = new GameData();

        filePath = Application.dataPath + gameDataProjectFilePath;

        LoadGameData();
    }

    private void LoadGameData()
    {
        // Path.Combine combines strings into a file path
        // Application.StreamingAssets points to Assets/StreamingAssets in the Editor, and the StreamingAssets folder in a build
        
        if (File.Exists(filePath))
        {
            // Read the json from the file into a string
            string dataAsJson = File.ReadAllText(filePath);

            // Pass the json to JsonUtility, and tell it to create a GameData object from it
            //print(JsonUtility.FromJson<GameData>(dataAsJson));
            gameData = JsonUtility.FromJson<GameData>(dataAsJson);
            //print(gameData.highestScore);
            //print("Loaded Game Date: " + filePath + "\n");
        }
        else
        {
            //Debug.LogError("Cannot load game data!");
            gameData = new GameData();
            //print("Created New Game Date File: " + filePath + "\n");
        }
        
    }

    public void SaveGameData()
    {
        GameObject Player = GameObject.Find("ThirdPersonController");
        gameData.health = Player.GetComponent<PlayerHealth>().curHealth;
        gameData.location = Player.GetComponent<Transform>().position;
        gameData.rotation = Player.GetComponent<Transform>().rotation;
        gameData.cheatsEnabled = Player.GetComponent<Cheats>().cheatsEnabled;
        gameData.Scene = SceneManager.GetActiveScene().buildIndex;

        string dataAsJson = JsonUtility.ToJson(gameData);
        File.WriteAllText(filePath, dataAsJson);
        //print("Saved Game Date: " + filePath + "\n");
    }
}