using System;
using System.IO;
using System.Net.Http;

public class CatboxUploader
{
    // NOTE - You can also return Task<string> instead of void and remove the callback to just await this method
    public static async void UploadToCatbox(string filePath, Action<string> callback)
    {
        using var client = new HttpClient();
        client.BaseAddress = new Uri("https://catbox.moe/user/api.php");

        using var content = new MultipartFormDataContent();
			
        content.Add(new StringContent("fileupload"), "reqtype");
        content.Add(new StreamContent(File.OpenRead(filePath)), "fileToUpload", Path.GetFileName(filePath));

        var response = await client.PostAsync("", content);
        if (response.IsSuccessStatusCode)
        {
            var url = await response.Content.ReadAsStringAsync();
            callback(url);
        }
        else
        {
            // Handle your failed requests here
        }
    }
}
