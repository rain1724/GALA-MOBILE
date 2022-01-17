using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour, IPlayerTriggerable
{
    [SerializeField] int sceneToLoad = -1;
    [SerializeField] Transform spawnPoint;

    PlayerController player;
    public void OnplayerTriggered(PlayerController player)
    {
        StartCoroutine(SwitchScene());
    }

    IEnumerator SwitchScene()
    {
        DontDestroyOnLoad(gameObject);

        GameController.Instance.PauseGame(true);

        yield return SceneManager.LoadSceneAsync(sceneToLoad);

        var destPortal = FindObjectsOfType<Portal>().First(x => x != this);
        //player.Character.setPositionAndSnapToTile(destPortal.SpawnPoint.position);
       

        GameController.Instance.PauseGame(false);
        
        Destroy(gameObject);
    }

    public Transform SpawnPoint => spawnPoint;
}
