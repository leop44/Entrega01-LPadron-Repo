using UnityEngine;
using UnityEngine.UI;

public class HUDGame : MonoBehaviour
{
    public Text coinScore;
    public static int coinCount = 0;
    private void Update()
    {
        coinScore.text = coinCount.ToString();
    }
}
