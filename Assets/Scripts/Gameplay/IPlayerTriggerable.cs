using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerTriggerable
{
    //if triggered the controller will pass to the player
    void OnplayerTriggered(PlayerController player);

    //if the player trigger the triggerable objects this function sets the trigger
    //certain trigger objects to triggered once or repeatedly
    bool TriggerRepeatedly { get;}
}
