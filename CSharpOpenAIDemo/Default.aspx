<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Panel ID="pnlAPIKey" runat="server" class="row jumbotron">
        <h1>OpenAI API-Key</h1>
        <div class="col">
            <p>
                Enter your API key here. The key is stored in the project directory in the file APIKey.json.
            </p>
            <p>
                <a href='https://platform.openai.com/account/api-keys' target="_blank">Here you can create an API-Key.</a>
            </p>
            <p>
                <asp:TextBox ID="txtAPIKey"  runat="server" MaxLength="2000" AutoCompleteType="None"></asp:TextBox>
            </p>
            <p>
                <asp:LinkButton ID="lbSaveAPIKey" runat="server" CssClass="btn btn-success" OnClick="lbSaveAPIKey_Click">Save</asp:LinkButton>

            </p>
        </div>
    </asp:Panel>

    <asp:Panel ID="pnlInput" runat="server" class="row jumbotron">
        <div class="col">
            <h1>KI Text Request</h1>
            <p>
                <asp:TextBox ID="txtTextRequest" runat="server" MaxLength="2000"  AutoCompleteType="None"></asp:TextBox><br />
            </p>
            <p>
                <asp:LinkButton ID="lbDOAI" runat="server" ToolTip="Send request..." CssClass="btn btn-success" OnClick="lbDOAIText_Click">Do it...</asp:LinkButton>
            </p>
            <p>
                <asp:Label ID="lblTextRespsone" runat="server"></asp:Label>
            </p>
        </div>

           <div class="col">
            <h1>KI Image Request</h1>
            <p>
                <asp:TextBox ID="txtImageRequest" runat="server" MaxLength="2000" AutoCompleteType="None"></asp:TextBox><br />
            </p>
            <p>
                <asp:LinkButton ID="lbDOAIImage" runat="server" ToolTip="Send request..." CssClass="btn btn-success" OnClick="lbDOAIImage_Click">Do it...</asp:LinkButton>
            </p>
            <p>
                <asp:Label ID="lblImageResponse" runat="server"></asp:Label>
            </p>
        </div>
    </asp:Panel>


</asp:Content>
