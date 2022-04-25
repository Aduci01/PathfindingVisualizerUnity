using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FMGames.Pathfinding
{
    public class ColorUi : MonoBehaviour
    {
        [SerializeField] private Image colorImage;
        public Color color;

        public void Setup(Color c)
        {
            color = c;
            colorImage.color = c;
        }
    }
}