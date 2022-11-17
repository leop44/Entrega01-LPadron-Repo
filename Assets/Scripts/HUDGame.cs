using UnityEngine;
using UnityEngine.UI;

public class HUDGame : MonoBehaviour
{
    public Text coinScore;
    public static int coinCount = 0;
    public Image lifeBar;
    public float actualLife;
    public float maxLife = 100f;

    private void Update()
    {
        coinScore.text = coinCount.ToString();

        actualLife = PlayerController.hpHero;
        lifeBar.fillAmount = actualLife / maxLife;
    }
}
