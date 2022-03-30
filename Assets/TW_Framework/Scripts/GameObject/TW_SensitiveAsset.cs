using System;
using UnityEngine;

namespace TW_Framework.Scripts.GameObject
{
    public class TW_SensitiveAsset : MonoBehaviour
    {
        [SerializeField]
        private string triggerKey;
        
        private void Awake()
        {
            WarnManager();
        }

        private void WarnManager()
        {
            throw new NotImplementedException();
        }
    }
}