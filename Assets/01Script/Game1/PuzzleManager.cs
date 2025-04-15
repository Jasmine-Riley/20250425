using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    private Dictionary<PuzzleSlot, Puzzle> correct = new Dictionary<PuzzleSlot, Puzzle>();
    private Dictionary<PuzzleSlot, Puzzle> slots = new Dictionary<PuzzleSlot, Puzzle>();

    private PuzzleSlot nowPuzzleSlot;
    private PuzzleSlot selectedPuzzleSlot;
    private Puzzle selectedPuzzle;

    private GameObject player;

    private void Awake()
    {
        var puzzle = GameObject.Find("Puzzle");
        for(int i = 0; i < puzzle.transform.childCount; i++)
        {
            if (puzzle.transform.GetChild(i).TryGetComponent<PuzzleSlot>(out var puzzleSlot))
            {
                puzzleSlot.Init(this);

                correct.Add(puzzleSlot, puzzleSlot.puzzle);
                slots.Add(puzzleSlot, puzzleSlot.puzzle);
            }
        }
    }

    private void Start()
    {
        player = GameManager.Instance.Player;

        if (player.TryGetComponent<PlayerController>(out var controller))
            controller.RemoveButtonInteraction();

        UIManager.OnPressBtnSlot1 += Select;
    }

    private void Update()
    {
        //if (!selectedPuzzleSlot) return;

        //selectedPuzzleSlot.puzzle.transform.position = player.transform.position;

        if (!selectedPuzzle) return;

        selectedPuzzle.transform.position = player.transform.position;
            
    }

    public void Current(PuzzleSlot slot)
    {
        nowPuzzleSlot = slot;
    }

    public void CurrentOut(PuzzleSlot slot)
    {
        if (slot == nowPuzzleSlot)
            nowPuzzleSlot = null;
    }

    public void Select()
    {
        //if (!selectedPuzzleSlot)
        //    selectedPuzzleSlot = nowPuzzleSlot;
        //else
        //{
        //    if (selectedPuzzleSlot == nowPuzzleSlot) return;

        //    ChangeSlot(selectedPuzzleSlot, nowPuzzleSlot);
        //    selectedPuzzleSlot = null;
        //}
        Debug.Log("선택");

        if (!selectedPuzzle)
            PutUpPuzzle(nowPuzzleSlot);
        else
        {
            PutDownPuzzle(nowPuzzleSlot);
            //ChangeSlot(selectedPuzzleSlot, nowPuzzleSlot);
            //selectedPuzzleSlot = null;
        }
    }

    public void PutUpPuzzle(PuzzleSlot slot)
    {
        Debug.Log("들어올려");
        selectedPuzzle = slot.puzzle;
        slot.puzzle = null;

        Debug.Log(selectedPuzzle.name);
    }

    public void PutDownPuzzle(PuzzleSlot slot)
    {
        var puzzle = slot.puzzle;

        slot.puzzle = selectedPuzzle;
        selectedPuzzle = null;

        if (puzzle)
            selectedPuzzle = puzzle;
    }

    public void ChangeSlot(PuzzleSlot slot1, PuzzleSlot slot2)
    {
        Puzzle puzzleSlice = slot1.puzzle;
        slots[slot1] = slot2.puzzle;
        if (slots.TryGetValue(slot1, out var puzzle1))
        {
            puzzle1.transform.SetParent(slot1.transform);
            puzzle1.transform.localPosition = Vector3.zero;
        }

        slots[slot2] = puzzleSlice;
        if (slots.TryGetValue(slot2, out var puzzle2))
        {
            puzzle2.transform.SetParent(slot2.transform);
            puzzle2.transform.localPosition = Vector3.zero;
        }
    }
}
