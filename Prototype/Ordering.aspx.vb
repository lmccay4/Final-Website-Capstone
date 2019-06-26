Option Strict On
Option Explicit On

Imports System.Net.Mail
Imports System.Data
Imports System.Data.SqlClient

Partial Class Ordering
    Inherits System.Web.UI.Page
    Dim ObjDT As System.Data.DataTable
    Dim ObjDR As System.Data.DataRow
    Dim ds As New DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click


        'Dim Smtp_Server As New SmtpClient
        'Dim e_mail As New MailMessage()

        'Smtp_Server.UseDefaultCredentials = False
        'Smtp_Server.Credentials = New Net.NetworkCredential("capskeystone@gmail.com", "KeystoneCaps123")
        'Smtp_Server.Port = 587
        'Smtp_Server.EnableSsl = True
        'Smtp_Server.Host = "smtp.gmail.com"

        'e_mail = New MailMessage()
        'e_mail.From = New MailAddress(txtFrom.Text)
        'e_mail.To.Add(txtTo.Text)
        'e_mail.Subject = "Email Sending"
        'e_mail.IsBodyHtml = False
        'e_mail.Body = "body"
        'Smtp_Server.Send(e_mail)

        Dim strConnection As String = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Products.mdf;Integrated Security=True"
        Dim objConnection As New SqlConnection(strConnection)
        Dim mySqlDataAdapter As New SqlDataAdapter("Select * From SAMPLE_ORDERS", objConnection)


        Dim myDataRow As DataRow
        Dim myDataRowsCommandBuilder As New SqlCommandBuilder(mySqlDataAdapter)
        mySqlDataAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey

        mySqlDataAdapter.Fill(ds, "SAMPLE_ORDERS")
        myDataRow = ds.Tables("SAMPLE_ORDERS").NewRow()


        myDataRow("name") = nameBox.Text
        myDataRow("address") = addressBox.Text
        myDataRow("city") = cityBox.Text
        myDataRow("state") = stateDDL.SelectedValue
        myDataRow("zip") = zipBox.Text
        'If apronsCh.Checked Then
        myDataRow("apron") = apronsCh.Checked
        'End If
        myDataRow("apron") = apronsCh.Checked

        'If sleevesCh.Checked Then
        myDataRow("sleeve") = sleevesCh.Checked
        'End If

        'If capsCh.Checked Then
        myDataRow("caps") = capsCh.Checked
            'End If

            'If shoeCh.Checked Then
            myDataRow("shoeCovers") = shoeCh.Checked
            'End If

            'If coverallsCh.Checked Then
            myDataRow("coveralls") = coverallsCh.Checked
        'End If


        ds.Tables("SAMPLE_ORDERS").Rows.Add(myDataRow)
        mySqlDataAdapter.Update(ds, "SAMPLE_ORDERS")



        Server.Transfer("Confirmation.aspx")


    End Sub

End Class

