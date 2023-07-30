using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This script is copied from Tarodev's video for creating a trajectory line:
/// https://www.youtube.com/watch?v=p8e4Kpl9b28
/// I will probably make some changes to it later on, if needed
/// </summary>

public class Projection : MonoBehaviour
{
    private Scene simulationScene;
    private PhysicsScene physicsScene;

    [SerializeField] private Transform obstaclesParent;

    private void Start()
    {
        CreatePhysicsScene();
    }
    
    private void CreatePhysicsScene()
    {
        simulationScene =
            SceneManager.CreateScene("Simulation", new CreateSceneParameters(LocalPhysicsMode.Physics3D));
        physicsScene = simulationScene.GetPhysicsScene();

        foreach (Transform obj in obstaclesParent)
        {
            var ghostObj = Instantiate(obj.gameObject, obj.position, obj.rotation);
            ghostObj.GetComponent<Renderer>().enabled = false;
            SceneManager.MoveGameObjectToScene(ghostObj, simulationScene);
        }
        
    }


}
