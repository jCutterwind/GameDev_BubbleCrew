
using UnityEngine;
using UnityEngine.UI;

public class StarSystem : MonoBehaviour
{
    public Image[] starImages;  // Array di immagini delle stelle
    public Sprite starFilledSprite;  // Sprite per le stelle piene
    public Sprite starEmptySprite;   // Sprite per le stelle vuote

    private int currentRating = 0;  // Punteggio attuale (da 1 a 5)

    // Delegato per l'evento di aggiornamento del punteggio
    public delegate void RatingUpdated(int newRating);
    public event RatingUpdated OnRatingUpdated;

    // Start is called before the first frame update
    void Start()
    {
        // Imposta il punteggio iniziale (ad esempio, 3 stelle)
        SetRating(3);
    }

    // Imposta il punteggio e chiama l'evento di aggiornamento
    public void SetRating(int rating)
    {
        // Assicurati che il punteggio sia compreso tra 1 e 5
        rating = Mathf.Clamp(rating, 1, 5);

        // Imposta il punteggio attuale
        currentRating = rating;

        // Aggiorna l'aspetto delle stelle
        for (int i = 0; i < starImages.Length; i++)
        {
            if (i < rating)
            {
                // Mostra la stella piena
                starImages[i].sprite = starFilledSprite;
            }
            else
            {
                // Mostra la stella vuota
                starImages[i].sprite = starEmptySprite;
            }
        }

        // Chiamare l'evento di aggiornamento del punteggio
        if (OnRatingUpdated != null)
        {
            OnRatingUpdated(currentRating);
        }
    }

    // Aggiunto un metodo per ottenere il punteggio attuale
    public int GetCurrentRating()
    {
        return currentRating;
    }
}