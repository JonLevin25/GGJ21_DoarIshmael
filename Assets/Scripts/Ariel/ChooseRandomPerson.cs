using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

namespace Ariel
{
    public class ChooseRandomPerson : MonoBehaviour
    {
        List<GameObject> characters = new List<GameObject>();

        private Vector3 savedPos;
        private Quaternion savedRot;

        [SerializeField]
        private ChooseRandomPersonOptions randomPersonOptions;

        private void OnEnable()
        {
            savedPos = transform.position;
            savedRot = transform.rotation;
            ChangeCharacter();
        }

        List<GameObject> FindOptions()
        {
            if (characters.Count > 0)
                return characters;
            for (int i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i).gameObject;
                if (child.name.StartsWith("Character_"))
                {
                    characters.Add(child);
                }
            }
            return characters;
        }
        
        [ContextMenu("Change Character")]
        public void ChangeCharacter()
        {
            var options = FindOptions();
            foreach (var option in options)
            {
                option.SetActive(false);
            }

            var character = options.RandomItem();
            character.SetActive(true);
            character.GetComponent<SkinnedMeshRenderer>().material.mainTexture = randomPersonOptions.textures.Choice();
            var scale = Random.Range(0.8f, 1f);
            transform.localScale = new Vector3(scale, scale, scale);
        }

        public void CrouchStarted()
        {
            transform.position = savedPos;
            transform.rotation = savedRot;
            ChangeCharacter();
        }
    }
}