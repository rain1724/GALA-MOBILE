using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryItem : MonoBehaviour, IPlayerTriggerable
{
    [SerializeField] Dialog dialog;
    public void OnplayerTriggered(PlayerController player)
    {
        //Debug.Log("Cant go Outside");
        player.Character.Animator.IsMoving = false;
        StartCoroutine(DialogManager.Instance.ShowDialog(dialog));

    }

    public bool TriggerRepeatedly => false;

}
