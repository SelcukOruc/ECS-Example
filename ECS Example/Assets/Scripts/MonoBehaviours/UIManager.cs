using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIManager : MonoBehaviour
{
    public delegate void OnEnemyKilled();
    public static OnEnemyKilled OnScored;

    [SerializeField] private TextMeshProUGUI scoreText;

    private void OnEnable()
    {
        OnScored += UIManager_OnScored;
    }
    private void OnDisable()
    {
        OnScored -= UIManager_OnScored;
    }

    void UIManager_OnScored() => scoreText.text = "Score : " + GameVariables.Score++;

}
