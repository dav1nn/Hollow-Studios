using System.IO;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.Video; 

public class WallpaperEngineFetcher : MonoBehaviour
{

    public Image targetImage;     
    public VideoPlayer videoPlayer; 


    private const string wallpaperEnginePath = @"C:\Program Files (x86)\Steam\steamapps\common\wallpaper_engine\projects\user\";

    void Start()
    {
  
        if (Directory.Exists(wallpaperEnginePath))
        {
  
            string[] folders = Directory.GetDirectories(wallpaperEnginePath);
            foreach (string folder in folders)
            {
 
                string[] files = Directory.GetFiles(folder, "*.*", SearchOption.TopDirectoryOnly);
                foreach (string file in files)
                {

                    if (file.EndsWith(".png") || file.EndsWith(".jpg") || file.EndsWith(".jpeg"))
                    {
                        Debug.Log("Found static wallpaper: " + file);
                        LoadStaticWallpaper(file);
                        return; 
                    }
                 
                    else if (file.EndsWith(".webm") || file.EndsWith(".mp4"))
                    {
                        Debug.Log("Found animated wallpaper: " + file);
                        LoadAnimatedWallpaper(file);
                        return; 
                    }
                }
            }
        }
        else
        {
            Debug.LogError("Wallpaper Engine path does not exist.");
        }
    }

    private void LoadStaticWallpaper(string filePath)
    {

        byte[] wallpaperBytes = File.ReadAllBytes(filePath);
        Texture2D wallpaperTexture = new Texture2D(2, 2);
        wallpaperTexture.LoadImage(wallpaperBytes); 


        Sprite wallpaperSprite = Sprite.Create(wallpaperTexture, new Rect(0.0f, 0.0f, wallpaperTexture.width, wallpaperTexture.height), new Vector2(0.5f, 0.5f));

   
        targetImage.sprite = wallpaperSprite;

  
        videoPlayer.Stop();
        videoPlayer.gameObject.SetActive(false);
        targetImage.gameObject.SetActive(true);
    }

    private void LoadAnimatedWallpaper(string filePath)
    {
  
        videoPlayer.url = filePath; 
        videoPlayer.Play();

    
        targetImage.gameObject.SetActive(false); 
        videoPlayer.gameObject.SetActive(true); 
    }
}
