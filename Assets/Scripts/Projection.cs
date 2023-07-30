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
    [SerializeField] private LineRenderer line;
    [SerializeField] private int maxPhysicsFrameIterations;

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
            CreateGhostObjectInSimulationScene(obj, true);
        }
        
    }

    public void SimulateTrajectory(Ball ballPrefab, Vector3 pos, Vector3 velocity)
    {
        var ghostBall = CreateGhostObjectInSimulationScene(ballPrefab.transform, pos, false);
        
        ghostBall.GetComponent<Ball>().Init(velocity, true);

        line.positionCount = maxPhysicsFrameIterations;

        for (int i = 0; i < maxPhysicsFrameIterations; i++)
        {
            physicsScene.Simulate(Time.fixedDeltaTime);
            line.SetPosition(i, ghostBall.transform.position);
        }
        
        Destroy(ghostBall);
    }
    
    private GameObject CreateGhostObjectInSimulationScene(Transform obj, bool isObjectPersistent)
    {
        return CreateGhostObjectInSimulationScene(obj, obj.position, isObjectPersistent);
    }

    private GameObject CreateGhostObjectInSimulationScene(Transform obj, Vector3 pos, bool isObjectPersistent)
    {
        var ghostObj = Instantiate(obj.gameObject, pos, obj.rotation);
        if(isObjectPersistent)
            ghostObj.GetComponent<Renderer>().enabled = false;
        SceneManager.MoveGameObjectToScene(ghostObj, simulationScene);

        return ghostObj;
    }
}
