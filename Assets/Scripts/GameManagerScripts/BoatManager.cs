using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatManager : MonoBehaviour
{
    public Transform firstBoat, secondBoat;
    public BoatAnimations boatAnimations;

    public static Vector2 firstBoatPosition, secondBoatPosition;

    private void Update()
    {
        GetBoatPositions();
    }

    /// <summary>
    /// Permet de récupérer les positions des bateaux, et les stocker dans des variable static
    /// </summary>
    void GetBoatPositions()
    {
        firstBoatPosition = firstBoat.position;
        secondBoatPosition = secondBoat.position;
    }

    public void ActiveGameOverCanvas()
    {
        GameManager.gameManager.gameOverGUI.CanvasAppear();
    }
}
