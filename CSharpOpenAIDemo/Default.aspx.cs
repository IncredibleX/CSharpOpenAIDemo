using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
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


    public void Test()
    {
        TSOpenAIAPI oTSOpenAIApi = new TSOpenAIAPI(TSOpenAIAPI.LoadJsonKeyFile(this.Page));
        string sAnswer = oTSOpenAIApi.callOpenAI(1000, "Wann gibt es schönes Wetter?", "text-davinci-003", 0.7, 1, 0, 0);
        this.lblTextRespsone.Text = sAnswer;


        TSOpenAIAPI.Images oImages;
        oImages = oTSOpenAIApi.callOpenAICreateImage("Hässliche Katze");

        string sImageHTML = "";
        foreach (TSOpenAIAPI._Data oUrl in oImages.data)
        {
            sImageHTML = sImageHTML + "<div class='row'>";
            sImageHTML = sImageHTML + "<div class='col'>";
            sImageHTML = sImageHTML + "<img src='" + oUrl.url + "'>";
            sImageHTML = sImageHTML + "</div>";
            sImageHTML = sImageHTML + "</div>";
            this.lblImageResponse.Text = sImageHTML;
        }
    }

    private void CheckAPIKey()
    {
        if (TSOpenAIAPI.LoadJsonKeyFile(this.Page) != null)
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
        TSOpenAIAPI oTSOpenAIAPI = new TSOpenAIAPI(TSOpenAIAPI.LoadJsonKeyFile(this.Page));
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
            TSOpenAIAPI oTSOpenAIAPI = new TSOpenAIAPI(TSOpenAIAPI.LoadJsonKeyFile(this.Page));
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
            this.lblImageResponse.Text = ex.Message;
        }
    }



    public void lbSaveAPIKey_Click(object sender, EventArgs e)
    {
       TSOpenAIAPI.SaveJsonKeyFile(txtAPIKey.Text,this.Page);
        this.CheckAPIKey();
    }




}