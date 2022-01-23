using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


    public FixedJoystick joystick;
    private Vector2 input;
    private Character character;

    private void Awake()
    {
     
        character = GetComponent<Character>();
        
    }
   
    public void HandleUpdate()
    {
        if (!character.IsMoving)
        {   //clearly its a touch input
            input.x = joystick.Horizontal;
            input.y = joystick.Vertical;

         
            //diagonal movements detected
            //if (input.x != 0) input.y = 0;
            
            //start moving if touch is not equal to zero
            if (input != Vector2.zero)
            {
                StartCoroutine(character.Move(input, OnMoveOver));
           
            }
        }

        character.HandleUpdate();
    }


    //for colliding solid object
    private void OnMoveOver()
    {
        var colliders = Physics2D.OverlapCircleAll(transform.position - new Vector3(0, character.OffsetY),0.2f, GameLayers.i.TriggerableLayers);

        foreach (var collider in colliders)
        {
            var triggerable = collider.GetComponent<IPlayerTriggerable>();
            if (triggerable != null)
            {
                triggerable.OnplayerTriggered(this);
                break;
            }
        }
    }

    public Character Character => character;
 
}
