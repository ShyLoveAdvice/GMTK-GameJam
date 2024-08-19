using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Animals
{
    Bat, Chicken, Deer, Dog, Duck, Elephant, Frog, GoldFish, Mouse, Ladybug, Pig, Rabbit, Raccoon, Sheep, Snake, Spider, Tiger, Turtle
}
public class SFXPlayer : Singleton<SFXPlayer>
{
    public List<AudioSource> Bat;
    public List<AudioSource> Chicken;
    public List<AudioSource> Deer;
    public List<AudioSource> Dog;
    public List<AudioSource> Duck;
    public List<AudioSource> Elephant;
    public List<AudioSource> Frog;
    public List<AudioSource> GoldFish;
    public List<AudioSource> Mouse;
    public List<AudioSource> Ladybug;
    public List<AudioSource> Pig;
    public List<AudioSource> Rabbit;
    public List<AudioSource> Raccoon;
    public List<AudioSource> Sheep;
    public List<AudioSource> Snake;
    public List<AudioSource> Spider;
    public List<AudioSource> Tiger;
    public List<AudioSource> Turtle;

    public List<AudioSource> PieceLetGo;
    public List<AudioSource> PiecePickUp;
    public List<AudioSource> PiecePlace;

    public List<AudioSource> UIButtonClicked;


    private void Start()
    {
        GetSFX();
    }
    private void GetSFX()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            AudioSource audioSource = transform.GetChild(i).GetComponent<AudioSource>();
            if (transform.GetChild(i).name.Contains("Bat"))
                Bat.Add(audioSource);
            if (transform.GetChild(i).name.Contains("Chicken"))
                Chicken.Add(audioSource);
            if (transform.GetChild(i).name.Contains("Deer"))
                Deer.Add(audioSource);
            if (transform.GetChild(i).name.Contains("Dog"))
                Dog.Add(audioSource);
            if (transform.GetChild(i).name.Contains("Duck"))
                Duck.Add(audioSource);
            if (transform.GetChild(i).name.Contains("Elephant"))
                Elephant.Add(audioSource);
            if (transform.GetChild(i).name.Contains("Frog"))
                Frog.Add(audioSource);
            if (transform.GetChild(i).name.Contains("GoldFish"))
                GoldFish.Add(audioSource);
            if (transform.GetChild(i).name.Contains("Mouse"))
                Mouse.Add(audioSource);
            if (transform.GetChild(i).name.Contains("Ladybug"))
                Ladybug.Add(audioSource);
            if (transform.GetChild(i).name.Contains("Rabbit"))
                Rabbit.Add(audioSource);
            if (transform.GetChild(i).name.Contains("Raccoon"))
                Raccoon.Add(audioSource);
            if (transform.GetChild(i).name.Contains("Sheep"))
                Sheep.Add(audioSource);
            if (transform.GetChild(i).name.Contains("Snake"))
                Snake.Add(audioSource);
            if (transform.GetChild(i).name.Contains("Spider"))
                Spider.Add(audioSource);
            if (transform.GetChild(i).name.Contains("Tiger"))
                Tiger.Add(audioSource);
            if (transform.GetChild(i).name.Contains("Turtle"))
                Turtle.Add(audioSource);

            if (transform.GetChild(i).name.Contains("Piece Let Go"))
                PieceLetGo.Add(audioSource);
            if (transform.GetChild(i).name.Contains("Piece Pick Up"))
                PiecePickUp.Add(audioSource);
            if (transform.GetChild(i).name.Contains("Piece Place"))
                PiecePlace.Add(audioSource);

            if (transform.GetChild(i).name.Contains("UI Button Clicked"))
                UIButtonClicked.Add(audioSource);
        }
    }
    public void PlayAnimalSFX(Animals animal)
    {
        switch (animal)
        {
            case Animals.Bat:
                PlayBatSFX();
                return;
            case Animals.Chicken:
                PlayChickenSFX();
                return;
            case Animals.Deer:
                PlayDeerSFX();
                return;
            case Animals.Dog:
                PlayDogSFX();
                return;
            case Animals.Duck:
                PlayDuckSFX();
                return;
            case Animals.Elephant:
                PlayElephantSFX();
                return;
            case Animals.Frog:
                PlayFrogSFX();
                return;
            case Animals.GoldFish:
                PlayGoldFishSFX();
                return;
            case Animals.Mouse:
                PlayMouseSFX();
                return;
            case Animals.Ladybug:
                PlayLadybugSFX();
                return;
            case Animals.Pig:
                PlayPigSFX();
                return;
            case Animals.Rabbit:
                PlayRabbitSFX();
                return;
            case Animals.Raccoon:
                PlayRaccoonSFX();
                return;
            case Animals.Snake:
                PlaySnakeSFX();
                return;
            case Animals.Spider:
                PlaySpiderSFX();
                return;
            case Animals.Tiger:
                PlayBatSFX();
                return;
            case Animals.Turtle:
                PlayTurtleSFX();
                return;
                //default:
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
    public void PlayDogSFX()
    {
        PlayRandomSFX(Dog);
    }
    public void PlayDuckSFX()
    {
        PlayRandomSFX(Duck);
    }
    public void PlayElephantSFX()
    {
        PlayRandomSFX(Elephant);
    }
    public void PlayFrogSFX()
    {
        PlayRandomSFX(Frog);
    }
    public void PlayGoldFishSFX()
    {
        PlayRandomSFX(Deer);
    }
    public void PlayMouseSFX()
    {
        PlayRandomSFX(Mouse);
    }
    public void PlayLadybugSFX()
    {
        PlayRandomSFX(Ladybug);
    }
    public void PlayBatSFX()
    {
        PlayRandomSFX(Bat);
    }
    public void PlayPigSFX()
    {
        PlayRandomSFX(Pig);
    }
    public void PlayRabbitSFX()
    {
        PlayRandomSFX(Rabbit);
    }
    public void PlayRaccoonSFX()
    {
        PlayRandomSFX(Raccoon);
    }
    public void PlaySheepSFX()
    {
        PlayRandomSFX(Sheep);
    }
    public void PlaySnakeSFX()
    {
        PlayRandomSFX(Snake);
    }
    public void PlaySpiderSFX()
    {
        PlayRandomSFX(Spider);
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
        if(audioSourceList.Count == 0)
            return;
        var i = Random.Range(0, audioSourceList.Count);
        audioSourceList[i].Play();
    }
}
