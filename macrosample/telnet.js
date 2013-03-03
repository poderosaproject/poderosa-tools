import VaraTerm;
import VaraTerm.Macro;
import System.Drawing;

var env = new Environment();

/*
	Please modify the following values before you run this macro!
*/
var host = "myhost.mydomain";
var account = "pavel";
var password = "nedved";

telnettest();

function telnettest() {
	if(host=="myhost.mydomain") {
		env.Util.MessageBox(String.Format("This telnet sample requires to set the target host.\nPlease modify the 'host','account',and 'password' variables in {0} and try again.", env.MacroFileName));
		return;
	}

	/*
		//if you want to connect via SSH, the connection procudure gets simpler.
	var param = new SSHTerminalParam(host, account, password);
	var c = env.Connections.Open(param);
	*/

	var param = new TelnetTerminalParam(host);
	var prof = new RenderProfile();
	prof.FontSize = 10;
	prof.SetBackColor(Color.Black);
	prof.SetForeColor(Color.White);
	param.RenderProfile = prof;

	var c = env.Connections.Open(param);
	var r = c.ReceiveData();
	while(r.indexOf("login:")==-1) r = c.ReceiveData(); //waiting prompt for account
	c.TransmitLn(account);
	r = c.ReceiveData();
	while(r.indexOf("Password:")==-1) r = c.ReceiveData(); //waiting prompt for password
	c.TransmitLn(password);

}
