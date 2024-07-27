using Common.Attributes;
using UnityEditor;

namespace NaughtyAttributes.Editor
{
    public class RequiredEnumPropertyValidator : PropertyValidatorBase
    {
        public override void ValidateProperty(SerializedProperty property)
        {
            var requiredAttribute = PropertyUtility.GetAttribute<RequiredEnumAttribute>(property);

            if (property.propertyType == SerializedPropertyType.Enum)
            {
                if (property.enumValueIndex == requiredAttribute.ExcludedValue)
                {
                    var errorMessage = $"{property.displayName} must not be {property.enumValueIndex}";
                    if (!string.IsNullOrEmpty(requiredAttribute.Message))
                    {
                        errorMessage = requiredAttribute.Message;
                    }

                    NaughtyEditorGUI.HelpBox_Layout(errorMessage, MessageType.Error, context: property.serializedObject.targetObject);
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