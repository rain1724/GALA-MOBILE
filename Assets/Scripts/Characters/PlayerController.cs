using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour, ISavable
{
    //[SerializeField] string name;

    
    private Vector2 input;
    private Character character;
    private CharacterAnimator animator;

    public static string playername;


    private void Awake()
    {

        
        character = GetComponent<Character>();
        animator = GetComponent<CharacterAnimator>();
        
    }


    public void HandleUpdate()
    {
        if (!character.IsMoving)
        {   //clearly its a touch input
            //input.x = joystick.Horizontal;
            //input.y = joystick.Vertical;

            input.x = CrossPlatformInputManager.GetAxis("Horizontal");
            input.y = CrossPlatformInputManager.GetAxis("Vertical");



            //input.x = Input.GetAxisRaw("Horizontal");
            //input.y = Input.GetAxisRaw("Vertical");

            //diagonal movements detected
            if (input.x != 0) input.y = 0;

            //start moving if touch is not equal to zero
            if (input != Vector2.zero)
            {
                StartCoroutine(character.Move(input, OnMoveOver));
            }
        }

        character.HandleUpdate();

        // if (Input.GetKey(KeyCode.Z))
        if (CrossPlatformInputManager.GetButtonDown("interact"))
           StartCoroutine(Interact());
    }

    //interaction function obviously
    IEnumerator Interact()
    {
        var facingDir = new Vector3(character.Animator.MoveX, character.Animator.MoveY);
        var interactPos = transform.position + facingDir;

        var collider = Physics2D.OverlapCircle(interactPos, 0.5f, GameLayers.i.InteractableLayer);
        if (collider != null)
        {
            yield return collider.GetComponent<Interactable>()?.Interact(transform);
        }

    }

    IPlayerTriggerable currentlyInTrgigger;

    //for colliding solid object
    private void OnMoveOver()
    {
        var colliders = Physics2D.OverlapCircleAll(transform.position - new Vector3(0, character.OffsetY),0.2f, GameLayers.i.TriggerableLayers);
        IPlayerTriggerable triggerable = null;

        foreach (var collider in colliders)
        {
            triggerable = collider.GetComponent<IPlayerTriggerable>();
            if (triggerable != null)
            {
                if (triggerable == currentlyInTrgigger && !triggerable.TriggerRepeatedly)
                    break;


                triggerable.OnplayerTriggered(this);
                currentlyInTrgigger = triggerable;
                break;
            }
        }

        if (colliders.Count() == 0 || triggerable != currentlyInTrgigger)
            currentlyInTrgigger = null;
    }

    //saving position via classes are serializable
    //so that we can convert that class into binary
    public object CaptureState()
    {
        //getting the position of object vector3(x,y,z) and store it in float array
        //since C# object can represent any type of classes stored in object
        float[] position = new float[] { transform.position.x, transform.position.y };
        return position;
    }

    //loading position
    //convert the object back to float array to pass the position in variable
    public void RestoreState(object state)
    {
        var position = (float[])state;
        transform.position = new Vector3(position[0], position[1]);
    }

    public string Name
    {
        get => playername;
    }
    public Character Character => character;

 
}
