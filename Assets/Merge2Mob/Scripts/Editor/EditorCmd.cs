using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace MergeTwoMob.Editor
{
    public class EditorCmd : MonoBehaviour
    {

        private const string PATH = "Assets/Merge2Mob/Scenes/{0}";

        [MenuItem("MergeTwoMob/Play")]
        private static void Play()
        {
            EditorSceneManager.OpenScene(string.Format(PATH, "Lobby.unity"));
            EditorApplication.isPlaying = true;
        }

        [MenuItem("MergeTwoMob/Load Lobby")]
        private static void LoadLobby()
        {
            EditorSceneManager.OpenScene(string.Format(PATH, "Lobby.unity"));
            EditorApplication.isPlaying = false;
        }
        
        [MenuItem("MergeTwoMob/Load Game")]
        private static void LoadGame()
        {
            EditorSceneManager.OpenScene(string.Format(PATH, "Game.unity"));
            EditorApplication.isPlaying = false;
        }
        
        [MenuItem("MergeTwoMob/Load UI")]
        private static void LoadUI()
        {
            EditorSceneManager.OpenScene(string.Format(PATH, "UI.unity"));
            EditorApplication.isPlaying = false;
        }
    }
    
}