using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDetails : MonoBehaviour
{   
    //to load the scenes connected to each other in inspector tab
    [SerializeField] List<SceneDetails> connectedScenes;


    //Scene Loader Checker
    public bool IsLoaded { get; private set; }

    List<SavableEntity> savableEntities;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Scene Loader
        if (collision.tag== "Player")
        {
            Debug.Log($"Entered {gameObject.name}");

            LoadScene();
            //call the function for currentscene loaded
            GameController.Instance.SetCurrentScene(this);

            //Load all Conencted Scenes
            foreach (var scene in connectedScenes)
            {
                scene.LoadScene();
            }

            var prevScene = GameController.Instance.PrevScene;
            //unload scenes not connected to current scene
           if (prevScene != null)
            {
                var previouslyLoadedScenes = GameController.Instance.PrevScene.connectedScenes;
                foreach (var scene in previouslyLoadedScenes)
                {
                    if (!connectedScenes.Contains(scene) && scene != this)
                        scene.UnLoadScene();
                }

                //to unload scene that is not connected
                //when loading the save file happens
                if (!connectedScenes.Contains(prevScene))
                    prevScene.UnLoadScene();
            }
        }
    }

    //function to call loaded scene from multiple place
    public void LoadScene()
    {
        if (!IsLoaded)
        {
            //load Scene(since LoadSceneAsync is loading in background and may cause bug or glitch)
            //and store it in the variable operation
            //so the bug will not appear
            var operation = SceneManager.LoadSceneAsync(gameObject.name, LoadSceneMode.Additive);
            IsLoaded = true;

            //when loading Asnync complete execute this.
            operation.completed += (AsyncOperation op) =>
            {
                savableEntities = GetSavableEntitiesInScene();

                //restoring save entities
                SavingSystem.i.RestoreEntityStates(savableEntities);

            };
            
        }
    }

    //clearly it is to unload scene
    public void UnLoadScene()
    {
        if (IsLoaded)
        {
            //before unloading scene, save the savable entities on there
            SavingSystem.i.CaptureEntityStates(savableEntities);

            SceneManager.UnloadSceneAsync(gameObject.name);
            IsLoaded = false;
        }
    }

    List<SavableEntity> GetSavableEntitiesInScene()
    {
        //stored the current scene name in a variable
        var currScene = SceneManager.GetSceneByName(gameObject.name);

        //load scene from the variable
        //load object that have savable Entity wo se call it when the scene is reloaded
        //to prevent quest to repeatedly
        var savableEntities = FindObjectsOfType<SavableEntity>().Where(x => x.gameObject.scene == currScene).ToList();
        
        //obviously to return the result/value
        return savableEntities;
    }
}
