using UnityEngine;
using UnityEngine.UI; 
using System.IO;

public class WallpaperManager : MonoBehaviour
{
    public Image targetImage; 

    void Start()
    {
        string wallpaperPath = WallpaperFetcher.GetWallpaperPath();

        if (File.Exists(wallpaperPath))
        {
            byte[] wallpaperBytes = File.ReadAllBytes(wallpaperPath);
            Texture2D wallpaperTexture = new Texture2D(2, 2);
            wallpaperTexture.LoadImage(wallpaperBytes); 

            Sprite wallpaperSprite = Sprite.Create(wallpaperTexture, new Rect(0.0f, 0.0f, wallpaperTexture.width, wallpaperTexture.height), new Vector2(0.5f, 0.5f));

            targetImage.sprite = wallpaperSprite;
        }
        else
        {
            Debug.LogError("Could not find wallpaper.");
        }
    }
}
