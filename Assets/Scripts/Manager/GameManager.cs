using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   private GameSystems gameSystems;
    
    private void Start()
    {
        Contexts contexts = Contexts.sharedInstance;
        gameSystems = new GameSystems(contexts);
        gameSystems.Initialize();
    }
    
      void Update() {
        gameSystems.Execute();
        gameSystems.Cleanup();
    }

    void OnDestroy() {
        gameSystems.TearDown();
    }
}
