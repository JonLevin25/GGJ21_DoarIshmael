using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utils.ScriptableObjects
{
    [CreateAssetMenu(fileName = "SceneLoader", menuName = "Utils/SceneLoader", order = 2000)]
    public class SceneLoader : ScriptableObject
    {
        // Not the best way, but linking to scenes needs a bigger framework
        [SerializeField] private SceneReference gameScene;

        public AsyncOperation LoadGameScene()
        {
            return SceneManager.LoadSceneAsync(gameScene);
        }
    }
}