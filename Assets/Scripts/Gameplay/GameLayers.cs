using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLayers : MonoBehaviour
{
    [SerializeField] LayerMask solidObjectsLayer;
    [SerializeField] LayerMask interactable;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] LayerMask portalLayer;

    public static GameLayers i { get; set; }

    private void Awake()
    {
        i = this;
    }

    public LayerMask SolidLayer
    {
        get => solidObjectsLayer;
    }

    public LayerMask interactableLayer
    {
        get => interactable;
    }

    public LayerMask PlayerLayer
    {
        get => playerLayer;
    }


    public LayerMask PortalLayer
    {
        get => portalLayer;
    }
     
    public LayerMask TriggerableLayers
    {
        get => solidObjectsLayer | portalLayer;
    }

}
