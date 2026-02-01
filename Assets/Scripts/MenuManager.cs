using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip click;
    [SerializeField] private GameObject instructions;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadGame()
    {
        StartCoroutine(LoadGameCoroutine());
    }

    private IEnumerator LoadGameCoroutine()
    {
        audioSource.PlayOneShot(click);
        if (click != null)
            yield return new WaitForSeconds(click.length);
        else
            yield return null;
        SceneManager.LoadScene("MainScene");
    }
    public void ShowInstruct()
    {
        StartCoroutine(PlaySound());
        instructions.SetActive(true);
    }
    public void HideInstruct()
    {
        StartCoroutine(PlaySound());
        instructions.SetActive(false);
    }
    public IEnumerator PlaySound()
    {
        audioSource.PlayOneShot(click);
        if (click != null)
            yield return new WaitForSeconds(click.length);
        else
            yield return null;
        
    }
}
