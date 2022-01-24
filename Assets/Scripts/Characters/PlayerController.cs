using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{


    public FixedJoystick joystick;
    private Vector2 input;
    private Character character;
    private CharacterAnimator animator;

    

    private void Awake()
    {
     
        character = GetComponent<Character>();
        animator = GetComponent<CharacterAnimator>();
        
    }


    public void HandleUpdate()
    {
        if (!character.IsMoving)
        {   //clearly its a touch input
            input.x = joystick.Horizontal;
            input.y = joystick.Vertical;
           
           //input.x = Input.GetAxisRaw("Horizontal");
           //input.y = Input.GetAxisRaw("Vertical");
         
            //diagonal movements detected
            //if (input.x != 0) input.y = 0;
            
            //start moving if touch is not equal to zero
            if (input != Vector2.zero)
            {
                StartCoroutine(character.Move(input, OnMoveOver));
           
            }
        }
        

       // if (Input.GetKey(KeyCode.Z))

      if (CrossPlatformInputManager.GetButtonDown("interact"))
           Interact();

        // to get the button if its press
        character.HandleUpdate();

    }

    //interaction function obviously
    void Interact()
    {
        var facingDir = new Vector3(character.Animator.MoveX, character.Animator.MoveY);
        var interactPos = transform.position + facingDir;
        var collider = Physics2D.OverlapCircle(interactPos, 0.5f, GameLayers.i.InteractableLayer);
        if (collider != null)
        {
            collider.GetComponent<Interactable>()?.Interact(transform);
        }

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
