using System;
using UnityEngine;

public class OpenedBook : MonoBehaviour
{
    [SerializeField] private Book book; // Reference to the Book component
    public bool IsBookOpen = false;

    private void Awake()
    {
        if (book == null)
        {
            book = GetComponentInParent<Book>();
        }
    }

    private void OnEnable()
    {
        IsBookOpen = true;
        SoundManager.Instance.PlaySFX(SoundManager.Instance.OpenBookSFX);

        book.CurrentIndex = 0; // Reset to the first page when the book is opened
        ItemSO[] currentItems = book.GetCurrentItemPage();
        for (int i = 0; i < currentItems.Length; i++)
        {
            book.UpdateUIElements(i, currentItems[i]);
        }
    }

    private void Update()
    {
        if(book.Items.Count <= 2)
        {
            book.NextButton.gameObject.SetActive(false);
            book.PreviousButton.gameObject.SetActive(false);
        }
        else
        {
            if(book.CurrentIndex >= book.Items.Count - 1)
            {
                book.NextButton.gameObject.SetActive(false);
            }
            else
            {
                book.NextButton.gameObject.SetActive(true);
            }

            if (book.CurrentIndex <= 0)
            {
                book.PreviousButton.gameObject.SetActive(false);
            }
            else
            {
                book.PreviousButton.gameObject.SetActive(true);
            }
        }
    }

    private void OnDisable()
    {
        IsBookOpen = false;
        SoundManager.Instance.PlaySFX(SoundManager.Instance.OpenBookSFX);
    }
}
