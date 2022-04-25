using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FMGames.Pathfinding
{
    public class RandomImage : MonoBehaviour
    {
        [SerializeField] Sprite[] images;

        // Start is called before the first frame update
        void Start()
        {
            GetComponent<Image>().sprite = images[Random.Range(0, images.Length)];
        }
    }
}