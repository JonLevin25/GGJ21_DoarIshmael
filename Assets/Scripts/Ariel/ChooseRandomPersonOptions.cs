using UnityEngine;

namespace Ariel
{
    [CreateAssetMenu(fileName = "ChooseRandomPerson", menuName = "SO/ChooseRandomPerson", order = 0)]
    public class ChooseRandomPersonOptions : ScriptableObject
    {
        [SerializeField] public Texture[] textures;
        
    }
}