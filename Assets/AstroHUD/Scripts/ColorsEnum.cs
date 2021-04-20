using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace ColorsEnumExtension
{
    public enum ColorsEnum
    {
        Positive,
        Neutral,
        Negative
    }

    public static class Extensions
    {
        public static Color ToColor(this ColorsEnum colorsEnum)
        {
            switch (colorsEnum)
            {
                case ColorsEnum.Positive:
                    return new Color32(118, 239, 152, 255);
                    break;
                case ColorsEnum.Neutral:
                    return new Color32(118, 179, 239, 255);
                    break;
                case ColorsEnum.Negative:
                    return new Color32(252, 50, 50, 255);
                    break;
                default:
                    return Color.clear;
                    break;
            }
        }
    }
}