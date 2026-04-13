using System;
using UnityEngine;

[Serializable]
public class NavigationNode : MonoBehaviour
{
    [Header("References")]
    public GameObject SelectionOutline;
    public GameObject[] PlayerText;
    private bool[] _selected = new bool[2];

    [Header("Directional Navigation Nodes")]
    public NavigationNode[] North = new NavigationNode[2];
    public NavigationNode[] East = new NavigationNode[2];
    public NavigationNode[] South = new NavigationNode[2];
    public NavigationNode[] West = new NavigationNode[2];
    public NavigationNode[] NorthConnections;
    public NavigationNode[] EastConnections;
    public NavigationNode[] SouthConnections;
    public NavigationNode[] WestConnections;

    public void Select(int playerIndex)
    {
        _selected[playerIndex] = true;
        UpdateSelectionVisual(playerIndex);
        UpdateNodeConnections(playerIndex);
    }

    public void Deselect(int playerIndex)
    {
        _selected[playerIndex] = false;
        UpdateSelectionVisual(playerIndex);
    }

    private void UpdateSelectionVisual(int playerIndex)
    {
        if (_selected[playerIndex])
        {
            PlayerText[playerIndex].SetActive(true);
        }
        else
        {
            PlayerText[playerIndex].SetActive(false);
        }

        if (_selected[0] || _selected[1])
        {
            SelectionOutline.SetActive(true);
        }
        else
        {
            SelectionOutline.SetActive(false);
        }
    }

    private void UpdateNodeConnections(int playerIndex)
    {
        foreach (var node in NorthConnections)
        {
            node.South[playerIndex] = this;
        }

        foreach (var node in EastConnections)
        {
            node.West[playerIndex] = this;
        }

        foreach (var node in SouthConnections)
        {
            node.North[playerIndex] = this;
        }

        foreach (var node in WestConnections)
        {
            node.East[playerIndex] = this;
        }
    }
}
