using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Net.Mail;
using System.Text;

public partial class Contact : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }
    protected void submit_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtmail.Text) || !IsValidEmail(txtmail.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter a valid email address')", true);
            return; // Stop further execution
        }


        string to = "pravinnarale2003@gmail.com"; //To address
        string from = txtmail.Text; //From customer    
        if (string.IsNullOrEmpty(from))
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Enter All Info')", true);
        }


        MailMessage message = new MailMessage(from, to);


        string mailbody = "Hi Sir, These is Customer Enquiry mail<br/> <br/> Cusomer Name : " + txtname.Text + " <br/> Mail ID : " + txtmail.Text + " <br/>  Subject : " + txtsubject.Text + "<br/> Message : " + txtmsg.Text + "<br/><br/> Thanks and Regards, <br/><br/><br/> Note: These is Customer Enquiry Mail.";
        message.Subject = txtsubject.Text;
        message.Body = mailbody;
        message.BodyEncoding = Encoding.UTF8;
        message.IsBodyHtml = true;
        SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Gmail smtp    
        System.Net.NetworkCredential basicCredential1 = new
        System.Net.NetworkCredential("fullstackjava95@gmail.com", "ulcrfcizqvthydnt");
        client.EnableSsl = true;
        client.UseDefaultCredentials = false;
        client.Credentials = basicCredential1;

        try
        {
            client.Send(message);

            txtname.Text = string.Empty;
            txtmail.Text = string.Empty;
            txtsubject.Text = string.Empty;
            txtmsg.Text = string.Empty;

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Send Your Enquiry')", true);
        }
        catch (Exception ex)
        {
            string sms = ex.Message;
            object abc = ex.InnerException;
            string st = ex.StackTrace;
            string dd = ex.Source;
            throw ex;
        }
    }



}
