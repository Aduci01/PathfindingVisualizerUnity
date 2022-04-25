using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace FMGames.Pathfinding
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private MazeSettings mazeSettings;
        [SerializeField] private PathfinderSettings pathfinderSettings;

        [SerializeField] private TMP_Dropdown mazeDropdown;
        [SerializeField] private TMP_Dropdown mazeGeneratorDropdown;
        [SerializeField] private TMP_Dropdown pathfinderDropdown;


        [SerializeField] private Slider delaySlider;


        [Space(10)]
        [Header("Stats of Pathfinding")]
        [SerializeField] private TMP_Text stepCountText;
        [SerializeField] private TMP_Text shortestPathText;
        [SerializeField] private GameObject statObject;


        // Start is called before the first frame update
        void Start()
        {
            SetMazeDropDown();

            SetMazeGeneratorDropDown();

            SetPathfinderDropDown();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void SetMazeGeneratorDropDown()
        {
            mazeGeneratorDropdown.ClearOptions();

            List<TMP_Dropdown.OptionData> optionDatas = new List<TMP_Dropdown.OptionData>();
            optionDatas.Add(new TMP_Dropdown.OptionData("Generate Maze"));

            foreach (IMazeGenerator img in mazeSettings.GetGenerators())
            {
                TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData();
                option.text = img.GetName();

                optionDatas.Add(option);
            }

            mazeGeneratorDropdown.AddOptions(optionDatas);
        }

        private void SetMazeDropDown()
        {
            mazeDropdown.ClearOptions();

            List<TMP_Dropdown.OptionData> optionDatas = new List<TMP_Dropdown.OptionData>();
            optionDatas.Add(new TMP_Dropdown.OptionData("Choose Board Size"));

            foreach (MazeSetting m in mazeSettings.GetSettings())
            {
                TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData();
                option.text = m.xSize + " X " + m.ySize;

                optionDatas.Add(option);
            }

            mazeDropdown.AddOptions(optionDatas);
        }

        private void SetPathfinderDropDown()
        {
            pathfinderDropdown.ClearOptions();

            List<TMP_Dropdown.OptionData> optionDatas = new List<TMP_Dropdown.OptionData>();
            optionDatas.Add(new TMP_Dropdown.OptionData("Choose Pathfinder"));

            foreach (IPathfinder p in pathfinderSettings.GetPathfinders())
            {
                TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData();
                option.text = p.GetName();

                optionDatas.Add(option);
            }

            pathfinderDropdown.AddOptions(optionDatas);
        }

        public void SetDelaySlider()
        {
            GameManager._Instance.SetDelay(delaySlider.value);
        }

        public void OnClickFieldGeneration()
        {
            if (mazeDropdown.value == 0) return;

            statObject.SetActive(false);

            GameManager._Instance.GenerateField(mazeSettings.GetSettings()[mazeDropdown.value - 1]);
            mazeDropdown.value = 0;
        }

        public void OnClickMazeGeneration()
        {
            if (mazeGeneratorDropdown.value == 0) return;

            statObject.SetActive(false);

            GameManager._Instance.GenerateMaze(mazeSettings.GetGenerators()[mazeGeneratorDropdown.value - 1]);
            mazeGeneratorDropdown.value = 0;
        }

        public void OnClickPathfinding()
        {
            if (pathfinderDropdown.value == 0) return;

            statObject.SetActive(true);
            shortestPathText.text = "-";

            GameManager._Instance.FindPath(pathfinderSettings.GetPathfinders()[pathfinderDropdown.value - 1]);
            pathfinderDropdown.value = 0;
        }

        public void SetSteps(int stepCount)
        {
            stepCountText.text = "" + stepCount;
        }

        public void SetShortestPath(int stepCount)
        {
            shortestPathText.text = "" + stepCount;
        }
    }
}