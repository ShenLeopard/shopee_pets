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

namespace �����`�ʭ�;
public static class GeminiApiService
{
    public static async Task CallApiAsync(Bitmap bitmap)
    {
        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Accept.Clear();
        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        // �N Bitmap �ഫ�� byte[]
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
                        text = "�H�W�Ϥ����@�D�ءA�خؤ��@���|�ӿﶵ�A�ѤW�ܤU���O��ABCD�ﶵ�A�Ш̷��D�ءA��X�@�ӳ̲ŦX���ﶵ�A�û�����]�A�Ҧp�Y�A�{���Ĥ@�ӿﶵ���T�A�h�^��\r\n        �H�U�榡:\r\n          �D��: XXXX(�C�X�D��)\r\n          A:XXXX(�C�X�ѤW�ӤU���Ĥ@�ӿﶵ)\r\n          B:XXXX(�C�X�ѤW�ӤU���ĤG�ӿﶵ)\r\n          C:XXXX(�C�X�ѤW�ӤU���ĤT�ӿﶵ)\r\n          C:XXXX(�C�X�ѤW�ӤU���̫�@��ﶵ)\r\n          ���D���׬� XX(ABCD��@��)�A�]��XXX(������]�A²�u�Y�i)"
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
                // �N�^���� JSON �r���ഫ�� ApiResponseModel ����
                var apiResponse = JsonConvert.DeserializeObject<ApiResponseModel>(responseContent);

                // �H�u�X�������覡��ܦ^�����e
                MessageBox.Show($"�^���T��: {apiResponse.Candidates[0].Content.Parts[0].Text}", "API Response", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // �H�u�X�������覡��ܿ��~�T���M HTTP ���A�X
                if (MessageBox.Show(responseContent, "(��OK�ƻs���~�T��)", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                { Clipboard.SetText(responseContent); }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error: {ex.Message}", "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }


}
