using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameHUD : MonoBehaviour
{
    [SerializeField]TextMeshProUGUI score;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void showScore(int x)
    {
        score.text = x.ToString();
    }
}
