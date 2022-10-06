using UnityEngine;

public class Lvl1Controller : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            GameController.GetGameController().SetPlayerLife(0.1f);

        }
    }
}
