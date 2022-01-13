using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData 
{
    public int sceneIndex;
    public int energy;
    public int sceneInitialEnergy;
    public int gameVolume;
    public float x;
    public float y;
    public float z;

    public PlayerData(int sceneIndex, int energy, int sceneInitialEnergy, int gameVolume, float x, float y, float z)
    {
        this.sceneIndex = sceneIndex;
        this.energy = energy;
        this.sceneInitialEnergy = sceneInitialEnergy;
        this.gameVolume = gameVolume;
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public override string ToString()
    {
        return $"scene: {sceneIndex}, energy: {energy}, initial energy:{sceneInitialEnergy}, volume: {gameVolume}, x: {x}, y: {y}, z: {z}";
    }
}
