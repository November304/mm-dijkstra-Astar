using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class UIManager : MonoBehaviour
{
    private VisualElement rootVisualElement;

    private Button hideShowButton;
    private ScrollView mainScroll;

    private IntegerField seedIntField;
    private Button randomizeSeed;
    private Vector2IntField gridSizeField;
    private Slider wallChanceSlider;
    private Label labelErrorGenerate;
    private Button generateGridButton;

    private Vector2IntField startPosField;
    private Vector2IntField endPosField;
    private Button randomizeButton;
    private EnumField pathfindingAlgorithmField;
    private Label labelErrorPathfinding;
    private Button findPathButton;

    private bool isUiHidden = false;

    private void Awake()
    {
        rootVisualElement = GetComponent<UIDocument>().rootVisualElement;

        hideShowButton = rootVisualElement.Q<Button>("hideShowButton");
        mainScroll = rootVisualElement.Q<ScrollView>("mainScroll");

        seedIntField = rootVisualElement.Q<IntegerField>("seedIntField");
        randomizeSeed = rootVisualElement.Q<Button>("randomizeSeedBtn");
        gridSizeField = rootVisualElement.Q<Vector2IntField>("gridSizeField");
        wallChanceSlider = rootVisualElement.Q<Slider>("wallChanceSlider");
        labelErrorGenerate = rootVisualElement.Q<Label>("labelErrorGen");
        generateGridButton = rootVisualElement.Q<Button>("generateBtn");

        startPosField = rootVisualElement.Q<Vector2IntField>("startPosField");
        endPosField = rootVisualElement.Q<Vector2IntField>("endPosField");
        randomizeButton = rootVisualElement.Q<Button>("randomizePosBtn");
        pathfindingAlgorithmField = rootVisualElement.Q<EnumField>("choixAlgoEnum");
        labelErrorPathfinding = rootVisualElement.Q<Label>("labelErrorPath");
        findPathButton = rootVisualElement.Q<Button>("generationChemin");
    }

    private void OnEnable()
    {
        hideShowButton.clicked += SwitchUiVisibility;
        randomizeSeed.clicked += RandomizeSeed;
        generateGridButton.clicked += StartGeneration;
        findPathButton.clicked += StartPathfinding;
        randomizeButton.clicked += GeneratePositions;
    }

    private void OnDisable()
    {
        hideShowButton.clicked -= SwitchUiVisibility;
        randomizeSeed.clicked -= RandomizeSeed;
        generateGridButton.clicked -= StartGeneration;
        findPathButton.clicked -= StartPathfinding;
        randomizeButton.clicked -= GeneratePositions;
    }

    private void SwitchUiVisibility()
    {
        isUiHidden = !isUiHidden;

        if (isUiHidden)
        {
            mainScroll.style.display = DisplayStyle.None;
            hideShowButton.text = ">";
        }
        else
        {
            mainScroll.style.display = DisplayStyle.Flex;
            hideShowButton.text = "<";
        }
    }

    private void RandomizeSeed()
    {
        seedIntField.value = Random.Range(0, 100000);
    }

    private void StartGeneration()
    {
        Vector2Int gridSize = gridSizeField.value;
        float wallChance = wallChanceSlider.value;
        int seed = seedIntField.value;

        Debug.Log($"Start generation with seed:{seed} size:{gridSize.x}x,{gridSize.y}y wallChance:{wallChance}");

        if (gridSize.x <= 0 || gridSize.y <= 0)
        {
            labelErrorGenerate.text = "Grid size must be greater than 0";
            labelErrorGenerate.style.display = DisplayStyle.Flex;
            return;
        }

        if (wallChance < 0 || wallChance > 1)
        {
            labelErrorGenerate.text = "Wall chance must be between 0 and 1";
            labelErrorGenerate.style.display = DisplayStyle.Flex;
            return;
        }

        labelErrorGenerate.style.display = DisplayStyle.None;

        Generator.Instance.Generate(seed, gridSize, wallChance);
    }

    private void GeneratePositions()
    {
        Vector2Int mapSize = Generator.Instance.GetMapSize();
        startPosField.value = new Vector2Int(Random.Range(0, mapSize.x), Random.Range(0, mapSize.y));
        endPosField.value = new Vector2Int(Random.Range(0, mapSize.x), Random.Range(0, mapSize.y));
    }

    private void StartPathfinding()
    {
        Vector2Int mapSize = Generator.Instance.GetMapSize();
        Vector2Int startPos = startPosField.value;
        if (startPos.x < 0 || startPos.x >= mapSize.x || startPos.y < 0 || startPos.y >= mapSize.y)
        {
            labelErrorPathfinding.text = "Start position is out of bounds";
            labelErrorPathfinding.style.display = DisplayStyle.Flex;
            return;
        }

        Vector2Int endPos = endPosField.value;
        if (endPos.x < 0 || endPos.x >= mapSize.x || endPos.y < 0 || endPos.y >= mapSize.y)
        {
            labelErrorPathfinding.text = "End position is out of bounds";
            labelErrorPathfinding.style.display = DisplayStyle.Flex;
            return;
        }

        Enum value = pathfindingAlgorithmField.value;
        if (value == null)
        {
            labelErrorPathfinding.text = "No pathfinding algorithm selected";
            labelErrorPathfinding.style.display = DisplayStyle.Flex;
            return;
        }
        AlgoGeneration algo = (AlgoGeneration)value;

        labelErrorPathfinding.style.display = DisplayStyle.None;
        AlgoPathfinding pathfinding = algo switch
        {
            AlgoGeneration.DIJKSTRA => new AlgoDijsktra(Generator.Instance.GetGrid(), startPos, endPos),
            AlgoGeneration.A_STAR => new AlgoAStar(Generator.Instance.GetGrid(), startPos, endPos),
            _ => throw new NotImplementedException(),
        };

        StartGeneration();
        List<Vector2Int> chemin = pathfinding.FindPath();


        if (chemin == null)
        {
            Generator.Instance.SetPos(startPosField.value, TileState.START);
            Generator.Instance.SetPos(endPosField.value, TileState.END);

            Generator.Instance.RenderGrid();
            labelErrorPathfinding.text = "No path found";
            labelErrorPathfinding.style.display = DisplayStyle.Flex;
            return;
        }


        Generator.Instance.SetChemin(chemin.ToArray());

        Generator.Instance.SetPos(startPos, TileState.START);
        Generator.Instance.SetPos(endPos, TileState.END);

        Generator.Instance.RenderGrid();


    }
}
