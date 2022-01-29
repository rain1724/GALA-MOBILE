using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;


//portal teleporter without switching scene
public class LocationPortal : MonoBehaviour, IPlayerTriggerable
{
    
    [SerializeField] DestinationIdentifier destinationPortal;
    [SerializeField] Transform spawnPoint;

    PlayerController player;
    public void OnplayerTriggered(PlayerController player)
    {
        player.Character.Animator.IsMoving = false;
        this.player = player;
        StartCoroutine(Teleport());
    }

    public bool TriggerRepeatedly => false;

    Fader fader;
    private void Start()
    {
        //fadein/out effect
        fader = FindObjectOfType<Fader>();
    }

    //for switching Scenes
    IEnumerator Teleport()
    {
        //fade in
        GameController.Instance.PauseGame(true);
        yield return fader.FadeIn(0.5f);

        //teleport to designated portal
        var destPortal = FindObjectsOfType<LocationPortal>().First(x => x != this && x.destinationPortal == this.destinationPortal);
        player.Character.setPositionAndSnapToTile(destPortal.SpawnPoint.position);

        //fade out
        yield return fader.FadeOut(0.5f);
        GameController.Instance.PauseGame(false);
    }

    public Transform SpawnPoint => spawnPoint;
}
