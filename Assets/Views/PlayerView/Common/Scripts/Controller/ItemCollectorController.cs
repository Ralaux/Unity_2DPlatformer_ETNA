using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class ItemCollectorController : MonoBehaviour
{
    [SerializeField] private Text coinText;
    [SerializeField] private Text maxCoinText;
    [SerializeField] private float msAddedPotion;
    [SerializeField] private float msPotionDuration;
    [SerializeField] private float jumpForceAddedPotion;
    [SerializeField] private float jumpPotionDuration;
    private PlayerMovementController pm;

    private void Start()
    {        
        if (!PlayerPrefs.HasKey("Coin")) {
            PlayerPrefs.SetInt("Coin", 0);
        }
        if (!PlayerPrefs.HasKey("maxCoins")) {
            PlayerPrefs.SetInt("maxCoins", 0);
        }
        if (PlayerPrefs.GetInt("Coin") > PlayerPrefs.GetInt("maxCoins")) {
            PlayerPrefs.SetInt("maxCoins", PlayerPrefs.GetInt("Coin"));
        }
        maxCoinText.text = ":" + PlayerPrefs.GetInt("maxCoins");
        coinText.text = ":" + PlayerPrefs.GetInt("Coin");
        pm = GetComponent<PlayerMovementController>();
    }

    

    private void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Piecerise")) {
            Destroy(collision.gameObject);
            PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") + 1);
            coinText.text = ":" + PlayerPrefs.GetInt("Coin");
        }
        if (collision.gameObject.CompareTag("Kiwitesse")) {
            Destroy(collision.gameObject);
            pm.model.MovementSpeedInit += msAddedPotion;
            pm.model.MovementSpeed += msAddedPotion;
            StartCoroutine(BecomeTemporarilyFast());

        }
        if (collision.gameObject.CompareTag("Jumpomme")) {
            Destroy(collision.gameObject);
            pm.model.JumpForceInit += jumpForceAddedPotion;
            pm.model.JumpForce += jumpForceAddedPotion;
            StartCoroutine(BecomeTemporarilyJumper());
        }
    }

    private IEnumerator BecomeTemporarilyFast()
    {
        yield return new WaitForSeconds(msPotionDuration);
        pm.model.MovementSpeedInit -= msAddedPotion;
        pm.model.MovementSpeed -= msAddedPotion;
    } 

    private IEnumerator BecomeTemporarilyJumper()
    {
        yield return new WaitForSeconds(jumpPotionDuration);
        pm.model.JumpForceInit -= jumpForceAddedPotion;
        pm.model.JumpForce -= jumpForceAddedPotion;
    } 

}
