using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;



public partial class _Default : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //this.Test();
        //return;
        this.CheckAPIKey();
    }

    private void CheckAPIKey()
    {
        if (this.LoadJsonKeyFile() != null)
        {
            this.pnlAPIKey.Visible = false;
            this.pnlInput.Visible = true;
            this.txtTextRequest.Focus();
        }
        else
        {
            this.pnlAPIKey.Visible = true;
            this.pnlInput.Visible = false;
            this.txtAPIKey.Focus();
        }
    }

    public void lbDOAIText_Click(object sender, EventArgs e)
    {
        TSOpenAIAPI oTSOpenAIAPI = new TSOpenAIAPI(LoadJsonKeyFile());
        string sAnswer = oTSOpenAIAPI.callOpenAI(1000, this.txtTextRequest.Text, "text-davinci-003", 0.7, 1, 0, 0);
        this.lblTextRespsone.Text = sAnswer;
    }

    public void lbDOAIImage_Click(object sender, EventArgs e)
    {
        try
        {
            this.lblImageResponse.Text = "";
            string sRequest = "stupid man";
            if (this.txtImageRequest.Text.Trim().Length > 5) sRequest = this.txtImageRequest.Text;
            TSOpenAIAPI oTSOpenAIAPI = new TSOpenAIAPI(LoadJsonKeyFile());
            TSOpenAIAPI.Images oImages = oTSOpenAIAPI.callOpenAICreateImage(sRequest);

            string sImageHTML = "";
            if (oImages.data != null)
                foreach (TSOpenAIAPI._Data oUrl in oImages.data)
                {
                    sImageHTML = sImageHTML + "<div class='row'>";
                    sImageHTML = sImageHTML + "<div class='col'>";
                    sImageHTML = sImageHTML + "<img src='" + oUrl.url + "'>";
                    sImageHTML = sImageHTML + "</div>";
                    sImageHTML = sImageHTML + "</div>";
                    this.lblImageResponse.Text = sImageHTML;
                }
            else
                this.lblImageResponse.Text = oImages.Response;

        }
        catch (Exception ex)
        {
            this.lblImageResponse.Text =  ex.Message;
        }
    }

    public void Test()
    {
        JObject oJasonFile = JObject.Parse(File.ReadAllText("c:/dummy/images.json"));
        TSOpenAIAPI.Images images = (TSOpenAIAPI.Images)JsonConvert.DeserializeObject<TSOpenAIAPI.Images>(oJasonFile.ToString());
    }


    public void lbSaveAPIKey_Click(object sender, EventArgs e)
    {
        this.SaveJsonKeyFile(txtAPIKey.Text);
        this.CheckAPIKey();
    }

    public void SaveJsonKeyFile(string Key)
    {
        JObject APIKeyFile = new JObject(
            new JProperty("Key", Key));

        File.WriteAllText(Server.MapPath("/") + "/APIKey.json", APIKeyFile.ToString());

        // write JSON directly to a file
        using (StreamWriter file = File.CreateText(Server.MapPath("/") + "/APIKey.json"))
        using (JsonTextWriter writer = new JsonTextWriter(file))
        {
            APIKeyFile.WriteTo(writer);
        }
    }

    public TSOpenAIAPI.OpenAIApiKey LoadJsonKeyFile()
    {
        TSOpenAIAPI.OpenAIApiKey oKey = null;
        if (System.IO.File.Exists(Server.MapPath("/") + "/APIKey.json"))
        {
            JObject oJasonKey = JObject.Parse(File.ReadAllText(Server.MapPath("/") + "/APIKey.json"));
            TSOpenAIAPI.OpenAIApiKey oOpenAIApiKey = new TSOpenAIAPI.OpenAIApiKey();
            oOpenAIApiKey = JsonConvert.DeserializeObject<TSOpenAIAPI.OpenAIApiKey>(oJasonKey.ToString());
            oKey = oOpenAIApiKey;
        }
        return oKey;
    }


}