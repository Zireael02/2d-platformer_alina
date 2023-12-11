using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platformer
{
    public class EndGame : MonoBehaviour
    {
        [SerializeField] private GameObject _canvasEndMenu;

        private void Awake()
        {
            _canvasEndMenu.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                _canvasEndMenu.SetActive(true);
                Time.timeScale = 0f;

            }          
        }

        public void NewGame()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(0);
        }

        public void Quit()
        {
            ;
            Application.Quit();
        }

    }
}