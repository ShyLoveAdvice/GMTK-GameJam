using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
    public List<AudioSource> Chicken;
    public List<AudioSource> Deer;
    public List<AudioSource> Elephant;
    public List<AudioSource> Mosue;
    public List<AudioSource> Rabbit;
    public List<AudioSource> Sheep;
    public List<AudioSource> Snake;
    public List<AudioSource> Tiger;
    public List<AudioSource> Turtle;

    public List<AudioSource> PieceLetGo;
    public List<AudioSource> PiecePickUp;
    public List<AudioSource> PiecePlace;

    public List<AudioSource> UIButtonClicked;


    private void Start() {
        GetSFX();
    }
    private void GetSFX()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            AudioSource audioSource = transform.GetChild(i).GetComponent<AudioSource>();
            if(transform.GetChild(i).name.Contains("Chicken"))
                Chicken.Add(audioSource);
            if(transform.GetChild(i).name.Contains("Deer"))
                Deer.Add(audioSource);
            if(transform.GetChild(i).name.Contains("Elephant"))
                Elephant.Add(audioSource);
            if(transform.GetChild(i).name.Contains("Mosue"))
                Mosue.Add(audioSource);
            if(transform.GetChild(i).name.Contains("Rabbit"))
                Rabbit.Add(audioSource);
            if(transform.GetChild(i).name.Contains("Sheep"))
                Sheep.Add(audioSource);
            if(transform.GetChild(i).name.Contains("Snake"))
                Snake.Add(audioSource);
            if(transform.GetChild(i).name.Contains("Tiger"))
                Tiger.Add(audioSource);
            if(transform.GetChild(i).name.Contains("Turtle"))
                Turtle.Add(audioSource);

            if(transform.GetChild(i).name.Contains("Piece Let Go"))
                PieceLetGo.Add(audioSource);
            if(transform.GetChild(i).name.Contains("Piece Pick Up"))
                PiecePickUp.Add(audioSource);
            if(transform.GetChild(i).name.Contains("Piece Place"))
                PiecePlace.Add(audioSource);

            if(transform.GetChild(i).name.Contains("UI Button Clicked"))
                UIButtonClicked.Add(audioSource);
        }
    }

    public void PlayChickenSFX()
    {
        PlayRandomSFX(Chicken);
    }
    public void PlayDeerSFX()
    {
        PlayRandomSFX(Deer);
    }
    public void PlayElephantSFX()
    {
        PlayRandomSFX(Elephant);
    }
    public void PlayMosueSFX()
    {
        PlayRandomSFX(Mosue);
    }
    public void PlayRabbitSFX()
    {
        PlayRandomSFX(Rabbit);
    }
    public void PlaySheepSFX()
    {
        PlayRandomSFX(Sheep);
    }
    public void PlaySnakeSFX()
    {
        PlayRandomSFX(Snake);
    }
    public void PlayTigerSFX()
    {
        PlayRandomSFX(Tiger);
    }
    public void PlayTurtleSFX()
    {
        PlayRandomSFX(Turtle);
    }
    public void PlayPieceLetGoSFX()
    {
        PlayRandomSFX(PieceLetGo);
    }
    public void PlayPiecePickUpSFX()
    {
        PlayRandomSFX(PiecePickUp);
    }
    public void PlayPiecePlaceSFX()
    {
        PlayRandomSFX(PiecePlace);
    }
    public void PlayUIButtonClickedSFX()
    {
        PlayRandomSFX(UIButtonClicked);
    }
    private void PlayRandomSFX(List<AudioSource> audioSourceList)
    {
        var i = Random.Range(0, audioSourceList.Count);
        audioSourceList[i].Play();
    }
}
