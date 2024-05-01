using Newtonsoft.Json;
using OpenCvSharp;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace 蝦皮總動員;
public static class GeminiApiService
{
    public static async Task CallApiAsync(Bitmap bitmap)
    {
        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Accept.Clear();
        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        // 將 Bitmap 轉換為 byte[]
        MemoryStream ms = new MemoryStream();
        bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
        byte[] bitmapBytes = ms.ToArray();
        var inlineData = new InlineData
        {
            mimeType = "image/jpeg",
            data = Convert.ToBase64String(bitmapBytes)
        };
        var requestBody = new ApiRequestModel
        {
            contents = new Content[]
            {
            new Content
            {
                parts = new object[]
                {
                    new TextPart
                    {
                        text = "以上圖片為一題目，框框內共有四個選項，由上至下分別為ABCD選項，請依照題目，選出一個最符合的選項，並說明原因，例如若你認為第一個選項正確，則回答\r\n        以下格式:\r\n          題目: XXXX(列出題目)\r\n          A:XXXX(列出由上而下的第一個選項)\r\n          B:XXXX(列出由上而下的第二個選項)\r\n          C:XXXX(列出由上而下的第三個選項)\r\n          C:XXXX(列出由上而下的最後一格選項)\r\n          此題答案為 XX(ABCD選一個)，因為XXX(說明原因，簡短即可)"
                    },
                    new InlineDataPart
                    {
                        inlineData = inlineData
                    }
                }
            }
            }
        };

        string apiJson = JsonConvert.SerializeObject(requestBody);

        string url = Program.config["GeminiApi:Url"];
        string key = Program.config["GeminiApi:Key"];

        try
        {
            HttpResponseMessage response = await httpClient.PostAsync(new Uri(url + key), new StringContent(apiJson, Encoding.UTF8, "application/json"));

            string responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                // 將回應的 JSON 字串轉換為 ApiResponseModel 物件
                var apiResponse = JsonConvert.DeserializeObject<ApiResponseModel>(responseContent);

                // 以彈出視窗的方式顯示回應內容
                MessageBox.Show($"回應訊息: {apiResponse.Candidates[0].Content.Parts[0].Text}", "API Response", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // 以彈出視窗的方式顯示錯誤訊息和 HTTP 狀態碼
                if (MessageBox.Show(responseContent, "(按OK複製錯誤訊息)", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                { Clipboard.SetText(responseContent); }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error: {ex.Message}", "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }


}
