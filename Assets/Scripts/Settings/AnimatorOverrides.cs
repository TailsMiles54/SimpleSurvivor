using System;
using System.Collections.Generic;
using DefaultNamespace;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "AnimatorOverrides", menuName = "SimpleSurvival/AnimatorOverrides", order = 1)]
    public class AnimatorOverrides : SerializedScriptableObject
    {
        [field: SerializeField] public List<ClassOverrideAnimator> ClassOverrideAnimators;

        public ModelImporter _ModelImporter;
    }

    [Serializable]
    public class ClassOverrideAnimator
    {
        public PlayerClasses Class;
        public AnimatorOverrideController AnimatorOverrideController;
    }
}