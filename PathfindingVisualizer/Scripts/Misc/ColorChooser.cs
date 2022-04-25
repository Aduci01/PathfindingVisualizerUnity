using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FMGames.Pathfinding
{
    public class ColorChooser : MonoBehaviour
    {
        private List<ColorUi> colors;
        [SerializeField] private Transform parent;
        [SerializeField] private ColorUi colorPrefab;

        [SerializeField] private Slider redSlider, blueSlider, greenSlider;
        [SerializeField] private TMP_Text redText, blueText, greenText;

        [SerializeField] private Image addImage;

        // Start is called before the first frame update
        void Start()
        {
            colors = new List<ColorUi>();

            foreach (Color c in PathfindingTools.GetColors())
            {
                var newColor = Instantiate(colorPrefab);
                newColor.transform.SetParent(parent);

                newColor.Setup(c);
                colors.Add(newColor);

                newColor.gameObject.SetActive(true);
            }
        }

        public void AddColor()
        {
            var newColor = Instantiate(colorPrefab);
            newColor.transform.SetParent(parent);

            newColor.Setup(new Color(redSlider.value, greenSlider.value, blueSlider.value));
            colors.Add(newColor);

            newColor.gameObject.SetActive(true);

            SetColors();
        }

        public void RemoveColor(ColorUi c)
        {
            if (colors.Count == 1) return;

            colors.Remove(c);
            Destroy(c.gameObject);
        }

        private void SetColors()
        {
            Color[] colorArray = new Color[colors.Count];

            for (int i = 0; i < colorArray.Length; i++)
                colorArray[i] = colors[i].color;

            PathfindingTools.SetColors(colorArray);
        }

        public void SetRedSlider()
        {
            redText.text = (int)(255 * redSlider.value) + "";
            SetAddImage();
        }

        public void SetBlueSlider()
        {
            blueText.text = (int)(255 * blueSlider.value) + "";
            SetAddImage();
        }

        public void SetGreenSlider()
        {
            greenText.text = (int)(255 * greenSlider.value) + "";
            SetAddImage();
        }

        public void IncreaseTransformChildPosition(ColorUi c)
        {
            c.transform.SetSiblingIndex(c.transform.GetSiblingIndex() + 1);
        }

        private void SetAddImage()
        {
            addImage.color = new Color(redSlider.value, greenSlider.value, blueSlider.value);
        }
    }
}