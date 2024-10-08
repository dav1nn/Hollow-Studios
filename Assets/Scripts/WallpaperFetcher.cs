using System;
using System.Runtime.InteropServices;
using UnityEngine;
using System.IO;

public class WallpaperFetcher : MonoBehaviour
{
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern int SystemParametersInfo(int uAction, int uParam, String lpvParam, int fuWinIni);

    private const int SPI_GETDESKWALLPAPER = 0x0073;

    public static string GetWallpaperPath()
    {
        // Wallpaper path buffer
        string wallpaperPath = new string('\0', 260);
        SystemParametersInfo(SPI_GETDESKWALLPAPER, wallpaperPath.Length, wallpaperPath, 0);
        return wallpaperPath.TrimEnd('\0');
    }
}
