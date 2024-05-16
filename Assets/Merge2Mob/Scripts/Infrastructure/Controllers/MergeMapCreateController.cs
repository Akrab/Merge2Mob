using MergeTwoMob.DIMerge;
using MergeTwoMob.GameScripts;
using MergeTwoMob.Merge2Mob.Scripts.Base;
using MergeTwoMob.RuntimeScripts;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;

namespace MergeTwoMob.Infrastructure.Controllers
{
    public class MergeMapCreateController
    {
        [Inject] private DiContainer diContainer;

        
        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name != CONSTANTS.GAME_SCENE) return;
            
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(CONSTANTS.GAME_SCENE));
            using var pooledObject1 = ListPool<GameObject>.Get(out var rootGameObjects);
            scene.GetRootGameObjects(rootGameObjects);
            
            foreach (var obj in rootGameObjects)
            {   

                var worldCreator = obj.GetComponent<MergeMapCreator>();
                if (worldCreator != null)
                {
                    diContainer.Inject(worldCreator);
                    worldCreator.Create();
                    return;
                }
            }
        }
        
        public MergeMapCreateController()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

 
    }
}