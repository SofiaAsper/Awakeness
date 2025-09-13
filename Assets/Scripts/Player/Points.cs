using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Points : MonoBehaviour
{

    public TextMeshProUGUI pointsText;
    public PlayerAspects playerAspects;

    // Start is called before the first frame update
    void Start()
    {
         pointsText.text = "Points:" + playerAspects.playerPoints;
    }

    // Update is called once per frame
    void Update()
    {
        pointsText.text = "Points:" + playerAspects.playerPoints;
    }
}
