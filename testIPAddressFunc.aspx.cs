using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;

public partial class testIPAddressFunc : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        try
        {
            string strData = "";
            string deviceName = Dns.GetHostName();
            strData += "Device Name: " + deviceName + "<br/>";

            // Get IP addresses associated with all network interfaces
            IPAddress[] localIPs = Dns.GetHostAddresses(deviceName);
            foreach (IPAddress ip in localIPs)
            {
                strData += "IP Address: " + ip + "<br/>";
            }

            // Alternatively, you can get IP addresses associated with specific network interfaces
            // This example retrieves IPv4 addresses only
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface adapter in nics)
            {
                if (adapter.OperationalStatus == OperationalStatus.Up)
                {
                    foreach (UnicastIPAddressInformation ip in adapter.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            strData += "Interface: " + adapter.Name + "<br/>";
                            strData += "IP Address: " + ip.Address + "<br/>";
                        }
                    }
                }
            }
            lblMsg.Text =strData;
        }
        catch (Exception ex)
        {

            lblMsg.Text = ex.Message;
        }

    }
}