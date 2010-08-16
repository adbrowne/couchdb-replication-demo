<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<CouchConflictDemo.Models.CouchIndexModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="firstDocument">
        <% using (Html.BeginForm("EditMasterDocument", "Couch"))
           { %>
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
        <% } %>
        <% using (Html.BeginForm("ReplicateToSlave", "Couch"))
           { %>
        <div class="replicate-to-slave">
            <input type="submit" value="Replicate &gt;" />
        </div>
        <% } %>
        <div>
            <% foreach (var conflictResult in Model.MasterConflicts)
               { %>
            <pre>
                    <%= conflictResult %>
                </pre>
            <% } %>
        </div>
    </div>
    <div class="secondDocument">
        <% using (Html.BeginForm("EditSlaveDocument", "Couch"))
           { %>
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
        <% } %>
        <% using (Html.BeginForm("ReplicateToMaster", "Couch"))
           { %>
        <div class="replicate-to-master">
            <input type="submit" value="&lt; Replicate" />
        </div>
        <% } %>
        <div>
            <% foreach (var conflictResult in Model.SlaveConflicts)
               { %>
            <pre>
                    <%= conflictResult %>
                </pre>
            <% } %>
        </div>
    </div>
    <div class="clear">
    </div>
</asp:Content>
