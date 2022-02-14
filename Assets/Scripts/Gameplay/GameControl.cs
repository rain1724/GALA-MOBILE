using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    public enum Difficulties { Easy, Medium, Hard };
    public GameObject[] characters;
    public Transform playerStartPosition;
    public string menuScene = "Character Selection Menu";
    private string selectedCharacterDataName = "SelectedCharacter";
    int selectedCharacter;
    public GameObject playerObject;

    public static Difficulties Difficulty = Difficulties.Easy;

    // Start is called before the first frame update
    void Start()
    {
        selectedCharacter = PlayerPrefs.GetInt(selectedCharacterDataName,0);
        playerObject = Instantiate(characters[selectedCharacter],playerStartPosition.position,characters[selectedCharacter].transform.rotation);
    }

   
}
