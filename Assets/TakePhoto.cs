using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakePhoto : MonoBehaviour
{
    public Camera cam;

    Texture2D RTImage(Camera camera)
    {
        var currentRT = RenderTexture.active;
        RenderTexture.active = camera.targetTexture;

        camera.Render();

        Texture2D image = new Texture2D(camera.targetTexture.width, camera.targetTexture.height);
        image.ReadPixels(new Rect(0, 0, camera.targetTexture.width, camera.targetTexture.height), 0, 0);
        image.Apply();

        RenderTexture.active = currentRT;
        return image;
    }
    public void Render()
    {
        RTImage(cam);

    };


    public void Take()
    {
        TakePhoto newPhoto = CreatePhoto();
        SetPhotoImage(newPhoto);
    }

    private Photo CreatePhoto()
    {
        GameObject photoObject = Instantiate(photoPrefab, spawnLocation.position, spawnLocation.rotation, transform);
        return photoObject.GetComponent<photoObject>();
    }

    private void SetPhotoImage(Photo photo)
    {
        Texture2D newTexture = RenderCameraToTexture(renderCamera);
        photo.SetImage(newTexture);
    }

    private Texture2D RenderCameraToTexture(Camera camera)
    {
        camera.Render();
        RenderTexture.active = camera.targetTexture;

        Texture2D photo = new Texture2D(256, 256, TextureFormat.RGB24, false);
        photo.ReadPixels(new Rect(0, 0, 256, 256), 0, 0);
        photo.Apply();

        return photo;
    }

    public void TurnOn()
    {
        RenderCameraToTexture().enabled = true;
        screenRenderer.material.color = Color.white;
    }

    public void TurnOff()
    {
        RenderCameraToTexture().enabled = false;
        screenRenderer.material.color = Color.black;
    }
}
