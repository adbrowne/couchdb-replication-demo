<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<CouchConflictDemo.Models.CouchIndexModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <% using (Html.BeginForm("EditMasterDocument", "Couch")) { %>
    <div class="firstDocument">
        <fieldset>
            <legend>Master</legend>

            <div class="editor-label">
                <%: Html.LabelFor(m => m.MasterDocument) %>
            </div>

            <div class="editor-field">
                <%: Html.TextAreaFor(m => m.MasterDocument, new { rows = "20", cols = "40" })%>
            </div>

            <p>
                <input type="submit" value="Update" />
            </p>
        </fieldset>

        <div>
            <% foreach(var conflictResult in Model.MasterConflicts) { %>
                <pre>
                    <%= conflictResult %>
                </pre>
            <% } %>
        </div>
    </div>
    <% } %>

    <div class="replicationPanel">
        <% using (Html.BeginForm("ReplicateToSlave", "Couch")) { %>
            <p>
                <input type="submit" value="Replicate &gt;"/>
            </p>
        <% } %>
   
        <% using (Html.BeginForm("ReplicateToMaster", "Couch")) { %>
            <p>
                <input type="submit" value="&lt; Replicate"/>
            </p>
        <% } %>
    </div>

    <% using (Html.BeginForm("EditSlaveDocument", "Couch")) { %>
    <div class="secondDocument">
        <fieldset>
            <legend>Slave</legend>

            <div class="editor-label">
                <%: Html.LabelFor(m => m.SlaveDocument) %>
            </div>

            <div class="editor-field">
                <%: Html.TextAreaFor(m => m.SlaveDocument, new { rows = "20", cols = "40" })%>
            </div>

            <p>
                <input type="submit" value="Update" />
            </p>
        </fieldset>

         <div>
            <% foreach(var conflictResult in Model.SlaveConflicts) { %>
                <pre>
                    <%= conflictResult %>
                </pre>
            <% } %>
        </div>
    </div>
    <% } %>

    <div class="clear"></div>

    
</asp:Content>
