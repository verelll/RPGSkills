using UnityEditor;
using UnityEngine;

namespace RPGSkills.SkillsBuilder
{
    [CustomEditor(typeof(UISkillsBuilder))]
    public class UISkillsBuilderEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            var builder = (UISkillsBuilder)target;

            DrawDefaultInspector();

            if (GUILayout.Button("Rebuild"))
            {
                builder.Rebuild();
            }
        }
    }
}

