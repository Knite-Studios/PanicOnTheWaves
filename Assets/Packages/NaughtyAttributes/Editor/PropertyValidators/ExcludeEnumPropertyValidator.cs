using System.Linq;
using Common.Attributes;
using UnityEditor;

namespace NaughtyAttributes.Editor
{
    public class ExcludeEnumPropertyValidator : PropertyValidatorBase
    {
        public override void ValidateProperty(SerializedProperty property)
        {
            var requiredAttribute = PropertyUtility.GetAttribute<ExcludeEnumAttribute>(property);

            if (property.propertyType == SerializedPropertyType.Enum)
            {
                if (requiredAttribute.ExcludedValues.Any(excludedValue => property.intValue == excludedValue))
                {
                    var warning = $"{property.displayName} cannot be {property.enumNames[property.intValue]}";
                    NaughtyEditorGUI.HelpBox_Layout(warning, MessageType.Error, context: property.serializedObject.targetObject);
                }
            }
            else
            {
                var warning = $"{requiredAttribute.GetType().Name} works only on enum types";
                NaughtyEditorGUI.HelpBox_Layout(warning, MessageType.Warning, context: property.serializedObject.targetObject);
            }
        }
    }
}