
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoopList : MonoBehaviour {

    public Data[] data;

    public Data GetData(int index) {
        return data[Mathf.Abs(index % data.Length)];
    }

    public LoopListItem[] children;

    public LinkedList<LoopListItem> childrenDeque;
    
    public float offset = 200;

    public float duration = 0.2f;

    private void Start() {
        childrenDeque = new LinkedList<LoopListItem>();

        foreach (LoopListItem item in children) {
            childrenDeque.AddLast(item);
            item.SetData(item.index < 0 ? GetData(data.Length + item.index) : GetData(item.index));
            item.GetComponent<Button>().onClick.AddListener(() => Shift(item));
        }
    }

    private void Shift(LoopListItem item) {
        switch (item.index) {
            case < 0:
                ShiftRight();
                break;
            case > 0:
                ShiftLeft();
                break;
        }
    }

    private void ShiftRight() {
        ReplaceRightOverflow();
        
        foreach (LoopListItem item in children) {
            item.ShiftRight(offset, duration);
        }
    }

    private void ReplaceRightOverflow() {
        LoopListItem last = childrenDeque.Last.Value;
        childrenDeque.RemoveLast();

        last.index = -last.index - 1;
        last.SetData(GetData(data.Length + (childrenDeque.First.Value.CurrentData.index - 1)));

        childrenDeque.AddFirst(last);
    }

    private void ShiftLeft() {
        ReplaceLeftOverflow();
        
        foreach (LoopListItem item in children) {
            item.ShiftLeft(offset, duration);
        }
    }
    
    private void ReplaceLeftOverflow() {
        LoopListItem first = childrenDeque.First.Value;
        childrenDeque.RemoveFirst();

        first.index = Mathf.Abs(first.index) + 1;
        first.SetData(GetData(childrenDeque.Last.Value.CurrentData.index + 1));

        childrenDeque.AddLast(first);
    }

}

