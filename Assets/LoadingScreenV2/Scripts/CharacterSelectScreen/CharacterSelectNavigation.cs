using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class CharacterSelectNavigation : MonoBehaviour
{
    public PlayerInput playerInput;
    public NavigationNode[] SelectedNode = new NavigationNode[2];

    void Start()
    {
        SelectedNode[0].Select(0);
        SelectedNode[1].Select(1);
    }

    private void OnEnable()
    {
        playerInput.actions.FindActionMap("Player 1 UI").Enable();
        playerInput.actions.FindActionMap("Player 2 UI").Enable();
    }

    private void OnDisable()
    {
        playerInput.actions.FindActionMap("Player 1 UI").Disable();
        playerInput.actions.FindActionMap("Player 2 UI").Disable();
    }

    public void Player1Navigate(CallbackContext context)
    {
        Vector2 direction = context.ReadValue<Vector2>();
        NavigateToNewNode(0, direction);
    }

    public void Player2Navigate(CallbackContext context)
    {
        Vector2 direction = context.ReadValue<Vector2>();
        NavigateToNewNode(1, direction);
    }

    private void NavigateToNewNode(int playerIndex, Vector2 direction)
    {
        SelectedNode[playerIndex].Deselect(playerIndex);

        if (direction == Vector2.up)
        {
            SelectedNode[playerIndex] = SelectedNode[playerIndex].North[playerIndex];
        }
        else if (direction == Vector2.right)
        {
            SelectedNode[playerIndex] = SelectedNode[playerIndex].East[playerIndex];
        }
        else if (direction == Vector2.down)
        {
            SelectedNode[playerIndex] = SelectedNode[playerIndex].South[playerIndex];
        }
        else if (direction == Vector2.left)
        {
            SelectedNode[playerIndex] = SelectedNode[playerIndex].West[playerIndex];
        }

        SelectedNode[playerIndex].Select(playerIndex);
    }
}
