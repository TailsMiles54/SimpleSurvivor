using System;
using System.Collections.Generic;
using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "AnimatorOverrides", menuName = "SimpleSurvival/AnimatorOverrides", order = 1)]
    public class AnimatorOverrides : ScriptableObject
    {
        public List<ClassOverrideAnimator> ClassOverrideAnimators;
    }

    [Serializable]
    public class ClassOverrideAnimator
    {
        public PlayerClasses Class;
        public AnimatorOverrideController AnimatorOverrideController;
    }
}